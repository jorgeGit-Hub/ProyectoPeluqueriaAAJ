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
        final result = await _authService.validateToken();

        if (result["success"] == true) {
          id = result["id"];
          nombre = result["nombre"];
          apellidos = result["apellidos"];
          correo = result["correo"];
          rol = result["rol"];
          isLogged = true;
        } else {
          await _api.clearToken();
          isLogged = false;
        }
      } else {
        isLogged = false;
      }
    } catch (e) {
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
      if (result["success"] == true) {
        id = result["id"];
        nombre = result["nombre"];
        apellidos = result["apellidos"];
        correo = result["correo"];
        rol = result["rol"];
        isLogged = true;
        return true;
      }
      return false;
    } catch (e) {
      return false;
    } finally {
      setLoading(false);
    }
  }

  // ✅ PROVEEDOR PARA LOGIN SOCIAL CORREGIDO
  Future<bool> socialLogin(
      String emailGoogle, String nombreGoogle, String? fotoUrl) async {
    setLoading(true);
    try {
      final result =
          await _authService.socialLogin(emailGoogle, nombreGoogle, fotoUrl);

      if (result["success"] == true) {
        id = result["id"];
        nombre = result[
            "nombre"]; // ✅ Ahora se guarda en la variable global correctamente
        apellidos = result["apellidos"];
        correo = result[
            "correo"]; // ✅ Ahora se guarda en la variable global correctamente
        rol = result["rol"];
        isLogged = true;
        return true;
      }
      return false;
    } catch (e) {
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
    required String telefono,
    required String direccion,
    required String alergenos,
    String? observaciones,
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
        alergenos: alergenos,
        observaciones: observaciones,
      );
      return result["success"] == true;
    } catch (e) {
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
