import 'package:flutter/material.dart';
import '../../services/auth_service.dart';
import '../../services/base_api.dart';

class SplashScreen extends StatefulWidget {
  const SplashScreen({super.key});

  @override
  State<SplashScreen> createState() => _SplashScreenState();
}

class _SplashScreenState extends State<SplashScreen> {
  final AuthService _auth = AuthService();

  @override
  void initState() {
    super.initState();
    _bootstrap();
  }

  Future<void> _bootstrap() async {
    try {
      final saved = await _auth.getSavedToken();

      if (saved == null || saved.isEmpty) {
        _goLogin();
        return;
      }

      BaseApi.token = saved;

      // Valida token (si falla, salta al catch)
      await _auth.validateAndGetUser();

      _goHome();
    } catch (_) {
      await _auth.logout();
      _goLogin();
    }
  }

  void _goHome() {
    if (!mounted) return;
    Navigator.of(context).pushReplacementNamed("/home");
  }

  void _goLogin() {
    if (!mounted) return;
    Navigator.of(context).pushReplacementNamed("/login");
  }

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: Center(
        child: CircularProgressIndicator(),
      ),
    );
  }
}
