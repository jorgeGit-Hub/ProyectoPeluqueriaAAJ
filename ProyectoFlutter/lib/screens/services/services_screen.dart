import 'package:flutter/material.dart';
import 'package:peluqueria_aja/services/servicio_service.dart';
import 'package:peluqueria_aja/models/servicio.dart';
import '../../utils/theme.dart';

class ServicesScreen extends StatefulWidget {
  const ServicesScreen({super.key});

  @override
  State<ServicesScreen> createState() => _ServicesScreenState();
}

class _ServicesScreenState extends State<ServicesScreen> {
  List<Servicio> servicios = [];
  bool loading = true;
  bool error = false;

  @override
  void initState() {
    super.initState();
    _load();
  }

  Future<void> _load() async {
    if (!mounted) return;
    setState(() {
      loading = true;
      error = false;
    });

    try {
      final data = await ServicioService().getServicios();
      if (mounted) {
        setState(() {
          servicios = data.map<Servicio>((e) => Servicio.fromJson(e)).toList();
          loading = false;
        });
      }
    } catch (e) {
      if (mounted) {
        setState(() {
          error = true;
          loading = false;
        });
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        title: const Text(
          "Nuestros Servicios",
          style: TextStyle(fontWeight: FontWeight.w600),
        ),
        centerTitle: true,
        backgroundColor: AppTheme.primary,
        foregroundColor: Colors.white,
        elevation: 0,
      ),
      body: loading
          ? const Center(child: CircularProgressIndicator())
          : error
              ? _buildErrorView()
              : servicios.isEmpty
                  ? _buildEmptyView()
                  : ListView.separated(
                      padding: const EdgeInsets.symmetric(
                          horizontal: 20, vertical: 20),
                      itemCount: servicios.length,
                      physics: const BouncingScrollPhysics(),
                      separatorBuilder: (ctx, index) =>
                          const SizedBox(height: 16),
                      itemBuilder: (_, i) {
                        final s = servicios[i];
                        return _ServiceCard(
                          servicio: s,
                          index: i, // Para alternar colores/iconos si quisieras
                          onTap: () {
                            Navigator.pushNamed(
                              context,
                              "/service-detail",
                              arguments: s,
                            );
                          },
                        );
                      },
                    ),
    );
  }

  // Vista de Error estilizada
  Widget _buildErrorView() {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Icon(Icons.wifi_off_rounded, size: 64, color: Colors.grey[400]),
          const SizedBox(height: 16),
          Text(
            "No se pudieron cargar los servicios",
            style: TextStyle(color: Colors.grey[600], fontSize: 16),
          ),
          const SizedBox(height: 16),
          ElevatedButton.icon(
            onPressed: _load,
            icon: const Icon(Icons.refresh),
            label: const Text("Reintentar"),
            style: ElevatedButton.styleFrom(
              backgroundColor: AppTheme.primary,
              foregroundColor: Colors.white,
            ),
          )
        ],
      ),
    );
  }

  // Vista Vacía estilizada
  Widget _buildEmptyView() {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Icon(Icons.content_cut_outlined, size: 64, color: Colors.grey[300]),
          const SizedBox(height: 16),
          Text(
            "No hay servicios disponibles",
            style: TextStyle(color: Colors.grey[500], fontSize: 18),
          ),
        ],
      ),
    );
  }
}

// ---------------------------------------------
// WIDGET TARJETA DE SERVICIO (Diseño Principal)
// ---------------------------------------------
class _ServiceCard extends StatelessWidget {
  final Servicio servicio;
  final VoidCallback onTap;
  final int index;

  const _ServiceCard({
    required this.servicio,
    required this.onTap,
    required this.index,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(20),
        boxShadow: [
          BoxShadow(
            color: Colors.black.withOpacity(0.04),
            blurRadius: 12,
            offset: const Offset(0, 4),
          ),
        ],
      ),
      child: Material(
        color: Colors.transparent,
        child: InkWell(
          onTap: onTap,
          borderRadius: BorderRadius.circular(20),
          child: Padding(
            padding: const EdgeInsets.all(16),
            child: Row(
              children: [
                // 1. Icono / Avatar del servicio
                Container(
                  height: 55,
                  width: 55,
                  decoration: BoxDecoration(
                    color: AppTheme.primary.withOpacity(0.1),
                    borderRadius: BorderRadius.circular(15),
                  ),
                  child: Icon(
                    Icons
                        .spa_outlined, // Icono genérico (cámbialo si tienes lógica específica)
                    color: AppTheme.primary,
                    size: 28,
                  ),
                ),

                const SizedBox(width: 16),

                // 2. Información del servicio (Nombre y Precio)
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        servicio.nombre,
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.bold,
                          color: Colors.black87,
                        ),
                        maxLines: 2,
                        overflow: TextOverflow.ellipsis,
                      ),
                      const SizedBox(height: 4),
                      // Precio destacado
                      Text(
                        "${servicio.precio.toStringAsFixed(2)} €",
                        style: TextStyle(
                          fontSize: 18,
                          fontWeight: FontWeight.w800,
                          color: AppTheme
                              .primary, // El color da importancia al precio
                        ),
                      ),
                    ],
                  ),
                ),

                // 3. Flecha de acción
                Container(
                  padding: const EdgeInsets.all(8),
                  decoration: BoxDecoration(
                    color: Colors.grey[50],
                    borderRadius: BorderRadius.circular(10),
                  ),
                  child: Icon(
                    Icons.arrow_forward_ios_rounded,
                    size: 16,
                    color: Colors.grey[400],
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
