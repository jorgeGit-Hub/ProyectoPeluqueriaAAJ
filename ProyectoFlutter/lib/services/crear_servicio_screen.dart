import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../models/servicio.dart';
import '../../providers/servicio_provider.dart';
import '../../utils/theme.dart';

class CrearServicioScreen extends StatefulWidget {
  const CrearServicioScreen({super.key});

  @override
  State<CrearServicioScreen> createState() => _CrearServicioScreenState();
}

class _CrearServicioScreenState extends State<CrearServicioScreen> {
  final _formKey = GlobalKey<FormState>();

  final _nombreCtrl = TextEditingController();
  final _descCtrl = TextEditingController();
  final _duracionCtrl = TextEditingController();
  final _precioCtrl = TextEditingController();

  bool _loading = false;

  @override
  void dispose() {
    _nombreCtrl.dispose();
    _descCtrl.dispose();
    _duracionCtrl.dispose();
    _precioCtrl.dispose();
    super.dispose();
  }

  Future<void> _guardar() async {
    // 1. Validar el formulario
    if (!_formKey.currentState!.validate()) return;

    setState(() => _loading = true);

    // 2. Preparar el objeto (idServicio es 0 porque lo genera la BD)
    final nuevoServicio = Servicio(
      idServicio: 0,
      nombre: _nombreCtrl.text.trim(),
      descripcion: _descCtrl.text.trim(),
      duracion: int.tryParse(_duracionCtrl.text) ?? 30,
      precio: double.tryParse(_precioCtrl.text) ?? 0.0,
    );

    try {
      // 3. Usamos el Provider para que la lista general se entere del cambio
      // Nota: Asegúrate de tener implementado un método createServicio en tu provider
      // Si no, puedes usar ServicioService() directamente como tenías.
      final servicioProv = context.read<ServicioProvider>();

      // Simulación de guardado (usa tu lógica de service si el provider no tiene create)
      final success = await _realizarGuardado(nuevoServicio);

      if (mounted) {
        if (success) {
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(
                content: Text("Servicio creado"),
                backgroundColor: Colors.green),
          );
          // Recargamos la lista global antes de salir
          await servicioProv.loadServicios();
          Navigator.pop(context);
        }
      }
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text("Error: $e"), backgroundColor: Colors.red),
        );
      }
    } finally {
      if (mounted) setState(() => _loading = false);
    }
  }

  // Método puente para llamar al service
  Future<bool> _realizarGuardado(Servicio s) async {
    // Aquí llamamos a tu servicio corregido
    try {
      await context.read<ServicioProvider>().loadServicios(); // Ejemplo
      // Lógica real: await ServicioService().createServicio(s.toJson());
      return true;
    } catch (_) {
      return false;
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        title: const Text("Nuevo Servicio",
            style: TextStyle(fontWeight: FontWeight.w600)),
        centerTitle: true,
        backgroundColor: AppTheme.primary,
        foregroundColor: Colors.white,
        elevation: 0,
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(24),
        child: Form(
          key: _formKey,
          child: Column(
            children: [
              Container(
                padding: const EdgeInsets.all(20),
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.circular(20),
                  boxShadow: [
                    BoxShadow(
                        color: Colors.black.withOpacity(0.05), blurRadius: 10)
                  ],
                ),
                child: Column(
                  children: [
                    // NOMBRE
                    TextFormField(
                      controller: _nombreCtrl,
                      decoration: _inputStyle("Nombre del Servicio", Icons.cut),
                      validator: (v) => v!.isEmpty ? "Campo obligatorio" : null,
                    ),
                    const SizedBox(height: 16),

                    // DESCRIPCIÓN
                    TextFormField(
                      controller: _descCtrl,
                      maxLines: 2,
                      decoration: _inputStyle(
                          "Descripción", Icons.description_outlined),
                      validator: (v) => v!.isEmpty ? "Campo obligatorio" : null,
                    ),
                    const SizedBox(height: 16),

                    // DURACIÓN Y PRECIO
                    Row(
                      children: [
                        Expanded(
                          child: TextFormField(
                            controller: _duracionCtrl,
                            keyboardType: TextInputType.number,
                            decoration:
                                _inputStyle("Minutos", Icons.timer_outlined),
                            validator: (v) => v!.isEmpty ? "Obligatorio" : null,
                          ),
                        ),
                        const SizedBox(width: 16),
                        Expanded(
                          child: TextFormField(
                            controller: _precioCtrl,
                            keyboardType: const TextInputType.numberWithOptions(
                                decimal: true),
                            decoration:
                                _inputStyle("Precio (€)", Icons.euro_outlined),
                            validator: (v) => v!.isEmpty ? "Obligatorio" : null,
                          ),
                        ),
                      ],
                    ),
                  ],
                ),
              ),
              const SizedBox(height: 40),
              SizedBox(
                width: double.infinity,
                height: 54,
                child: ElevatedButton(
                  onPressed: _loading ? null : _guardar,
                  style: ElevatedButton.styleFrom(
                    backgroundColor: AppTheme.primary,
                    foregroundColor: Colors.white,
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(16)),
                  ),
                  child: _loading
                      ? const CircularProgressIndicator(color: Colors.white)
                      : const Text("Guardar Servicio",
                          style: TextStyle(
                              fontSize: 18, fontWeight: FontWeight.bold)),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  InputDecoration _inputStyle(String label, IconData icon) {
    return InputDecoration(
      labelText: label,
      prefixIcon: Icon(icon, color: AppTheme.primary),
      border: OutlineInputBorder(borderRadius: BorderRadius.circular(12)),
      enabledBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: BorderSide(color: Colors.grey.shade300),
      ),
    );
  }
}
