import 'package:flutter/material.dart';
import 'base_api.dart';

class ServicioService {
  final String _endpoint = "/servicios";

  // ======================================================
  // Obtener todos los servicios (Corte, Tinte, etc.)
  // ======================================================
  Future<List<dynamic>> getServicios() async {
    try {
      final res = await BaseApi.get(_endpoint);
      // Verificamos que sea una lista para evitar errores de mapeo en el Provider
      if (res is List) return res;
      return [];
    } catch (e) {
      debugPrint("Error en getServicios: $e");
      return []; // Devolvemos lista vacía para que la UI no explote
    }
  }

  // ======================================================
  // Obtener un solo servicio por su ID
  // ======================================================
  Future<Map<String, dynamic>> getServicio(int id) async {
    try {
      final res = await BaseApi.get("$_endpoint/$id");
      return res as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en getServicio ($id): $e");
      rethrow;
    }
  }

  // ======================================================
  // Crear un nuevo servicio (Rol: Administrador)
  // ======================================================
  Future<Map<String, dynamic>> createServicio(Map<String, dynamic> data) async {
    try {
      final res = await BaseApi.post(_endpoint, data);
      return res as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en createServicio: $e");
      rethrow;
    }
  }

  // ======================================================
  // Actualizar un servicio existente (Rol: Administrador)
  // ======================================================
  Future<Map<String, dynamic>> updateServicio(
      int id, Map<String, dynamic> data) async {
    try {
      final res = await BaseApi.put("$_endpoint/$id", data);
      return res as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en updateServicio ($id): $e");
      rethrow;
    }
  }

  // ======================================================
  // Eliminar un servicio (Rol: Administrador)
  // ======================================================
  Future<void> deleteServicio(int id) async {
    try {
      await BaseApi.delete("$_endpoint/$id");
    } catch (e) {
      debugPrint("Error en deleteServicio ($id): $e");
      rethrow;
    }
  }
}
