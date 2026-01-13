import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:peluqueria_aja/models/cliente.dart';
import 'package:peluqueria_aja/providers/cliente_provider.dart';
import '../../utils/theme.dart';

class EditAccountScreen extends StatefulWidget {
  const EditAccountScreen({super.key});

  @override
  State<EditAccountScreen> createState() => _EditAccountScreenState();
}

class _EditAccountScreenState extends State<EditAccountScreen> {
  // Clave global única para identificar este formulario y realizar validaciones.
  final _formKey = GlobalKey<FormState>();

  // Controladores para manejar el texto de cada input.
  late TextEditingController _telefonoCtrl;
  late TextEditingController _direccionCtrl;
  late TextEditingController _alergenosCtrl;
  late TextEditingController _observacionesCtrl;

  // Variable de estado para mostrar el spinner de carga en el botón.
  bool _loading = false;
  // Objeto para guardar los datos del cliente que vienen de la pantalla anterior.
  Cliente? _clienteOriginal;

  // Se usa didChangeDependencies en lugar de initState porque necesitamos acceso seguro
  // al 'context' para recuperar los argumentos de la ruta (ModalRoute).
  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    // Carga segura de argumentos una sola vez (si _clienteOriginal es null).
    if (_clienteOriginal == null) {
      final args = ModalRoute.of(context)?.settings.arguments;
      // Verificamos que los argumentos sean del tipo esperado (Modelo Cliente).
      if (args is Cliente) {
        _clienteOriginal = args;
        _initControllers(args);
      }
    }
  }

  // Inicializa los controladores con los textos actuales del cliente.
  void _initControllers(Cliente cliente) {
    _telefonoCtrl = TextEditingController(text: cliente.telefono);
    _direccionCtrl = TextEditingController(text: cliente.direccion);
    // Si es null, ponemos cadena vacía para evitar errores.
    _alergenosCtrl = TextEditingController(text: cliente.alergenos ?? "");
    _observacionesCtrl =
        TextEditingController(text: cliente.observaciones ?? "");
  }

  // Importante: Liberar memoria de los controladores cuando la pantalla se cierra.
  @override
  void dispose() {
    _telefonoCtrl.dispose();
    _direccionCtrl.dispose();
    _alergenosCtrl.dispose();
    _observacionesCtrl.dispose();
    super.dispose();
  }

  // Función principal para enviar el formulario.
  Future<void> _submit() async {
    // 1. Validar formato: Ejecuta las funciones 'validator' de cada TextFormField.
    if (!_formKey.currentState!.validate()) return;

    // 2. Cerrar teclado visualmente para mejor experiencia de usuario.
    FocusScope.of(context).unfocus();

    // Activa el estado de carga (el botón mostrará el spinner).
    setState(() => _loading = true);

    // Obtiene la instancia del Provider (listen: false porque aquí solo ejecutamos una acción, no escuchamos cambios).
    final provider = Provider.of<ClienteProvider>(context, listen: false);

    // Prepara el mapa JSON con los datos capturados de los inputs.
    final dataToUpdate = {
      "telefono":
          _telefonoCtrl.text.trim(), // trim() elimina espacios al inicio/final.
      "direccion": _direccionCtrl.text.trim(),
      "alergenos": _alergenosCtrl.text.trim(),
      "observaciones": _observacionesCtrl.text.trim(),
    };

    // Llama al método del provider que conecta con la API (BaseApi.put muy probablemente).
    final success = await provider.updateCliente(
      _clienteOriginal!.idUsuario,
      dataToUpdate,
    );

    // 'mounted' verifica si el widget sigue en pantalla antes de usar setState o context.
    // Esto previene errores si el usuario salió de la pantalla mientras cargaba.
    if (mounted) {
      setState(() => _loading = false);

      if (success) {
        // Muestra mensaje de éxito.
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text("Perfil actualizado correctamente"),
            backgroundColor: Colors.green,
            behavior: SnackBarBehavior.floating, // Flota sobre el contenido
          ),
        );
        Navigator.pop(context,
            true); // Retorna true para que la pantalla anterior sepa que debe recargar datos.
      } else {
        // Muestra mensaje de error.
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text("Error al actualizar. Inténtalo de nuevo."),
            backgroundColor: Colors.redAccent,
            behavior: SnackBarBehavior.floating,
          ),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    // Protección: Si por alguna razón no llegaron los argumentos, mostramos carga.
    if (_clienteOriginal == null) {
      return const Scaffold(body: Center(child: CircularProgressIndicator()));
    }

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        title: const Text("Editar Perfil",
            style: TextStyle(fontWeight: FontWeight.w600)),
        centerTitle: true,
        backgroundColor: AppTheme.primary,
        foregroundColor: Colors.white,
        elevation: 0,
        // Cambiamos la flecha por una X, común en formularios de edición modal.
        leading: IconButton(
          icon: const Icon(Icons.close),
          onPressed: () => Navigator.pop(context),
        ),
      ),
      // GestureDetector detecta toques en cualquier parte de la pantalla (fuera de inputs).
      // Se usa para cerrar el teclado al tocar el fondo.
      body: GestureDetector(
        onTap: () => FocusScope.of(context).unfocus(),
        child: SingleChildScrollView(
          physics:
              const BouncingScrollPhysics(), // Efecto de rebote al hacer scroll.
          padding: const EdgeInsets.all(20.0),
          child: Column(
            children: [
              // -----------------------------
              // TARJETA DEL FORMULARIO
              // -----------------------------
              Container(
                padding: const EdgeInsets.all(24),
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.circular(24),
                  // Sombra suave para dar efecto de elevación.
                  boxShadow: [
                    BoxShadow(
                      color: Colors.black.withOpacity(0.05),
                      blurRadius: 15,
                      offset: const Offset(0, 5),
                    ),
                  ],
                ),
                child: Form(
                  key: _formKey, // Asigna la clave para validación.
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        "Información de Contacto",
                        style: TextStyle(
                            fontSize: 18,
                            fontWeight: FontWeight.bold,
                            color: AppTheme.primary),
                      ),
                      const SizedBox(height: 20),

                      // INPUT TELÉFONO
                      TextFormField(
                        controller: _telefonoCtrl,
                        keyboardType:
                            TextInputType.phone, // Teclado numérico telefónico.
                        decoration: _buildInputDecoration(
                            "Teléfono", Icons.phone_outlined),
                        // Validación específica para teléfono.
                        validator: (value) {
                          if (value == null || value.isEmpty)
                            return "El teléfono es obligatorio";
                          if (value.length < 9)
                            return "Introduce un teléfono válido";
                          return null;
                        },
                      ),
                      const SizedBox(height: 20),

                      // INPUT DIRECCIÓN
                      TextFormField(
                        controller: _direccionCtrl,
                        keyboardType: TextInputType.streetAddress,
                        maxLines:
                            2, // Permite que ocupe más espacio si es larga.
                        minLines: 1,
                        decoration: _buildInputDecoration(
                            "Dirección", Icons.location_on_outlined),
                        validator: (value) => value!.isEmpty
                            ? "La dirección es obligatoria"
                            : null,
                      ),

                      const SizedBox(height: 30),

                      Text(
                        "Información Adicional",
                        style: TextStyle(
                            fontSize: 18,
                            fontWeight: FontWeight.bold,
                            color: AppTheme.primary),
                      ),
                      const SizedBox(height: 20),

                      // INPUT ALÉRGENOS (Sin validator porque es opcional)
                      TextFormField(
                        controller: _alergenosCtrl,
                        decoration: _buildInputDecoration(
                            "Alergenos (Opcional)",
                            Icons.warning_amber_rounded),
                      ),
                      const SizedBox(height: 20),

                      // INPUT OBSERVACIONES (Sin validator porque es opcional)
                      TextFormField(
                        controller: _observacionesCtrl,
                        maxLines: 3,
                        decoration: _buildInputDecoration(
                            "Observaciones (Opcional)",
                            Icons.note_alt_outlined),
                      ),

                      const SizedBox(height: 30),

                      // -----------------------------
                      // BOTÓN DE ACCIÓN
                      // -----------------------------
                      SizedBox(
                        width: double.infinity,
                        height: 54, // Botón más alto para facilitar el toque.
                        child: ElevatedButton(
                          // Si está cargando, deshabilita el botón (onPressed: null).
                          onPressed: _loading ? null : _submit,
                          style: ElevatedButton.styleFrom(
                            backgroundColor: AppTheme.primary,
                            foregroundColor: Colors.white,
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(14),
                            ),
                            elevation: 4,
                          ),
                          // Cambio dinámico: Muestra Spinner o Texto según _loading.
                          child: _loading
                              ? const SizedBox(
                                  height: 24,
                                  width: 24,
                                  child: CircularProgressIndicator(
                                    color: Colors.white,
                                    strokeWidth: 2.5,
                                  ),
                                )
                              : const Text(
                                  "Guardar Cambios",
                                  style: TextStyle(
                                      fontSize: 16,
                                      fontWeight: FontWeight.bold),
                                ),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  // --- WIDGET AUXILIAR PARA ESTILOS REPETITIVOS ---
  // Crea el diseño de los inputs para no repetir código en cada TextFormField.
  InputDecoration _buildInputDecoration(String label, IconData icon) {
    return InputDecoration(
      labelText: label,
      prefixIcon: Icon(icon, color: Colors.grey[500]),
      filled: true,
      fillColor: Colors.grey[
          50], // Fondo muy suave para diferenciar del blanco de la tarjeta.
      contentPadding: const EdgeInsets.symmetric(horizontal: 20, vertical: 16),
      border: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: BorderSide(color: Colors.grey[300]!),
      ),
      enabledBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: BorderSide(color: Colors.grey[300]!),
      ),
      // Borde cuando el usuario está escribiendo (Color primario).
      focusedBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: BorderSide(color: AppTheme.primary, width: 2),
      ),
      // Borde cuando hay un error de validación (Rojo).
      errorBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12),
        borderSide: const BorderSide(color: Colors.redAccent),
      ),
    );
  }
}
