class Cita {
  final int? idCita;
  final String fecha; // "YYYY-MM-DD"
  final String horaInicio; // "HH:mm:ss"
  final String horaFin; // "HH:mm:ss"
  final String estado; // pendiente | realizada | cancelada

  final int idCliente; // Cliente.idUsuario
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
      horaInicio: (json["horaInicio"] ?? json["hora_inicio"] ?? "").toString(),
      horaFin: (json["horaFin"] ?? json["hora_fin"] ?? "").toString(),
      estado: (json["estado"] ?? "pendiente").toString(),

      // vienen como objetos (ManyToOne)
      idCliente:
          json["cliente"]?["idUsuario"] ?? json["cliente"]?["id_usuario"],
      idGrupo: json["grupo"]?["idGrupo"] ?? json["grupo"]?["id_grupo"],
      idServicio:
          json["servicio"]?["idServicio"] ?? json["servicio"]?["id_servicio"],
    );
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
