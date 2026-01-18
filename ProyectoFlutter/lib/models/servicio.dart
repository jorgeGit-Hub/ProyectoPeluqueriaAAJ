class Servicio {
  final int? idServicio;
  final String nombre;
  final String descripcion;
  final int duracion;
  final double precio;
  final String? imagen;
  final Grupo? grupo;

  Servicio({
    this.idServicio,
    required this.nombre,
    required this.descripcion,
    required this.duracion,
    required this.precio,
    this.imagen,
    this.grupo,
  });

  factory Servicio.fromJson(Map<String, dynamic> json) {
    try {
      // ✅ Log para debug
      print("🔍 Parseando servicio: ${json['nombre']}");

      return Servicio(
        idServicio: json['idServicio'] ?? json['id_servicio'],
        nombre: (json['nombre'] ?? "").toString(),
        descripcion: (json['descripcion'] ?? json['modulo'] ?? "").toString(),
        duracion: _parseDuracion(json),
        precio: _parsePrecio(json['precio']),
        imagen: json['imagen'],
        grupo: json['grupo'] != null ? Grupo.fromJson(json['grupo']) : null,
      );
    } catch (e) {
      print("❌ ERROR parseando servicio: $e");
      print("📄 JSON problemático: $json");
      rethrow; // ✅ Relanzar para ver el error en el provider
    }
  }

  static int _parseDuracion(Map<String, dynamic> json) {
    // Prioridad 1: campo 'duracion'
    if (json['duracion'] != null) {
      return int.tryParse(json['duracion'].toString()) ?? 0;
    }

    // Prioridad 2: campo 'tiempo_cliente'
    if (json['tiempo_cliente'] != null) {
      String tiempo = json['tiempo_cliente'].toString();

      // Formato: "45'" → 45 minutos
      if (tiempo.contains("'")) {
        return int.tryParse(tiempo.replaceAll("'", "")) ?? 0;
      }

      // Formato: "1h" o "1,5h" → convertir a minutos
      if (tiempo.contains("h")) {
        double horas = double.tryParse(
              tiempo.replaceAll("h", "").replaceAll(",", "."),
            ) ??
            0;
        return (horas * 60).toInt();
      }

      // Formato numérico directo
      return int.tryParse(tiempo) ?? 0;
    }

    // Por defecto: 30 minutos
    return 30;
  }

  // ✅ NUEVO: Método más robusto para parsear precio
  static double _parsePrecio(dynamic precio) {
    if (precio == null) return 0.0;

    if (precio is num) {
      return precio.toDouble();
    }

    if (precio is String) {
      return double.tryParse(precio.replaceAll(',', '.')) ?? 0.0;
    }

    return 0.0;
  }

  Map<String, dynamic> toJson() {
    return {
      "idServicio": idServicio,
      "nombre": nombre,
      "descripcion": descripcion,
      "duracion": duracion,
      "precio": precio,
      "imagen": imagen,
      if (grupo != null) "grupo": grupo!.toJson(),
    };
  }
}

// ✅ CLASE GRUPO
class Grupo {
  final int idGrupo;
  final String curso;
  final String email;
  final String turno;
  final int? cantAlumnos;

  Grupo({
    required this.idGrupo,
    required this.curso,
    required this.email,
    required this.turno,
    this.cantAlumnos,
  });

  factory Grupo.fromJson(Map<String, dynamic> json) {
    try {
      return Grupo(
        idGrupo: json["idGrupo"] ?? json["id_grupo"] ?? 0,
        curso: (json["curso"] ?? "").toString(),
        email: (json["email"] ?? "").toString(),
        turno: (json["turno"] ?? "").toString(),
        cantAlumnos: json["cantAlumnos"] ?? json["cant_alumnos"],
      );
    } catch (e) {
      print("❌ ERROR parseando grupo: $e");
      print("📄 JSON problemático: $json");
      rethrow;
    }
  }

  Map<String, dynamic> toJson() {
    return {
      "idGrupo": idGrupo,
      "curso": curso,
      "email": email,
      "turno": turno,
      if (cantAlumnos != null) "cantAlumnos": cantAlumnos,
    };
  }
}
