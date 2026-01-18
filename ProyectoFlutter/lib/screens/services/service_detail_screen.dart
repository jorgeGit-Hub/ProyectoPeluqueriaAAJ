import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:peluqueria_aja/models/servicio.dart';
import '../../providers/valoracion_provider.dart';
import '../../models/valoracion.dart';
import '../../utils/theme.dart';

class ServiceDetailScreen extends StatefulWidget {
  const ServiceDetailScreen({super.key});

  @override
  State<ServiceDetailScreen> createState() => _ServiceDetailScreenState();
}

class _ServiceDetailScreenState extends State<ServiceDetailScreen> {
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<ValoracionProvider>().loadValoraciones();
    });
  }

  @override
  Widget build(BuildContext context) {
    final args = ModalRoute.of(context)!.settings.arguments;
    if (args == null || args is! Servicio) {
      return const Scaffold(
          body: Center(child: Text("Error: No se encontró el servicio")));
    }
    final Servicio servicio = args;

    final valoracionProv = context.watch<ValoracionProvider>();
    final todasValoraciones = valoracionProv.valoraciones;

    double promedio = 0;
    if (todasValoraciones.isNotEmpty) {
      promedio =
          todasValoraciones.map((v) => v.puntuacion).reduce((a, b) => a + b) /
              todasValoraciones.length;
    }

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      extendBodyBehindAppBar: true,
      appBar: AppBar(
        backgroundColor: Colors.transparent,
        elevation: 0,
        foregroundColor: Colors.white,
        leading: Container(
          margin: const EdgeInsets.all(8),
          decoration: const BoxDecoration(
              color: Colors.black26, shape: BoxShape.circle),
          child: IconButton(
            icon: const Icon(Icons.arrow_back, size: 20),
            onPressed: () => Navigator.pop(context),
          ),
        ),
      ),
      body: Column(
        children: [
          Expanded(
            flex: 3,
            child: Container(
              width: double.infinity,
              decoration: const BoxDecoration(
                color: AppTheme.primary,
                borderRadius: BorderRadius.only(
                  bottomLeft: Radius.circular(40),
                  bottomRight: Radius.circular(40),
                ),
              ),
              child: SafeArea(
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Container(
                      height: 90,
                      width: 90,
                      decoration: BoxDecoration(
                        color: Colors.white.withOpacity(0.2),
                        shape: BoxShape.circle,
                      ),
                      child: const Icon(Icons.spa_rounded,
                          size: 45, color: Colors.white),
                    ),
                    const SizedBox(height: 16),
                    Padding(
                      padding: const EdgeInsets.symmetric(horizontal: 30),
                      child: Text(
                        servicio.nombre,
                        textAlign: TextAlign.center,
                        style: const TextStyle(
                            color: Colors.white,
                            fontSize: 26,
                            fontWeight: FontWeight.bold),
                      ),
                    ),
                  ],
                ),
              ),
            ),
          ),
          Expanded(
            flex: 5,
            child: SingleChildScrollView(
              padding: const EdgeInsets.all(24),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text("Precio del Servicio",
                              style: TextStyle(color: Colors.grey[600])),
                          Text(
                            "${servicio.precio.toStringAsFixed(2)} €",
                            style: const TextStyle(
                                color: AppTheme.primary,
                                fontSize: 30,
                                fontWeight: FontWeight.w800),
                          ),
                        ],
                      ),
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.end,
                        children: [
                          Text("Duración",
                              style: TextStyle(color: Colors.grey[600])),
                          Row(
                            children: [
                              const Icon(Icons.access_time,
                                  size: 18, color: AppTheme.primary),
                              const SizedBox(width: 4),
                              Text(
                                "${servicio.duracion} min",
                                style: const TextStyle(
                                    fontSize: 18, fontWeight: FontWeight.bold),
                              ),
                            ],
                          ),
                        ],
                      ),
                    ],
                  ),
                  const SizedBox(height: 30),
                  const Text("Sobre este servicio",
                      style:
                          TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
                  const SizedBox(height: 12),
                  Text(
                    servicio.descripcion.isNotEmpty
                        ? servicio.descripcion
                        : "Disfruta de una experiencia profesional con los mejores productos en nuestro salón.",
                    style: TextStyle(
                        fontSize: 16, color: Colors.grey[700], height: 1.5),
                  ),
                  const SizedBox(height: 30),
                  const Divider(),
                  const SizedBox(height: 20),
                  const Text("Valoraciones",
                      style:
                          TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
                  const SizedBox(height: 12),
                  if (todasValoraciones.isEmpty)
                    const Text("No hay valoraciones aún",
                        style: TextStyle(color: Colors.grey))
                  else ...[
                    Row(
                      children: [
                        Text(
                          promedio.toStringAsFixed(1),
                          style: const TextStyle(
                              fontSize: 32, fontWeight: FontWeight.bold),
                        ),
                        const SizedBox(width: 8),
                        Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Row(
                              children: List.generate(5, (index) {
                                return Icon(
                                  index < promedio.round()
                                      ? Icons.star
                                      : Icons.star_border,
                                  color: Colors.amber,
                                  size: 16,
                                );
                              }),
                            ),
                            Text(
                              "${todasValoraciones.length} valoraciones",
                              style: TextStyle(
                                  color: Colors.grey[600], fontSize: 12),
                            ),
                          ],
                        ),
                      ],
                    ),
                    const SizedBox(height: 16),
                    ...todasValoraciones
                        .take(3)
                        .map((v) => _buildValoracionCompact(v)),
                  ],
                ],
              ),
            ),
          ),
          Container(
            padding: const EdgeInsets.all(24),
            decoration: BoxDecoration(
              color: Colors.white,
              borderRadius: const BorderRadius.only(
                  topLeft: Radius.circular(30), topRight: Radius.circular(30)),
              boxShadow: [
                BoxShadow(
                    color: Colors.black.withOpacity(0.05),
                    blurRadius: 20,
                    offset: const Offset(0, -5))
              ],
            ),
            child: SafeArea(
              top: false,
              child: SizedBox(
                width: double.infinity,
                height: 54,
                child: ElevatedButton.icon(
                  onPressed: () => Navigator.pushNamed(context, "/booking",
                      arguments: servicio),
                  icon: const Icon(Icons.calendar_month_outlined),
                  label: const Text("Reservar Ahora",
                      style:
                          TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
                  style: ElevatedButton.styleFrom(
                    backgroundColor: AppTheme.primary,
                    foregroundColor: Colors.white,
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(16)),
                  ),
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildValoracionCompact(Valoracion v) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 12),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            children: List.generate(v.puntuacion, (index) {
              return const Icon(Icons.star, color: Colors.amber, size: 14);
            }),
          ),
          const SizedBox(width: 8),
          Expanded(
            child: Text(
              v.comentario,
              maxLines: 2,
              overflow: TextOverflow.ellipsis,
              style: const TextStyle(fontSize: 13),
            ),
          ),
        ],
      ),
    );
  }
}
