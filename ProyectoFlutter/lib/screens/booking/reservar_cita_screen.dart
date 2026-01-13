import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../providers/user_provider.dart';
import '../../services/cita_service.dart';
import '../../utils/theme.dart';

class ReservarCitaScreen extends StatefulWidget {
  const ReservarCitaScreen({super.key});

  @override
  State<ReservarCitaScreen> createState() => _ReservarCitaScreenState();
}

class _ReservarCitaScreenState extends State<ReservarCitaScreen> {
  DateTime? fecha;
  TimeOfDay? hora;

  final servicioCtrl = TextEditingController(text: "1");
  final grupoCtrl = TextEditingController(text: "1");

  bool loading = false;

  @override
  void dispose() {
    servicioCtrl.dispose();
    grupoCtrl.dispose();
    super.dispose();
  }

  Future<void> _pickFecha() async {
    final now = DateTime.now();
    final picked = await showDatePicker(
      context: context,
      initialDate: now,
      firstDate: now,
      lastDate: now.add(const Duration(days: 365)),
    );
    if (picked != null) setState(() => fecha = picked);
  }

  Future<void> _pickHora() async {
    final picked = await showTimePicker(
      context: context,
      initialTime: TimeOfDay.now(),
    );
    if (picked != null) setState(() => hora = picked);
  }

  Future<void> _crear() async {
    final user = context.read<UserProvider>().usuario;
    if (user == null) return;

    if (fecha == null || hora == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Selecciona fecha y hora")),
      );
      return;
    }

    final idServicio = int.tryParse(servicioCtrl.text.trim());
    final idGrupo = int.tryParse(grupoCtrl.text.trim());

    if (idServicio == null || idGrupo == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Servicio y Grupo deben ser números")),
      );
      return;
    }

    setState(() => loading = true);

    final fechaStr = "${fecha!.year.toString().padLeft(4, '0')}-"
        "${fecha!.month.toString().padLeft(2, '0')}-"
        "${fecha!.day.toString().padLeft(2, '0')}";

    final h = hora!.hour.toString().padLeft(2, '0');
    final m = hora!.minute.toString().padLeft(2, '0');
    final horaInicioStr = "$h:$m:00";

    // duración fija 30 min
    final start = DateTime(
      fecha!.year,
      fecha!.month,
      fecha!.day,
      hora!.hour,
      hora!.minute,
    );
    final end = start.add(const Duration(minutes: 30));
    final horaFinStr =
        "${end.hour.toString().padLeft(2, '0')}:${end.minute.toString().padLeft(2, '0')}:00";

    // Payload compatible con backend (mínimo con ids)
    final payload = <String, dynamic>{
      "fecha": fechaStr,
      "horaInicio": horaInicioStr,
      "horaFin": horaFinStr,
      "estado": "PENDIENTE",
      "cliente": {"idUsuario": user["id"]},
      "servicio": {"idServicio": idServicio},
      // si tu backend usa grupo como objeto, lo mandamos así:
      "grupo": {"idGrupo": idGrupo},
    };

    try {
      await CitaService().createCita(payload);

      if (!mounted) return;
      setState(() => loading = false);

      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Cita creada")),
      );

      Navigator.pop(context, true); // devuelve true para refrescar MisCitas
    } catch (e) {
      if (!mounted) return;
      setState(() => loading = false);
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text("Error: $e")),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        title: const Text("Reservar cita"),
        backgroundColor: AppTheme.primary,
        foregroundColor: Colors.white,
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: [
            Card(
              child: ListTile(
                title: Text(
                  fecha == null
                      ? "Seleccionar fecha"
                      : "${fecha!.toLocal()}".split(" ")[0],
                ),
                trailing: const Icon(Icons.calendar_month),
                onTap: _pickFecha,
              ),
            ),
            Card(
              child: ListTile(
                title: Text(
                  hora == null
                      ? "Seleccionar hora"
                      : "${hora!.hour.toString().padLeft(2, '0')}:${hora!.minute.toString().padLeft(2, '0')}",
                ),
                trailing: const Icon(Icons.access_time),
                onTap: _pickHora,
              ),
            ),
            const SizedBox(height: 10),
            TextField(
              controller: servicioCtrl,
              keyboardType: TextInputType.number,
              decoration:
                  const InputDecoration(labelText: "ID Servicio (por ahora)"),
            ),
            const SizedBox(height: 10),
            TextField(
              controller: grupoCtrl,
              keyboardType: TextInputType.number,
              decoration:
                  const InputDecoration(labelText: "ID Grupo (por ahora)"),
            ),
            const Spacer(),
            SizedBox(
              width: double.infinity,
              child: ElevatedButton(
                onPressed: loading ? null : _crear,
                child: loading
                    ? const SizedBox(
                        height: 22,
                        width: 22,
                        child: CircularProgressIndicator(
                          color: Colors.white,
                          strokeWidth: 2.5,
                        ),
                      )
                    : const Text("Confirmar cita"),
              ),
            )
          ],
        ),
      ),
    );
  }
}
