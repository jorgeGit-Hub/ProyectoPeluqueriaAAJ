import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'package:peluqueria_aja/models/servicio.dart';
import 'package:peluqueria_aja/providers/user_provider.dart';
import 'package:peluqueria_aja/services/cita_service.dart';
import '../../utils/theme.dart';

class BookingScreen extends StatefulWidget {
  const BookingScreen({super.key});

  @override
  State<BookingScreen> createState() => _BookingScreenState();
}

class _BookingScreenState extends State<BookingScreen> {
  DateTime? fecha;
  TimeOfDay? hora;
  bool loading = false;

  Future<void> _pickDate() async {
    final newDate = await showDatePicker(
      context: context,
      initialDate: DateTime.now(),
      firstDate: DateTime.now(),
      lastDate: DateTime(2030),
      builder: (context, child) {
        return Theme(
          data: Theme.of(context).copyWith(
            colorScheme: const ColorScheme.light(
              primary: AppTheme.primary,
              onPrimary: Colors.white,
              onSurface: Colors.black,
            ),
          ),
          child: child!,
        );
      },
    );
    if (newDate != null) setState(() => fecha = newDate);
  }

  Future<void> _pickTime() async {
    final newTime = await showTimePicker(
      context: context,
      initialTime: TimeOfDay.now(),
      builder: (context, child) {
        return Theme(
          data: Theme.of(context).copyWith(
            colorScheme: const ColorScheme.light(
              primary: AppTheme.primary,
              onPrimary: Colors.white,
              onSurface: Colors.black,
            ),
          ),
          child: child!,
        );
      },
    );
    if (newTime != null) setState(() => hora = newTime);
  }

  Future<void> _confirmBooking(Servicio servicio) async {
    final usuario = context.read<UserProvider>().usuario;

    if (usuario == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("Usuario no disponible"),
          backgroundColor: Colors.redAccent,
          behavior: SnackBarBehavior.floating,
        ),
      );
      return;
    }

    if (fecha == null || hora == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("Por favor, selecciona fecha y hora"),
          backgroundColor: Colors.orange,
          behavior: SnackBarBehavior.floating,
        ),
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

    // Duración fija 30 min (igual que reservar_cita_screen)
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

    // Payload alineado con lo que venimos usando:
    final citaData = <String, dynamic>{
      "fecha": fechaStr,
      "horaInicio": horaInicioStr,
      "horaFin": horaFinStr,
      "estado": "PENDIENTE",
      "cliente": {"idUsuario": usuario["id"]},
      "servicio": {"idServicio": servicio.idServicio},
      // Si tu backend NO usa grupo aquí, no lo mandamos.
    };

    try {
      await CitaService().createCita(citaData);

      if (!mounted) return;

      // Si tienes ruta de confirmación, úsala; si no, vuelve atrás.
      Navigator.pushReplacementNamed(context, "/reservation-confirmed");
    } catch (_) {
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("Error al crear la cita. Inténtalo de nuevo."),
          backgroundColor: Colors.redAccent,
          behavior: SnackBarBehavior.floating,
        ),
      );
    } finally {
      if (mounted) setState(() => loading = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    final Servicio servicio =
        ModalRoute.of(context)!.settings.arguments as Servicio;

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        title: const Text(
          "Confirmar Reserva",
          style: TextStyle(fontWeight: FontWeight.w600),
        ),
        centerTitle: true,
        backgroundColor: AppTheme.primary,
        foregroundColor: Colors.white,
        elevation: 0,
      ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            // 1. RESUMEN DEL SERVICIO
            Container(
              width: double.infinity,
              padding: const EdgeInsets.all(24),
              decoration: const BoxDecoration(
                color: AppTheme.primary,
                borderRadius: BorderRadius.only(
                  bottomLeft: Radius.circular(30),
                  bottomRight: Radius.circular(30),
                ),
              ),
              child: Column(
                children: [
                  const Text(
                    "Estás reservando:",
                    style: TextStyle(color: Colors.white70, fontSize: 14),
                  ),
                  const SizedBox(height: 8),
                  Text(
                    servicio.nombre,
                    textAlign: TextAlign.center,
                    style: const TextStyle(
                      color: Colors.white,
                      fontSize: 24,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  const SizedBox(height: 8),
                  Container(
                    padding: const EdgeInsets.symmetric(
                      horizontal: 12,
                      vertical: 6,
                    ),
                    decoration: BoxDecoration(
                      color: Colors.white.withOpacity(0.2),
                      borderRadius: BorderRadius.circular(20),
                    ),
                    child: Text(
                      "${servicio.precio.toStringAsFixed(2)} €",
                      style: const TextStyle(
                        color: Colors.white,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),
                  const SizedBox(height: 20),
                ],
              ),
            ),

            const SizedBox(height: 30),

            // 2. FORMULARIO DE SELECCIÓN
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 20),
              child: Container(
                padding: const EdgeInsets.all(24),
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.circular(24),
                  boxShadow: [
                    BoxShadow(
                      color: Colors.black.withOpacity(0.05),
                      blurRadius: 15,
                      offset: const Offset(0, 5),
                    ),
                  ],
                ),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      "Elige tu horario",
                      style: TextStyle(
                        fontSize: 18,
                        fontWeight: FontWeight.bold,
                        color: Colors.grey[800],
                      ),
                    ),
                    const SizedBox(height: 24),
                    _SelectionTile(
                      label: "Fecha",
                      value: fecha == null
                          ? "Seleccionar día"
                          : "${fecha!.day}/${fecha!.month}/${fecha!.year}",
                      icon: Icons.calendar_today_rounded,
                      isSelected: fecha != null,
                      onTap: _pickDate,
                    ),
                    const SizedBox(height: 16),
                    const Divider(),
                    const SizedBox(height: 16),
                    _SelectionTile(
                      label: "Hora",
                      value: hora == null
                          ? "Seleccionar hora"
                          : hora!.format(context),
                      icon: Icons.access_time_rounded,
                      isSelected: hora != null,
                      onTap: _pickTime,
                    ),
                  ],
                ),
              ),
            ),

            const SizedBox(height: 40),

            // 3. BOTÓN CONFIRMAR
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 24),
              child: SizedBox(
                width: double.infinity,
                height: 56,
                child: ElevatedButton(
                  onPressed: loading ? null : () => _confirmBooking(servicio),
                  style: ElevatedButton.styleFrom(
                    backgroundColor: AppTheme.primary,
                    foregroundColor: Colors.white,
                    elevation: 8,
                    shadowColor: AppTheme.primary.withOpacity(0.4),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(16),
                    ),
                  ),
                  child: loading
                      ? const SizedBox(
                          height: 24,
                          width: 24,
                          child: CircularProgressIndicator(
                            color: Colors.white,
                            strokeWidth: 2.5,
                          ),
                        )
                      : const Text(
                          "Confirmar Cita",
                          style: TextStyle(
                            fontSize: 18,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                ),
              ),
            ),
            const SizedBox(height: 30),
          ],
        ),
      ),
    );
  }
}

