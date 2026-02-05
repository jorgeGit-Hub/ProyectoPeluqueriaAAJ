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
  String? franjaHorariaSeleccionada;
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
          horarioServicio = response.data[0];
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
        franjaHorariaSeleccionada = null;
      });
      await _generarFranjasHorarias(servicio);
    }
  }

  Future<void> _generarFranjasHorarias(Servicio servicio) async {
    if (fecha == null || horarioServicio == null) return;

    setState(() => loadingHorarios = true);

    try {
      final horaInicio = _parseTime(
          horarioServicio!['horaInicio'] ?? horarioServicio!['hora_inicio']);
      final horaFin = _parseTime(
          horarioServicio!['horaFin'] ?? horarioServicio!['hora_fin']);

      if (horaInicio == null || horaFin == null) {
        throw Exception("Horarios mal formateados");
      }

      final duracionMinutos = servicio.duracion;
      final citasDelDia =
          await _obtenerCitasDelDia(servicio.idServicio!, fecha!);

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

        final estaOcupada = citasDelDia.any((cita) {
          final citaInicio =
              _parseTime(cita['horaInicio'] ?? cita['hora_inicio']);
          final citaFin = _parseTime(cita['horaFin'] ?? cita['hora_fin']);

          if (citaInicio == null || citaFin == null) return false;

          final citaInicioDate = DateTime(fecha!.year, fecha!.month, fecha!.day,
              citaInicio.hour, citaInicio.minute);
          final citaFinDate = DateTime(fecha!.year, fecha!.month, fecha!.day,
              citaFin.hour, citaFin.minute);

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

  String _getMonthName(int month) {
    const months = [
      'Enero',
      'Febrero',
      'Marzo',
      'Abril',
      'Mayo',
      'Junio',
      'Julio',
      'Agosto',
      'Septiembre',
      'Octubre',
      'Noviembre',
      'Diciembre'
    ];
    return months[month - 1];
  }

  @override
  Widget build(BuildContext context) {
    final Servicio servicio =
        ModalRoute.of(context)!.settings.arguments as Servicio;

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      body: Stack(
        children: [
          CustomScrollView(
            slivers: [
              // Header
              _buildHeader(servicio),

              // Contenido
              SliverToBoxAdapter(
                child: Column(
                  children: [
                    const SizedBox(height: 20),

                    // Título de sección
                    Padding(
                      padding: const EdgeInsets.symmetric(horizontal: 20),
                      child: Row(
                        children: [
                          Container(
                            height: 4,
                            width: 40,
                            decoration: BoxDecoration(
                              color: AppTheme.primary,
                              borderRadius: BorderRadius.circular(2),
                            ),
                          ),
                          const SizedBox(width: 12),
                          const Text(
                            'Selecciona tu horario',
                            style: TextStyle(
                              fontSize: 20,
                              fontWeight: FontWeight.bold,
                              color: Colors.black87,
                            ),
                          ),
                        ],
                      ),
                    ),

                    const SizedBox(height: 20),

                    // Selección de fecha
                    _buildDateSelector(),

                    // Horarios disponibles
                    if (fecha != null) ...[
                      const SizedBox(height: 24),
                      _buildTimeSlots(),
                    ],

                    // Resumen
                    if (franjaHorariaSeleccionada != null) ...[
                      const SizedBox(height: 24),
                      _buildSummary(servicio),
                    ],

                    const SizedBox(height: 120),
                  ],
                ),
              ),
            ],
          ),

          // Botón de confirmación
          if (franjaHorariaSeleccionada != null) _buildConfirmButton(servicio),
        ],
      ),
    );
  }

  Widget _buildHeader(Servicio servicio) {
    return SliverAppBar(
      expandedHeight: 200,
      pinned: true,
      backgroundColor: AppTheme.primary,
      leading: Container(
        margin: const EdgeInsets.all(8),
        decoration: BoxDecoration(
          color: Colors.white,
          shape: BoxShape.circle,
        ),
        child: IconButton(
          icon: const Icon(Icons.arrow_back, color: AppTheme.primary),
          onPressed: () => Navigator.pop(context),
        ),
      ),
      flexibleSpace: FlexibleSpaceBar(
        background: Container(
          decoration: BoxDecoration(
            gradient: LinearGradient(
              begin: Alignment.topCenter,
              end: Alignment.bottomCenter,
              colors: [
                AppTheme.primary,
                AppTheme.primary.withOpacity(0.8),
              ],
            ),
          ),
          child: SafeArea(
            child: Padding(
              padding: const EdgeInsets.fromLTRB(20, 60, 20, 20),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisAlignment: MainAxisAlignment.end,
                children: [
                  const Text(
                    'Reservar Cita',
                    style: TextStyle(
                      color: Colors.white,
                      fontSize: 28,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  const SizedBox(height: 8),
                  Container(
                    padding:
                        const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
                    decoration: BoxDecoration(
                      color: Colors.white.withOpacity(0.2),
                      borderRadius: BorderRadius.circular(20),
                    ),
                    child: Row(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        const Icon(Icons.content_cut,
                            color: Colors.white, size: 16),
                        const SizedBox(width: 6),
                        Text(
                          servicio.nombre,
                          style: const TextStyle(
                            color: Colors.white,
                            fontWeight: FontWeight.w500,
                          ),
                        ),
                      ],
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildDateSelector() {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 20),
      child: Container(
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(20),
          boxShadow: [
            BoxShadow(
              color: Colors.black.withOpacity(0.06),
              blurRadius: 15,
              offset: const Offset(0, 5),
            ),
          ],
        ),
        child: Material(
          color: Colors.transparent,
          child: InkWell(
            onTap: () => _pickDate(
                ModalRoute.of(context)!.settings.arguments as Servicio),
            borderRadius: BorderRadius.circular(20),
            child: Padding(
              padding: const EdgeInsets.all(20),
              child: Row(
                children: [
                  Container(
                    height: 56,
                    width: 56,
                    decoration: BoxDecoration(
                      color: AppTheme.primary.withOpacity(0.1),
                      borderRadius: BorderRadius.circular(14),
                    ),
                    child: const Icon(
                      Icons.calendar_today_rounded,
                      color: AppTheme.primary,
                      size: 28,
                    ),
                  ),
                  const SizedBox(width: 16),
                  Expanded(
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(
                          'Fecha',
                          style: TextStyle(
                            fontSize: 13,
                            color: Colors.grey[600],
                            fontWeight: FontWeight.w500,
                          ),
                        ),
                        const SizedBox(height: 4),
                        Text(
                          fecha == null
                              ? 'Seleccionar día'
                              : "${fecha!.day} de ${_getMonthName(fecha!.month)}, ${fecha!.year}",
                          style: TextStyle(
                            fontSize: 16,
                            fontWeight: FontWeight.bold,
                            color: fecha == null
                                ? Colors.grey[400]
                                : Colors.black87,
                          ),
                        ),
                      ],
                    ),
                  ),
                  Icon(
                    Icons.arrow_forward_ios_rounded,
                    size: 16,
                    color: Colors.grey[400],
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildTimeSlots() {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 20),
      child: Container(
        padding: const EdgeInsets.all(24),
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(20),
          boxShadow: [
            BoxShadow(
              color: Colors.black.withOpacity(0.06),
              blurRadius: 15,
              offset: const Offset(0, 5),
            ),
          ],
        ),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const Text(
              'Horarios disponibles',
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.bold,
                color: Colors.black87,
              ),
            ),
            const SizedBox(height: 16),
            if (loadingHorarios)
              const Center(
                child: Padding(
                  padding: EdgeInsets.all(20),
                  child: CircularProgressIndicator(),
                ),
              )
            else if (franjasDisponibles.isEmpty)
              Center(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Icon(Icons.event_busy, size: 48, color: Colors.grey[300]),
                      const SizedBox(height: 12),
                      Text(
                        "No hay horarios disponibles para este día",
                        textAlign: TextAlign.center,
                        style: TextStyle(color: Colors.grey[600]),
                      ),
                    ],
                  ),
                ),
              )
            else
              Wrap(
                spacing: 12,
                runSpacing: 12,
                children: franjasDisponibles.map((franja) {
                  final isSelected = franjaHorariaSeleccionada == franja;
                  return InkWell(
                    onTap: () =>
                        setState(() => franjaHorariaSeleccionada = franja),
                    borderRadius: BorderRadius.circular(12),
                    child: Container(
                      padding: const EdgeInsets.symmetric(
                          horizontal: 20, vertical: 14),
                      decoration: BoxDecoration(
                        color: isSelected ? AppTheme.primary : Colors.grey[50],
                        borderRadius: BorderRadius.circular(12),
                        border: Border.all(
                          color:
                              isSelected ? AppTheme.primary : Colors.grey[300]!,
                          width: isSelected ? 2 : 1,
                        ),
                      ),
                      child: Row(
                        mainAxisSize: MainAxisSize.min,
                        children: [
                          Icon(
                            Icons.access_time,
                            size: 18,
                            color: isSelected ? Colors.white : Colors.grey[600],
                          ),
                          const SizedBox(width: 8),
                          Text(
                            franja,
                            style: TextStyle(
                              color: isSelected ? Colors.white : Colors.black87,
                              fontWeight: isSelected
                                  ? FontWeight.bold
                                  : FontWeight.w500,
                              fontSize: 15,
                            ),
                          ),
                        ],
                      ),
                    ),
                  );
                }).toList(),
              ),
          ],
        ),
      ),
    );
  }

  Widget _buildSummary(Servicio servicio) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 20),
      child: Container(
        padding: const EdgeInsets.all(24),
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(20),
          border:
              Border.all(color: AppTheme.primary.withOpacity(0.3), width: 2),
          boxShadow: [
            BoxShadow(
              color: AppTheme.primary.withOpacity(0.1),
              blurRadius: 15,
              offset: const Offset(0, 5),
            ),
          ],
        ),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              children: [
                const Icon(Icons.check_circle, color: Colors.green, size: 24),
                const SizedBox(width: 12),
                const Text(
                  'Resumen de tu reserva',
                  style: TextStyle(
                    fontSize: 18,
                    fontWeight: FontWeight.bold,
                    color: Colors.black87,
                  ),
                ),
              ],
            ),
            const SizedBox(height: 20),
            _buildSummaryRow('Servicio', servicio.nombre),
            const Divider(height: 24),
            _buildSummaryRow(
                'Fecha', "${fecha!.day}/${fecha!.month}/${fecha!.year}"),
            const Divider(height: 24),
            _buildSummaryRow('Horario', franjaHorariaSeleccionada!),
            const Divider(height: 24),
            _buildSummaryRow(
                'Precio', '\$${servicio.precio.toStringAsFixed(2)}',
                isPrice: true),
          ],
        ),
      ),
    );
  }

  Widget _buildSummaryRow(String label, String value, {bool isPrice = false}) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Text(
          label,
          style: TextStyle(
            fontSize: 15,
            color: Colors.grey[600],
          ),
        ),
        Text(
          value,
          style: TextStyle(
            fontSize: isPrice ? 20 : 15,
            fontWeight: isPrice ? FontWeight.bold : FontWeight.w600,
            color: isPrice ? AppTheme.primary : Colors.black87,
          ),
        ),
      ],
    );
  }

  Widget _buildConfirmButton(Servicio servicio) {
    return Positioned(
      bottom: 0,
      left: 0,
      right: 0,
      child: Container(
        padding: const EdgeInsets.all(20),
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: const BorderRadius.only(
            topLeft: Radius.circular(30),
            topRight: Radius.circular(30),
          ),
          boxShadow: [
            BoxShadow(
              color: Colors.black.withOpacity(0.1),
              blurRadius: 20,
              offset: const Offset(0, -5),
            ),
          ],
        ),
        child: SafeArea(
          top: false,
          child: SizedBox(
            width: double.infinity,
            height: 56,
            child: ElevatedButton(
              onPressed: loading ? null : () => _confirmBooking(servicio),
              style: ElevatedButton.styleFrom(
                backgroundColor: AppTheme.primary,
                foregroundColor: Colors.white,
                elevation: 0,
                disabledBackgroundColor: Colors.grey[300],
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
                  : const Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Icon(Icons.check_circle_outline, size: 22),
                        SizedBox(width: 12),
                        Text(
                          'Confirmar Cita',
                          style: TextStyle(
                            fontSize: 18,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ],
                    ),
            ),
          ),
        ),
      ),
    );
  }
}
