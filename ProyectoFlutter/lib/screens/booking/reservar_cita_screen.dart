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
      builder: (context, child) {
        return Theme(
          data: Theme.of(context).copyWith(
            colorScheme: const ColorScheme.light(primary: AppTheme.primary),
          ),
          child: child!,
        );
      },
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
    if (user == null) {
      _showMsg("Debes iniciar sesión", Colors.red);
      return;
    }

    if (fecha == null || hora == null) {
      _showMsg("Selecciona fecha y hora", Colors.orange);
      return;
    }

    final idServicio = int.tryParse(servicioCtrl.text.trim());
    final idGrupo = int.tryParse(grupoCtrl.text.trim());

    if (idServicio == null || idGrupo == null) {
      _showMsg("Los IDs deben ser números válidos", Colors.red);
      return;
    }

    setState(() => loading = true);

    final fechaStr =
        "${fecha!.year}-${fecha!.month.toString().padLeft(2, '0')}-${fecha!.day.toString().padLeft(2, '0')}";

    final h = hora!.hour.toString().padLeft(2, '0');
    final m = hora!.minute.toString().padLeft(2, '0');
    final horaInicioStr = "$h:$m:00";

    final start = DateTime(
        fecha!.year, fecha!.month, fecha!.day, hora!.hour, hora!.minute);
    final end = start.add(const Duration(minutes: 30));
    final horaFinStr =
        "${end.hour.toString().padLeft(2, '0')}:${end.minute.toString().padLeft(2, '0')}:00";

    final payload = {
      "fecha": fechaStr,
      "horaInicio": horaInicioStr,
      "horaFin": horaFinStr,
      "estado": "PENDIENTE",
      "cliente": {"idUsuario": user["id"]},
      "servicio": {"idServicio": idServicio},
      "grupo": {"idGrupo": idGrupo},
    };

    try {
      final success = await CitaService().createCita(payload);

      if (!mounted) return;
      setState(() => loading = false);

      if (success) {
        _showMsg("Cita reservada con éxito", Colors.green);
        Navigator.pop(context, true);
      }
    } catch (e) {
      if (!mounted) return;
      setState(() => loading = false);
      _showMsg("Error de conexión: Verifica tu servidor", Colors.red);
    }
  }

  void _showMsg(String text, Color color) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
          content: Text(text),
          backgroundColor: color,
          behavior: SnackBarBehavior.floating),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        title: const Text("Reservar cita",
            style: TextStyle(fontWeight: FontWeight.w600)),
        backgroundColor: AppTheme.primary,
        foregroundColor: Colors.white,
        centerTitle: true,
        elevation: 0,
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: [
            Card(
              shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12)),
              child: ListTile(
                title: Text(fecha == null
                    ? "Seleccionar fecha"
                    : "${fecha!.day}/${fecha!.month}/${fecha!.year}"),
                trailing:
                    const Icon(Icons.calendar_month, color: AppTheme.primary),
                onTap: _pickFecha,
              ),
            ),
            const SizedBox(height: 10),
            Card(
              shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12)),
              child: ListTile(
                title: Text(
                    hora == null ? "Seleccionar hora" : hora!.format(context)),
                trailing:
                    const Icon(Icons.access_time, color: AppTheme.primary),
                onTap: _pickHora,
              ),
            ),
            const SizedBox(height: 20),
            TextField(
              controller: servicioCtrl,
              keyboardType: TextInputType.number,
              decoration: InputDecoration(
                labelText: "ID Servicio",
                border:
                    OutlineInputBorder(borderRadius: BorderRadius.circular(12)),
                filled: true,
                fillColor: Colors.white,
              ),
            ),
            const SizedBox(height: 15),
            TextField(
              controller: grupoCtrl,
              keyboardType: TextInputType.number,
              decoration: InputDecoration(
                labelText: "ID Grupo",
                border:
                    OutlineInputBorder(borderRadius: BorderRadius.circular(12)),
                filled: true,
                fillColor: Colors.white,
              ),
            ),
            const SizedBox(height: 40),
            SizedBox(
              width: double.infinity,
              height: 54,
              child: ElevatedButton(
                onPressed: loading ? null : _crear,
                style: ElevatedButton.styleFrom(
                  backgroundColor: AppTheme.primary,
                  foregroundColor: Colors.white,
                  shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(14)),
                ),
                child: loading
                    ? const CircularProgressIndicator(color: Colors.white)
                    : const Text("Confirmar cita",
                        style: TextStyle(
                            fontSize: 16, fontWeight: FontWeight.bold)),
              ),
            )
          ],
        ),
      ),
    );
  }
}
