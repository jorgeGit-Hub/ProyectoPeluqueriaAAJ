/// Modelo Cita actualizado para la nueva estructura del backend Spring Boot.
/// La entidad Cita ya NO tiene horaInicio/horaFin/idServicio directamente.
/// Esos datos ahora viven en HorarioSemanal, que se incluye en la respuesta.
class Cita {
  final int? idCita;
  final String fecha;
  final String estado;
  final int idCliente;

  // Datos del horario semanal (anidados en la respuesta del backend)
  final int? idHorario;
  final String horaInicio; // extraído de horarioSemanal
  final String horaFin; // extraído de horarioSemanal
  final int? idServicio; // extraído de horarioSemanal.servicio
  final String? nombreServicio; // extraído de horarioSemanal.servicio
  final String? diaSemana; // extraído de horarioSemanal

  Cita({
    this.idCita,
    required this.fecha,
    required this.estado,
    required this.idCliente,
    this.idHorario,
    this.horaInicio = "00:00",
    this.horaFin = "00:00",
    this.idServicio,
    this.nombreServicio,
    this.diaSemana,
  });

  factory Cita.fromJson(Map<String, dynamic> json) {
    final int idCliente = _parseId(json["cliente"], ["idUsuario", "id_usuario"]);

    final horario = json["horarioSemanal"] ?? json["horario_semanal"];
    String horaInicio = "00:00";
    String horaFin = "00:00";
    int? idHorario;
    int? idServicio;
    String? nombreServicio;
    String? diaSemana;

    if (horario != null && horario is Map<String, dynamic>) {
      idHorario = horario["idHorario"] ?? horario["id_horario"];
      horaInicio = (horario["horaInicio"] ?? horario["hora_inicio"] ?? "00:00").toString();
      horaFin = (horario["horaFin"] ?? horario["hora_fin"] ?? "00:00").toString();
      diaSemana = horario["diaSemana"]?.toString();

      final servicio = horario["servicio"];
      if (servicio != null && servicio is Map<String, dynamic>) {
        idServicio = servicio["idServicio"] ?? servicio["id_servicio"];
        nombreServicio = servicio["nombre"]?.toString();
      }
    }

    return Cita(
      idCita: json["idCita"] ?? json["id_cita"],
      fecha: (json["fecha"] ?? "").toString(),
      estado: (json["estado"] ?? "pendiente").toString().toLowerCase(),
      idCliente: idCliente,
      idHorario: idHorario,
      horaInicio: horaInicio,
      horaFin: horaFin,
      idServicio: idServicio,
      nombreServicio: nombreServicio,
      diaSemana: diaSemana,
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

  /// JSON para CREAR — backend espera { fecha, horarioSemanal: { idHorario }, cliente: { idUsuario }, estado }
  Map<String, dynamic> toCreateJson() {
    return {
      "fecha": fecha,
      "estado": estado.toLowerCase(),
      "cliente": {"idUsuario": idCliente},
      if (idHorario != null) "horarioSemanal": {"idHorario": idHorario},
    };
  }

  /// JSON para ACTUALIZAR (p.ej. cancelar)
  Map<String, dynamic> toUpdateJson() {
    return {
      "fecha": fecha,
      "estado": estado.toLowerCase(),
      "cliente": {"idUsuario": idCliente},
      if (idHorario != null) "horarioSemanal": {"idHorario": idHorario},
    };
  }
}
