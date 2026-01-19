import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:peluqueria_aja/models/servicio.dart';
import 'package:peluqueria_aja/providers/user_provider.dart';
import 'package:peluqueria_aja/services/cita_service.dart';
import 'package:peluqueria_aja/services/api_client.dart';
import '../../utils/theme.dart';

class BookingScreen extends StatefulWidget {
  const BookingScreen({super.key});

  @override
  State<BookingScreen> createState() => _BookingScreenState();
}

class _BookingScreenState extends State<BookingScreen> {
  DateTime? fecha;
  String? franjaHorariaSeleccionada; // Ejemplo: "09:00-09:30"
  bool loading = false;
  bool loadingHorarios = false;

  List<String> franjasDisponibles = [];
  Map<String, dynamic>? horarioServicio;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      final Servicio servicio =
          ModalRoute.of(context)!.settings.arguments as Servicio;
      if (servicio.idServicio != null) {
        _cargarHorarioServicio(servicio.idServicio!);
      }
    });
  }

  Future<void> _cargarHorarioServicio(int idServicio) async {
    setState(() => loadingHorarios = true);

    try {
      final response = await ApiClient().get('/horarios/servicio/$idServicio');

      if (response.data is List && (response.data as List).isNotEmpty) {
        setState(() {
          horarioServicio = response.data[0]; // Tomar el primer horario
          debugPrint("✅ Horario cargado: $horarioServicio");
        });
      } else {
        debugPrint("⚠️ No hay horarios configurados para este servicio");
      }
    } catch (e) {
      debugPrint("❌ Error cargando horario: $e");
    } finally {
      setState(() => loadingHorarios = false);
    }
  }

  Future<void> _pickDate(Servicio servicio) async {
    final newDate = await showDatePicker(
      context: context,
      initialDate: DateTime.now(),
      firstDate: DateTime.now(),
      lastDate: DateTime.now().add(const Duration(days: 90)),
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

    if (newDate != null) {
      setState(() {
        fecha = newDate;
        franjaHorariaSeleccionada = null; // Resetear franja
      });
      await _generarFranjasHorarias(servicio);
    }
  }

  Future<void> _generarFranjasHorarias(Servicio servicio) async {
    if (fecha == null || horarioServicio == null) return;

    setState(() => loadingHorarios = true);

    try {
      // Parsear horario del servicio
      final horaInicio = _parseTime(
          horarioServicio!['horaInicio'] ?? horarioServicio!['hora_inicio']);
      final horaFin = _parseTime(
          horarioServicio!['horaFin'] ?? horarioServicio!['hora_fin']);

      if (horaInicio == null || horaFin == null) {
        throw Exception("Horarios mal formateados");
      }

      final duracionMinutos = servicio.duracion;

      // Obtener citas ya reservadas para este día
      final citasDelDia =
          await _obtenerCitasDelDia(servicio.idServicio!, fecha!);

      // Generar franjas
      List<String> franjas = [];
      DateTime actual = DateTime(fecha!.year, fecha!.month, fecha!.day,
          horaInicio.hour, horaInicio.minute);
      final finDelDia = DateTime(
          fecha!.year, fecha!.month, fecha!.day, horaFin.hour, horaFin.minute);

      while (
          actual.add(Duration(minutes: duracionMinutos)).isBefore(finDelDia) ||
              actual
                  .add(Duration(minutes: duracionMinutos))
                  .isAtSameMomentAs(finDelDia)) {
        final inicioFranja = TimeOfDay.fromDateTime(actual);
        final finFranja = TimeOfDay.fromDateTime(
            actual.add(Duration(minutes: duracionMinutos)));

        final franjaStr =
            "${_formatTime(inicioFranja)}-${_formatTime(finFranja)}";

        // Verificar si la franja está ocupada
        final estaOcupada = citasDelDia.any((cita) {
          final citaInicio =
              _parseTime(cita['horaInicio'] ?? cita['hora_inicio']);
          final citaFin = _parseTime(cita['horaFin'] ?? cita['hora_fin']);

          if (citaInicio == null || citaFin == null) return false;

          final citaInicioDate = DateTime(fecha!.year, fecha!.month, fecha!.day,
              citaInicio.hour, citaInicio.minute);
          final citaFinDate = DateTime(fecha!.year, fecha!.month, fecha!.day,
              citaFin.hour, citaFin.minute);

          // Verificar solapamiento
          return actual.isBefore(citaFinDate) &&
              actual
                  .add(Duration(minutes: duracionMinutos))
                  .isAfter(citaInicioDate);
        });

        if (!estaOcupada) {
          franjas.add(franjaStr);
        }

        actual = actual.add(Duration(minutes: duracionMinutos));
      }

      setState(() {
        franjasDisponibles = franjas;
      });

      debugPrint("✅ Franjas generadas: ${franjas.length}");
    } catch (e) {
      debugPrint("❌ Error generando franjas: $e");
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
            content: Text("Error al cargar horarios: $e"),
            backgroundColor: Colors.red),
      );
    } finally {
      setState(() => loadingHorarios = false);
    }
  }

  Future<List<dynamic>> _obtenerCitasDelDia(
      int idServicio, DateTime fecha) async {
    try {
      final fechaStr = "${fecha.year.toString().padLeft(4, '0')}-"
          "${fecha.month.toString().padLeft(2, '0')}-"
          "${fecha.day.toString().padLeft(2, '0')}";

      final response = await ApiClient().get('/citas');
      final todasCitas = response.data as List;

      return todasCitas.where((cita) {
        final citaServicioId =
            cita['servicio']?['idServicio'] ?? cita['servicio']?['id_servicio'];
        final citaFecha = cita['fecha'];
        final citaEstado = (cita['estado'] ?? '').toString().toLowerCase();

        return citaServicioId == idServicio &&
            citaFecha == fechaStr &&
            citaEstado != 'cancelada';
      }).toList();
    } catch (e) {
      debugPrint("❌ Error obteniendo citas: $e");
      return [];
    }
  }

  TimeOfDay? _parseTime(dynamic time) {
    try {
      if (time == null) return null;

      String timeStr = time.toString();
      final parts = timeStr.split(':');

      if (parts.length >= 2) {
        return TimeOfDay(
          hour: int.parse(parts[0]),
          minute: int.parse(parts[1]),
        );
      }
    } catch (e) {
      debugPrint("Error parseando hora: $time");
    }
    return null;
  }

  String _formatTime(TimeOfDay time) {
    return "${time.hour.toString().padLeft(2, '0')}:${time.minute.toString().padLeft(2, '0')}";
  }

  Future<void> _confirmBooking(Servicio servicio) async {
    final usuario = context.read<UserProvider>().usuario;

    if (usuario == null) {
      _showError("Usuario no disponible");
      return;
    }

    if (fecha == null || franjaHorariaSeleccionada == null) {
      _showError("Por favor, selecciona fecha y hora");
      return;
    }

    setState(() => loading = true);

    final fechaStr = "${fecha!.year.toString().padLeft(4, '0')}-"
        "${fecha!.month.toString().padLeft(2, '0')}-"
        "${fecha!.day.toString().padLeft(2, '0')}";

    final horarios = franjaHorariaSeleccionada!.split('-');
    final horaInicio = "${horarios[0]}:00";
    final horaFin = "${horarios[1]}:00";

    final citaData = <String, dynamic>{
      "fecha": fechaStr,
      "horaInicio": horaInicio,
      "horaFin": horaFin,
      "estado": "pendiente",
      "cliente": {"idUsuario": usuario["id"]},
      "servicio": {"idServicio": servicio.idServicio},
    };

    try {
      await CitaService().createCita(citaData);

      if (!mounted) return;
      Navigator.pushReplacementNamed(context, "/reservation-confirmed");
    } catch (e) {
      if (!mounted) return;
      _showError("Error al crear la cita: ${e.toString()}");
    } finally {
      if (mounted) setState(() => loading = false);
    }
  }

  void _showError(String message) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
          content: Text(message),
          backgroundColor: Colors.redAccent,
          behavior: SnackBarBehavior.floating),
    );
  }

  @override
  Widget build(BuildContext context) {
    final Servicio servicio =
        ModalRoute.of(context)!.settings.arguments as Servicio;

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        title: const Text("Confirmar Reserva",
            style: TextStyle(fontWeight: FontWeight.w600)),
        centerTitle: true,
        backgroundColor: AppTheme.primary,
        foregroundColor: Colors.white,
        elevation: 0,
      ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            // ENCABEZADO
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
                  ClipRRect(
                    borderRadius: BorderRadius.circular(16),
                    child:
                        servicio.imagen != null && servicio.imagen!.isNotEmpty
                            ? Image.network(servicio.imagen!,
                                height: 120,
                                width: 120,
                                fit: BoxFit.cover,
                                errorBuilder: (_, __, ___) => Container(
                                      height: 120,
                                      width: 120,
                                      color: Colors.white.withOpacity(0.2),
                                      child: const Icon(Icons.content_cut,
                                          size: 50, color: Colors.white),
                                    ))
                            : Container(
                                height: 120,
                                width: 120,
                                decoration: BoxDecoration(
                                  color: Colors.white.withOpacity(0.2),
                                  borderRadius: BorderRadius.circular(16),
                                ),
                                child: const Icon(Icons.content_cut,
                                    size: 50, color: Colors.white),
                              ),
                  ),
                  const SizedBox(height: 16),
                  Text(servicio.nombre,
                      textAlign: TextAlign.center,
                      style: const TextStyle(
                          color: Colors.white,
                          fontSize: 24,
                          fontWeight: FontWeight.bold)),
                  const SizedBox(height: 8),
                  Container(
                    padding:
                        const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
                    decoration: BoxDecoration(
                      color: Colors.white.withOpacity(0.2),
                      borderRadius: BorderRadius.circular(20),
                    ),
                    child: Text("${servicio.precio.toStringAsFixed(2)} €",
                        style: const TextStyle(
                            color: Colors.white, fontWeight: FontWeight.bold)),
                  ),
                ],
              ),
            ),

            const SizedBox(height: 30),

            // SELECCIÓN DE FECHA
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
                        offset: const Offset(0, 5))
                  ],
                ),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text("Selecciona tu horario",
                        style: TextStyle(
                            fontSize: 18,
                            fontWeight: FontWeight.bold,
                            color: Colors.grey[800])),
                    const SizedBox(height: 24),
                    _SelectionTile(
                      label: "Fecha",
                      value: fecha == null
                          ? "Seleccionar día"
                          : "${fecha!.day}/${fecha!.month}/${fecha!.year}",
                      icon: Icons.calendar_today_rounded,
                      isSelected: fecha != null,
                      onTap: () => _pickDate(servicio),
                    ),
                  ],
                ),
              ),
            ),

            // FRANJAS HORARIAS
            if (fecha != null) ...[
              const SizedBox(height: 20),
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
                          offset: const Offset(0, 5))
                    ],
                  ),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text("Horarios disponibles",
                          style: TextStyle(
                              fontSize: 18,
                              fontWeight: FontWeight.bold,
                              color: Colors.grey[800])),
                      const SizedBox(height: 16),
                      if (loadingHorarios)
                        const Center(child: CircularProgressIndicator())
                      else if (franjasDisponibles.isEmpty)
                        const Center(
                            child: Text(
                                "No hay horarios disponibles para este día",
                                style: TextStyle(color: Colors.grey)))
                      else
                        Wrap(
                          spacing: 10,
                          runSpacing: 10,
                          children: franjasDisponibles.map((franja) {
                            final isSelected =
                                franjaHorariaSeleccionada == franja;
                            return InkWell(
                              onTap: () => setState(
                                  () => franjaHorariaSeleccionada = franja),
                              child: Container(
                                padding: const EdgeInsets.symmetric(
                                    horizontal: 16, vertical: 12),
                                decoration: BoxDecoration(
                                  color: isSelected
                                      ? AppTheme.primary
                                      : Colors.grey[100],
                                  borderRadius: BorderRadius.circular(12),
                                  border: Border.all(
                                      color: isSelected
                                          ? AppTheme.primary
                                          : Colors.grey[300]!,
                                      width: 2),
                                ),
                                child: Text(franja,
                                    style: TextStyle(
                                      color: isSelected
                                          ? Colors.white
                                          : Colors.black87,
                                      fontWeight: isSelected
                                          ? FontWeight.bold
                                          : FontWeight.normal,
                                    )),
                              ),
                            );
                          }).toList(),
                        ),
                    ],
                  ),
                ),
              ),
            ],

            const SizedBox(height: 40),

            // BOTÓN CONFIRMAR
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
                        borderRadius: BorderRadius.circular(16)),
                  ),
                  child: loading
                      ? const CircularProgressIndicator(
                          color: Colors.white, strokeWidth: 2.5)
                      : const Text("Confirmar Cita",
                          style: TextStyle(
                              fontSize: 18, fontWeight: FontWeight.bold)),
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
              width: isSelected ? 1.5 : 1),
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
              child: Icon(icon,
                  color: isSelected ? AppTheme.primary : Colors.grey[400],
                  size: 22),
            ),
            const SizedBox(width: 16),
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(label,
                    style: TextStyle(
                        fontSize: 12,
                        color: Colors.grey[600],
                        fontWeight: FontWeight.w500)),
                const SizedBox(height: 4),
                Text(value,
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight:
                          isSelected ? FontWeight.bold : FontWeight.normal,
                      color: isSelected ? Colors.black87 : Colors.grey[500],
                    )),
              ],
            ),
            const Spacer(),
            Icon(Icons.arrow_forward_ios_rounded,
                size: 14, color: Colors.grey[400])
          ],
        ),
      ),
    );
  }
}
