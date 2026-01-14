import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

class BaseApi {
  // REEMPLAZA ESTA IP por la IPv4 de tu PC (ej: 192.168.1.35)
  // El puerto 8090 debe estar abierto en tu Firewall de Windows.
  static const String baseUrl = 'http://192.168.1.XX:8090/api';
  static String? token;

  // Generador de cabeceras dinámico
  static Map<String, String> _headers({bool auth = true}) {
    final h = {"Content-Type": "application/json"};
    if (auth && token != null) {
      h["Authorization"] = "Bearer $token";
    }
    return h;
  }

  // Procesador de respuestas centralizado
  static dynamic _process(http.Response res) {
    if (res.statusCode >= 200 && res.statusCode < 300) {
      return res.body.isEmpty ? null : jsonDecode(res.body);
    }
    // Si el error es 401 o 403, es probable que el token haya caducado
    if (res.statusCode == 401 || res.statusCode == 403) {
      throw Exception("Sesión caducada o sin permisos");
    }
    throw Exception("Error ${res.statusCode}: ${res.body}");
  }

  // MÉTODO GET
  static Future<dynamic> get(String path) async {
    try {
      final res =
          await http.get(Uri.parse("$baseUrl$path"), headers: _headers());
      return _process(res);
    } catch (e) {
      debugPrint("Error GET en $path: $e");
      rethrow;
    }
  }

  // MÉTODO POST
  static Future<dynamic> post(String path, Map<String, dynamic> data) async {
    try {
      final res = await http.post(
        Uri.parse("$baseUrl$path"),
        headers: _headers(),
        body: jsonEncode(data),
      );
      return _process(res);
    } catch (e) {
      debugPrint("Error POST en $path: $e");
      rethrow;
    }
  }

  // MÉTODO PUT
  static Future<dynamic> put(String path, Map<String, dynamic> data) async {
    try {
      final res = await http.put(
        Uri.parse("$baseUrl$path"),
        headers: _headers(),
        body: jsonEncode(data),
      );
      return _process(res);
    } catch (e) {
      debugPrint("Error PUT en $path: $e");
      rethrow;
    }
  }

  // MÉTODO DELETE
  static Future<void> delete(String path) async {
    try {
      final res =
          await http.delete(Uri.parse("$baseUrl$path"), headers: _headers());
      if (res.statusCode < 200 || res.statusCode >= 300) {
        throw Exception("No se pudo eliminar el recurso");
      }
    } catch (e) {
      debugPrint("Error DELETE en $path: $e");
      rethrow;
    }
  }
}
