class Cita {
  final int? idCita;
  final String fecha;
  final String horaInicio;
  final String horaFin;
  final String estado;
  final int idCliente;
  final int idGrupo;
  final int idServicio;

  Cita({
    this.idCita,
    required this.fecha,
    required this.horaInicio,
    required this.horaFin,
    required this.estado,
    required this.idCliente,
    required this.idGrupo,
    required this.idServicio,
  });

  factory Cita.fromJson(Map<String, dynamic> json) {
    return Cita(
      idCita: json["idCita"] ?? json["id_cita"],
      fecha: (json["fecha"] ?? "").toString(),
      horaInicio:
          (json["horaInicio"] ?? json["hora_inicio"] ?? "00:00").toString(),
      horaFin: (json["horaFin"] ?? json["hora_fin"] ?? "00:00").toString(),
      estado: (json["estado"] ?? "pendiente").toString(),
      idCliente: _parseId(json["cliente"]),
      idGrupo: _parseId(json["grupo"]),
      idServicio: _parseId(json["servicio"]),
    );
  }

  static int _parseId(dynamic data) {
    if (data == null) return 0;
    if (data is int) return data;
    return data["idUsuario"] ??
        data["id_usuario"] ??
        data["idServicio"] ??
        data["id_servicio"] ??
        data["idGrupo"] ??
        0;
  }

  Map<String, dynamic> toCreateJson() {
    return {
      "fecha": fecha,
      "horaInicio": horaInicio,
      "horaFin": horaFin,
      "estado": estado,
      "cliente": {"idUsuario": idCliente},
      "grupo": {"idGrupo": idGrupo},
      "servicio": {"idServicio": idServicio},
    };
  }
}
