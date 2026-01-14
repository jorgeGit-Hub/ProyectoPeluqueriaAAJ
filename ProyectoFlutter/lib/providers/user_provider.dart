import 'package:flutter/material.dart';
// Asegúrate de que esta ruta sea la correcta para tu archivo de servicio
import '../services/auth_service.dart';

class UserProvider with ChangeNotifier {
  // 1. Propiedades del estado
  int? id;
  String? nombre;
  String? apellidos;
  String? correo;
  String? rol;
  bool isLogged = false;
  bool loading = false;

  final AuthService _authService = AuthService();

  // 2. Getter para obtener los datos como un Map (usado en AccountScreen)
  Map<String, dynamic>? get usuario => id == null
      ? null
      : {
          "id": id,
          "nombre": nombre,
          "apellidos": apellidos,
          "correo": correo,
          "rol": rol,
        };

  // 3. Control de carga
  void setLoading(bool value) {
    loading = value;
    notifyListeners();
  }

  // 4. Método para cargar usuario desde el token (Requerido por Splash)
  Future<void> loadUserFromToken() async {
    setLoading(true);
    try {
      // Aquí iría tu lógica real de verificar token guardado
      await Future.delayed(const Duration(seconds: 2));
      // isLogged = true; (Si el token es válido)
    } catch (e) {
      isLogged = false;
    } finally {
      setLoading(false);
      notifyListeners();
    }
  }

  // 5. Método Login (Requerido por LoginScreen)
  Future<bool> login(String email, String password) async {
    setLoading(true);
    try {
      // Simulación de éxito (conecta con _authService.login aquí)
      await Future.delayed(const Duration(seconds: 1));

      // Datos de prueba para que veas algo en tu perfil
      id = 1;
      nombre = "Usuario";
      apellidos = "Prueba";
      correo = email;
      rol = "Cliente";
      isLogged = true;

      return true;
    } catch (e) {
      return false;
    } finally {
      setLoading(false);
      notifyListeners();
    }
  }

  // 6. Método Register (Requerido por RegisterScreen)
  Future<bool> register({
    required String nombre,
    required String apellidos,
    required String correo,
    required String password,
  }) async {
    setLoading(true);
    try {
      // Lógica de registro aquí
      await Future.delayed(const Duration(seconds: 1));
      return true;
    } catch (e) {
      return false;
    } finally {
      setLoading(false);
      notifyListeners();
    }
  }

  // 7. Método Logout (Requerido por AccountScreen)
  Future<void> logout() async {
    id = null;
    nombre = null;
    apellidos = null;
    correo = null;
    rol = null;
    isLogged = false;
    notifyListeners();
  }
}
