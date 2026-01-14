import 'package:flutter/material.dart';
// Asegúrate de que las rutas coincidan con tu proyecto
import '../models/servicio.dart';
import '../services/servicio_service.dart';

class ServicioProvider with ChangeNotifier {
  List<Servicio> servicios = [];
  Servicio? servicioSeleccionado;
  bool loading = false;

  // Creamos una única instancia del servicio
  final ServicioService _service = ServicioService();

  // ======================================================
  // Cargar todos los servicios
  // ======================================================
  Future<void> loadServicios() async {
    loading = true;
    notifyListeners();

    try {
      final List<dynamic> data = await _service.getServicios();

      // El modelo Servicio.fromJson ya se encarga de mapear duracion y descripcion
      servicios = data.map((e) => Servicio.fromJson(e)).toList();
    } catch (e) {
      debugPrint("Error al cargar servicios: $e");
      servicios = [];
    }

    loading = false;
    notifyListeners();
  }

  // ======================================================
  // Cargar un servicio específico por ID
  // ======================================================
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

  // Limpiar selección (útil al navegar)
  void clearSelection() {
    servicioSeleccionado = null;
    notifyListeners();
  }
}
