import 'package:flutter/material.dart';
import '../models/cita.dart';
import '../services/cita_service.dart';

class CitaProvider with ChangeNotifier {
  final CitaService _service = CitaService();

  // Cambiamos a List<Cita> porque el modelo ya es robusto contra nulos e IDs sueltos
  List<Cita> citas = [];

  bool loading = false;
  bool error = false;

  /// Cargar citas del cliente (usamos idUsuario porque Cliente hereda de Usuario)
  Future<void> loadCitasByCliente(int idUsuario) async {
    loading = true;
    error = false;
    notifyListeners();

    try {
      // Llamamos al servicio (asegúrate de que el nombre coincida en cita_service.dart)
      final List<dynamic> data = await _service.getCitasByCliente(idUsuario);

      // El modelo Cita.fromJson ya se encarga de limpiar los nulos de la BD
      citas = data.map((item) => Cita.fromJson(item)).toList();
    } catch (e) {
      debugPrint("Error al cargar citas: $e");
      error = true;
    }

    loading = false;
    notifyListeners();
  }

  /// Crear cita enviando el JSON estructurado para Spring Boot
  Future<bool> crearCita(Cita nuevaCita) async {
    try {
      // Usamos toCreateJson() que definimos en el modelo para enviar IDs, no objetos
      final bool success = await _service.createCita(nuevaCita.toCreateJson());

      if (success) {
        // Refrescamos la lista local tras crear una nueva
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
