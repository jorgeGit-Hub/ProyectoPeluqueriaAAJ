class Grupo {
  final int idGrupo;
  final String curso;
  final String email;
  final String turno;

  Grupo({
    required this.idGrupo,
    required this.curso,
    required this.email,
    required this.turno,
  });

  factory Grupo.fromJson(Map<String, dynamic> json) {
    return Grupo(
      idGrupo: json["idGrupo"] ?? json["id_grupo"] ?? 0,
      curso: (json["curso"] ?? "").toString(),
      email: (json["email"] ?? "").toString(),
      turno: (json["turno"] ?? "").toString(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      "idGrupo": idGrupo,
      "curso": curso,
      "email": email,
      "turno": turno,
    };
  }
}
