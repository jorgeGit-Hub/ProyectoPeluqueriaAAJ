class Servicio {
  final int? idServicio;
  final String nombre;
  final String descripcion;
  final int duracion;
  final double precio;
  final String? imagen;

  Servicio({
    this.idServicio,
    required this.nombre,
    required this.descripcion,
    required this.duracion,
    required this.precio,
    this.imagen,
  });

  factory Servicio.fromJson(Map<String, dynamic> json) {
    return Servicio(
      idServicio: json['idServicio'] ?? json['id_servicio'],
      nombre: json['nombre'] ?? "",
      descripcion: (json['descripcion'] ?? "").toString(),
      duracion: int.tryParse(json['duracion']?.toString() ?? "0") ?? 0,
      precio: (json['precio'] as num?)?.toDouble() ?? 0.0,
      imagen: json['imagen'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      "idServicio": idServicio,
      "nombre": nombre,
      "descripcion": descripcion,
      "duracion": duracion,
      "precio": precio,
      "imagen": imagen,
    };
  }
}
