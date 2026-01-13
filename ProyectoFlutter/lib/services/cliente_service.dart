import 'package:peluqueria_aja/services/base_api.dart';

class ClienteService {
  Future<Map<String, dynamic>> getCliente(int idUsuario) async {
    return await BaseApi.get("/clientes/$idUsuario");
  }

  Future<Map<String, dynamic>> createCliente(Map<String, dynamic> data) async {
    return await BaseApi.post("/clientes", data);
  }

  Future<Map<String, dynamic>> updateCliente(
      int id, Map<String, dynamic> data) async {
    return await BaseApi.put("/clientes/$id", data);
  }
}
