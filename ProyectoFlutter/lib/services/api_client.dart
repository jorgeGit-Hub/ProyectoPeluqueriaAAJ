import 'package:dio/dio.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutter/foundation.dart';

class ApiClient {
  static final ApiClient _instance = ApiClient._internal();
  factory ApiClient() => _instance;
  ApiClient._internal();

  static const String baseUrl = 'http://192.168.18.74:8090/api';

  late Dio _dio;
  String? _token;
  bool _tokenLoaded = false;

  // ✅ CAMBIO: Usar Flutter Secure Storage
  final FlutterSecureStorage _secureStorage = const FlutterSecureStorage(
    aOptions: AndroidOptions(encryptedSharedPreferences: true),
  );

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
        if (!_tokenLoaded) {
          await loadToken();
        }

        if (_token != null && _token!.isNotEmpty) {
          options.headers['Authorization'] = 'Bearer $_token';
          debugPrint('✅ Token añadido al request');
        }

        debugPrint('📤 REQUEST[${options.method}] => ${options.path}');
        return handler.next(options);
      },
      onResponse: (response, handler) {
        debugPrint('📥 RESPONSE[${response.statusCode}] => OK');
        return handler.next(response);
      },
      onError: (DioException e, handler) {
        debugPrint('❌ ERROR[${e.response?.statusCode}] => ${e.message}');

        // ✅ IMPORTANTE: Si el token está caducado (401), limpiar storage
        if (e.response?.statusCode == 401) {
          clearToken();
        }

        return handler.next(e);
      },
    ));
  }

  // ✅ CAMBIO: Guardar token en Secure Storage
  Future<void> setToken(String? newToken) async {
    _token = newToken;
    _tokenLoaded = true;

    if (newToken != null) {
      await _secureStorage.write(key: 'auth_token', value: newToken);
      debugPrint('🔒 Token guardado de forma segura');
    }
  }

  // ✅ CAMBIO: Cargar token desde Secure Storage
  Future<void> loadToken() async {
    if (_tokenLoaded && _token != null) {
      return;
    }

    _token = await _secureStorage.read(key: 'auth_token');
    _tokenLoaded = true;

    if (_token != null) {
      debugPrint('🔓 Token cargado desde almacenamiento seguro');
    } else {
      debugPrint('⚠️ No hay token guardado');
    }
  }

  // ✅ CAMBIO: Eliminar token de Secure Storage
  Future<void> clearToken() async {
    _token = null;
    _tokenLoaded = false;
    await _secureStorage.delete(key: 'auth_token');
    debugPrint('🗑️ Token eliminado del almacenamiento seguro');
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
