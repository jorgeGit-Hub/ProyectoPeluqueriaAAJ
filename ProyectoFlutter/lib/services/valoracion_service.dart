import 'package:flutter/material.dart';
import 'api_client.dart';

class ValoracionService {
  final ApiClient _api = ApiClient();

  Future<List<dynamic>> getValoraciones() async {
    try {
      final response = await _api.get('/valoraciones');
      if (response.data is List) return response.data;
      return [];
    } catch (e) {
      debugPrint("Error en getValoraciones: $e");
      return [];
    }
  }

  Future<Map<String, dynamic>> getValoracion(int id) async {
    try {
      final response = await _api.get('/valoraciones/$id');
      return response.data as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en getValoracion ($id): $e");
      rethrow;
    }
  }

  Future<List<dynamic>> getValoracionesByMinPuntuacion(
      int minPuntuacion) async {
    try {
      final response = await _api.get('/valoraciones/min/$minPuntuacion');
      if (response.data is List) return response.data;
      return [];
    } catch (e) {
      debugPrint("Error en getValoracionesByMinPuntuacion: $e");
      return [];
    }
  }

  // ✅ NUEVO: Valoraciones filtradas por servicio
  Future<List<dynamic>> getValoracionesByServicio(int idServicio) async {
    try {
      final response = await _api.get('/valoraciones/servicio/$idServicio');
      if (response.data is List) return response.data;
      return [];
    } catch (e) {
      debugPrint("Error en getValoracionesByServicio ($idServicio): $e");
      return [];
    }
  }

  Future<Map<String, dynamic>> createValoracion(
      Map<String, dynamic> data) async {
    try {
      debugPrint("🔵 Creando valoración: $data");
      final response = await _api.post('/valoraciones', data: data);
      return response.data as Map<String, dynamic>;
    } catch (e) {
      debugPrint("❌ Error en createValoracion: $e");
      rethrow;
    }
  }

  Future<Map<String, dynamic>> updateValoracion(
      int id, Map<String, dynamic> data) async {
    try {
      final response = await _api.put('/valoraciones/$id', data: data);
      return response.data as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en updateValoracion ($id): $e");
      rethrow;
    }
  }

  Future<void> deleteValoracion(int id) async {
    try {
      await _api.delete('/valoraciones/$id');
    } catch (e) {
      debugPrint("Error en deleteValoracion ($id): $e");
      rethrow;
    }
  }
}
