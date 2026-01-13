import 'package:flutter/material.dart';

// Asegúrate de que los imports coincidan con tu estructura de carpetas
import '../../models/servicio.dart';
import '../../services/servicio_service.dart';

class CrearServicioScreen extends StatefulWidget {
  const CrearServicioScreen({super.key});

  @override
  State<CrearServicioScreen> createState() => _CrearServicioScreenState();
}

class _CrearServicioScreenState extends State<CrearServicioScreen> {
  // Controladores para los campos de texto
  final _nombreCtrl = TextEditingController();
  final _descCtrl = TextEditingController();
  final _duracionCtrl = TextEditingController();
  final _precioCtrl = TextEditingController();

  bool _loading = false;

  // Lógica para Guardar (Solo datos de texto)
  Future<void> _guardar() async {
    // 1. Validaciones básicas
    if (_nombreCtrl.text.isEmpty || _precioCtrl.text.isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("El nombre y el precio son obligatorios"),
          backgroundColor: Colors.orange,
        ),
      );
      return;
    }

    setState(() => _loading = true);

    // 2. Crear el objeto Servicio (La imagen va como null)
    final nuevoServicio = Servicio(
      nombre: _nombreCtrl.text.trim(),
      descripcion: _descCtrl.text.trim(),
      duracion: int.tryParse(_duracionCtrl.text) ?? 30, // 30 min por defecto
      precio: double.tryParse(_precioCtrl.text) ?? 0.0,
      imagen: null, // No enviamos imagen
    );

    try {
      // 3. Llamar al backend
      await ServicioService().createServicio(nuevoServicio.toJson());

      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text("Servicio creado correctamente"),
            backgroundColor: Colors.green,
          ),
        );
        Navigator.pop(context); // Volver a la lista
      }
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text("Error al guardar: $e"),
            backgroundColor: Colors.red,
          ),
        );
      }
    } finally {
      if (mounted) setState(() => _loading = false);
    }
  }

  @override
  void dispose() {
    // Limpieza de controladores
    _nombreCtrl.dispose();
    _descCtrl.dispose();
    _duracionCtrl.dispose();
    _precioCtrl.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Nuevo Servicio"),
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(24),
        child: Column(
          children: [
            // --- CAMPO NOMBRE ---
            TextField(
              controller: _nombreCtrl,
              textCapitalization: TextCapitalization.sentences,
              decoration: const InputDecoration(
                labelText: "Nombre del Servicio",
                prefixIcon: Icon(Icons.cut),
                border: OutlineInputBorder(),
              ),
            ),
            const SizedBox(height: 16),

            // --- CAMPO DESCRIPCIÓN ---
            TextField(
              controller: _descCtrl,
              textCapitalization: TextCapitalization.sentences,
              maxLines: 2,
              decoration: const InputDecoration(
                labelText: "Descripción",
                prefixIcon: Icon(Icons.description_outlined),
                border: OutlineInputBorder(),
              ),
            ),
            const SizedBox(height: 16),

            // --- FILA: DURACIÓN Y PRECIO ---
            Row(
              children: [
                // Duración
                Expanded(
                  child: TextField(
                    controller: _duracionCtrl,
                    keyboardType: TextInputType.number,
                    decoration: const InputDecoration(
                      labelText: "Duración (min)",
                      prefixIcon: Icon(Icons.timer_outlined),
                      border: OutlineInputBorder(),
                    ),
                  ),
                ),
                const SizedBox(width: 16),
                // Precio
                Expanded(
                  child: TextField(
                    controller: _precioCtrl,
                    keyboardType:
                        const TextInputType.numberWithOptions(decimal: true),
                    decoration: const InputDecoration(
                      labelText: "Precio (€)",
                      prefixIcon: Icon(Icons.euro_outlined),
                      border: OutlineInputBorder(),
                    ),
                  ),
                ),
              ],
            ),
            const SizedBox(height: 30),

            // --- BOTÓN GUARDAR ---
            SizedBox(
              width: double.infinity,
              height: 50,
              child: ElevatedButton(
                onPressed: _loading ? null : _guardar,
                child: _loading
                    ? const SizedBox(
                        height: 24,
                        width: 24,
                        child: CircularProgressIndicator(strokeWidth: 2),
                      )
                    : const Text(
                        "Guardar Servicio",
                        style: TextStyle(fontSize: 18),
                      ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
