import 'package:flutter/material.dart';
import '../models/valoracion.dart';
import '../services/valoracion_service.dart';

class ValoracionProvider with ChangeNotifier {
  final ValoracionService _service = ValoracionService();

  List<Valoracion> valoraciones = [];
  bool loading = false;
  String? error;

  Future<void> loadValoraciones() async {
    loading = true;
    error = null;
    notifyListeners();

    try {
      final List<dynamic> data = await _service.getValoraciones();
      valoraciones = data.map((item) => Valoracion.fromJson(item)).toList();
    } catch (e) {
      debugPrint("Error al cargar valoraciones: $e");
      error = e.toString();
    }

    loading = false;
    notifyListeners();
  }

  Future<void> loadValoracionesByMinPuntuacion(int minPuntuacion) async {
    loading = true;
    error = null;
    notifyListeners();

    try {
      final List<dynamic> data =
          await _service.getValoracionesByMinPuntuacion(minPuntuacion);
      valoraciones = data.map((item) => Valoracion.fromJson(item)).toList();
    } catch (e) {
      debugPrint("Error al cargar valoraciones filtradas: $e");
      error = e.toString();
    }

    loading = false;
    notifyListeners();
  }

  Future<bool> createValoracion(Valoracion nuevaValoracion) async {
    try {
      await _service.createValoracion(nuevaValoracion.toJson());
      await loadValoraciones();
      return true;
    } catch (e) {
      debugPrint("Error al crear valoración: $e");
      error = e.toString();
      notifyListeners();
      return false;
    }
  }

  Future<bool> updateValoracion(
      int id, Valoracion valoracionActualizada) async {
    try {
      await _service.updateValoracion(id, valoracionActualizada.toJson());
      await loadValoraciones();
      return true;
    } catch (e) {
      debugPrint("Error al actualizar valoración: $e");
      error = e.toString();
      notifyListeners();
      return false;
    }
  }

  Future<bool> deleteValoracion(int id) async {
    try {
      await _service.deleteValoracion(id);
      await loadValoraciones();
      return true;
    } catch (e) {
      debugPrint("Error al eliminar valoración: $e");
      error = e.toString();
      notifyListeners();
      return false;
    }
  }

  void clear() {
    valoraciones.clear();
    loading = false;
    error = null;
    notifyListeners();
  }
}
