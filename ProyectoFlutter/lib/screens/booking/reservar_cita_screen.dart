import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../providers/user_provider.dart';
import '../../services/cita_service.dart';
import '../../services/api_client.dart';
import '../../utils/theme.dart';

/// Pantalla de reserva rápida (alternativa a booking_screen).
/// Carga los horarios disponibles y permite al usuario elegir fecha + horario.
class ReservarCitaScreen extends StatefulWidget {
  const ReservarCitaScreen({super.key});

  @override
  State<ReservarCitaScreen> createState() => _ReservarCitaScreenState();
}

class _ReservarCitaScreenState extends State<ReservarCitaScreen> {
  DateTime? fecha;
  Map<String, dynamic>? horarioSeleccionado;
  List<Map<String, dynamic>> todosHorarios = [];
  List<Map<String, dynamic>> horariosDelDia = [];
  bool loading = false;
  bool loadingHorarios = false;

  @override
  void initState() {
    super.initState();
    _cargarTodosHorarios();
  }

  Future<void> _cargarTodosHorarios() async {
    setState(() => loadingHorarios = true);
    try {
      final response = await ApiClient().get('/horarios');
      if (response.data is List) {
        setState(() {
          todosHorarios = List<Map<String, dynamic>>.from(response.data);
        });
      }
    } catch (e) {
      debugPrint('Error: $e');
    } finally {
      setState(() => loadingHorarios = false);
    }
  }

  void _onFechaSeleccionada(DateTime d) {
    const dias = [
      '',
      'lunes',
      'martes',
      'miercoles',
      'jueves',
      'viernes',
      'sabado',
      'domingo'
    ];
    final dia = dias[d.weekday];
    setState(() {
      fecha = d;
      horarioSeleccionado = null;
      horariosDelDia = todosHorarios
          .where((h) => (h['diaSemana'] ?? '').toString().toLowerCase() == dia)
          .toList();
    });
  }

  Future<void> _pickDate() async {
    final d = await showDatePicker(
      context: context,
      initialDate: DateTime.now().add(const Duration(days: 1)),
      firstDate: DateTime.now(),
      lastDate: DateTime.now().add(const Duration(days: 90)),
      builder: (ctx, child) => Theme(
          data: Theme.of(ctx).copyWith(
              colorScheme: const ColorScheme.light(primary: AppTheme.primary)),
          child: child!),
    );
    if (d != null) _onFechaSeleccionada(d);
  }

  Future<void> _crear() async {
    final user = context.read<UserProvider>().usuario;
    if (user == null) {
      _msg("Debes iniciar sesión", Colors.red);
      return;
    }
    if (fecha == null || horarioSeleccionado == null) {
      _msg("Selecciona fecha y horario", Colors.orange);
      return;
    }

    setState(() => loading = true);
    final fechaStr =
        "${fecha!.year.toString().padLeft(4, '0')}-${fecha!.month.toString().padLeft(2, '0')}-${fecha!.day.toString().padLeft(2, '0')}";

    // ✅ Extracción de horas del horario seleccionado
    final String horaInicio = horarioSeleccionado!["horaInicio"].toString();
    final String horaFin = horarioSeleccionado!["horaFin"].toString();

    // 🚀 Print de control: Si no ves esto en consola, el emulador ejecuta código viejo
    debugPrint(
        "🚀 ESTOY EJECUTANDO EL CÓDIGO NUEVO. Mandando cita de $horaInicio a $horaFin");

    try {
      await CitaService().createCita({
        "fecha": fechaStr,
        "horaInicio": horaInicio, // ✅ Ahora sí se envía al backend
        "horaFin": horaFin, // ✅ Ahora sí se envía al backend
        "estado": "pendiente",
        "cliente": {"idUsuario": user["id"]},
        "horarioSemanal": {"idHorario": horarioSeleccionado!["idHorario"]},
      });

      if (!mounted) return;
      _msg("Cita reservada con éxito", Colors.green);
      Navigator.pop(context, true);
    } catch (e) {
      if (!mounted) return;
      _msg("Error: $e", Colors.red);
    } finally {
      if (mounted) setState(() => loading = false);
    }
  }

