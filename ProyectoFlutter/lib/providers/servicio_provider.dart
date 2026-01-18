import 'package:flutter/material.dart';
import '../models/servicio.dart';
import '../services/servicio_service.dart';

class ServicioProvider with ChangeNotifier {
  List<Servicio> servicios = [];
  Servicio? servicioSeleccionado;
  bool loading = false;
  String? errorMessage; // ✅ NUEVO: Guardar el error real

  final ServicioService _service = ServicioService();

  Future<void> loadServicios() async {
    loading = true;
    errorMessage = null; // ✅ Limpiar error previo
    notifyListeners();

    try {
      final List<dynamic> data = await _service.getServicios();

      debugPrint("📥 Servicios recibidos del backend: ${data.length}");
      debugPrint("📦 Datos raw: $data");

      // ✅ Convertir uno por uno para detectar errores específicos
      servicios = [];
      for (int i = 0; i < data.length; i++) {
        try {
          final servicio = Servicio.fromJson(data[i]);
          servicios.add(servicio);
          debugPrint("✅ Servicio $i convertido: ${servicio.nombre}");
        } catch (e) {
          debugPrint("❌ Error convirtiendo servicio $i: $e");
          debugPrint("📄 Datos del servicio problemático: ${data[i]}");
        }
      }

      debugPrint("✅ Total servicios cargados: ${servicios.length}");
    } catch (e) {
      debugPrint("❌ Error al cargar servicios desde el servidor: $e");
      errorMessage = e.toString();
      servicios = [];
    }

    loading = false;
    notifyListeners();
  }

  Future<void> loadServicio(int id) async {
    loading = true;
    errorMessage = null;
    notifyListeners();

    try {
      final data = await _service.getServicio(id);
      servicioSeleccionado = Servicio.fromJson(data);
      debugPrint(
          "✅ Servicio individual cargado: ${servicioSeleccionado?.nombre}");
    } catch (e) {
      debugPrint("❌ Error al cargar servicio individual ($id): $e");
      errorMessage = e.toString();
      servicioSeleccionado = null;
    }

    loading = false;
    notifyListeners();
  }

  void clearSelection() {
    servicioSeleccionado = null;
    errorMessage = null;
    notifyListeners();
  }
}
