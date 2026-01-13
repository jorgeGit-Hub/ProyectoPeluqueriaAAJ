import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../utils/theme.dart';
import '../../providers/user_provider.dart';
import '../../providers/cita_provider.dart';

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
        await context.read<CitaProvider>().loadCitasByUsuario(user["id"]);
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    final citaProv = context.watch<CitaProvider>();

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        title: const Text("Mis Citas"),
        backgroundColor: AppTheme.primary,
        foregroundColor: Colors.white,
      ),
      body: citaProv.loading
          ? const Center(child: CircularProgressIndicator())
          : citaProv.error
              ? const Center(child: Text("Error cargando citas"))
              : citaProv.citas.isEmpty
                  ? const Center(child: Text("No tienes citas"))
                  : ListView.builder(
                      padding: const EdgeInsets.all(16),
                      itemCount: citaProv.citas.length,
                      itemBuilder: (context, index) {
                        final Map<String, dynamic> c = citaProv.citas[index];

                        final servicio = c["servicio"];
                        final servicioNombre = servicio is Map
                            ? servicio["nombre"] ?? "Servicio"
                            : "Servicio";

                        final fecha = c["fecha"]?.toString() ?? "";
                        final hora = c["horaInicio"]?.toString() ??
                            c["hora_inicio"]?.toString() ??
                            "";

                        final estado = c["estado"]?.toString() ?? "";

                        return Card(
                          margin: const EdgeInsets.only(bottom: 12),
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(12),
                          ),
                          child: ListTile(
                            title: Text(servicioNombre),
                            subtitle: Text("$fecha · $hora"),
                            trailing: Text(
                              estado,
                              style: TextStyle(
                                color: estado == "CANCELADA"
                                    ? Colors.red
                                    : AppTheme.primary,
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                          ),
                        );
                      },
                    ),
    );
  }
}
