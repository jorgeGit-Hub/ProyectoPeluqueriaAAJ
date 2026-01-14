import 'base_api.dart';

class CitaService {
  final String _base = "/citas";

  // ======================================================
  // Obtener TODAS las citas del servidor
  // ======================================================
  Future<List<dynamic>> getCitas() async {
    try {
      final res = await BaseApi.get(_base);
      if (res is List) return res;
      return [];
    } catch (e) {
      return [];
    }
  }

  // ======================================================
  // Obtener citas de un usuario específico
  // Sincronizado con CitaProvider.loadCitasByCliente
  // ======================================================
  Future<List<dynamic>> getCitasByCliente(int idUsuario) async {
    final all = await getCitas();

    return all.where((c) {
      if (c is! Map<String, dynamic>) return false;

      final cliente = c["cliente"];

      // Caso 1: El backend manda el objeto cliente completo
      if (cliente is Map<String, dynamic>) {
        final id = cliente["idUsuario"] ?? cliente["id_usuario"];
        return id == idUsuario;
      }

      // Caso 2: El backend manda solo el ID (por @JsonIdentityInfo)
      if (cliente is int) {
        return cliente == idUsuario;
      }

      return false;
    }).toList();
  }

  // ======================================================
  // Crear una nueva cita en el servidor
  // ======================================================
  Future<bool> createCita(Map<String, dynamic> data) async {
    try {
      await BaseApi.post(_base, data);
      return true;
    } catch (e) {
      // Re-lanzamos el error para que la pantalla de reserva
      // pueda mostrar el SnackBar de error.
      rethrow;
    }
  }
}
