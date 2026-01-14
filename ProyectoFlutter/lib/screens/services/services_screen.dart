import 'package:flutter/material.dart';
import 'package:provider/provider.dart'; // Usamos Provider ahora
import 'package:peluqueria_aja/providers/servicio_provider.dart';
import 'package:peluqueria_aja/models/servicio.dart';
import '../../utils/theme.dart';

class ServicesScreen extends StatefulWidget {
  const ServicesScreen({super.key});

  @override
  State<ServicesScreen> createState() => _ServicesScreenState();
}

class _ServicesScreenState extends State<ServicesScreen> {
  @override
  void initState() {
    super.initState();
    // Cargamos los servicios a través del Provider al iniciar
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<ServicioProvider>().loadServicios();
    });
  }

  @override
  Widget build(BuildContext context) {
    // Escuchamos los cambios en el ServicioProvider
    final servicioProv = context.watch<ServicioProvider>();

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
      // RefreshIndicator permite al usuario actualizar tirando hacia abajo
      body: RefreshIndicator(
        onRefresh: () => servicioProv.loadServicios(),
        color: AppTheme.primary,
        child: servicioProv.loading && servicioProv.servicios.isEmpty
            ? const Center(child: CircularProgressIndicator())
            : servicioProv.servicios.isEmpty
                ? _buildEmptyView()
                : ListView.separated(
                    padding: const EdgeInsets.all(20),
                    itemCount: servicioProv.servicios.length,
                    physics:
                        const AlwaysScrollableScrollPhysics(), // Necesario para el RefreshIndicator
                    separatorBuilder: (ctx, index) =>
                        const SizedBox(height: 16),
                    itemBuilder: (_, i) {
                      final s = servicioProv.servicios[i];
                      return _ServiceCard(
                        servicio: s,
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
      ),
    );
  }

  Widget _buildEmptyView() {
    return SingleChildScrollView(
      physics: const AlwaysScrollableScrollPhysics(),
      child: Container(
        height: MediaQuery.of(context).size.height * 0.7,
        alignment: Alignment.center,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(Icons.content_cut_outlined, size: 64, color: Colors.grey[300]),
            const SizedBox(height: 16),
            Text(
              "No hay servicios disponibles",
              style: TextStyle(color: Colors.grey[500], fontSize: 18),
            ),
            const SizedBox(height: 10),
            TextButton(
              onPressed: () => context.read<ServicioProvider>().loadServicios(),
              child: const Text("Toca para reintentar"),
            )
          ],
        ),
      ),
    );
  }
}

class _ServiceCard extends StatelessWidget {
  final Servicio servicio;
  final VoidCallback onTap;

  const _ServiceCard({required this.servicio, required this.onTap});

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
                // Icono decorativo
                Container(
                  height: 55,
                  width: 55,
                  decoration: BoxDecoration(
                    color: AppTheme.primary.withOpacity(0.1),
                    borderRadius: BorderRadius.circular(15),
                  ),
                  child: const Icon(Icons.spa_outlined,
                      color: AppTheme.primary, size: 28),
                ),
                const SizedBox(width: 16),
                // Info del Servicio
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
                      ),
                      const SizedBox(height: 4),
                      Text(
                        "${servicio.precio.toStringAsFixed(2)} €",
                        style: const TextStyle(
                          fontSize: 18,
                          fontWeight: FontWeight.w800,
                          color: AppTheme.primary,
                        ),
                      ),
                    ],
                  ),
                ),
                // Duración rápida
                Column(
                  children: [
                    Icon(Icons.access_time, size: 16, color: Colors.grey[400]),
                    Text(
                      "${servicio.duracion} min",
                      style: TextStyle(fontSize: 12, color: Colors.grey[600]),
                    ),
                  ],
                ),
                const SizedBox(width: 10),
                const Icon(Icons.arrow_forward_ios_rounded,
                    size: 14, color: Colors.grey),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
