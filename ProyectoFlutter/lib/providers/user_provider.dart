import 'package:flutter/material.dart';
import '../services/auth_service.dart';
import '../services/api_client.dart';

class UserProvider with ChangeNotifier {
  final AuthService _authService = AuthService();
  final ApiClient _api = ApiClient();

  int? id;
  String? nombre;
  String? apellidos;
  String? correo;
  String? rol;
  bool isLogged = false;
  bool loading = false;

  Map<String, dynamic>? get usuario => id == null
      ? null
      : {
          "id": id,
          "nombre": nombre,
          "apellidos": apellidos,
          "correo": correo,
          "rol": rol,
        };

  void setLoading(bool value) {
    loading = value;
    notifyListeners();
  }

  Future<void> loadUserFromToken() async {
    setLoading(true);
    try {
      await _api.loadToken();

      if (_api.token != null) {
        debugPrint("Token encontrado: ${_api.token}"); // DEBUG
        final result = await _authService.validateToken();

        if (result["success"] == true) {
          id = result["id"];
          nombre = result["nombre"];
          apellidos = result["apellidos"];
          correo = result["correo"];
          rol = result["rol"];
          isLogged = true;
          debugPrint("Usuario validado: $nombre"); // DEBUG
        } else {
          await _api.clearToken();
          isLogged = false;
        }
      } else {
        debugPrint("No hay token guardado"); // DEBUG
        isLogged = false;
      }
    } catch (e) {
      debugPrint("Error en loadUserFromToken: $e");
      await _api.clearToken();
      isLogged = false;
    } finally {
      setLoading(false);
    }
  }

  Future<bool> login(String email, String password) async {
    setLoading(true);
    try {
      final result = await _authService.login(email, password);
      debugPrint("Login result: $result"); // DEBUG

      if (result["success"] == true) {
        id = result["id"];
        nombre = result["nombre"];
        apellidos = result["apellidos"];
        correo = result["correo"];
        rol = result["rol"];
        isLogged = true;

        debugPrint("Login exitoso. Token: ${_api.token}"); // DEBUG
        return true;
      }
      return false;
    } catch (e) {
      debugPrint("Error en login: $e");
      return false;
    } finally {
      setLoading(false);
    }
  }

  Future<bool> register({
    required String nombre,
    required String apellidos,
    required String correo,
    required String password,
    String? telefono,
    String? direccion,
  }) async {
    setLoading(true);
    try {
      final result = await _authService.register(
        nombre: nombre,
        apellidos: apellidos,
        correo: correo,
        password: password,
        telefono: telefono,
        direccion: direccion,
      );

      return result["success"] == true;
    } catch (e) {
      debugPrint("Error en register: $e");
      return false;
    } finally {
      setLoading(false);
    }
  }

  Future<void> logout() async {
    await _authService.logout();
    id = null;
    nombre = null;
    apellidos = null;
    correo = null;
    rol = null;
    isLogged = false;
    notifyListeners();
  }
}
