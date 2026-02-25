import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../models/cita.dart';
import '../../providers/valoracion_provider.dart';
import '../../models/valoracion.dart';
import '../../utils/theme.dart';

class DetalleCitaScreen extends StatefulWidget {
  const DetalleCitaScreen({super.key});

  @override
  State<DetalleCitaScreen> createState() => _DetalleCitaScreenState();
}

class _DetalleCitaScreenState extends State<DetalleCitaScreen> {
  final _comentarioController = TextEditingController();
  int _calificacion = 5;
  bool _loading = false;
  bool _cargandoValoracion = true;
  Valoracion? _valoracionExistente;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) => _cargarValoracion());
  }

  Future<void> _cargarValoracion() async {
    final cita = ModalRoute.of(context)!.settings.arguments as Cita;
    if (cita.estado.toLowerCase() != 'realizada') {
      setState(() => _cargandoValoracion = false);
      return;
    }

    await context.read<ValoracionProvider>().loadValoraciones();
    final valoraciones = context.read<ValoracionProvider>().valoraciones;
    final existente =
        valoraciones.where((v) => v.idCita == cita.idCita).firstOrNull;

    setState(() {
      _valoracionExistente = existente;
      _cargandoValoracion = false;
    });
  }

  @override
  void dispose() {
    _comentarioController.dispose();
    super.dispose();
  }

  Future<void> _enviarValoracion(Cita cita) async {
    if (_comentarioController.text.trim().isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Debes escribir un comentario'),
          backgroundColor: Colors.orange,
        ),
      );
      return;
    }

    setState(() => _loading = true);

    final nuevaValoracion = Valoracion(
      puntuacion: _calificacion,
      comentario: _comentarioController.text.trim(),
      idCita: cita.idCita!,
      fechaValoracion: DateTime.now().toString().split(' ')[0],
    );

    final success = await context
        .read<ValoracionProvider>()
        .createValoracion(nuevaValoracion);

    if (mounted) {
      setState(() => _loading = false);

      if (success) {
        setState(() => _valoracionExistente = nuevaValoracion.copyWith(
              puntuacion: _calificacion,
              comentario: _comentarioController.text.trim(),
              fechaValoracion: DateTime.now().toString().split(' ')[0],
            ));
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text('¡Valoración enviada con éxito!'),
            backgroundColor: Colors.green,
          ),
        );
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text('Error al enviar la valoración'),
            backgroundColor: Colors.red,
          ),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final Cita cita = ModalRoute.of(context)!.settings.arguments as Cita;
    final bool puedeValorar = cita.estado.toLowerCase() == 'realizada';

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        title: const Text('Detalle de Cita',
            style: TextStyle(fontWeight: FontWeight.w600)),
        backgroundColor: AppTheme.primary,
        foregroundColor: Colors.white,
        centerTitle: true,
        elevation: 0,
      ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            // ENCABEZADO CON ESTADO
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
                  Container(
                    padding: const EdgeInsets.all(16),
                    decoration: const BoxDecoration(
                      color: Colors.white,
                      shape: BoxShape.circle,
                    ),
                    child: const Icon(
                      Icons.event_available,
                      size: 50,
                      color: AppTheme.primary,
                    ),
                  ),
                  const SizedBox(height: 16),
                  Container(
                    padding:
                        const EdgeInsets.symmetric(horizontal: 20, vertical: 8),
                    decoration: BoxDecoration(
                      color: _getEstadoColor(cita.estado).withOpacity(0.2),
                      borderRadius: BorderRadius.circular(20),
                    ),
                    child: Text(
                      cita.estado.toUpperCase(),
                      style: TextStyle(
                        color: _getEstadoColor(cita.estado),
                        fontWeight: FontWeight.bold,
                        fontSize: 16,
                      ),
                    ),
                  ),
                ],
              ),
            ),

            const SizedBox(height: 20),

            // INFORMACIÓN DE LA CITA
            Padding(
              padding: const EdgeInsets.all(20),
              child: Container(
                padding: const EdgeInsets.all(20),
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.circular(20),
                  boxShadow: [
                    BoxShadow(
                      color: Colors.black.withOpacity(0.05),
                      blurRadius: 10,
                      offset: const Offset(0, 5),
                    ),
                  ],
                ),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Información de la Cita',
                      style:
                          TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                    ),
                    const SizedBox(height: 20),
                    _buildInfoField('Fecha', cita.fecha, Icons.calendar_today),
                    const SizedBox(height: 12),
                    _buildInfoField('Hora de inicio',
                        cita.horaInicio.substring(0, 5), Icons.access_time),
                    const SizedBox(height: 12),
                    _buildInfoField('Hora de fin', cita.horaFin.substring(0, 5),
                        Icons.access_time_filled),
                    const SizedBox(height: 12),
                    _buildInfoField('Estado', cita.estado.toUpperCase(),
                        Icons.info_outline),
                  ],
                ),
              ),
            ),

            // SECCIÓN DE VALORACIÓN
            if (puedeValorar) ...[
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 20),
                child: _cargandoValoracion
                    ? const Center(child: CircularProgressIndicator())
                    : _valoracionExistente != null
                        ? _buildResumenValoracion(_valoracionExistente!)
                        : _buildFormularioValoracion(cita),
              ),
            ] else
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 20),
                child: Container(
                  padding: const EdgeInsets.all(20),
                  decoration: BoxDecoration(
                    color: Colors.orange[50],
                    borderRadius: BorderRadius.circular(12),
                    border: Border.all(color: Colors.orange[200]!),
                  ),
                  child: Row(
                    children: [
                      Icon(Icons.info_outline, color: Colors.orange[700]),
                      const SizedBox(width: 12),
                      Expanded(
                        child: Text(
                          'Solo puedes valorar citas que ya estén realizadas',
                          style: TextStyle(color: Colors.orange[900]),
                        ),
                      ),
                    ],
                  ),
                ),
              ),

            const SizedBox(height: 40),
          ],
        ),
      ),
    );
  }

  // ── RESUMEN de valoración ya enviada ──────────────────────────────────────
  Widget _buildResumenValoracion(Valoracion v) {
    return Container(
      padding: const EdgeInsets.all(20),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(20),
        boxShadow: [
          BoxShadow(
            color: Colors.black.withOpacity(0.05),
            blurRadius: 10,
            offset: const Offset(0, 5),
          ),
        ],
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            children: [
              Container(
                padding: const EdgeInsets.all(8),
                decoration: BoxDecoration(
                  color: Colors.green.withOpacity(0.1),
                  shape: BoxShape.circle,
                ),
                child: const Icon(Icons.check_circle,
                    color: Colors.green, size: 22),
              ),
              const SizedBox(width: 12),
              const Text(
                'Tu valoración',
                style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
              ),
            ],
          ),
          const SizedBox(height: 20),

          // Estrellas (solo lectura)
          const Text('Calificación',
              style: TextStyle(fontSize: 13, color: Colors.grey)),
          const SizedBox(height: 8),
          Row(
            children: List.generate(
                5,
                (i) => Icon(
                      i < v.puntuacion ? Icons.star : Icons.star_border,
                      color: Colors.amber,
                      size: 32,
                    )),
          ),
          const SizedBox(height: 20),

          // Comentario
          const Text('Comentario',
              style: TextStyle(fontSize: 13, color: Colors.grey)),
          const SizedBox(height: 8),
          Container(
            width: double.infinity,
            padding: const EdgeInsets.all(14),
            decoration: BoxDecoration(
              color: AppTheme.pastelLavender,
              borderRadius: BorderRadius.circular(12),
            ),
            child: Text(
              v.comentario,
              style: const TextStyle(fontSize: 15, height: 1.5),
            ),
          ),

          if (v.fechaValoracion != null) ...[
            const SizedBox(height: 16),
            Row(
              children: [
                Icon(Icons.calendar_today, size: 14, color: Colors.grey[500]),
                const SizedBox(width: 6),
                Text(
                  'Enviada el ${v.fechaValoracion}',
                  style: TextStyle(fontSize: 13, color: Colors.grey[500]),
                ),
              ],
            ),
          ],
        ],
      ),
    );
  }

  // ── FORMULARIO para enviar valoración ────────────────────────────────────
  Widget _buildFormularioValoracion(Cita cita) {
    return Container(
      padding: const EdgeInsets.all(20),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(20),
        boxShadow: [
          BoxShadow(
            color: Colors.black.withOpacity(0.05),
            blurRadius: 10,
            offset: const Offset(0, 5),
          ),
        ],
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text(
            'Valora tu experiencia',
            style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
          ),
          const SizedBox(height: 20),

          // CALIFICACIÓN CON ESTRELLAS
          const Text('Calificación',
              style: TextStyle(fontWeight: FontWeight.w500)),
          const SizedBox(height: 10),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: List.generate(5, (index) {
              return IconButton(
                iconSize: 40,
                icon: Icon(
                  index < _calificacion ? Icons.star : Icons.star_border,
                  color: Colors.amber,
                ),
                onPressed: () => setState(() => _calificacion = index + 1),
              );
            }),
          ),
          const SizedBox(height: 20),

          // COMENTARIO
          const Text('Comentario',
              style: TextStyle(fontWeight: FontWeight.w500)),
          const SizedBox(height: 10),
          TextField(
            controller: _comentarioController,
            maxLines: 4,
            decoration: InputDecoration(
              hintText: 'Cuéntanos tu experiencia...',
              filled: true,
              fillColor: Colors.grey[50],
              border: OutlineInputBorder(
                borderRadius: BorderRadius.circular(12),
                borderSide: BorderSide.none,
              ),
            ),
          ),
          const SizedBox(height: 30),

          // BOTÓN ENVIAR
          SizedBox(
            width: double.infinity,
            height: 54,
            child: ElevatedButton(
              onPressed: _loading ? null : () => _enviarValoracion(cita),
              style: ElevatedButton.styleFrom(
                backgroundColor: AppTheme.primary,
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(14),
                ),
              ),
              child: _loading
                  ? const CircularProgressIndicator(color: Colors.white)
                  : const Text(
                      'Enviar Valoración',
                      style:
                          TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                    ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildInfoField(String label, String value, IconData icon) {
    return Row(
      children: [
        Container(
          padding: const EdgeInsets.all(8),
          decoration: BoxDecoration(
            color: AppTheme.primary.withOpacity(0.1),
            borderRadius: BorderRadius.circular(8),
          ),
          child: Icon(icon, color: AppTheme.primary, size: 20),
        ),
        const SizedBox(width: 12),
        Expanded(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(label,
                  style: TextStyle(fontSize: 12, color: Colors.grey[600])),
              const SizedBox(height: 2),
              Text(value,
                  style: const TextStyle(
                      fontSize: 16, fontWeight: FontWeight.w500)),
            ],
          ),
        ),
      ],
    );
  }

  Color _getEstadoColor(String estado) {
    switch (estado.toLowerCase()) {
      case 'pendiente':
        return Colors.orange;
      case 'confirmada':
      case 'realizada':
        return Colors.green;
      case 'cancelada':
        return Colors.red;
      default:
        return AppTheme.primary;
    }
  }
}
