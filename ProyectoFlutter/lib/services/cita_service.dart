import 'base_api.dart';

class CitaService {
  final String _base = "/citas";

  /// Obtener TODAS las citas (requiere token)
  Future<List<dynamic>> getCitas() async {
    final res = await BaseApi.get(_base);
    if (res is List) return res;
    return [];
  }

  /// Obtener citas del usuario filtrando por cliente.idUsuario
  Future<List<dynamic>> getCitasByUsuario(int idUsuario) async {
    final all = await getCitas();

    return all.where((c) {
      if (c is! Map<String, dynamic>) return false;

      final cliente = c["cliente"];
      if (cliente is! Map<String, dynamic>) return false;

      final id = cliente["idUsuario"] ?? cliente["id_usuario"];
      return id == idUsuario;
    }).toList();
  }

  /// Crear cita
  Future<void> createCita(Map<String, dynamic> data) async {
    await BaseApi.post(_base, data);
  }
}
