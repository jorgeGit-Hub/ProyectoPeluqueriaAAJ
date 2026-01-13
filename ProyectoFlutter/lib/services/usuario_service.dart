import 'package:peluqueria_aja/services/base_api.dart';

class UsuarioService {
  Future<List<dynamic>> getUsuarios() async {
    return await BaseApi.get("/usuarios");
  }

  Future<Map<String, dynamic>> getUsuario(int id) async {
    return await BaseApi.get("/usuarios/$id");
  }

  Future<Map<String, dynamic>> createUsuario(Map<String, dynamic> data) async {
    return await BaseApi.post("/usuarios", data);
  }

  Future<Map<String, dynamic>> updateUsuario(
      int id, Map<String, dynamic> data) async {
    return await BaseApi.put("/usuarios/$id", data);
  }

  Future<void> deleteUsuario(int id) async {
    await BaseApi.delete("/usuarios/$id");
  }
}
