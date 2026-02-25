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
  Map<String, dynamic>? horarioSeleccionado;
  bool loading = false;
  bool loadingHorarios = false;
  List<Map<String, dynamic>> horariosDelServicio = [];
  List<Map<String, dynamic>> horariosDelDia = [];

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      final Servicio servicio =
          ModalRoute.of(context)!.settings.arguments as Servicio;
      if (servicio.idServicio != null)
        _cargarHorariosServicio(servicio.idServicio!);
    });
  }

  Future<void> _cargarHorariosServicio(int idServicio) async {
    setState(() => loadingHorarios = true);
    try {
      final response = await ApiClient().get('/horarios/servicio/$idServicio');
      if (response.data is List) {
        setState(() {
          horariosDelServicio = List<Map<String, dynamic>>.from(response.data);
        });
      }
    } catch (e) {
      debugPrint('Error cargando horarios: $e');
    } finally {
      setState(() => loadingHorarios = false);
    }
  }

  void _onFechaSeleccionada(DateTime nuevaFecha) {
    final dia = _getDiaSemana(nuevaFecha.weekday);
    final filtrados = horariosDelServicio
        .where((h) => (h['diaSemana'] ?? '').toString().toLowerCase() == dia)
        .toList();
    setState(() {
      fecha = nuevaFecha;
      horarioSeleccionado = null;
      horariosDelDia = filtrados;
    });
  }

  String _getDiaSemana(int weekday) {
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
    return dias[weekday];
  }

  Future<void> _pickDate() async {
    final d = await showDatePicker(
      context: context,
      initialDate: DateTime.now().add(const Duration(days: 1)),
      firstDate: DateTime.now(),
      lastDate: DateTime.now().add(const Duration(days: 90)),
      builder: (ctx, child) => Theme(
        data: Theme.of(ctx).copyWith(
            colorScheme: const ColorScheme.light(
                primary: AppTheme.primary, onPrimary: Colors.white)),
        child: child!,
      ),
    );
    if (d != null) _onFechaSeleccionada(d);
  }

  Future<void> _confirmarCita(Servicio servicio) async {
    final usuario = context.read<UserProvider>().usuario;
    if (usuario == null) {
      _showError("Debes iniciar sesión");
      return;
    }
    if (fecha == null || horarioSeleccionado == null) {
      _showError("Selecciona fecha y horario");
      return;
    }

    setState(() => loading = true);
    final fechaStr =
        "${fecha!.year.toString().padLeft(4, '0')}-${fecha!.month.toString().padLeft(2, '0')}-${fecha!.day.toString().padLeft(2, '0')}";

    // ✅ EXTRAEMOS LAS HORAS DEL HORARIO SELECCIONADO
    final String horaInicio = horarioSeleccionado!["horaInicio"].toString();
    final String horaFin = horarioSeleccionado!["horaFin"].toString();

    // 🚀 PRINT DE CONTROL
    debugPrint(
        "🚀 EJECUTANDO BOOKING_SCREEN NUEVO. Horas: $horaInicio a $horaFin");

    final citaData = {
      "fecha": fechaStr,
      "horaInicio": horaInicio, // ✅ AÑADIDO
      "horaFin": horaFin, // ✅ AÑADIDO
      "estado": "pendiente",
      "cliente": {"idUsuario": usuario["id"]},
      "horarioSemanal": {"idHorario": horarioSeleccionado!["idHorario"]},
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

  void _showError(String msg) =>
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(
          content: Text(msg),
          backgroundColor: Colors.redAccent,
          behavior: SnackBarBehavior.floating));

  String _getMonthName(int m) => [
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
      ][m - 1];

  @override
  Widget build(BuildContext context) {
    final Servicio servicio =
        ModalRoute.of(context)!.settings.arguments as Servicio;
    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      body: Stack(children: [
        CustomScrollView(slivers: [
          _buildHeader(servicio),
          SliverToBoxAdapter(
              child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Text('Selecciona tu horario',
                          style: TextStyle(
                              fontSize: 20, fontWeight: FontWeight.bold)),
                      const SizedBox(height: 20),
                      _buildDateSelector(),
                      if (fecha != null) ...[
                        const SizedBox(height: 24),
                        _buildHorariosDelDia()
                      ],
                      if (horarioSeleccionado != null) ...[
                        const SizedBox(height: 24),
                        _buildSummary(servicio)
                      ],
                      const SizedBox(height: 120),
                    ],
                  ))),
        ]),
        if (horarioSeleccionado != null) _buildConfirmButton(servicio),
      ]),
    );
  }

  Widget _buildHeader(Servicio servicio) {
    return SliverAppBar(
      expandedHeight: 200,
      pinned: true,
      backgroundColor: AppTheme.primary,
      leading: Container(
        margin: const EdgeInsets.all(8),
        decoration:
            const BoxDecoration(color: Colors.white, shape: BoxShape.circle),
        child: IconButton(
            icon: const Icon(Icons.arrow_back, color: AppTheme.primary),
            onPressed: () => Navigator.pop(context)),
      ),
      flexibleSpace: FlexibleSpaceBar(
        background: Container(
          decoration: BoxDecoration(
              gradient: LinearGradient(
                  begin: Alignment.topCenter,
                  end: Alignment.bottomCenter,
                  colors: [
                AppTheme.primary,
                AppTheme.primary.withOpacity(0.8)
              ])),
          child: SafeArea(
              child: Padding(
            padding: const EdgeInsets.fromLTRB(20, 60, 20, 20),
            child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisAlignment: MainAxisAlignment.end,
                children: [
                  const Text('Reservar Cita',
                      style: TextStyle(
                          color: Colors.white,
                          fontSize: 28,
                          fontWeight: FontWeight.bold)),
                  const SizedBox(height: 8),
                  Container(
                    padding:
                        const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
                    decoration: BoxDecoration(
                        color: Colors.white.withOpacity(0.2),
                        borderRadius: BorderRadius.circular(20)),
                    child: Row(mainAxisSize: MainAxisSize.min, children: [
                      const Icon(Icons.content_cut,
                          color: Colors.white, size: 16),
                      const SizedBox(width: 6),
                      Text(servicio.nombre,
                          style: const TextStyle(
                              color: Colors.white,
                              fontWeight: FontWeight.w500)),
                    ]),
                  ),
                ]),
          )),
        ),
      ),
    );
  }

  Widget _buildDateSelector() {
    return GestureDetector(
      onTap: _pickDate,
      child: Container(
        padding: const EdgeInsets.all(20),
        decoration: BoxDecoration(
            color: Colors.white,
            borderRadius: BorderRadius.circular(20),
            boxShadow: [
              BoxShadow(
                  color: Colors.black.withOpacity(0.06),
                  blurRadius: 15,
                  offset: const Offset(0, 5))
            ]),
        child: Row(children: [
          Container(
              height: 56,
              width: 56,
              decoration: BoxDecoration(
                  color: AppTheme.primary.withOpacity(0.1),
                  borderRadius: BorderRadius.circular(14)),
              child: const Icon(Icons.calendar_today_rounded,
                  color: AppTheme.primary, size: 28)),
          const SizedBox(width: 16),
          Expanded(
              child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                Text('Fecha',
                    style: TextStyle(fontSize: 13, color: Colors.grey[600])),
                const SizedBox(height: 4),
                Text(
                    fecha == null
                        ? 'Seleccionar día'
                        : "${fecha!.day} de ${_getMonthName(fecha!.month)}, ${fecha!.year}",
                    style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.bold,
                        color:
                            fecha == null ? Colors.grey[400] : Colors.black87)),
              ])),
          Icon(Icons.arrow_forward_ios_rounded,
              size: 16, color: Colors.grey[400]),
        ]),
      ),
    );
  }

  Widget _buildHorariosDelDia() {
    return Container(
      padding: const EdgeInsets.all(24),
      decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(20),
          boxShadow: [
            BoxShadow(
                color: Colors.black.withOpacity(0.06),
                blurRadius: 15,
                offset: const Offset(0, 5))
          ]),
      child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
        const Text('Horarios disponibles',
            style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
        const SizedBox(height: 16),
        if (loadingHorarios)
          const Center(
              child: Padding(
                  padding: EdgeInsets.all(20),
                  child: CircularProgressIndicator()))
        else if (horariosDelDia.isEmpty)
          Center(
              child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(children: [
                    Icon(Icons.event_busy, size: 48, color: Colors.grey[300]),
                    const SizedBox(height: 12),
                    Text("No hay horarios para este día de la semana",
                        textAlign: TextAlign.center,
                        style: TextStyle(color: Colors.grey[600])),
                  ])))
        else
          Wrap(
            spacing: 12,
            runSpacing: 12,
            children: horariosDelDia.map((h) {
              final sel = horarioSeleccionado?['idHorario'] == h['idHorario'];
              final ini = (h['horaInicio'] ?? '').toString();
              final fin = (h['horaFin'] ?? '').toString();
              final label =
                  "${ini.length >= 5 ? ini.substring(0, 5) : ini} - ${fin.length >= 5 ? fin.substring(0, 5) : fin}";
              return GestureDetector(
                onTap: () => setState(() => horarioSeleccionado = h),
                child: Container(
                  padding:
                      const EdgeInsets.symmetric(horizontal: 20, vertical: 14),
                  decoration: BoxDecoration(
                      color: sel ? AppTheme.primary : Colors.grey[50],
                      borderRadius: BorderRadius.circular(12),
                      border: Border.all(
                          color: sel ? AppTheme.primary : Colors.grey[300]!,
                          width: sel ? 2 : 1)),
                  child: Row(mainAxisSize: MainAxisSize.min, children: [
                    Icon(Icons.access_time,
                        size: 18, color: sel ? Colors.white : Colors.grey[600]),
                    const SizedBox(width: 8),
                    Text(label,
                        style: TextStyle(
                            color: sel ? Colors.white : Colors.black87,
                            fontWeight: sel ? FontWeight.bold : FontWeight.w500,
                            fontSize: 15)),
                  ]),
                ),
              );
            }).toList(),
          ),
      ]),
    );
  }

  Widget _buildSummary(Servicio servicio) {
    final ini = (horarioSeleccionado!['horaInicio'] ?? '').toString();
    final fin = (horarioSeleccionado!['horaFin'] ?? '').toString();
    final label =
        "${ini.length >= 5 ? ini.substring(0, 5) : ini} - ${fin.length >= 5 ? fin.substring(0, 5) : fin}";
    return Container(
      padding: const EdgeInsets.all(24),
      decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(20),
          border:
              Border.all(color: AppTheme.primary.withOpacity(0.3), width: 2)),
      child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
        const Row(children: [
          Icon(Icons.check_circle, color: Colors.green),
          SizedBox(width: 12),
          Text('Resumen de tu reserva',
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold))
        ]),
        const SizedBox(height: 20),
        _row('Servicio', servicio.nombre),
        const Divider(height: 24),
        _row('Fecha', "${fecha!.day}/${fecha!.month}/${fecha!.year}"),
        const Divider(height: 24),
        _row('Horario', label),
        const Divider(height: 24),
        _row('Precio', '${servicio.precio.toStringAsFixed(2)} €',
            isPrice: true),
      ]),
    );
  }

  Widget _row(String l, String v, {bool isPrice = false}) =>
      Row(mainAxisAlignment: MainAxisAlignment.spaceBetween, children: [
        Text(l, style: TextStyle(fontSize: 15, color: Colors.grey[600])),
        Text(v,
            style: TextStyle(
                fontSize: isPrice ? 20 : 15,
                fontWeight: isPrice ? FontWeight.bold : FontWeight.w600,
                color: isPrice ? AppTheme.primary : Colors.black87)),
      ]);

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
                  topLeft: Radius.circular(30), topRight: Radius.circular(30)),
              boxShadow: [
                BoxShadow(
                    color: Colors.black.withOpacity(0.1),
                    blurRadius: 20,
                    offset: const Offset(0, -5))
              ]),
          child: SafeArea(
              top: false,
              child: SizedBox(
                width: double.infinity,
                height: 56,
                child: ElevatedButton(
                  onPressed: loading ? null : () => _confirmarCita(servicio),
                  style: ElevatedButton.styleFrom(
                      backgroundColor: AppTheme.primary,
                      foregroundColor: Colors.white,
                      elevation: 0,
                      shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(16))),
                  child: loading
                      ? const SizedBox(
                          height: 24,
                          width: 24,
                          child: CircularProgressIndicator(
                              color: Colors.white, strokeWidth: 2.5))
                      : const Row(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                              Icon(Icons.check_circle_outline, size: 22),
                              SizedBox(width: 12),
                              Text('Confirmar Cita',
                                  style: TextStyle(
                                      fontSize: 18,
                                      fontWeight: FontWeight.bold)),
                            ]),
                ),
              )),
        ));
  }
}
