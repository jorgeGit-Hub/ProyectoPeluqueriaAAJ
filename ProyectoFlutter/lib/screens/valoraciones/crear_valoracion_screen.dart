import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../providers/valoracion_provider.dart';
import '../../providers/user_provider.dart';
import '../../providers/cita_provider.dart';
import '../../models/valoracion.dart';
import '../../utils/theme.dart';

class CrearValoracionScreen extends StatefulWidget {
  const CrearValoracionScreen({super.key});

  @override
  State<CrearValoracionScreen> createState() => _CrearValoracionScreenState();
}

class _CrearValoracionScreenState extends State<CrearValoracionScreen> {
  final _formKey = GlobalKey<FormState>();
  final _comentarioCtrl = TextEditingController();

  int _puntuacion = 5;
  int? _citaSeleccionada;
  bool _loading = false;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) async {
      final user = context.read<UserProvider>().usuario;
      if (user != null) {
        final userId =
            user["idUsuario"] ?? user["id"] ?? user["id_usuario"] ?? 0;
        await context.read<CitaProvider>().loadCitasByCliente(userId);
      }
    });
  }

  @override
  void dispose() {
    _comentarioCtrl.dispose();
    super.dispose();
  }

  Future<void> _enviarValoracion() async {
    if (!_formKey.currentState!.validate()) return;
    if (_citaSeleccionada == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("Selecciona una cita para valorar"),
          backgroundColor: Colors.orange,
        ),
      );
      return;
    }

    setState(() => _loading = true);

    final nuevaValoracion = Valoracion(
      puntuacion: _puntuacion,
      comentario: _comentarioCtrl.text.trim(),
      idCita: _citaSeleccionada!,
    );

    final success = await context
        .read<ValoracionProvider>()
        .createValoracion(nuevaValoracion);

    if (mounted) {
      setState(() => _loading = false);

      if (success) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text("¡Valoración enviada! Gracias por tu opinión"),
            backgroundColor: Colors.green,
          ),
        );
        Navigator.pop(context);
      } else {
        final error =
            context.read<ValoracionProvider>().error ?? "Error desconocido";
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text("Error: $error"),
            backgroundColor: Colors.red,
          ),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final citaProv = context.watch<CitaProvider>();

    final citasDisponibles = citaProv.citas
        .where((c) => c.estado.toLowerCase() == 'realizada')
        .toList();

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        title: const Text("Nueva Valoración",
            style: TextStyle(fontWeight: FontWeight.w600)),
        backgroundColor: AppTheme.primary,
        foregroundColor: Colors.white,
        centerTitle: true,
        elevation: 0,
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(24),
        child: Form(
          key: _formKey,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text("Selecciona una cita",
                  style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold)),
              const SizedBox(height: 12),
              if (citasDisponibles.isEmpty)
                Container(
                  padding: const EdgeInsets.all(16),
                  decoration: BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.circular(12),
                  ),
                  child: const Text("No tienes citas completadas para valorar"),
                )
              else
                Container(
                  padding: const EdgeInsets.symmetric(horizontal: 12),
                  decoration: BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.circular(12),
                  ),
                  child: DropdownButton<int>(
                    isExpanded: true,
                    underline: const SizedBox(),
                    value: _citaSeleccionada,
                    hint: const Text("Selecciona una cita"),
                    items: citasDisponibles.map((cita) {
                      return DropdownMenuItem<int>(
                        value: cita.idCita,
                        child: Text("Cita #${cita.idCita} - ${cita.fecha}"),
                      );
                    }).toList(),
                    onChanged: (value) =>
                        setState(() => _citaSeleccionada = value),
                  ),
                ),
              const SizedBox(height: 24),
              const Text("Puntuación",
                  style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold)),
              const SizedBox(height: 12),
              Center(
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: List.generate(5, (index) {
                    final puntuacion = index + 1;
                    return IconButton(
                      iconSize: 40,
                      icon: Icon(
                        puntuacion <= _puntuacion
                            ? Icons.star
                            : Icons.star_border,
                        color: Colors.amber,
                      ),
                      onPressed: () => setState(() => _puntuacion = puntuacion),
                    );
                  }),
                ),
              ),
              const SizedBox(height: 24),
              const Text("Comentario",
                  style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold)),
              const SizedBox(height: 12),
              TextFormField(
                controller: _comentarioCtrl,
                maxLines: 5,
                decoration: InputDecoration(
                  hintText: "Escribe tu opinión sobre el servicio...",
                  filled: true,
                  fillColor: Colors.white,
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(12),
                    borderSide: BorderSide.none,
                  ),
                ),
                validator: (value) {
                  if (value == null || value.trim().isEmpty) {
                    return "El comentario es obligatorio";
                  }
                  if (value.trim().length < 10) {
                    return "Mínimo 10 caracteres";
                  }
                  return null;
                },
              ),
              const SizedBox(height: 40),
              SizedBox(
                width: double.infinity,
                height: 54,
                child: ElevatedButton(
                  onPressed: _loading ? null : _enviarValoracion,
                  style: ElevatedButton.styleFrom(
                    backgroundColor: AppTheme.primary,
                    foregroundColor: Colors.white,
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(14)),
                  ),
                  child: _loading
                      ? const CircularProgressIndicator(color: Colors.white)
                      : const Text("Enviar Valoración",
                          style: TextStyle(
                              fontSize: 16, fontWeight: FontWeight.bold)),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
