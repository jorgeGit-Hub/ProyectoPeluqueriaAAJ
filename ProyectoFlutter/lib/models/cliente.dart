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
    this.alergenos,
    this.observaciones,
  });

  factory Cliente.fromJson(Map<String, dynamic> json) {
    return Cliente(
      // Detecta el ID en ambos formatos posibles del backend
      idUsuario: json["idUsuario"] ?? json["id_usuario"] ?? 0,
      telefono: (json["telefono"] ?? "").toString(),
      direccion: (json["direccion"] ?? "").toString(),
      // Estos pueden ser nulos en tu base de datos, así que los dejamos opcionales
      alergenos: json["alergenos"]?.toString(),
      observaciones: json["observaciones"]?.toString(),
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
