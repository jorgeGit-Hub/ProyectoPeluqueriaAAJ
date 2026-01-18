import 'package:flutter/material.dart';
import 'api_client.dart';

class ServicioService {
  final ApiClient _api = ApiClient();

  Future<List<dynamic>> getServicios() async {
    try {
      final response = await _api.get('/servicios');
      if (response.data is List) return response.data;
      return [];
    } catch (e) {
      debugPrint("Error en getServicios: $e");
      return [];
    }
  }

  Future<Map<String, dynamic>> getServicio(int id) async {
    try {
      final response = await _api.get('/servicios/$id');
      return response.data as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en getServicio ($id): $e");
      rethrow;
    }
  }

  Future<Map<String, dynamic>> createServicio(Map<String, dynamic> data) async {
    try {
      final response = await _api.post('/servicios', data: data);
      return response.data as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en createServicio: $e");
      rethrow;
    }
  }

  Future<Map<String, dynamic>> updateServicio(
      int id, Map<String, dynamic> data) async {
    try {
      final response = await _api.put('/servicios/$id', data: data);
      return response.data as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en updateServicio ($id): $e");
      rethrow;
    }
  }

  Future<void> deleteServicio(int id) async {
    try {
      await _api.delete('/servicios/$id');
    } catch (e) {
      debugPrint("Error en deleteServicio ($id): $e");
      rethrow;
    }
  }
}
