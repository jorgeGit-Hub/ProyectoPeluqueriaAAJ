import 'package:flutter/material.dart';
import '../models/cita.dart';
import '../services/cita_service.dart';

class CitaProvider with ChangeNotifier {
  final CitaService _service = CitaService();

  List<Cita> citas = [];
  bool loading = false;
  bool error = false;

  Future<void> loadCitasByCliente(int idUsuario) async {
    loading = true;
    error = false;
    notifyListeners();

    try {
      final List<dynamic> data = await _service.getCitasByCliente(idUsuario);
      citas = data.map((item) => Cita.fromJson(item)).toList();
    } catch (e) {
      debugPrint("Error al cargar citas: $e");
      error = true;
    }

    loading = false;
    notifyListeners();
  }

  Future<bool> crearCita(Cita nuevaCita) async {
    try {
      final bool success = await _service.createCita(nuevaCita.toCreateJson());

      if (success) {
        await loadCitasByCliente(nuevaCita.idCliente);
      }
      return success;
    } catch (e) {
      debugPrint("Error al crear cita: $e");
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
