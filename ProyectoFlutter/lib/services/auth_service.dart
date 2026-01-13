import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:shared_preferences/shared_preferences.dart';

import 'base_api.dart';

class AuthService {
  final String _authBase = "${BaseApi.baseUrl}/auth";

  /// LOGIN
  Future<bool> login(String correo, String password) async {
    final res = await http
        .post(
          Uri.parse("$_authBase/signin"),
          headers: {"Content-Type": "application/json"},
          body: jsonEncode({
            "correo": correo,
            "password": password,
          }),
        )
        .timeout(const Duration(seconds: 10));

    if (res.statusCode != 200) {
      throw Exception("Login inválido (${res.statusCode}): ${res.body}");
    }

    final data = jsonDecode(res.body) as Map<String, dynamic>;
    final token = data["accessToken"]?.toString();

    if (token == null || token.isEmpty) {
      throw Exception("No se recibió accessToken");
    }

    BaseApi.token = token;

    final prefs = await SharedPreferences.getInstance();
    await prefs.setString("token", token);

    if (data["rol"] != null) {
      await prefs.setString("rol", data["rol"].toString());
    }
    if (data["id"] is int) {
      await prefs.setInt("userId", data["id"] as int);
    }

    return true;
  }

  /// REGISTER
  Future<bool> register({
    required String nombre,
    required String apellidos,
    required String correo,
    required String password,
  }) async {
    final res = await http
        .post(
          Uri.parse("$_authBase/signup"),
          headers: {"Content-Type": "application/json"},
          body: jsonEncode({
            "nombre": nombre,
            "apellidos": apellidos,
            "correo": correo,
            "password": password,
          }),
        )
        .timeout(const Duration(seconds: 10));

    if (res.statusCode == 200) return true;

    throw Exception("Registro fallido (${res.statusCode}): ${res.body}");
  }

  /// TOKEN GUARDADO
  Future<String?> getSavedToken() async {
    final prefs = await SharedPreferences.getInstance();
    return prefs.getString("token");
  }

  /// LOGOUT
  Future<void> logout() async {
    BaseApi.token = null;
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove("token");
    await prefs.remove("rol");
    await prefs.remove("userId");
  }

  /// VALIDAR TOKEN + DATOS USUARIO
  Future<Map<String, dynamic>> validateAndGetUser() async {
    final token = BaseApi.token;
    if (token == null || token.isEmpty) {
      throw Exception("No hay token");
    }

    final res = await http.get(
      Uri.parse("$_authBase/validate"),
      headers: {"Authorization": "Bearer $token"},
    ).timeout(const Duration(seconds: 10));

    if (res.statusCode != 200) {
      throw Exception("Token inválido (${res.statusCode}): ${res.body}");
    }

    return jsonDecode(res.body) as Map<String, dynamic>;
  }
}
