class Cliente {
  final int idUsuario;
  final String telefono;
  final String direccion;
  final String? alergenos;
  final String? observaciones;

  Cliente({
    required this.idUsuario,
    required this.telefono,
    required this.direccion,
    required this.alergenos,
    required this.observaciones,
  });

  factory Cliente.fromJson(Map<String, dynamic> json) {
    return Cliente(
      idUsuario: json["idUsuario"],
      telefono: json["telefono"] ?? "",
      direccion: json["direccion"] ?? "",
      alergenos: json["alergenos"],
      observaciones: json["observaciones"],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      "idUsuario": idUsuario,
      "telefono": telefono,
      "direccion": direccion,
      "alergenos": alergenos,
      "observaciones": observaciones,
    };
  }
}
