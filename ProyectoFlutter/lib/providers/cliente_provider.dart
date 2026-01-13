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
  Future<void> loadCliente(int idCliente) async {
    loading = true;
    notifyListeners();

    try {
      final data = await _service.getCliente(idCliente);
      cliente = Cliente.fromJson(data);
    } catch (e) {
      cliente = null;
    }

    loading = false;
    notifyListeners();
  }

  // ======================================================
  // Actualizar cliente
  // ======================================================
  Future<bool> updateCliente(
      int idCliente, Map<String, dynamic> changes) async {
    loading = true;
    notifyListeners();

    try {
      final data = await _service.updateCliente(idCliente, changes);
      cliente = Cliente.fromJson(data);

      loading = false;
      notifyListeners();
      return true;
    } catch (e) {
      loading = false;
      notifyListeners();
      return false;
    }
  }
}
