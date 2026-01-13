import 'package:peluqueria_aja/services/base_api.dart';

class ServicioService {
  Future<List<dynamic>> getServicios() async {
    return await BaseApi.get("/servicios");
  }

  Future<Map<String, dynamic>> getServicio(int id) async {
    return await BaseApi.get("/servicios/$id");
  }

  Future<Map<String, dynamic>> createServicio(Map<String, dynamic> data) async {
    return await BaseApi.post("/servicios", data);
  }

  Future<Map<String, dynamic>> updateServicio(
      int id, Map<String, dynamic> data) async {
    return await BaseApi.put("/servicios/$id", data);
  }

  Future<void> deleteServicio(int id) async {
    await BaseApi.delete("/servicios/$id");
  }
}
