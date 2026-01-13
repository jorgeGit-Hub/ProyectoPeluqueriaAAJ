import 'package:flutter/material.dart';
import 'package:peluqueria_aja/models/servicio.dart';
import 'package:peluqueria_aja/services/servicio_service.dart';

class ServicioProvider with ChangeNotifier {
  List<Servicio> servicios = [];
  Servicio? servicioSeleccionado;
  bool loading = false;

  Future<void> loadServicios() async {
    loading = true;
    notifyListeners();

    try {
      final data = await ServicioService().getServicios();
      servicios = data.map<Servicio>((e) => Servicio.fromJson(e)).toList();
    } catch (e) {
      servicios = [];
    }

    loading = false;
    notifyListeners();
  }

  Future<void> loadServicio(int id) async {
    loading = true;
    notifyListeners();

    try {
      final data = await ServicioService().getServicio(id);
      servicioSeleccionado = Servicio.fromJson(data);
    } catch (e) {
      servicioSeleccionado = null;
    }

    loading = false;
    notifyListeners();
  }
}
