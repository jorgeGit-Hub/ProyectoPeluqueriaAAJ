class Validators {
  /// Validador genérico para campos que no pueden estar vacíos
  static String? required(String? value,
      {String message = "Este campo es obligatorio"}) {
    if (value == null || value.trim().isEmpty) return message;
    return null;
  }

  /// Validador de correo electrónico con Regex robusto
  static String? email(String? email) {
    if (email == null || email.isEmpty) return "El correo es obligatorio";

    // Regex estándar para validación de emails
    final regex = RegExp(r"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

    if (!regex.hasMatch(email.trim())) {
      return "Introduce un formato de correo válido";
    }
    return null;
  }

  /// Validador de contraseña (ajustado a 6 caracteres según tu RegisterScreen)
  static String? password(String? pass) {
    if (pass == null || pass.isEmpty) return "La contraseña es obligatoria";
    if (pass.length < 6)
      return "La contraseña debe tener al menos 6 caracteres";
    return null;
  }

  /// Validador de teléfono (específico para España/estándar 9 dígitos)
  static String? phone(String? value) {
    if (value == null || value.isEmpty) return "El teléfono es obligatorio";
    if (value.length < 9) return "Introduce un teléfono válido (9 dígitos)";
    return null;
  }
}
