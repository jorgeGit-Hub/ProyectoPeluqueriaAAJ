import 'package:flutter/material.dart';
import 'api_client.dart';

class AuthService {
  final ApiClient _api = ApiClient();

  Future<Map<String, dynamic>> login(String correo, String password) async {
    try {
      final response = await _api.post(
        '/auth/signin',
        data: {"correo": correo, "contrasena": password},
      );

      final data = response.data;
      final String token = data["accessToken"];

      await _api.setToken(token);

      return {
        "success": true,
        "token": token,
        "id": data["id"],
        "nombre": data["nombre"],
        "apellidos": data["apellidos"],
        "correo": data["correo"],
        "rol": data["rol"],
      };
    } catch (e) {
      debugPrint("Error en login: $e");
      return {"success": false, "error": e.toString()};
    }
  }

  Future<Map<String, dynamic>> register({
    required String nombre,
    required String apellidos,
    required String correo,
    required String password,
    String? telefono,
    String? direccion,
  }) async {
    try {
      final response = await _api.post(
        '/auth/signup',
        data: {
          "nombre": nombre,
          "apellidos": apellidos,
          "correo": correo,
          "contrasena": password,
          "rol": "cliente",
          if (telefono != null) "telefono": telefono,
          if (direccion != null) "direccion": direccion,
        },
      );

      return {"success": true, "data": response.data};
    } catch (e) {
      debugPrint("Error en register: $e");
      return {"success": false, "error": e.toString()};
    }
  }

  Future<Map<String, dynamic>> validateToken() async {
    try {
      final response = await _api.get('/auth/validate');
      final data = response.data;

      return {
        "success": true,
        "id": data["id"],
        "nombre": data["nombre"],
        "apellidos": data["apellidos"],
        "correo": data["correo"],
        "rol": data["rol"],
      };
    } catch (e) {
      debugPrint("Error en validateToken: $e");
      return {"success": false, "error": e.toString()};
    }
  }

  Future<void> logout() async {
    await _api.clearToken();
  }
}
