import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:shared_preferences/shared_preferences.dart';
import 'base_api.dart';

class AuthService {
  final String _authBase = "${BaseApi.baseUrl}/auth";

  // ======================================================
  // Inicio de Sesión
  // ======================================================
  Future<bool> login(String correo, String password) async {
    final res = await http.post(
      Uri.parse("$_authBase/signin"),
      headers: {"Content-Type": "application/json"},
      body: jsonEncode({"correo": correo, "password": password}),
    );

    if (res.statusCode != 200) return false;

    final data = jsonDecode(res.body);
    final String token = data["accessToken"];

    BaseApi.token = token;
    final prefs = await SharedPreferences.getInstance();

    // Guardamos los datos para el inicio automático (SplashScreen)
    await prefs.setString("token", token);
    await prefs.setString("userId", data["id"].toString());
    await prefs.setString("nombre", data["nombre"]?.toString() ?? "");
    await prefs.setString("rol", data["rol"]?.toString() ?? "cliente");

    return true;
  }

  // ======================================================
  // Registro de Nuevo Usuario (¡Añadido!)
  // ======================================================
  Future<bool> register(
      {required String nombre,
      required String apellidos,
      required String correo,
      required String password}) async {
    final res = await http.post(
      Uri.parse("$_authBase/signup"),
      headers: {"Content-Type": "application/json"},
      body: jsonEncode({
        "nombre": nombre,
        "apellidos": apellidos,
        "correo": correo,
        "password": password,
        "rol": "cliente" // Rol por defecto
      }),
    );

    // El backend de Spring Boot suele devolver 200 o 201 en caso de éxito
    return res.statusCode == 200 || res.statusCode == 201;
  }

  // ======================================================
  // Validación de Token y Recuperación de Usuario
  // ======================================================
  Future<Map<String, dynamic>> validateAndGetUser() async {
    final res = await http.get(
      Uri.parse("$_authBase/validate"),
      // Usamos el token que está en memoria
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer ${BaseApi.token}"
      },
    );

    if (res.statusCode != 200) {
      throw Exception("Token no válido");
    }

    return jsonDecode(res.body);
  }

  // ======================================================
  // Utilidades de Persistencia
  // ======================================================
  Future<String?> getSavedToken() async {
    final prefs = await SharedPreferences.getInstance();
    return prefs.getString("token");
  }

  Future<void> logout() async {
    BaseApi.token = null;
    final prefs = await SharedPreferences.getInstance();
    await prefs.clear();
  }
}
