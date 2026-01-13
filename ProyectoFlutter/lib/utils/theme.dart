import 'package:flutter/material.dart';

class AppTheme {
  // Colores principales
  static const Color pastelLavender = Color(0xFFE6E6FA);
  static const Color pastelPink = Color(0xFFFFD6E8);
  static const Color primary = Color(0xFF6741D9);

  // Tema principal de la aplicación
  static final ThemeData theme = ThemeData(
    useMaterial3: true,
    colorScheme: ColorScheme.fromSeed(
      seedColor: primary,
      primary: primary,
      secondary: pastelPink,
    ),
    scaffoldBackgroundColor: pastelLavender,
    appBarTheme: const AppBarTheme(
      backgroundColor: Color.fromARGB(255, 255, 153, 0),
      foregroundColor: Colors.white,
      elevation: 0,
    ),
    elevatedButtonTheme: ElevatedButtonThemeData(
      style: ElevatedButton.styleFrom(
        backgroundColor: primary,
        foregroundColor: Colors.white,
        textStyle: const TextStyle(fontWeight: FontWeight.w600),
        padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 12),
      ),
    ),
  );
}
