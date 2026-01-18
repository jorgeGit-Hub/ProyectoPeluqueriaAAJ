import 'package:dio/dio.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:flutter/foundation.dart';

class ApiClient {
  static final ApiClient _instance = ApiClient._internal();
  factory ApiClient() => _instance;
  ApiClient._internal();

  static const String baseUrl = 'http://192.168.18.74:8090/api';

  late Dio _dio;
  String? _token;
  bool _tokenLoaded = false; // ✅ Flag para evitar cargas múltiples

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
        // ✅ CRÍTICO: Cargar token automáticamente si no está cargado
        if (!_tokenLoaded) {
          await loadToken();
        }

        if (_token != null && _token!.isNotEmpty) {
          options.headers['Authorization'] = 'Bearer $_token';
          debugPrint('✅ Token añadido: Bearer ${_token!.substring(0, 20)}...');
        } else {
          debugPrint('⚠️ No hay token disponible');
        }

        debugPrint('📤 REQUEST[${options.method}] => ${options.path}');
        return handler.next(options);
      },
      onResponse: (response, handler) {
        debugPrint('📥 RESPONSE[${response.statusCode}] => ${response.data}');
        return handler.next(response);
      },
      onError: (DioException e, handler) {
        debugPrint('❌ ERROR[${e.response?.statusCode}] => ${e.message}');
        return handler.next(e);
      },
    ));
  }

  Future<void> setToken(String? newToken) async {
    _token = newToken;
    _tokenLoaded = true; // ✅ Marcar como cargado
    if (newToken != null) {
      final prefs = await SharedPreferences.getInstance();
      await prefs.setString('token', newToken);
      debugPrint('💾 Token guardado');
    }
  }

  Future<void> loadToken() async {
    if (_tokenLoaded && _token != null) {
      return; // Ya está cargado
    }

    final prefs = await SharedPreferences.getInstance();
    _token = prefs.getString('token');
    _tokenLoaded = true;

    if (_token != null) {
      debugPrint('🔑 Token cargado desde almacenamiento');
    } else {
      debugPrint('⚠️ No hay token guardado');
    }
  }

  Future<void> clearToken() async {
    _token = null;
    _tokenLoaded = false;
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove('token');
    debugPrint('🗑️ Token eliminado');
  }

  // ✅ Los métodos ya NO necesitan llamar a loadToken manualmente
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
