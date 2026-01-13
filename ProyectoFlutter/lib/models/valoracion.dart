import 'usuario.dart';
import 'servicio.dart';

class Valoracion {
  final int idValoracion;
  final int puntuacion;
  final String comentario;
  final Usuario usuario;
  final Servicio servicio;

  Valoracion({
    required this.idValoracion,
    required this.puntuacion,
    required this.comentario,
    required this.usuario,
    required this.servicio,
  });

  factory Valoracion.fromJson(Map<String, dynamic> json) {
    return Valoracion(
      idValoracion: json["idValoracion"] ?? json["id"] ?? 0,
      puntuacion: json["puntuacion"],
      comentario: json["comentario"] ?? "",
      usuario: Usuario.fromJson(json["usuario"]),
      servicio: Servicio.fromJson(json["servicio"]),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      "idValoracion": idValoracion,
      "puntuacion": puntuacion,
      "comentario": comentario,
      "usuario": usuario.toJson(),
      "servicio": servicio.toJson(),
    };
  }
}