class _SelectionTile extends StatelessWidget {
  final String label;
  final String value;
  final IconData icon;
  final VoidCallback onTap;
  final bool isSelected;

  const _SelectionTile({
    required this.label,
    required this.value,
    required this.icon,
    required this.onTap,
    required this.isSelected,
  });

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      borderRadius: BorderRadius.circular(12),
      child: Container(
        padding: const EdgeInsets.symmetric(vertical: 12, horizontal: 16),
        decoration: BoxDecoration(
          color: Colors.grey[50],
          borderRadius: BorderRadius.circular(12),
          border: Border.all(
            color: isSelected ? AppTheme.primary : Colors.grey[300]!,
            width: isSelected ? 1.5 : 1,
          ),
        ),
        child: Row(
          children: [
            Container(
              padding: const EdgeInsets.all(8),
              decoration: BoxDecoration(
                color: isSelected
                    ? AppTheme.primary.withOpacity(0.1)
                    : Colors.white,
                borderRadius: BorderRadius.circular(8),
              ),
              child: Icon(
                icon,
                color: isSelected ? AppTheme.primary : Colors.grey[400],
                size: 22,
              ),
            ),
            const SizedBox(width: 16),
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  label,
                  style: TextStyle(
                    fontSize: 12,
                    color: Colors.grey[600],
                    fontWeight: FontWeight.w500,
                  ),
                ),
                const SizedBox(height: 4),
                Text(
                  value,
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight:
                        isSelected ? FontWeight.bold : FontWeight.normal,
                    color: isSelected ? Colors.black87 : Colors.grey[500],
                  ),
                ),
              ],
            ),
            const Spacer(),
            Icon(
              Icons.arrow_forward_ios_rounded,
              size: 14,
              color: Colors.grey[400],
            )
          ],
        ),
      ),
    );
  }
}
