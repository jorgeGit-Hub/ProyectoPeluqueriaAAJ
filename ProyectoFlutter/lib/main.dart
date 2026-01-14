import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

// Importación de Providers
import 'providers/user_provider.dart';
import 'providers/servicio_provider.dart';
import 'providers/cita_provider.dart';

// Importación de Útiles y Pantallas
import 'utils/theme.dart';
import 'screens/auth/login_screen.dart';
import 'screens/auth/register_screen.dart';
import 'screens/home/home_shell.dart';
import 'screens/account/account_screen.dart';
// Corregido: El archivo en tu explorador se llama EditAccountScreen.dart con mayúsculas
import 'screens/account/EditAccountScreen.dart';
import 'screens/booking/mis_citas_screen.dart';
import 'screens/booking/reservar_cita_screen.dart';
import 'screens/services/service_detail_screen.dart';
import 'screens/home/splash_screen.dart';

void main() {
  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => UserProvider()),
        ChangeNotifierProvider(create: (_) => ServicioProvider()),
        ChangeNotifierProvider(create: (_) => CitaProvider()),
      ],
      child: const PeluqueriaApp(),
    ),
  );
}

class PeluqueriaApp extends StatelessWidget {
  const PeluqueriaApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Peluquería App',
      // Corregido: En el archivo theme.dart definimos "lightTheme"
      theme: AppTheme.lightTheme,
      initialRoute: '/splash',
      routes: {
        '/splash': (_) => const SplashScreen(),
        '/login': (_) => const LoginScreen(),
        '/register': (_) => const RegisterScreen(),
        '/home': (_) => const HomeShell(),
        '/service-detail': (_) => const ServiceDetailScreen(),
        '/reservar-cita': (_) => const ReservarCitaScreen(),
        // Corregido: Asegúrate de que la clase se llame EditAccountScreen
        '/edit-account': (_) => const EditAccountScreen(),
        '/mis-citas': (_) => const MisCitasScreen(),
      },
    );
  }
}
