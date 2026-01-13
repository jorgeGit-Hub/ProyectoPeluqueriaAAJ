import 'package:flutter/material.dart';

import '../services/auth_service.dart';
import '../services/base_api.dart';

class UserProvider with ChangeNotifier {
  int? id;
  String? nombre;
  String? apellidos;
  String? correo;
  String? rol;

  bool isLogged = false;
  bool isAdmin = false;
  bool loading = false;

  final AuthService _authService = AuthService();

  /// Getter compatible para TODAS las pantallas
  /// Uso: provider.usuario?["nombre"]
  Map<String, dynamic>? get usuario {
    if (id == null) return null;

    return {
      "id": id,
      "nombre": nombre,
      "apellidos": apellidos,
      "correo": correo,
      "rol": rol,
    };
  }

  /// Cargar sesión desde token
  Future<void> loadUserFromToken() async {
    loading = true;
    notifyListeners();

    try {
      final token = await _authService.getSavedToken();

      if (token == null || token.isEmpty) {
        _clearSession();
        return;
      }

      BaseApi.token = token;

      final userData = await _authService.validateAndGetUser();

      id = userData["id"] is int
          ? userData["id"] as int
          : int.tryParse("${userData["id"]}");

      nombre = userData["nombre"]?.toString();
      apellidos = userData["apellidos"]?.toString();
      correo = userData["correo"]?.toString();
      rol = userData["rol"]?.toString();

      isLogged = id != null;
      isAdmin = rol == "ROLE_ADMINISTRADOR";
    } catch (_) {
      await _authService.logout();
      _clearSession();
    }

    loading = false;
    notifyListeners();
  }

  /// LOGIN
  Future<bool> login(String correo, String password) async {
    loading = true;
    notifyListeners();

    try {
      await _authService.login(correo, password);
      await loadUserFromToken();
      return true;
    } catch (_) {
      loading = false;
      notifyListeners();
      return false;
    }
  }

  /// REGISTER
  Future<bool> register({
    required String nombre,
    required String apellidos,
    required String correo,
    required String password,
  }) async {
    loading = true;
    notifyListeners();

    try {
      final ok = await _authService.register(
        nombre: nombre,
        apellidos: apellidos,
        correo: correo,
        password: password,
      );

      loading = false;
      notifyListeners();
      return ok;
    } catch (_) {
      loading = false;
      notifyListeners();
      return false;
    }
  }

  /// LOGOUT
  Future<void> logout() async {
    await _authService.logout();
    _clearSession();
    notifyListeners();
  }

  void _clearSession() {
    id = null;
    nombre = null;
    apellidos = null;
    correo = null;
    rol = null;

    isLogged = false;
    isAdmin = false;
    loading = false;
  }
}
