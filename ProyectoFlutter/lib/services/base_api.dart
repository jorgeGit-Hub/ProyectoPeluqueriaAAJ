import 'dart:convert';
import 'package:http/http.dart' as http;

/// Clase base para gestionar la comunicación con la API.
class BaseApi {
  /// URL base del backend
  /// 👉 configurable por entorno (no hardcodear IP)
  static const String baseUrl = String.fromEnvironment(
    'BASE_URL',
    defaultValue: 'http://172.20.10.9:8090/api',
  );

  /// Token JWT en memoria
  static String? token;

  /// Headers comunes
  static Map<String, String> _headers({bool auth = true}) {
    final headers = <String, String>{
      "Content-Type": "application/json",
    };

    if (auth && token != null && token!.isNotEmpty) {
      headers["Authorization"] = "Bearer $token";
    }

    return headers;
  }

  /// Procesa respuestas HTTP
  static dynamic _processResponse(http.Response res) {
    if (res.statusCode >= 200 && res.statusCode < 300) {
      if (res.body.isEmpty) return null;
      return jsonDecode(res.body);
    }

    throw Exception(
      "API error ${res.statusCode}: ${res.body}",
    );
  }

  /// GET
  static Future<dynamic> get(String endpoint, {bool auth = true}) async {
    final res = await http
        .get(
          Uri.parse("$baseUrl$endpoint"),
          headers: _headers(auth: auth),
        )
        .timeout(const Duration(seconds: 10));

    return _processResponse(res);
  }

  /// POST
  static Future<dynamic> post(
    String endpoint,
    Map<String, dynamic> data, {
    bool auth = true,
  }) async {
    final res = await http
        .post(
          Uri.parse("$baseUrl$endpoint"),
          headers: _headers(auth: auth),
          body: jsonEncode(data),
        )
        .timeout(const Duration(seconds: 10));

    return _processResponse(res);
  }

  /// PUT
  static Future<dynamic> put(
    String endpoint,
    Map<String, dynamic> data, {
    bool auth = true,
  }) async {
    final res = await http
        .put(
          Uri.parse("$baseUrl$endpoint"),
          headers: _headers(auth: auth),
          body: jsonEncode(data),
        )
        .timeout(const Duration(seconds: 10));

    return _processResponse(res);
  }

  /// DELETE
  static Future<void> delete(String endpoint, {bool auth = true}) async {
    final res = await http
        .delete(
          Uri.parse("$baseUrl$endpoint"),
          headers: _headers(auth: auth),
        )
        .timeout(const Duration(seconds: 10));

    if (res.statusCode < 200 || res.statusCode >= 300) {
      throw Exception("DELETE error ${res.statusCode}: ${res.body}");
    }
  }
}
