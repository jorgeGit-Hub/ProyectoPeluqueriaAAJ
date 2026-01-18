import 'package:flutter/material.dart';
import '../models/servicio.dart';
import '../services/servicio_service.dart';

class ServicioProvider with ChangeNotifier {
  List<Servicio> servicios = [];
  Servicio? servicioSeleccionado;
  bool loading = false;

  final ServicioService _service = ServicioService();

  Future<void> loadServicios() async {
    loading = true;
    notifyListeners();

    try {
      final List<dynamic> data = await _service.getServicios();
      servicios = data.map((e) => Servicio.fromJson(e)).toList();
    } catch (e) {
      debugPrint("Error al cargar servicios: $e");
      servicios = [];
    }

    loading = false;
    notifyListeners();
  }

  Future<void> loadServicio(int id) async {
    loading = true;
    notifyListeners();

    try {
      final data = await _service.getServicio(id);
      servicioSeleccionado = Servicio.fromJson(data);
    } catch (e) {
      debugPrint("Error al cargar servicio individual ($id): $e");
      servicioSeleccionado = null;
    }

    loading = false;
    notifyListeners();
  }

  void clearSelection() {
    servicioSeleccionado = null;
    notifyListeners();
  }
}
