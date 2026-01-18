import 'package:flutter/material.dart';
import 'api_client.dart';

class ClienteService {
  final ApiClient _api = ApiClient();

  Future<Map<String, dynamic>> getCliente(int idUsuario) async {
    try {
      final response = await _api.get('/clientes/$idUsuario');
      if (response.data == null) throw Exception("Cliente no encontrado");
      return response.data as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en getCliente ($idUsuario): $e");
      rethrow;
    }
  }

  Future<Map<String, dynamic>> createCliente(Map<String, dynamic> data) async {
    try {
      final response = await _api.post('/clientes', data: data);
      return response.data as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en createCliente: $e");
      rethrow;
    }
  }

  Future<Map<String, dynamic>> updateCliente(
      int idUsuario, Map<String, dynamic> data) async {
    try {
      final response = await _api.put('/clientes/$idUsuario', data: data);
      return response.data as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en updateCliente ($idUsuario): $e");
      rethrow;
    }
  }
}
