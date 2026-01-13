import 'package:flutter/material.dart';
import '../services/cita_service.dart';

class CitaProvider with ChangeNotifier {
  final CitaService _service = CitaService();

  /// Lista de citas como Map (más robusto que usar modelos aquí)
  List<Map<String, dynamic>> citas = [];

  bool loading = false;
  bool error = false;

  /// Cargar citas del usuario
  Future<void> loadCitasByUsuario(int idUsuario) async {
    loading = true;
    error = false;
    notifyListeners();

    try {
      final List<dynamic> data = await _service.getCitasByUsuario(idUsuario);

      citas = data.whereType<Map<String, dynamic>>().toList();
    } catch (_) {
      error = true;
    }

    loading = false;
    notifyListeners();
  }

  /// Crear cita
  Future<bool> crearCita(Map<String, dynamic> citaData) async {
    try {
      await _service.createCita(citaData);
      return true;
    } catch (_) {
      return false;
    }
  }

  void clear() {
    citas.clear();
    loading = false;
    error = false;
    notifyListeners();
  }
}
