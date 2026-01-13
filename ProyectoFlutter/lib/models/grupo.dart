class Grupo {
  final int idGrupo;
  final String nombre;
  final String descripcion;
  final String turno;

  Grupo({
    required this.idGrupo,
    required this.nombre,
    required this.descripcion,
    required this.turno,
  });

  factory Grupo.fromJson(Map<String, dynamic> json) {
    return Grupo(
      idGrupo: json["idGrupo"] ?? 0,
      nombre: json["nombre"] ?? "",
      descripcion: json["descripcion"] ?? "",
      turno: json["turno"]?.toString() ?? "",
    );
  }

  Map<String, dynamic> toJson() {
    return {
      "idGrupo": idGrupo,
      "nombre": nombre,
      "descripcion": descripcion,
      "turno": turno,
    };
  }
}
