import 'package:flutter/material.dart';
import 'package:peluqueria_aja/models/servicio.dart';
import '../../utils/theme.dart';

class ServiceDetailScreen extends StatelessWidget {
  const ServiceDetailScreen({super.key});

  @override
  Widget build(BuildContext context) {
    // Recuperamos el objeto Servicio pasado por argumentos
    final args = ModalRoute.of(context)!.settings.arguments;
    if (args == null || args is! Servicio) {
      return const Scaffold(
          body: Center(child: Text("Error: No se encontró el servicio")));
    }
    final Servicio servicio = args;

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
          // 1. CABECERA DINÁMICA
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
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ),
          ),

          // 2. CONTENIDO BASADO EN LA BASE DE DATOS
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
                              fontWeight: FontWeight.w800,
                            ),
                          ),
                        ],
                      ),
                      // Mostramos la DURACIÓN real que viene de Java
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
                  const Text(
                    "Sobre este servicio",
                    style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                  ),
                  const SizedBox(height: 12),
                  // Mostramos la DESCRIPCIÓN real (columna 'modulo' o 'descripcion' en MySQL)
                  Text(
                    servicio.descripcion.isNotEmpty
                        ? servicio.descripcion
                        : "Disfruta de una experiencia profesional con los mejores productos en nuestro salón.",
                    style: TextStyle(
                      fontSize: 16,
                      color: Colors.grey[700],
                      height: 1.5,
                    ),
                  ),
                ],
              ),
            ),
          ),

          // 3. ACCIÓN DE RESERVA
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
}
