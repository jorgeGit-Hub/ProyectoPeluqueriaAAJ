// buenas gentecilla, commit guapo, chencho

import 'package:flutter/material.dart';
import 'package:peluqueria_aja/screens/account/EditAccountScreen.dart';
import 'package:peluqueria_aja/screens/booking/mis_citas_screen.dart';
import 'package:peluqueria_aja/screens/booking/reservar_cita_screen.dart';
import 'package:provider/provider.dart';

// PROVIDERS
import 'package:peluqueria_aja/providers/user_provider.dart';
import 'package:peluqueria_aja/providers/cliente_provider.dart';
import 'package:peluqueria_aja/providers/servicio_provider.dart';
import 'package:peluqueria_aja/providers/cita_provider.dart';

// SCREENS
import 'package:peluqueria_aja/screens/home/splash_screen.dart';
import 'package:peluqueria_aja/screens/auth/login_screen.dart';
import 'package:peluqueria_aja/screens/auth/register_screen.dart';
import 'package:peluqueria_aja/screens/home/home_shell.dart';
import 'package:peluqueria_aja/screens/services/service_detail_screen.dart';
import 'package:peluqueria_aja/screens/booking/booking_screen.dart';
import 'package:peluqueria_aja/screens/booking/reservation_confirmed_screen.dart';

// THEME
import 'package:peluqueria_aja/utils/theme.dart';

void main() {
  runApp(const PeluqueriaAjaApp());
}

class PeluqueriaAjaApp extends StatelessWidget {
  const PeluqueriaAjaApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => UserProvider()),
        ChangeNotifierProvider(create: (_) => ClienteProvider()),
        ChangeNotifierProvider(create: (_) => ServicioProvider()),
        ChangeNotifierProvider(create: (_) => CitaProvider()),
      ],
      child: MaterialApp(
        debugShowCheckedModeBanner: false,
        title: "Peluquería AJA",
        theme: AppTheme.theme,
        initialRoute: "/splash",
        routes: {
          "/splash": (_) => const SplashScreen(),
          "/login": (_) => const LoginScreen(),
          "/register": (_) => const RegisterScreen(),
          "/home": (_) => const HomeShell(),
          "/service-detail": (_) => const ServiceDetailScreen(),
          "/booking": (_) => const BookingScreen(),
          "/reservation-confirmed": (_) => const ReservationConfirmedScreen(),
          "/edit-account": (_) => const EditAccountScreen(),
          "/mis-citas": (_) => const MisCitasScreen(),
          "/reservar-cita": (_) => const ReservarCitaScreen(),
        },
      ),
    );
  }
}
