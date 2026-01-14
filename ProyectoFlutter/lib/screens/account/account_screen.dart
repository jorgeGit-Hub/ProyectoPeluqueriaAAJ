import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../providers/user_provider.dart';
import '../../utils/theme.dart';

class AccountScreen extends StatelessWidget {
  const AccountScreen({super.key});

  @override
  Widget build(BuildContext context) {
    final userProvider = context.watch<UserProvider>();
    final user = userProvider.usuario;

    if (userProvider.loading) {
      return const Scaffold(
        body: Center(child: CircularProgressIndicator()),
      );
    }

    if (user == null) {
      return const Scaffold(
        body: Center(child: Text("Usuario no disponible")),
      );
    }

    // Seguridad para la inicial del nombre
    final String nombreStr = user["nombre"]?.toString() ?? "U";
    final String inicial =
        nombreStr.isNotEmpty ? nombreStr[0].toUpperCase() : "U";

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
                  _SectionCard(
                    title: "Información Básica",
                    children: [
                      _InfoTile(
                        icon: Icons.badge_outlined,
                        label: "ID Usuario",
                        value: user["id"]?.toString() ?? "N/A",
                      ),
                      const Divider(height: 1),
                      _InfoTile(
                        icon: Icons.security,
                        label: "Rol",
                        value: user["rol"]?.toString() ?? "Cliente",
                      ),
                    ],
                  ),
                  const SizedBox(height: 30),
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

// Los widgets _SectionCard y _InfoTile están perfectos, no hace falta tocarlos.
