class Servicio {
  final int? idServicio;
  final String nombre;
  final String descripcion;
  final int duracion; // En minutos
  final double precio; // En Java es BigDecimal, aquí double
  final String? imagen; // El Base64

  Servicio({
    this.idServicio,
    required this.nombre,
    required this.descripcion,
    required this.duracion,
    required this.precio,
    this.imagen,
  });

  Map<String, dynamic> toJson() {
    return {
      // "idServicio": idServicio, // Spring lo autogenera
      "nombre": nombre,
      "descripcion": descripcion,
      "duracion": duracion,
      "precio": precio,
      "imagen": imagen, // Enviamos la foto aquí
    };
  }

  factory Servicio.fromJson(Map<String, dynamic> json) {
    return Servicio(
      idServicio: json['idServicio'],
      nombre: json['nombre'],
      descripcion: json['descripcion'],
      duracion: json['duracion'],
      precio: (json['precio'] as num).toDouble(), // Conversión segura
      imagen: json['imagen'],
    );
  }
}
