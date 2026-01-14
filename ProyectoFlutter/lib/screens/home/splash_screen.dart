import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../providers/user_provider.dart';
import '../../utils/theme.dart';

class SplashScreen extends StatefulWidget {
  const SplashScreen({super.key});

  @override
  State<SplashScreen> createState() => _SplashScreenState();
}

class _SplashScreenState extends State<SplashScreen> {
  @override
  void initState() {
    super.initState();
    // Ejecutamos el arranque después del primer frame
    WidgetsBinding.instance.addPostFrameCallback((_) => _bootstrap());
  }

  Future<void> _bootstrap() async {
    // 1. Obtenemos el provider sin escuchar cambios (listen: false)
    final userProvider = context.read<UserProvider>();

    try {
      // 2. Usamos el método que ya corregimos en el Provider.
      // Este método busca el token, lo valida en el backend y carga los datos del usuario.
      await userProvider.loadUserFromToken();

      if (!mounted) return;

      // 3. Verificamos si el proceso fue exitoso
      if (userProvider.isLogged) {
        _goHome();
      } else {
        _goLogin();
      }
    } catch (e) {
      // Si hay un error de red o el token caducó, vamos al login por seguridad
      debugPrint("Error en el arranque: $e");
      _goLogin();
    }
  }

  void _goHome() {
    Navigator.of(context).pushReplacementNamed("/home");
  }

  void _goLogin() {
    Navigator.of(context).pushReplacementNamed("/login");
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // Usamos el color de fondo de tu tema para que no haya saltos bruscos de color
      backgroundColor: AppTheme.pastelLavender,
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            // Logo o Icono para que la carga no sea solo un spinner
            Icon(
              Icons.content_cut_rounded,
              size: 80,
              color: AppTheme.primary.withOpacity(0.5),
            ),
            const SizedBox(height: 30),
            const CircularProgressIndicator(
              color: AppTheme.primary,
              strokeWidth: 3,
            ),
          ],
        ),
      ),
    );
  }
}
