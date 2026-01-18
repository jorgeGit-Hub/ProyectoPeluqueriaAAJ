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
    WidgetsBinding.instance.addPostFrameCallback((_) => _bootstrap());
  }

  Future<void> _bootstrap() async {
    final userProvider = context.read<UserProvider>();

    try {
      await userProvider.loadUserFromToken();

      if (!mounted) return;

      if (userProvider.isLogged) {
        _goHome();
      } else {
        _goLogin();
      }
    } catch (e) {
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
      backgroundColor: AppTheme.pastelLavender,
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
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
