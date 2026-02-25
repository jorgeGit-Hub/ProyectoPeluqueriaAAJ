import 'dart:convert';
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:image_picker/image_picker.dart';
import '../../providers/user_provider.dart';
import '../../providers/cliente_provider.dart';
import '../../services/api_client.dart';
import '../../utils/theme.dart';

class AccountScreen extends StatefulWidget {
  const AccountScreen({super.key});

  @override
  State<AccountScreen> createState() => _AccountScreenState();
}

class _AccountScreenState extends State<AccountScreen> {
  String? _fotoPerfil;
  bool _subiendoFoto = false;
  bool _clienteNoEncontrado = false;
  bool _iniciando = true;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) async {
      final user = context.read<UserProvider>().usuario;
      if (user != null) {
        final int userId =
            user["idUsuario"] ?? user["id"] ?? user["id_usuario"] ?? 0;
        await _cargarTodo(userId);
      }
      if (mounted) setState(() => _iniciando = false);
    });
  }

  Future<void> _cargarTodo(int id) async {
    if (id == 0) {
      if (mounted) setState(() => _clienteNoEncontrado = true);
      return;
    }

    _cargarFotoPerfil(id);
    try {
      await context.read<ClienteProvider>().loadCliente(id);
      if (mounted) {
        setState(() {
          _clienteNoEncontrado =
              context.read<ClienteProvider>().cliente == null;
        });
      }
    } catch (e) {
      debugPrint("Cliente no encontrado: $e");
      if (mounted) setState(() => _clienteNoEncontrado = true);
    }
  }

  Future<void> _cargarFotoPerfil(int id) async {
    try {
      final response = await ApiClient().get('/usuarios/$id');
      if (mounted) {
        setState(() {
          _fotoPerfil = response.data["fotoPerfil"];
        });
      }
    } catch (e) {
      debugPrint("Error al cargar foto: $e");
    }
  }

  ImageProvider? _buildFotoProvider() {
    if (_fotoPerfil == null || _fotoPerfil!.isEmpty) return null;
    if (_fotoPerfil!.startsWith('http')) {
      return NetworkImage(_fotoPerfil!);
    }
    try {
      return MemoryImage(base64Decode(_fotoPerfil!));
    } catch (e) {
      debugPrint("Error decodificando foto: $e");
      return null;
    }
  }

  Future<void> _cambiarFoto(int userId) async {
    if (userId == 0) return;

    final picker = ImagePicker();
    final pickedFile = await picker.pickImage(
      source: ImageSource.gallery,
      imageQuality: 30,
    );

    if (pickedFile != null) {
      setState(() => _subiendoFoto = true);
      try {
        final bytes = await File(pickedFile.path).readAsBytes();
        final base64String = base64Encode(bytes);

        await ApiClient()
            .put('/usuarios/$userId', data: {"fotoPerfil": base64String});

        setState(() => _fotoPerfil = base64String);

        if (mounted) {
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(
                content: Text("Foto actualizada con éxito"),
                backgroundColor: Colors.green),
          );
        }
      } catch (e) {
        if (mounted) {
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(
                content: Text("Error al subir foto"),
                backgroundColor: Colors.red),
          );
        }
      } finally {
        setState(() => _subiendoFoto = false);
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final userProvider = context.watch<UserProvider>();
    final clienteProvider = context.watch<ClienteProvider>();
    final user = userProvider.usuario;

    if (_iniciando) {
      return const Scaffold(body: Center(child: CircularProgressIndicator()));
    }

    if (user == null) {
      return const Scaffold(body: Center(child: Text("Usuario no disponible")));
    }

    // ✅ Extracción segura de datos para evitar nulos y textos vacíos
    final String rawNombre = user["nombre"] ?? user["displayName"] ?? "";
    final String nombreCompleto =
        rawNombre.trim().isEmpty ? "Usuario" : rawNombre;

    final String apellidos = user["apellidos"] ?? "";

    final String rawCorreo = user["correo"] ?? user["email"] ?? "";
    final String correo =
        rawCorreo.trim().isEmpty ? "Sin correo electrónico" : rawCorreo;

    final int userId =
        user["idUsuario"] ?? user["id"] ?? user["id_usuario"] ?? 0;

    final String inicial =
        nombreCompleto.isNotEmpty ? nombreCompleto[0].toUpperCase() : "U";
    final cliente = clienteProvider.cliente;

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        elevation: 0,
        centerTitle: true,
        title: const Text("Mi Perfil",
            style: TextStyle(fontWeight: FontWeight.w600)),
        backgroundColor: AppTheme.primary,
        foregroundColor: Colors.white,
      ),
      body: SingleChildScrollView(
        physics: const BouncingScrollPhysics(),
        child: Column(
          children: [
            Container(
              width: double.infinity,
              decoration: const BoxDecoration(
                color: AppTheme.primary,
                borderRadius: BorderRadius.only(
                    bottomLeft: Radius.circular(30),
                    bottomRight: Radius.circular(30)),
              ),
              padding: const EdgeInsets.only(
                  bottom: 30, left: 20, right: 20, top: 10),
              child: Column(
                children: [
                  GestureDetector(
                    onTap: () => _cambiarFoto(userId),
                    child: Stack(
                      alignment: Alignment.bottomRight,
                      children: [
                        CircleAvatar(
                          radius: 50,
                          backgroundColor: Colors.white,
                          backgroundImage: _buildFotoProvider(),
                          child: _subiendoFoto
                              ? const CircularProgressIndicator()
                              : (_fotoPerfil == null || _fotoPerfil!.isEmpty)
                                  ? Text(inicial,
                                      style: const TextStyle(
                                          fontSize: 40,
                                          fontWeight: FontWeight.bold,
                                          color: AppTheme.primary))
                                  : null,
                        ),
                        Container(
                          padding: const EdgeInsets.all(6),
                          decoration: const BoxDecoration(
                              color: Colors.white, shape: BoxShape.circle),
                          child: const Icon(Icons.camera_alt,
                              color: AppTheme.primary, size: 20),
                        ),
                      ],
                    ),
                  ),
                  const SizedBox(height: 15),
                  Text(
                    "$nombreCompleto $apellidos".trim(),
                    style: const TextStyle(
                        fontSize: 22,
                        fontWeight: FontWeight.bold,
                        color: Colors.white),
                  ),
                  Text(correo,
                      style: TextStyle(
                          fontSize: 14, color: Colors.white.withOpacity(0.9))),
                ],
              ),
            ),
            const SizedBox(height: 20),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 20),
              child: Column(
                children: [
                  if (_clienteNoEncontrado || cliente == null)
                    _buildCompletarPerfilCard(user)
                  else ...[
                    _SectionCard(
                      title: "Información Personal",
                      children: [
                        _InfoTile(
                            icon: Icons.person,
                            label: "Nombre Completo",
                            value: "$nombreCompleto $apellidos".trim()),
                        const Divider(height: 1),
                        _InfoTile(
                            icon: Icons.email,
                            label: "Correo Electrónico",
                            value: correo),
                      ],
                    ),
                    const SizedBox(height: 20),
                    _SectionCard(
                      title: "Información de Contacto",
                      children: [
                        _InfoTile(
                            icon: Icons.phone,
                            label: "Teléfono",
                            // ✅ Si está vacío, se queda en blanco
                            value: cliente.telefono.isEmpty
                                ? "no registrado"
                                : cliente.telefono),
                        const Divider(height: 1),
                        _InfoTile(
                            icon: Icons.location_on,
                            label: "Dirección",
                            // ✅ Si está vacío, se queda en blanco
                            value: cliente.direccion.isEmpty
                                ? "no registrado"
                                : cliente.direccion),
                      ],
                    ),
                    const SizedBox(height: 20),
                    _SectionCard(
                      title: "Información Importante",
                      children: [
                        _InfoTile(
                            icon: Icons.warning_amber,
                            label: "Alérgenos",
                            // ✅ Si está vacío, se queda en blanco
                            value: cliente.alergenos?.isEmpty ?? true
                                ? "no registrado"
                                : cliente.alergenos!),
                        const Divider(height: 1),
                        _InfoTile(
                            icon: Icons.note,
                            label: "Observaciones",
                            // ✅ Si está vacío, se queda en blanco
                            value: cliente.observaciones?.isEmpty ?? true
                                ? "no registrado"
                                : cliente.observaciones!),
                      ],
                    ),
                    const SizedBox(height: 20),
                    SizedBox(
                      width: double.infinity,
                      height: 50,
                      child: ElevatedButton.icon(
                        icon: const Icon(Icons.edit),
                        label: const Text("Editar Mi Información"),
                        style: ElevatedButton.styleFrom(
                            backgroundColor: AppTheme.primary,
                            foregroundColor: Colors.white,
                            shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(12))),
                        onPressed: () async {
                          final result = await Navigator.pushNamed(
                              context, "/edit-account",
                              arguments: cliente);
                          if (result == true && mounted) {
                            final u = context.read<UserProvider>().usuario;
                            if (u != null) {
                              final uid = u["idUsuario"] ??
                                  u["id"] ??
                                  u["id_usuario"] ??
                                  0;
                              await context
                                  .read<ClienteProvider>()
                                  .loadCliente(uid);
                            }
                          }
                        },
                      ),
                    ),
                    const SizedBox(height: 10),
                  ],
                  SizedBox(
                    width: double.infinity,
                    height: 50,
                    child: OutlinedButton.icon(
                      icon: const Icon(Icons.logout),
                      label: const Text("Cerrar Sesión"),
                      style: OutlinedButton.styleFrom(
                          foregroundColor: Colors.redAccent,
                          side: const BorderSide(color: Colors.redAccent),
                          shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(12))),
                      onPressed: () async {
                        await context.read<UserProvider>().logout();
                        if (context.mounted) {
                          Navigator.pushReplacementNamed(context, "/login");
                        }
                      },
                    ),
                  ),
                  const SizedBox(height: 40),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildCompletarPerfilCard(Map<String, dynamic> user) {
    final String rawNombre = user["nombre"] ?? user["displayName"] ?? "";
    final String nombre = rawNombre.trim().isEmpty ? "Usuario" : rawNombre;

    return Container(
      margin: const EdgeInsets.only(bottom: 20),
      padding: const EdgeInsets.all(24),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(20),
        boxShadow: [
          BoxShadow(
              color: Colors.black.withOpacity(0.05),
              blurRadius: 10,
              offset: const Offset(0, 5))
        ],
      ),
      child: Column(
        children: [
          Icon(Icons.account_circle_outlined,
              size: 64, color: AppTheme.primary.withOpacity(0.6)),
          const SizedBox(height: 16),
          Text(
            "¡Bienvenido/a, $nombre!",
            style: const TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.bold,
                color: Colors.black87),
          ),
          const SizedBox(height: 8),
          Text(
            "Has iniciado sesión con Google. Completa tu perfil para poder reservar citas.",
            textAlign: TextAlign.center,
            style: TextStyle(fontSize: 14, color: Colors.grey[600]),
          ),
          const SizedBox(height: 20),
          SizedBox(
            width: double.infinity,
            height: 48,
            child: ElevatedButton.icon(
              icon: const Icon(Icons.edit),
              label: const Text("Completar mi perfil"),
              style: ElevatedButton.styleFrom(
                  backgroundColor: AppTheme.primary,
                  foregroundColor: Colors.white,
                  shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(12))),
              onPressed: () async {
                final result = await Navigator.pushNamed(
                    context, "/edit-account",
                    arguments: null);
                if (result == true && mounted) {
                  final u = context.read<UserProvider>().usuario;
                  if (u != null) {
                    final uid =
                        u["idUsuario"] ?? u["id"] ?? u["id_usuario"] ?? 0;
                    await _cargarTodo(uid);
                  }
                }
              },
            ),
          ),
        ],
      ),
    );
  }
}

class _SectionCard extends StatelessWidget {
  final String title;
  final List<Widget> children;
  const _SectionCard({required this.title, required this.children});
  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(20),
          boxShadow: [
            BoxShadow(
                color: Colors.black.withOpacity(0.05),
                blurRadius: 10,
                offset: const Offset(0, 5))
          ]),
      child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
        Padding(
            padding: const EdgeInsets.all(16.0),
            child: Text(title,
                style: const TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.bold,
                    color: Colors.black87))),
        ...children,
      ]),
    );
  }
}

class _InfoTile extends StatelessWidget {
  final IconData icon;
  final String label;
  final String value;
  const _InfoTile(
      {required this.icon, required this.label, required this.value});
  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
      child: Row(children: [
        Icon(icon, color: AppTheme.primary, size: 24),
        const SizedBox(width: 15),
        Expanded(
            child:
                Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
          Text(label, style: const TextStyle(fontSize: 12, color: Colors.grey)),
          const SizedBox(height: 2),
          Text(value,
              style: const TextStyle(
                  fontSize: 15,
                  fontWeight: FontWeight.w500,
                  color: Colors.black87)),
        ])),
      ]),
    );
  }
}
