import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../providers/user_provider.dart';
import '../../providers/cliente_provider.dart';
import '../../utils/theme.dart';

class AccountScreen extends StatefulWidget {
  const AccountScreen({super.key});

  @override
  State<AccountScreen> createState() => _AccountScreenState();
}

class _AccountScreenState extends State<AccountScreen> {
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) async {
      final user = context.read<UserProvider>().usuario;
      if (user != null) {
        await context.read<ClienteProvider>().loadCliente(user["id"]);
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    final userProvider = context.watch<UserProvider>();
    final clienteProvider = context.watch<ClienteProvider>();
    final user = userProvider.usuario;

    if (userProvider.loading || clienteProvider.loading) {
      return const Scaffold(
        body: Center(child: CircularProgressIndicator()),
      );
    }

    if (user == null) {
      return const Scaffold(
        body: Center(child: Text("Usuario no disponible")),
      );
    }

    final String nombreStr = user["nombre"]?.toString() ?? "U";
    final String inicial =
        nombreStr.isNotEmpty ? nombreStr[0].toUpperCase() : "U";
    final cliente = clienteProvider.cliente;

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        elevation: 0,
        centerTitle: true,
        title: const Text(
          "Mi Perfil",
          style: TextStyle(fontWeight: FontWeight.w600),
        ),
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
                  bottomRight: Radius.circular(30),
                ),
              ),
              padding: const EdgeInsets.only(
                  bottom: 30, left: 20, right: 20, top: 10),
              child: Column(
                children: [
                  CircleAvatar(
                    radius: 40,
                    backgroundColor: Colors.white,
                    child: Text(
                      inicial,
                      style: const TextStyle(
                        fontSize: 30,
                        fontWeight: FontWeight.bold,
                        color: AppTheme.primary,
                      ),
                    ),
                  ),
                  const SizedBox(height: 10),
                  Text(
                    "${user["nombre"] ?? ""} ${user["apellidos"] ?? ""}",
                    style: const TextStyle(
                      fontSize: 22,
                      fontWeight: FontWeight.bold,
                      color: Colors.white,
                    ),
                  ),
                  Text(
                    user["correo"] ?? "Sin correo electrónico",
                    style: TextStyle(
                      fontSize: 14,
                      color: Colors.white.withOpacity(0.9),
                    ),
                  ),
                ],
              ),
            ),
            const SizedBox(height: 20),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 20),
              child: Column(
                children: [
                  // INFORMACIÓN DEL CLIENTE (NO ID NI ROL)
                  if (cliente != null) ...[
                    _SectionCard(
                      title: "Información Personal",
                      children: [
                        _InfoTile(
                          icon: Icons.person,
                          label: "Nombre Completo",
                          value: "${user["nombre"]} ${user["apellidos"]}",
                        ),
                        const Divider(height: 1),
                        _InfoTile(
                          icon: Icons.email,
                          label: "Correo Electrónico",
                          value: user["correo"] ?? "N/A",
                        ),
                      ],
                    ),
                    const SizedBox(height: 20),
                    _SectionCard(
                      title: "Información de Contacto",
                      children: [
                        _InfoTile(
                          icon: Icons.phone,
                          label: "Teléfono",
                          value: cliente.telefono.isEmpty
                              ? "No registrado"
                              : cliente.telefono,
                        ),
                        const Divider(height: 1),
                        _InfoTile(
                          icon: Icons.location_on,
                          label: "Dirección",
                          value: cliente.direccion.isEmpty
                              ? "No registrada"
                              : cliente.direccion,
                        ),
                      ],
                    ),
                    const SizedBox(height: 20),
                    _SectionCard(
                      title: "Información Importante",
                      children: [
                        _InfoTile(
                          icon: Icons.warning_amber,
                          label: "Alérgenos",
                          value: cliente.alergenos?.isEmpty ?? true
                              ? "Ninguno registrado"
                              : cliente.alergenos!,
                        ),
                        const Divider(height: 1),
                        _InfoTile(
                          icon: Icons.note,
                          label: "Observaciones",
                          value: cliente.observaciones?.isEmpty ?? true
                              ? "Ninguna"
                              : cliente.observaciones!,
                        ),
                      ],
                    ),
                    const SizedBox(height: 20),

                    // BOTÓN EDITAR PERFIL
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
                            borderRadius: BorderRadius.circular(12),
                          ),
                        ),
                        onPressed: () async {
                          final result = await Navigator.pushNamed(
                            context,
                            "/edit-account",
                            arguments: cliente,
                          );

                          if (result == true && mounted) {
                            final user = context.read<UserProvider>().usuario;
                            if (user != null) {
                              await context
                                  .read<ClienteProvider>()
                                  .loadCliente(user["id"]);
                            }
                          }
                        },
                      ),
                    ),
                    const SizedBox(height: 10),
                  ] else ...[
                    const Center(
                      child: Padding(
                        padding: EdgeInsets.all(20.0),
                        child: Text("Cargando información del perfil..."),
                      ),
                    ),
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
                          borderRadius: BorderRadius.circular(12),
                        ),
                      ),
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
            offset: const Offset(0, 5),
          ),
        ],
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: Text(
              title,
              style: const TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.bold,
                color: Colors.black87,
              ),
            ),
          ),
          ...children,
        ],
      ),
    );
  }
}

class _InfoTile extends StatelessWidget {
  final IconData icon;
  final String label;
  final String value;

  const _InfoTile({
    required this.icon,
    required this.label,
    required this.value,
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
      child: Row(
        children: [
          Icon(icon, color: AppTheme.primary, size: 24),
          const SizedBox(width: 15),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  label,
                  style: const TextStyle(fontSize: 12, color: Colors.grey),
                ),
                const SizedBox(height: 2),
                Text(
                  value,
                  style: const TextStyle(
                    fontSize: 15,
                    fontWeight: FontWeight.w500,
                    color: Colors.black87,
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
