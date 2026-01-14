import 'package:flutter/material.dart';

class AppTheme {
  // ------------------------------------------------------
  // Paleta de Colores
  // ------------------------------------------------------
  static const Color primary = Color(0xFF6741D9); // Morado principal
  static const Color pastelLavender = Color(0xFFE6E6FA); // Fondo lavanda
  static const Color pastelPink = Color(0xFFFFD6E8); // Acento rosa
  static const Color surfaceWhite = Colors.white;

  // ------------------------------------------------------
  // Tema Principal de la Aplicación
  // ------------------------------------------------------
  static final ThemeData theme = ThemeData(
    useMaterial3: true,
    colorScheme: ColorScheme.fromSeed(
      seedColor: primary,
      primary: primary,
      secondary: pastelPink,
      surface: surfaceWhite,
      background: pastelLavender,
    ),

    scaffoldBackgroundColor: pastelLavender,

    // Estilo de la Barra Superior (AppBar)
    appBarTheme: const AppBarTheme(
      backgroundColor: primary,
      foregroundColor: Colors.white,
      elevation: 0,
      centerTitle: true,
      titleTextStyle: TextStyle(
        fontSize: 20,
        fontWeight: FontWeight.bold,
        color: Colors.white,
      ),
    ),

    // Estilo de las Tarjetas (Cards)
    cardTheme: CardTheme(
      color: Colors.white,
      elevation: 2,
      margin: const EdgeInsets.symmetric(vertical: 8),
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
    ),

    // Estilo de los Botones Principales
    elevatedButtonTheme: ElevatedButtonThemeData(
      style: ElevatedButton.styleFrom(
        backgroundColor: primary,
        foregroundColor: Colors.white,
        elevation: 2,
        textStyle: const TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
        padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 14),
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
      ),
    ),

    // Estilo de los Campos de Texto (Inputs)
    // Esto hace que todos los formularios de tu app se vean iguales automáticamente
    inputDecorationTheme: InputDecorationTheme(
      filled: true,
      fillColor: Colors.white,
      contentPadding: const EdgeInsets.symmetric(horizontal: 20, vertical: 16),
      border: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: BorderSide(color: Colors.grey.shade300),
      ),
      enabledBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: BorderSide(color: Colors.grey.shade300),
      ),
      focusedBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: const BorderSide(color: primary, width: 2),
      ),
      errorBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: const BorderSide(color: Colors.redAccent),
      ),
      labelStyle: const TextStyle(color: Colors.grey),
      prefixIconColor: primary,
    ),

    // Estilo de los textos
    textTheme: const TextTheme(
      displayLarge:
          TextStyle(fontSize: 32, fontWeight: FontWeight.bold, color: primary),
      titleLarge: TextStyle(
          fontSize: 20, fontWeight: FontWeight.bold, color: Colors.black87),
      bodyLarge: TextStyle(fontSize: 16, color: Colors.black87),
    ),
  );
}
