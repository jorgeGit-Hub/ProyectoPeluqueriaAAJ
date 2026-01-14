import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../providers/user_provider.dart';
import '../../providers/cita_provider.dart';
import '../../providers/servicio_provider.dart'; // Importante para obtener el nombre del servicio
import '../../models/cita.dart';
import '../../utils/theme.dart';

class MisCitasScreen extends StatefulWidget {
  const MisCitasScreen({super.key});

  @override
  State<MisCitasScreen> createState() => _MisCitasScreenState();
}

class _MisCitasScreenState extends State<MisCitasScreen> {
  @override
  void initState() {
    super.initState();
    // Cargamos las citas al entrar en la pantalla
    WidgetsBinding.instance.addPostFrameCallback((_) async {
      final user = context.read<UserProvider>().usuario;
      if (user != null) {
        // Usamos el método corregido del Provider
        await context.read<CitaProvider>().loadCitasByCliente(user["id"]);
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    final citaProv = context.watch<CitaProvider>();
    final servicioProv = context.watch<ServicioProvider>();

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        title: const Text("Mis Citas",
            style: TextStyle(fontWeight: FontWeight.w600)),
        backgroundColor: AppTheme.primary,
        foregroundColor: Colors.white,
        centerTitle: true,
        elevation: 0,
      ),
      body: citaProv.loading
          ? const Center(child: CircularProgressIndicator())
          : citaProv.error
              ? _buildErrorState()
              : citaProv.citas.isEmpty
                  ? _buildEmptyState()
                  : ListView.builder(
                      padding: const EdgeInsets.all(16),
                      itemCount: citaProv.citas.length,
                      itemBuilder: (context, index) {
                        final Cita c = citaProv.citas[index];

                        // Buscamos el nombre del servicio usando el ID que tiene la cita
                        // Buscamos el servicio de forma segura
               final servicioEncontrado = servicioProv.servicios.where(
                  (s) => s.idServicio == c.idServicio
                ).firstOrNull;

                final String servicioNombre = servicioEncontrado?.nombre ?? "Servicio Peluquería";
                return _buildCitaCard(c, servicioNombre);
                                      },
                                    ),
    );
  }

  // Widget para cada tarjeta de cita
  Widget _buildCitaCard(Cita c, String nombreServicio) {
    return Card(
      margin: const EdgeInsets.only(bottom: 16),
      elevation: 2,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
      child: ListTile(
        contentPadding:
            const EdgeInsets.symmetric(horizontal: 20, vertical: 10),
        leading: Container(
          padding: const EdgeInsets.all(10),
          decoration: BoxDecoration(
            color: AppTheme.primary.withOpacity(0.1),
            shape: BoxShape.circle,
          ),
          child: const Icon(Icons.event_available, color: AppTheme.primary),
        ),
        title: Text(
          nombreServicio,
          style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 16),
        ),
        subtitle: Padding(
          padding: const EdgeInsets.only(top: 4),
          child: Text(
            "${c.fecha}  •  ${c.horaInicio.substring(0, 5)}", // Mostramos HH:mm
            style: TextStyle(color: Colors.grey[600]),
          ),
        ),
        trailing: Container(
          padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
          decoration: BoxDecoration(
            color: _getEstadoColor(c.estado).withOpacity(0.1),
            borderRadius: BorderRadius.circular(20),
          ),
          child: Text(
            c.estado.toUpperCase(),
            style: TextStyle(
              color: _getEstadoColor(c.estado),
              fontWeight: FontWeight.bold,
              fontSize: 11,
            ),
          ),
        ),
      ),
    );
  }

  Color _getEstadoColor(String estado) {
    switch (estado.toUpperCase()) {
      case "PENDIENTE":
        return Colors.orange;
      case "CONFIRMADA":
        return Colors.green;
      case "CANCELADA":
        return Colors.red;
      default:
        return AppTheme.primary;
    }
  }

  Widget _buildEmptyState() {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Icon(Icons.calendar_today_outlined,
              size: 80, color: Colors.grey[400]),
          const SizedBox(height: 16),
          Text("No tienes citas programadas",
              style: TextStyle(color: Colors.grey[600], fontSize: 16)),
        ],
      ),
    );
  }

  Widget _buildErrorState() {
    return const Center(
      child: Text("Error al conectar con el servidor físico",
          style: TextStyle(color: Colors.red)),
    );
  }
}
