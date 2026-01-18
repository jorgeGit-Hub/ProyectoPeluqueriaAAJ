class Valoracion {
  final int? idValoracion;
  final int puntuacion;
  final String comentario;
  final int idCita;
  final String? fechaValoracion;

  Valoracion({
    this.idValoracion,
    required this.puntuacion,
    required this.comentario,
    required this.idCita,
    this.fechaValoracion,
  });

  factory Valoracion.fromJson(Map<String, dynamic> json) {
    return Valoracion(
      idValoracion: json["idValoracion"] ?? json["id_valoracion"],
      puntuacion: json["puntuacion"] ?? 0,
      comentario: (json["comentario"] ?? "").toString(),
      idCita: _parseCitaId(json["cita"]) ?? json["id_cita"] ?? 0,
      fechaValoracion: json["fechaValoracion"] ?? json["fecha_valoracion"],
    );
  }

  static int? _parseCitaId(dynamic data) {
    if (data == null) return null;
    if (data is int) return data;
    return data["idCita"] ?? data["id_cita"];
  }

  Map<String, dynamic> toJson() {
    return {
      if (idValoracion != null) "idValoracion": idValoracion,
      "puntuacion": puntuacion,
      "comentario": comentario,
      "cita": {"idCita": idCita},
      if (fechaValoracion != null) "fechaValoracion": fechaValoracion,
    };
  }
}
