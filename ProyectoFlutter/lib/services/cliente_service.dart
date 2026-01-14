import 'package:flutter/material.dart';
import 'base_api.dart';

class ClienteService {
  // ======================================================
  // Obtener los datos extra de un cliente
  // ======================================================
  Future<Map<String, dynamic>> getCliente(int idUsuario) async {
    try {
      final res = await BaseApi.get("/clientes/$idUsuario");
      if (res == null) throw Exception("Cliente no encontrado");
      return res as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en getCliente ($idUsuario): $e");
      rethrow;
    }
  }

  // ======================================================
  // Registrar datos de cliente por primera vez
  // ======================================================
  Future<Map<String, dynamic>> createCliente(Map<String, dynamic> data) async {
    try {
      final res = await BaseApi.post("/clientes", data);
      return res as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en createCliente: $e");
      rethrow;
    }
  }

  // ======================================================
  // Actualizar datos (Teléfono, Dirección, etc.)
  // ======================================================
  Future<Map<String, dynamic>> updateCliente(
      int idUsuario, Map<String, dynamic> data) async {
    try {
      // Sincronizado con el endpoint de Spring Boot: PUT /api/clientes/{id}
      final res = await BaseApi.put("/clientes/$idUsuario", data);
      return res as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en updateCliente ($idUsuario): $e");
      rethrow;
    }
  }
}
