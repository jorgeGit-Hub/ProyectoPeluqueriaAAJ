import 'package:peluqueria_aja/services/base_api.dart';

class ValoracionService {
  Future<List<dynamic>> getValoraciones() async {
    return await BaseApi.get("/valoraciones");
  }

  Future<Map<String, dynamic>> getValoracion(int id) async {
    return await BaseApi.get("/valoraciones/$id");
  }

  Future<Map<String, dynamic>> createValoracion(
      Map<String, dynamic> data) async {
    return await BaseApi.post("/valoraciones", data);
  }

  Future<Map<String, dynamic>> updateValoracion(
      int id, Map<String, dynamic> data) async {
    return await BaseApi.put("/valoraciones/$id", data);
  }

  Future<void> deleteValoracion(int id) async {
    await BaseApi.delete("/valoraciones/$id");
  }
}
