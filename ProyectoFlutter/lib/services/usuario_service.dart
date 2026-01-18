import 'package:flutter/material.dart';
import 'api_client.dart';

class UsuarioService {
  final ApiClient _api = ApiClient();

  Future<List<dynamic>> getUsuarios() async {
    try {
      final response = await _api.get('/usuarios');
      if (response.data is List) return response.data;
      return [];
    } catch (e) {
      debugPrint("Error en getUsuarios: $e");
      return [];
    }
  }

  Future<Map<String, dynamic>> getUsuario(int id) async {
    try {
      final response = await _api.get('/usuarios/$id');
      return response.data as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en getUsuario ($id): $e");
      rethrow;
    }
  }

  Future<Map<String, dynamic>> createUsuario(Map<String, dynamic> data) async {
    try {
      final response = await _api.post('/usuarios', data: data);
      return response.data as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en createUsuario: $e");
      rethrow;
    }
  }

  Future<Map<String, dynamic>> updateUsuario(
      int id, Map<String, dynamic> data) async {
    try {
      final response = await _api.put('/usuarios/$id', data: data);
      return response.data as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en updateUsuario ($id): $e");
      rethrow;
    }
  }

  Future<void> deleteUsuario(int id) async {
    try {
      await _api.delete('/usuarios/$id');
    } catch (e) {
      debugPrint("Error en deleteUsuario ($id): $e");
      rethrow;
    }
  }
}
