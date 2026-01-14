class UserProvider with ChangeNotifier {
  int? id;
  String? nombre;
  String? apellidos; // Añadido
  String? correo; // Añadido
  String? rol;
  bool isLogged = false;
  bool loading = false;

  final AuthService _authService = AuthService();

  // Getter actualizado con todos los campos que pide tu AccountScreen
  Map<String, dynamic>? get usuario => id == null
      ? null
      : {
          "id": id,
          "nombre": nombre,
          "apellidos": apellidos,
          "correo": correo,
          "rol": rol
        };

  Future<void> loadUserFromToken() async {
    loading = true;
    notifyListeners();
    try {
      final token = await _authService.getSavedToken();
      if (token != null) {
        BaseApi.token = token;
        final userData = await _authService.validateAndGetUser();

        // Mapeamos todos los campos que vienen del backend
        id = int.tryParse(userData["id"].toString());
        nombre = userData["nombre"]?.toString();
        apellidos = userData["apellidos"]?.toString(); // Mapeado
        correo = userData["correo"]?.toString(); // Mapeado
        rol = userData["rol"]?.toString();
        isLogged = true;
      }
    } catch (e) {
      await logout();
    }
    loading = false;
    notifyListeners();
  }
  // ... resto del código igual
}
