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

  /// Usa el endpoint /citas/mis-citas que filtra por el token JWT del usuario actual.
  /// Más eficiente y correcto que descargar todas las citas y filtrar en cliente.
  Future<List<dynamic>> getCitasByCliente(int idUsuario) async {
    try {
      final response = await _api.get('/citas/mis-citas');
      if (response.data is List) return response.data;
      return [];
    } catch (e) {
      debugPrint("Error en getCitasByCliente: $e");
      return [];
    }
  }

  Future<bool> createCita(Map<String, dynamic> data) async {
    try {
      debugPrint("🔵 Creando cita con datos: $data");
      await _api.post('/citas', data: data);
      return true;
    } catch (e) {
      debugPrint("❌ Error en createCita: $e");
      rethrow;
    }
  }

  Future<bool> updateCita(int id, Map<String, dynamic> data) async {
    try {
      debugPrint("🔵 Actualizando cita $id");
      await _api.put('/citas/$id', data: data);
      return true;
    } catch (e) {
      debugPrint("❌ Error en updateCita: $e");
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
