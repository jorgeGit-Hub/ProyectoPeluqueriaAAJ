import 'package:flutter/material.dart';
import 'base_api.dart';

class ValoracionService {
  final String _endpoint = "/valoraciones";

  // ======================================================
  // Obtener todas las valoraciones del salón
  // ======================================================
  Future<List<dynamic>> getValoraciones() async {
    try {
      final res = await BaseApi.get(_endpoint);
      if (res is List) return res;
      return [];
    } catch (e) {
      debugPrint("Error en getValoraciones: $e");
      return [];
    }
  }

  // ======================================================
  // Obtener una valoración específica por ID
  // ======================================================
  Future<Map<String, dynamic>> getValoracion(int id) async {
    try {
      final res = await BaseApi.get("$_endpoint/$id");
      return res as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en getValoracion ($id): $e");
      rethrow;
    }
  }

  // ======================================================
  // Enviar una nueva reseña (Estrellas + Comentario)
  // ======================================================
  Future<Map<String, dynamic>> createValoracion(
      Map<String, dynamic> data) async {
    try {
      final res = await BaseApi.post(_endpoint, data);
      return res as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en createValoracion: $e");
      rethrow;
    }
  }

  // ======================================================
  // Editar una valoración existente
  // ======================================================
  Future<Map<String, dynamic>> updateValoracion(
      int id, Map<String, dynamic> data) async {
    try {
      final res = await BaseApi.put("$_endpoint/$id", data);
      return res as Map<String, dynamic>;
    } catch (e) {
      debugPrint("Error en updateValoracion ($id): $e");
      rethrow;
    }
  }

  // ======================================================
  // Eliminar una valoración
  // ======================================================
  Future<void> deleteValoracion(int id) async {
    try {
      await BaseApi.delete("$_endpoint/$id");
    } catch (e) {
      debugPrint("Error en deleteValoracion ($id): $e");
      rethrow;
    }
  }
}