  void _msg(String t, Color c) =>
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(
          content: Text(t),
          backgroundColor: c,
          behavior: SnackBarBehavior.floating));

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
          title: const Text("Reservar cita"),
          backgroundColor: AppTheme.primary,
          foregroundColor: Colors.white,
          centerTitle: true),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(children: [
          Card(
            shape:
                RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
            child: ListTile(
              title: Text(fecha == null
                  ? "Seleccionar fecha"
                  : "${fecha!.day}/${fecha!.month}/${fecha!.year}"),
              trailing:
                  const Icon(Icons.calendar_month, color: AppTheme.primary),
              onTap: _pickDate,
            ),
          ),
          if (fecha != null) ...[
            const SizedBox(height: 16),
            Card(
              shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12)),
              child: Padding(
                  padding: const EdgeInsets.all(16),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Text("Horarios disponibles",
                          style: TextStyle(
                              fontWeight: FontWeight.bold, fontSize: 16)),
                      const SizedBox(height: 12),
                      if (loadingHorarios)
                        const Center(child: CircularProgressIndicator())
                      else if (horariosDelDia.isEmpty)
                        Text("No hay horarios para este día",
                            style: TextStyle(color: Colors.grey[600]))
                      else
                        Wrap(
                            spacing: 10,
                            runSpacing: 10,
                            children: horariosDelDia.map((h) {
                              final sel = horarioSeleccionado?['idHorario'] ==
                                  h['idHorario'];
                              final ini = (h['horaInicio'] ?? '').toString();
                              final fin = (h['horaFin'] ?? '').toString();
                              final servNombre =
                                  (h['servicio']?['nombre'] ?? 'Servicio')
                                      .toString();
                              return GestureDetector(
                                onTap: () =>
                                    setState(() => horarioSeleccionado = h),
                                child: Container(
                                  padding: const EdgeInsets.symmetric(
                                      horizontal: 16, vertical: 10),
                                  decoration: BoxDecoration(
                                      color: sel
                                          ? AppTheme.primary
                                          : Colors.grey[100],
                                      borderRadius: BorderRadius.circular(10),
                                      border: Border.all(
                                          color: sel
                                              ? AppTheme.primary
                                              : Colors.grey[300]!)),
                                  child: Column(
                                      crossAxisAlignment:
                                          CrossAxisAlignment.start,
                                      children: [
                                        Text(
                                            "${ini.length >= 5 ? ini.substring(0, 5) : ini} - ${fin.length >= 5 ? fin.substring(0, 5) : fin}",
                                            style: TextStyle(
                                                color: sel
                                                    ? Colors.white
                                                    : Colors.black87,
                                                fontWeight: FontWeight.bold)),
                                        Text(servNombre,
                                            style: TextStyle(
                                                color: sel
                                                    ? Colors.white70
                                                    : Colors.grey[600],
                                                fontSize: 12)),
                                      ]),
                                ),
                              );
                            }).toList()),
                    ],
                  )),
            ),
          ],
          const SizedBox(height: 30),
          SizedBox(
            width: double.infinity,
            height: 54,
            child: ElevatedButton(
              onPressed:
                  (loading || horarioSeleccionado == null) ? null : _crear,
              style: ElevatedButton.styleFrom(
                  backgroundColor: AppTheme.primary,
                  foregroundColor: Colors.white,
                  shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(14))),
              child: loading
                  ? const CircularProgressIndicator(color: Colors.white)
                  : const Text("Confirmar cita",
                      style:
                          TextStyle(fontSize: 16, fontWeight: FontWeight.bold)),
            ),
          ),
        ]),
      ),
    );
  }
}
