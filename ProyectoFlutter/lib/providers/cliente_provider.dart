import 'package:flutter/material.dart';
import '../models/cliente.dart';
import '../services/cliente_service.dart';

class ClienteProvider with ChangeNotifier {
  Cliente? cliente;
  bool loading = false;

  final ClienteService _service = ClienteService();

  Future<void> loadCliente(int idUsuario) async {
    loading = true;
    notifyListeners();

    try {
      final data = await _service.getCliente(idUsuario);
      cliente = Cliente.fromJson(data);
    } catch (e) {
      debugPrint("Error al cargar cliente: $e");
      cliente = null;
    }

    loading = false;
    notifyListeners();
  }

  Future<bool> updateCliente(
      int idUsuario, Map<String, dynamic> changes) async {
    loading = true;
    notifyListeners();

    try {
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

  void clear() {
    cliente = null;
    loading = false;
    notifyListeners();
  }
}
