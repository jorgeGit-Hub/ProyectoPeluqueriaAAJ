import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../providers/user_provider.dart';
import '../../utils/theme.dart';

class RegisterScreen extends StatefulWidget {
  const RegisterScreen({super.key});

  @override
  State<RegisterScreen> createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {
  final _formKey = GlobalKey<FormState>();

  final nombreCtrl = TextEditingController();
  final apellidosCtrl = TextEditingController();
  final correoCtrl = TextEditingController();
  final passCtrl = TextEditingController();

  final telefonoCtrl = TextEditingController();
  final direccionCtrl = TextEditingController();
  final alergenosCtrl = TextEditingController();
  final observacionesCtrl = TextEditingController();

  bool loading = false;
  bool _obscureText = true;

  @override
  void dispose() {
    nombreCtrl.dispose();
    apellidosCtrl.dispose();
    correoCtrl.dispose();
    passCtrl.dispose();
    telefonoCtrl.dispose();
    direccionCtrl.dispose();
    alergenosCtrl.dispose();
    observacionesCtrl.dispose();
    super.dispose();
  }

  Future<void> _signup() async {
    FocusScope.of(context).unfocus();

    // ✅ ESTO COMPRUEBA QUE NINGÚN CAMPO OBLIGATORIO ESTÉ VACÍO
    if (!_formKey.currentState!.validate()) return;

    setState(() => loading = true);

    try {
      final userProvider = context.read<UserProvider>();

      final success = await userProvider.register(
        nombre: nombreCtrl.text.trim(),
        apellidos: apellidosCtrl.text.trim(),
        correo: correoCtrl.text.trim(),
        password: passCtrl.text.trim(),
        telefono: telefonoCtrl.text.trim(),
        direccion: direccionCtrl.text.trim(),
        alergenos: alergenosCtrl.text.trim(),
        observaciones: observacionesCtrl.text.trim(),
      );

      if (!mounted) return;

      if (success) {
        _showSnackBar("¡Cuenta creada con éxito! Inicia sesión.", Colors.green);
        Navigator.pushReplacementNamed(context, "/login");
      } else {
        _showSnackBar(
            "No se pudo crear la cuenta. Verifica tus datos.", Colors.orange);
      }
    } catch (e) {
      if (!mounted) return;
      _showSnackBar("Error de conexión. Verifica que el servidor esté activo.",
          Colors.redAccent);
    } finally {
      if (mounted) setState(() => loading = false);
    }
  }

  void _showSnackBar(String message, Color color) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text(message),
        backgroundColor: color,
        behavior: SnackBarBehavior.floating,
        duration: const Duration(seconds: 4),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        backgroundColor: Colors.transparent,
        elevation: 0,
        leading: IconButton(
          icon: const Icon(Icons.arrow_back_ios_new, color: AppTheme.primary),
          onPressed: () => Navigator.pop(context),
        ),
      ),
      body: Center(
        child: SingleChildScrollView(
          physics: const BouncingScrollPhysics(),
          padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 10),
          child: Form(
            key: _formKey,
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  "Crear Cuenta",
                  style: TextStyle(
                    fontSize: 32,
                    fontWeight: FontWeight.bold,
                    color: AppTheme.primary,
                  ),
                ),
                const SizedBox(height: 8),
                Text(
                  "Rellena tus datos para unirte",
                  style: TextStyle(fontSize: 16, color: Colors.grey[600]),
                ),
                const SizedBox(height: 30),
                Container(
                  padding: const EdgeInsets.all(24),
                  decoration: BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.circular(24),
                    boxShadow: [
                      BoxShadow(
                        color: Colors.black.withOpacity(0.05),
                        blurRadius: 20,
                        offset: const Offset(0, 10),
                      ),
                    ],
                  ),
                  child: Column(
                    children: [
                      TextFormField(
                        controller: nombreCtrl,
                        decoration: const InputDecoration(
                            labelText: "Nombre",
                            prefixIcon: Icon(Icons.person_outline)),
                        validator: (v) =>
                            v!.isEmpty ? "El nombre es obligatorio" : null,
                      ),
                      const SizedBox(height: 16),
                      TextFormField(
                        controller: apellidosCtrl,
                        decoration: const InputDecoration(
                            labelText: "Apellidos",
                            prefixIcon: Icon(Icons.badge_outlined)),
                        validator: (v) => v!.isEmpty
                            ? "Los apellidos son obligatorios"
                            : null,
                      ),
                      const SizedBox(height: 16),
                      TextFormField(
                        controller: correoCtrl,
                        keyboardType: TextInputType.emailAddress,
                        decoration: const InputDecoration(
                            labelText: "Correo electrónico",
                            prefixIcon: Icon(Icons.email_outlined)),
                        validator: (v) {
                          if (v!.isEmpty) return "El correo es obligatorio";
                          if (!v.contains("@")) return "Correo inválido";
                          return null;
                        },
                      ),
                      const SizedBox(height: 16),
                      TextFormField(
                        controller: passCtrl,
                        obscureText: _obscureText,
                        decoration: InputDecoration(
                          labelText: "Contraseña",
                          prefixIcon: const Icon(Icons.lock_outline),
                          suffixIcon: IconButton(
                            icon: Icon(_obscureText
                                ? Icons.visibility_outlined
                                : Icons.visibility_off_outlined),
                            onPressed: () =>
                                setState(() => _obscureText = !_obscureText),
                          ),
                        ),
                        validator: (v) =>
                            v!.length < 6 ? "Mínimo 6 caracteres" : null,
                      ),
                      const Divider(height: 40),

                      // ✅ TÍTULO CAMBIADO (YA NO DICE OPCIONAL)
                      const Text("Información de Contacto y Salud",
                          style: TextStyle(
                              fontWeight: FontWeight.bold,
                              color: AppTheme.primary)),
                      const SizedBox(height: 16),

                      // ✅ CAMPOS CON VALIDACIÓN OBLIGATORIA
                      TextFormField(
                        controller: telefonoCtrl,
                        keyboardType: TextInputType.phone,
                        decoration: const InputDecoration(
                            labelText: "Teléfono",
                            prefixIcon: Icon(Icons.phone_outlined)),
                        validator: (v) =>
                            v!.isEmpty ? "El teléfono es obligatorio" : null,
                      ),
                      const SizedBox(height: 16),
                      TextFormField(
                        controller: direccionCtrl,
                        decoration: const InputDecoration(
                            labelText: "Dirección",
                            prefixIcon: Icon(Icons.home_outlined)),
                        validator: (v) =>
                            v!.isEmpty ? "La dirección es obligatoria" : null,
                      ),
                      const SizedBox(height: 16),
                      TextFormField(
                        controller: alergenosCtrl,
                        decoration: const InputDecoration(
                            labelText: "Alérgenos",
                            hintText: "Escribe 'Ninguno' si no tienes",
                            prefixIcon: Icon(Icons.warning_amber_rounded)),
                        validator: (v) => v!.isEmpty
                            ? "Por favor, indica si tienes alérgenos o escribe 'Ninguno'"
                            : null,
                      ),
                      const SizedBox(height: 16),

                      // ✅ ESTE ES EL ÚNICO OPCIONAL (SIN VALIDADOR)
                      TextFormField(
                        controller: observacionesCtrl,
                        maxLines: 2,
                        decoration: const InputDecoration(
                            labelText: "Observaciones (Opcional)",
                            prefixIcon: Icon(Icons.note_alt_outlined)),
                      ),

                      const SizedBox(height: 30),
                      SizedBox(
                        width: double.infinity,
                        height: 54,
                        child: ElevatedButton(
                          onPressed: loading ? null : _signup,
                          style: ElevatedButton.styleFrom(
                            backgroundColor: AppTheme.primary,
                            foregroundColor: Colors.white,
                            shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(16)),
                          ),
                          child: loading
                              ? const SizedBox(
                                  height: 24,
                                  width: 24,
                                  child: CircularProgressIndicator(
                                      color: Colors.white, strokeWidth: 2.5),
                                )
                              : const Text("Crear Cuenta",
                                  style: TextStyle(
                                      fontSize: 18,
                                      fontWeight: FontWeight.bold)),
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
    );
  }
}
