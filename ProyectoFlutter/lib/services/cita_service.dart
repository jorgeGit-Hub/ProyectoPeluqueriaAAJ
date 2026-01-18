import 'package:flutter/material.dart';
import 'api_client.dart';

class CitaService {
  final ApiClient _api = ApiClient();

  Future<List<dynamic>> getCitas() async {
    try {
      final response = await _api.get('/citas');
      if (response.data is List) return response.data;
      return [];
    } catch (e) {
      debugPrint("Error en getCitas: $e");
      return [];
    }
  }

  Future<List<dynamic>> getCitasByCliente(int idUsuario) async {
    try {
      final allCitas = await getCitas();
      return allCitas.where((c) {
        if (c is! Map<String, dynamic>) return false;
        final cliente = c["cliente"];
        if (cliente is Map<String, dynamic>) {
          final id = cliente["idUsuario"] ?? cliente["id_usuario"];
          return id == idUsuario;
        }
        if (cliente is int) return cliente == idUsuario;
        return false;
      }).toList();
    } catch (e) {
      debugPrint("Error en getCitasByCliente: $e");
      return [];
    }
  }

  Future<bool> createCita(Map<String, dynamic> data) async {
    try {
      await _api.post('/citas', data: data);
      return true;
    } catch (e) {
      debugPrint("Error en createCita: $e");
      rethrow;
    }
  }

  Future<bool> updateCita(int id, Map<String, dynamic> data) async {
    try {
      await _api.put('/citas/$id', data: data);
      return true;
    } catch (e) {
      debugPrint("Error en updateCita: $e");
      rethrow;
    }
  }

  Future<void> deleteCita(int id) async {
    try {
      await _api.delete('/citas/$id');
    } catch (e) {
      debugPrint("Error en deleteCita: $e");
      rethrow;
    }
  }
}
