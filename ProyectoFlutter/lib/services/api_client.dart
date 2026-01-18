import 'package:dio/dio.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:flutter/foundation.dart';

class ApiClient {
  static final ApiClient _instance = ApiClient._internal();
  factory ApiClient() => _instance;
  ApiClient._internal();

  // ⚠️ CAMBIA ESTA IP POR LA TUYA
  static const String baseUrl = 'http://192.168.18.74:8090/api';

  late Dio _dio;
  String? _token;

  Dio get dio => _dio;
  String? get token => _token;

  void initialize() {
    _dio = Dio(BaseOptions(
      baseUrl: baseUrl,
      connectTimeout: const Duration(seconds: 30),
      receiveTimeout: const Duration(seconds: 30),
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
      },
    ));

    _dio.interceptors.add(InterceptorsWrapper(
      onRequest: (options, handler) async {
        if (_token != null && _token!.isNotEmpty) {
          options.headers['Authorization'] = 'Bearer $_token';
        }
        debugPrint('REQUEST[${options.method}] => PATH: ${options.path}');
        return handler.next(options);
      },
      onResponse: (response, handler) {
        debugPrint(
            'RESPONSE[${response.statusCode}] => DATA: ${response.data}');
        return handler.next(response);
      },
      onError: (DioException e, handler) {
        debugPrint('ERROR[${e.response?.statusCode}] => MESSAGE: ${e.message}');
        return handler.next(e);
      },
    ));
  }

  Future<void> setToken(String? newToken) async {
    _token = newToken;
    if (newToken != null) {
      final prefs = await SharedPreferences.getInstance();
      await prefs.setString('token', newToken);
    }
  }

  Future<void> loadToken() async {
    final prefs = await SharedPreferences.getInstance();
    _token = prefs.getString('token');
  }

  Future<void> clearToken() async {
    _token = null;
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove('token');
  }

  Future<Response> get(String path,
      {Map<String, dynamic>? queryParameters}) async {
    try {
      return await _dio.get(path, queryParameters: queryParameters);
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<Response> post(String path, {dynamic data}) async {
    try {
      return await _dio.post(path, data: data);
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<Response> put(String path, {dynamic data}) async {
    try {
      return await _dio.put(path, data: data);
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  Future<Response> delete(String path) async {
    try {
      return await _dio.delete(path);
    } on DioException catch (e) {
      throw _handleError(e);
    }
  }

  String _handleError(DioException e) {
    if (e.response != null) {
      final statusCode = e.response!.statusCode;
      final data = e.response!.data;

      if (statusCode == 401) {
        return 'Sesión expirada. Por favor, inicia sesión nuevamente.';
      } else if (statusCode == 403) {
        return 'No tienes permisos para realizar esta acción.';
      } else if (statusCode == 404) {
        return 'Recurso no encontrado.';
      } else if (statusCode! >= 500) {
        return 'Error del servidor. Intenta más tarde.';
      }

      if (data is Map && data.containsKey('error')) {
        return data['error'].toString();
      }
      if (data is Map && data.containsKey('message')) {
        return data['message'].toString();
      }
    }

    if (e.type == DioExceptionType.connectionTimeout ||
        e.type == DioExceptionType.receiveTimeout) {
      return 'Tiempo de espera agotado. Verifica tu conexión.';
    }

    if (e.type == DioExceptionType.connectionError) {
      return 'Error de conexión. Verifica que el servidor esté activo.';
    }

    return 'Error inesperado: ${e.message}';
  }
}
