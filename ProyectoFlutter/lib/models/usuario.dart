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
      idUsuario: json["id"] ??
          json["idUsuario"] ??
          json["id_usuario"], // soporta todas las variantes
      nombre: json["nombre"] ?? "",
      apellidos: json["apellidos"] ?? "",
      correo: json["correo"] ?? "",
      rol: json["rol"] ?? "",
    );
  }

  Map<String, dynamic> toJson() {
    return {
      "id": idUsuario,
      "nombre": nombre,
      "apellidos": apellidos,
      "correo": correo,
      "rol": rol,
    };
  }
}
