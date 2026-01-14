import 'package:flutter/material.dart';
import '../models/cliente.dart';
import '../services/cliente_service.dart';

class ClienteProvider with ChangeNotifier {
  Cliente? cliente;
  bool loading = false;

  final ClienteService _service = ClienteService();

  // ======================================================
  // Cargar cliente por ID
  // ======================================================
  Future<void> loadCliente(int idUsuario) async {
    loading = true;
    notifyListeners();

    try {
      final data = await _service.getCliente(idUsuario);
      // El modelo Cliente.fromJson ya gestiona los nombres de columnas de tu BD
      cliente = Cliente.fromJson(data);
    } catch (e) {
      debugPrint("Error al cargar cliente: $e");
      cliente = null;
    }

    loading = false;
    notifyListeners();
  }

  // ======================================================
  // Actualizar cliente
  // ======================================================
  Future<bool> updateCliente(
      int idUsuario, Map<String, dynamic> changes) async {
    loading = true;
    notifyListeners();

    try {
      // Enviamos los cambios al servicio
      final data = await _service.updateCliente(idUsuario, changes);
      cliente = Cliente.fromJson(data);

      loading = false;
      notifyListeners();
      return true;
    } catch (e) {
      debugPrint("Error al actualizar cliente: $e");
      loading = false;
      notifyListeners();
      return false;
    }
  }

  // ======================================================
  // Limpiar datos (útil para el Logout)
  // ======================================================
  void clear() {
    cliente = null;
    loading = false;
    notifyListeners();
  }
}
