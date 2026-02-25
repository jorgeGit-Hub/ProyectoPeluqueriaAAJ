import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:google_sign_in/google_sign_in.dart';
import '../../providers/user_provider.dart';
import '../../utils/theme.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  final emailCtrl = TextEditingController();
  final passCtrl = TextEditingController();

  bool loading = false;
  bool _obscureText = true;

  @override
  void dispose() {
    emailCtrl.dispose();
    passCtrl.dispose();
    super.dispose();
  }

  Future<void> _login() async {
    FocusScope.of(context).unfocus();
    final correo = emailCtrl.text.trim();
    final pass = passCtrl.text.trim();

    if (correo.isEmpty || pass.isEmpty) {
      _showSnackBar("Por favor, rellena todos los campos", Colors.orange);
      return;
    }

    setState(() => loading = true);

    try {
      final userProvider = context.read<UserProvider>();
      final success = await userProvider.login(correo, pass);

      if (!mounted) return;

      if (success) {
        Navigator.pushReplacementNamed(context, "/home");
      } else {
        _showSnackBar("Correo o contraseña incorrectos", Colors.redAccent);
      }
    } catch (e) {
      if (!mounted) return;
      _showSnackBar(
        "Error de conexión: Verifica tu red Wi-Fi",
        Colors.redAccent,
      );
    } finally {
      if (mounted) setState(() => loading = false);
    }
  }

  Future<void> _loginConGoogle() async {
    try {
      final GoogleSignInAccount? googleUser = await GoogleSignIn().signIn();
      if (googleUser == null) return;

      setState(() => loading = true);

      final success = await context.read<UserProvider>().socialLogin(
            googleUser.email,
            googleUser.displayName ?? "Usuario Google",
            googleUser.photoUrl,
          );

      if (success && mounted) {
        Navigator.pushReplacementNamed(context, "/home");
      } else {
        if (mounted)
          _showSnackBar("Error al iniciar con Google", Colors.redAccent);
      }
    } catch (error) {
      debugPrint("Error Google: $error");
      if (mounted)
        _showSnackBar("No se pudo conectar con Google", Colors.redAccent);
    } finally {
      if (mounted) setState(() => loading = false);
    }
  }

  void _showSnackBar(String message, Color color) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text(message),
        behavior: SnackBarBehavior.floating,
        backgroundColor: color,
        duration: const Duration(seconds: 3),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () => FocusScope.of(context).unfocus(),
      child: Scaffold(
        backgroundColor: AppTheme.pastelLavender,
        body: Center(
          child: SingleChildScrollView(
            physics: const BouncingScrollPhysics(),
            padding: const EdgeInsets.symmetric(horizontal: 24),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Container(
                  padding:
                      const EdgeInsets.all(12), // Espacio para el borde blanco
                  decoration: BoxDecoration(
                    color: Colors.white,
                    shape: BoxShape.circle,
                    boxShadow: [
                      BoxShadow(
                        color: AppTheme.primary.withOpacity(0.2),
                        blurRadius: 20,
                        offset: const Offset(0, 10),
                      ),
                    ],
                  ),
                  child: ClipOval(
                    // ✅ Sustitución del Icon por Image.asset
                    child: Image.asset(
                      'assets/images/logo.jpg',
                      width: 100,
                      height: 100,
                      fit: BoxFit.cover,
                    ),
                  ),
                ),
                const SizedBox(height: 24),
                const Text(
                  "¡Hola de nuevo!",
                  style: TextStyle(
                    fontSize: 28,
                    fontWeight: FontWeight.bold,
                    color: AppTheme.primary,
                  ),
                ),
                const SizedBox(height: 8),
                Text(
                  "Inicia sesión para gestionar tus citas",
                  style: TextStyle(color: Colors.grey[600], fontSize: 16),
                ),
                const SizedBox(height: 40),
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
                      TextField(
                        controller: emailCtrl,
                        keyboardType: TextInputType.emailAddress,
                        decoration: InputDecoration(
                          labelText: "Correo electrónico",
                          prefixIcon: const Icon(Icons.email_outlined),
                          border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(12),
                          ),
                        ),
                      ),
                      const SizedBox(height: 20),
                      TextField(
                        controller: passCtrl,
                        obscureText: _obscureText,
                        decoration: InputDecoration(
                          labelText: "Contraseña",
                          prefixIcon: const Icon(Icons.lock_outline),
                          border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(12),
                          ),
                          suffixIcon: IconButton(
                            icon: Icon(
                              _obscureText
                                  ? Icons.visibility_outlined
                                  : Icons.visibility_off_outlined,
                              color: Colors.grey,
                            ),
                            onPressed: () =>
                                setState(() => _obscureText = !_obscureText),
                          ),
                        ),
                      ),
                      const SizedBox(height: 30),
                      SizedBox(
                        width: double.infinity,
                        height: 54,
                        child: ElevatedButton(
                          onPressed: loading ? null : _login,
                          style: ElevatedButton.styleFrom(
                            backgroundColor: AppTheme.primary,
                            foregroundColor: Colors.white,
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
                                  "Iniciar Sesión",
                                  style: TextStyle(
                                    fontSize: 18,
                                    fontWeight: FontWeight.bold,
                                  ),
                                ),
                        ),
                      ),
                      const SizedBox(height: 16),
                      TextButton(
                        onPressed: () => Navigator.pushNamed(
                          context,
                          "/forgot-password",
                        ),
                        child: const Text(
                          "¿Olvidaste tu contraseña?",
                          style: TextStyle(
                            fontSize: 14,
                            color: AppTheme.primary,
                            fontWeight: FontWeight.w500,
                          ),
                        ),
                      ),
                      const Divider(height: 30),
                      const Text("O inicia sesión con:",
                          style: TextStyle(color: Colors.grey)),
                      const SizedBox(height: 16),
                      Center(
                        child: ElevatedButton.icon(
                          icon: const Icon(Icons.g_mobiledata, size: 30),
                          label: const Text("Google"),
                          style: ElevatedButton.styleFrom(
                            backgroundColor: Colors.white,
                            foregroundColor: Colors.red,
                            elevation: 2,
                            padding: const EdgeInsets.symmetric(
                                horizontal: 24, vertical: 8),
                          ),
                          onPressed: loading ? null : _loginConGoogle,
                        ),
                      ),
                    ],
                  ),
                ),
                const SizedBox(height: 30),
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Text(
                      "¿No tienes cuenta?",
                      style: TextStyle(color: Colors.grey[700]),
                    ),
                    TextButton(
                      onPressed: () =>
                          Navigator.pushNamed(context, "/register"),
                      child: const Text(
                        "Regístrate aquí",
                        style: TextStyle(fontWeight: FontWeight.bold),
                      ),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
