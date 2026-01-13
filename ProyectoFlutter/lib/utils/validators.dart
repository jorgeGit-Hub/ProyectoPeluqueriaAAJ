class Validators {
  static String? email(String? email) {
    if (email == null || email.isEmpty) return "Campo obligatorio";
    final regex = RegExp(r"^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$");
    if (!regex.hasMatch(email)) return "Correo inválido";
    return null;
  }

  static String? password(String? pass) {
    if (pass == null || pass.isEmpty) return "Campo obligatorio";
    if (pass.length < 4) return "Mínimo 4 caracteres";
    return null;
  }
}
