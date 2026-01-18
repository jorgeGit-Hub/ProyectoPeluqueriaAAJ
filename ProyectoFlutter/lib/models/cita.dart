class Cita {
  final int? idCita;
  final String fecha;
  final String horaInicio;
  final String horaFin;
  final String estado;
  final int idCliente;
  final int idServicio;

  Cita({
    this.idCita,
    required this.fecha,
    required this.horaInicio,
    required this.horaFin,
    required this.estado,
    required this.idCliente,
    required this.idServicio,
  });

  factory Cita.fromJson(Map<String, dynamic> json) {
    return Cita(
      idCita: json["idCita"] ?? json["id_cita"],
      fecha: (json["fecha"] ?? "").toString(),
      horaInicio:
          (json["horaInicio"] ?? json["hora_inicio"] ?? "00:00").toString(),
      horaFin: (json["horaFin"] ?? json["hora_fin"] ?? "00:00").toString(),
      // ✅ Normalizar estado a minúsculas al recibir
      estado: (json["estado"] ?? "pendiente").toString().toLowerCase(),
      idCliente: _parseId(json["cliente"], ["idUsuario", "id_usuario"]),
      idServicio: _parseId(json["servicio"], ["idServicio", "id_servicio"]),
    );
  }

  static int _parseId(dynamic data, List<String> possibleKeys) {
    if (data == null) return 0;
    if (data is int) return data;
    if (data is Map<String, dynamic>) {
      for (String key in possibleKeys) {
        if (data.containsKey(key) && data[key] != null) {
          return data[key] is int
              ? data[key]
              : int.tryParse(data[key].toString()) ?? 0;
        }
      }
    }
    return 0;
  }

  Map<String, dynamic> toCreateJson() {
    return {
      "fecha": fecha,
      "horaInicio": horaInicio,
      "horaFin": horaFin,
      // ✅ Asegurar que el estado se envíe en minúsculas
      "estado": estado.toLowerCase(),
      "cliente": {"idUsuario": idCliente},
      "servicio": {"idServicio": idServicio},
    };
  }

  // ✅ Método adicional para actualización
  Map<String, dynamic> toUpdateJson() {
    return {
      "fecha": fecha,
      "horaInicio": horaInicio,
      "horaFin": horaFin,
      "estado": estado.toLowerCase(), // ✅ Minúsculas
      "cliente": {"idUsuario": idCliente},
      "servicio": {"idServicio": idServicio},
    };
  }
}
