class Usuario {
  final int idUsuario;
  final String nombre;
  final String apellidos;
  final String correo;
  final String rol;

  Usuario({
    required this.idUsuario,
    required this.nombre,
    required this.apellidos,
    required this.correo,
    required this.rol,
  });

  factory Usuario.fromJson(Map<String, dynamic> json) {
    return Usuario(
      idUsuario: json["idUsuario"] ?? json["id"] ?? json["id_usuario"] ?? 0,
      nombre: (json["nombre"] ?? "").toString(),
      apellidos: (json["apellidos"] ?? "").toString(),
      correo: (json["correo"] ?? "").toString(),
      rol: (json["rol"] ?? "cliente").toString(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      "idUsuario": idUsuario,
      "nombre": nombre,
      "apellidos": apellidos,
      "correo": correo,
      "rol": rol,
    };
  }
}
