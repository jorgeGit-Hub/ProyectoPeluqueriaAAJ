import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../providers/user_provider.dart';
import '../../providers/cita_provider.dart';
import '../../models/cita.dart';
import '../../services/cita_service.dart';
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
    WidgetsBinding.instance.addPostFrameCallback((_) async {
      final user = context.read<UserProvider>().usuario;
      if (user != null) {
        await context.read<CitaProvider>().loadCitasByCliente(user["id"]);
      }
    });
  }

  Future<void> _cancelarCita(Cita cita) async {
    final confirmar = await showDialog<bool>(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text("Cancelar Cita"),
        content: const Text("¿Estás seguro de que deseas cancelar esta cita?"),
        actions: [
          TextButton(onPressed: () => Navigator.pop(context, false), child: const Text("No")),
          ElevatedButton(
            onPressed: () => Navigator.pop(context, true),
            style: ElevatedButton.styleFrom(backgroundColor: Colors.red),
            child: const Text("Sí, cancelar"),
          ),
        ],
      ),
    );

    if (confirmar != true) return;

    try {
      // ✅ Usar toUpdateJson del modelo actualizado
      final citaActualizada = Cita(
        idCita: cita.idCita,
        fecha: cita.fecha,
        estado: "cancelada",
        idCliente: cita.idCliente,
        idHorario: cita.idHorario,
      );
      await CitaService().updateCita(cita.idCita!, citaActualizada.toUpdateJson());

      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text("Cita cancelada"), backgroundColor: Colors.green),
        );
        final user = context.read<UserProvider>().usuario;
        if (user != null) {
          await context.read<CitaProvider>().loadCitasByCliente(user["id"]);
        }
      }
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text("Error al cancelar: $e"), backgroundColor: Colors.red),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final citaProv = context.watch<CitaProvider>();

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        title: const Text("Mis Citas", style: TextStyle(fontWeight: FontWeight.w600)),
        backgroundColor: AppTheme.primary, foregroundColor: Colors.white,
        centerTitle: true, elevation: 0,
      ),
      body: citaProv.loading
          ? const Center(child: CircularProgressIndicator())
          : citaProv.error
              ? _buildErrorState()
              : citaProv.citas.isEmpty
                  ? _buildEmptyState()
                  : RefreshIndicator(
                      onRefresh: () async {
                        final user = context.read<UserProvider>().usuario;
                        if (user != null) await context.read<CitaProvider>().loadCitasByCliente(user["id"]);
                      },
                      child: ListView.builder(
                        padding: const EdgeInsets.all(16),
                        itemCount: citaProv.citas.length,
                        itemBuilder: (context, index) {
                          final c = citaProv.citas[index];
                          // ✅ Usar nombreServicio directamente del modelo actualizado
                          final nombreServicio = c.nombreServicio ?? "Servicio Peluquería";
                          return _buildCitaCard(c, nombreServicio);
                        },
                      ),
                    ),
    );
  }

  Widget _buildCitaCard(Cita c, String nombreServicio) {
    final puedeCancelar = c.estado.toLowerCase() == "pendiente";
    final horaLabel = c.horaInicio.length >= 5 ? c.horaInicio.substring(0, 5) : c.horaInicio;

    return Card(
      margin: const EdgeInsets.only(bottom: 16),
      elevation: 2,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
      child: Column(children: [
        ListTile(
          contentPadding: const EdgeInsets.symmetric(horizontal: 20, vertical: 10),
          leading: Container(
            padding: const EdgeInsets.all(10),
            decoration: BoxDecoration(color: AppTheme.primary.withOpacity(0.1), shape: BoxShape.circle),
            child: const Icon(Icons.event_available, color: AppTheme.primary),
          ),
          title: Text(nombreServicio, style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 16)),
          subtitle: Padding(
            padding: const EdgeInsets.only(top: 4),
            child: Text("${c.fecha}  •  $horaLabel", style: TextStyle(color: Colors.grey[600])),
          ),
          trailing: Container(
            padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
            decoration: BoxDecoration(
              color: _getEstadoColor(c.estado).withOpacity(0.1),
              borderRadius: BorderRadius.circular(20),
            ),
            child: Text(c.estado.toUpperCase(),
              style: TextStyle(color: _getEstadoColor(c.estado), fontWeight: FontWeight.bold, fontSize: 11)),
          ),
          onTap: () => Navigator.pushNamed(context, '/detalle-cita', arguments: c),
        ),
        if (puedeCancelar)
          Padding(
            padding: const EdgeInsets.fromLTRB(20, 0, 20, 12),
            child: SizedBox(
              width: double.infinity,
              child: OutlinedButton.icon(
                icon: const Icon(Icons.cancel_outlined),
                label: const Text("Cancelar Cita"),
                style: OutlinedButton.styleFrom(foregroundColor: Colors.red, side: const BorderSide(color: Colors.red)),
                onPressed: () => _cancelarCita(c),
              ),
            ),
          ),
      ]),
    );
  }

  Color _getEstadoColor(String estado) {
    switch (estado.toLowerCase()) {
      case "pendiente": return Colors.orange;
      case "realizada": return Colors.green;
      case "cancelada": return Colors.red;
      default: return AppTheme.primary;
    }
  }

  Widget _buildEmptyState() => Center(child: Column(mainAxisAlignment: MainAxisAlignment.center, children: [
    Icon(Icons.calendar_today_outlined, size: 80, color: Colors.grey[400]),
    const SizedBox(height: 16),
    Text("No tienes citas programadas", style: TextStyle(color: Colors.grey[600], fontSize: 16)),
  ]));

  Widget _buildErrorState() => Center(child: Column(mainAxisAlignment: MainAxisAlignment.center, children: [
    const Icon(Icons.error_outline, size: 80, color: Colors.red),
    const SizedBox(height: 16),
    const Text("Error al conectar con el servidor", style: TextStyle(color: Colors.red)),
    const SizedBox(height: 20),
    ElevatedButton.icon(
      onPressed: () async {
        final user = context.read<UserProvider>().usuario;
        if (user != null) await context.read<CitaProvider>().loadCitasByCliente(user["id"]);
      },
      icon: const Icon(Icons.refresh),
      label: const Text("Reintentar"),
      style: ElevatedButton.styleFrom(backgroundColor: AppTheme.primary, foregroundColor: Colors.white),
    ),
  ]));
}
