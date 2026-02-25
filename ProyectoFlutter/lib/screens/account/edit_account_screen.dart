import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../models/cliente.dart';
import '../../providers/cliente_provider.dart';
import '../../utils/theme.dart';

class EditAccountScreen extends StatefulWidget {
  const EditAccountScreen({super.key});

  @override
  State<EditAccountScreen> createState() => _EditAccountScreenState();
}

class _EditAccountScreenState extends State<EditAccountScreen> {
  final _formKey = GlobalKey<FormState>();

  late TextEditingController _telefonoCtrl;
  late TextEditingController _direccionCtrl;
  late TextEditingController _alergenosCtrl;
  late TextEditingController _observacionesCtrl;

  bool _loading = false;
  Cliente? _clienteOriginal;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    if (_clienteOriginal == null) {
      final args = ModalRoute.of(context)?.settings.arguments;
      if (args is Cliente) {
        _clienteOriginal = args;
        _initControllers(args);
      }
    }
  }

  void _initControllers(Cliente cliente) {
    _telefonoCtrl = TextEditingController(text: cliente.telefono);
    _direccionCtrl = TextEditingController(text: cliente.direccion);
    _alergenosCtrl = TextEditingController(text: cliente.alergenos ?? "");
    _observacionesCtrl =
        TextEditingController(text: cliente.observaciones ?? "");
  }

  @override
  void dispose() {
    _telefonoCtrl.dispose();
    _direccionCtrl.dispose();
    _alergenosCtrl.dispose();
    _observacionesCtrl.dispose();
    super.dispose();
  }

  Future<void> _submit() async {
    if (!_formKey.currentState!.validate()) return;

    FocusScope.of(context).unfocus();
    setState(() => _loading = true);

    final provider = Provider.of<ClienteProvider>(context, listen: false);

    final dataToUpdate = {
      "idUsuario": _clienteOriginal!.idUsuario,
      "telefono": _telefonoCtrl.text.trim(),
      "direccion": _direccionCtrl.text.trim(),
      "alergenos": _alergenosCtrl.text.trim(),
      "observaciones": _observacionesCtrl.text.trim(),
    };

    final success = await provider.updateCliente(
      _clienteOriginal!.idUsuario,
      dataToUpdate,
    );

    if (mounted) {
      setState(() => _loading = false);

      if (success) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text("Perfil actualizado correctamente"),
            backgroundColor: Colors.green,
            behavior: SnackBarBehavior.floating,
          ),
        );
        Navigator.pop(context, true);
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text("Error al actualizar. Verifica tu conexión."),
            backgroundColor: Colors.redAccent,
            behavior: SnackBarBehavior.floating,
          ),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    if (_clienteOriginal == null) {
      return const Scaffold(body: Center(child: CircularProgressIndicator()));
    }

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        title: const Text("Editar Perfil",
            style: TextStyle(fontWeight: FontWeight.w600)),
        centerTitle: true,
        backgroundColor: AppTheme.primary,
        foregroundColor: Colors.white,
        elevation: 0,
        leading: IconButton(
          icon: const Icon(Icons.close),
          onPressed: () => Navigator.pop(context),
        ),
      ),
      body: GestureDetector(
        onTap: () => FocusScope.of(context).unfocus(),
        child: SingleChildScrollView(
          physics: const BouncingScrollPhysics(),
          padding: const EdgeInsets.all(20.0),
          child: Column(
            children: [
              Container(
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
                child: Form(
                  key: _formKey,
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Text(
                        "Información de Contacto",
                        style: TextStyle(
                            fontSize: 18,
                            fontWeight: FontWeight.bold,
                            color: AppTheme.primary),
                      ),
                      const SizedBox(height: 20),
                      TextFormField(
                        controller: _telefonoCtrl,
                        keyboardType: TextInputType.phone,
                        decoration: _buildInputDecoration(
                            "Teléfono", Icons.phone_outlined),
                        validator: (value) {
                          if (value == null || value.isEmpty)
                            return "El teléfono es obligatorio";
                          if (value.length < 9) return "Mínimo 9 caracteres";
                          return null;
                        },
                      ),
                      const SizedBox(height: 20),
                      TextFormField(
                        controller: _direccionCtrl,
                        keyboardType: TextInputType.streetAddress,
                        maxLines: 2,
                        minLines: 1,
                        decoration: _buildInputDecoration(
                            "Dirección", Icons.location_on_outlined),
                        validator: (value) => value!.isEmpty
                            ? "La dirección es obligatoria"
                            : null,
                      ),
                      const SizedBox(height: 30),
                      const Text(
                        "Información Adicional",
                        style: TextStyle(
                            fontSize: 18,
                            fontWeight: FontWeight.bold,
                            color: AppTheme.primary),
                      ),
                      const SizedBox(height: 20),
                      TextFormField(
                        controller: _alergenosCtrl,
                        decoration: _buildInputDecoration(
                            "Alérgenos (Opcional)",
                            Icons.warning_amber_rounded),
                      ),
                      const SizedBox(height: 20),
                      TextFormField(
                        controller: _observacionesCtrl,
                        maxLines: 3,
                        decoration: _buildInputDecoration(
                            "Observaciones (Opcional)",
                            Icons.note_alt_outlined),
                      ),
                      const SizedBox(height: 30),
                      SizedBox(
                        width: double.infinity,
                        height: 54,
                        child: ElevatedButton(
                          onPressed: _loading ? null : _submit,
                          style: ElevatedButton.styleFrom(
                            backgroundColor: AppTheme.primary,
                            foregroundColor: Colors.white,
                            shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(14)),
                            elevation: 4,
                          ),
                          child: _loading
                              ? const SizedBox(
                                  height: 24,
                                  width: 24,
                                  child: CircularProgressIndicator(
                                      color: Colors.white, strokeWidth: 2.5),
                                )
                              : const Text("Guardar Cambios",
                                  style: TextStyle(
                                      fontSize: 16,
                                      fontWeight: FontWeight.bold)),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  InputDecoration _buildInputDecoration(String label, IconData icon) {
    return InputDecoration(
      labelText: label,
      prefixIcon: Icon(icon, color: Colors.grey[500]),
      filled: true,
      fillColor: Colors.grey[50],
      contentPadding: const EdgeInsets.symmetric(horizontal: 20, vertical: 16),
      border: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: BorderSide(color: Colors.grey[300]!),
      ),
      enabledBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: BorderSide(color: Colors.grey[300]!),
      ),
      focusedBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: const BorderSide(color: AppTheme.primary, width: 2),
      ),
    );
  }
}
