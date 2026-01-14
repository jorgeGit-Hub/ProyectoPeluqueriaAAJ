class Valoracion {
  final int idValoracion;
  final int puntuacion;
  final String comentario;
  final int idCita; // Enlace real en tu BD
  final String? fechaValoracion; // Campo existente en tu BD

  Valoracion({
    required this.idValoracion,
    required this.puntuacion,
    required this.comentario,
    required this.idCita,
    this.fechaValoracion,
  });

  factory Valoracion.fromJson(Map<String, dynamic> json) {
    return Valoracion(
      // Soporta idValoracion (Java) e id_valoracion (MySQL)
      idValoracion: json["idValoracion"] ?? json["id_valoracion"] ?? 0,
      puntuacion: json["puntuacion"] ?? 0,
      comentario: (json["comentario"] ?? "").toString(),
      // Maneja si viene el objeto Cita completo o solo el ID
      idCita: _parseCitaId(json["cita"]) ?? json["id_cita"] ?? 0,
      fechaValoracion: json["fechaValoracion"] ?? json["fecha_valoracion"],
    );
  }

  // Función de seguridad para procesar el ID de la cita
  static int? _parseCitaId(dynamic data) {
    if (data == null) return null;
    if (data is int) return data;
    return data["idCita"] ?? data["id_cita"];
  }

  Map<String, dynamic> toJson() {
    return {
      "idValoracion": idValoracion,
      "puntuacion": puntuacion,
      "comentario": comentario,
      "cita": {"idCita": idCita}, // Formato correcto para Spring Boot
      "fechaValoracion": fechaValoracion,
    };
  }
}
