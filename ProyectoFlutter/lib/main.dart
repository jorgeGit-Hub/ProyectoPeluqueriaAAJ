import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'providers/user_provider.dart';
import 'providers/servicio_provider.dart';
import 'providers/cita_provider.dart';
import 'providers/cliente_provider.dart';
import 'providers/valoracion_provider.dart';
import 'services/api_client.dart';

import 'utils/theme.dart';
import 'screens/auth/login_screen.dart';
import 'screens/auth/register_screen.dart';
import 'screens/auth/forgot_password_screen.dart';
import 'screens/home/home_shell.dart';
import 'screens/home/splash_screen.dart';
import 'screens/account/edit_account_screen.dart';
import 'screens/booking/mis_citas_screen.dart';
import 'screens/booking/reservar_cita_screen.dart';
import 'screens/booking/booking_screen.dart';
import 'screens/booking/reservation_confirmed_screen.dart';
import 'screens/booking/cita_detalle_screen.dart';
import 'screens/services/service_detail_screen.dart';
import 'screens/valoraciones/crear_valoracion_screen.dart';

void main() {
  ApiClient().initialize();

  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => UserProvider()),
        ChangeNotifierProvider(create: (_) => ServicioProvider()),
        ChangeNotifierProvider(create: (_) => CitaProvider()),
        ChangeNotifierProvider(create: (_) => ClienteProvider()),
        ChangeNotifierProvider(create: (_) => ValoracionProvider()),
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
      theme: AppTheme.lightTheme,
      initialRoute: '/splash',
      routes: {
        '/splash': (_) => const SplashScreen(),
        '/login': (_) => const LoginScreen(),
        '/register': (_) => const RegisterScreen(),
        '/forgot-password': (_) => const ForgotPasswordScreen(),
        '/home': (_) => const HomeShell(),
        '/service-detail': (_) => const ServiceDetailScreen(),
        '/booking': (_) => const BookingScreen(),
        '/reservar-cita': (_) => const ReservarCitaScreen(),
        '/reservation-confirmed': (_) => const ReservationConfirmedScreen(),
        '/edit-account': (_) => const EditAccountScreen(),
        '/mis-citas': (_) => const MisCitasScreen(),
        '/detalle-cita': (_) => const DetalleCitaScreen(),
        '/crear-valoracion': (_) => const CrearValoracionScreen(),
      },
    );
  }
}
