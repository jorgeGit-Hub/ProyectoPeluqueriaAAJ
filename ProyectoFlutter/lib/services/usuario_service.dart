import 'package:flutter/material.dart';
import 'base_api.dart';

class UsuarioService {
  final String _endpoint = "/usuarios";

  // ======================================================
  // Obtener lista de todos los usuarios (Rol: Admin)
  // ======================================================
  Future<List<dynamic>> getUsuarios() async {
    try {
      final res = await BaseApi.get(_endpoint);
      if (res is List) return res;
      return [];
    } catch (e) {
      debugPrint("Error en getUsuarios: $e");
      return [];
    }
  }

  // ======================================================
  // Obtener un usuario por ID
  // ======================================================
  Future<Map<String, dynamic>> getUsuario(int id) async {
    try {
      final res = await BaseApi.get("$_endpoint/$id");
      return res as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en getUsuario ($id): $e");
      rethrow;
    }
  }

  // ======================================================
  // Crear un usuario manualmente (Rol: Admin)
  // ======================================================
  Future<Map<String, dynamic>> createUsuario(Map<String, dynamic> data) async {
    try {
      final res = await BaseApi.post(_endpoint, data);
      return res as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en createUsuario: $e");
      rethrow;
    }
  }

  // ======================================================
  // Actualizar datos de usuario (Rol: Admin / Usuario)
  // ======================================================
  Future<Map<String, dynamic>> updateUsuario(
      int id, Map<String, dynamic> data) async {
    try {
      final res = await BaseApi.put("$_endpoint/$id", data);
      return res as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en updateUsuario ($id): $e");
      rethrow;
    }
  }

  // ======================================================
  // Eliminar un usuario (Rol: Admin)
  // ======================================================
  Future<void> deleteUsuario(int id) async {
    try {
      await BaseApi.delete("$_endpoint/$id");
    } catch (e) {
      debugPrint("Error en deleteUsuario ($id): $e");
      rethrow;
    }
  }
}
