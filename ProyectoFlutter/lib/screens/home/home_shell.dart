import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart'; // ✅ IMPORTACIÓN CRÍTICA
import '../services/services_screen.dart';
import '../booking/mis_citas_screen.dart';
import '../account/account_screen.dart';
import '../../utils/theme.dart';

class HomeShell extends StatefulWidget {
  const HomeShell({super.key});

  @override
  State<HomeShell> createState() => _HomeShellState();
}

class _HomeShellState extends State<HomeShell> {
  int index = 0;
  final String telefonoPeluqueria = "+34600123456";

  final List<Widget> pages = const [
    ServicesScreen(),
    MisCitasScreen(),
    AccountScreen(),
  ];

  // ✅ MÉTODO DE LLAMADA CORREGIDO
  Future<void> _llamarPeluqueria() async {
    final Uri url = Uri.parse('tel:$telefonoPeluqueria');
    try {
      if (await canLaunchUrl(url)) {
        await launchUrl(url);
      } else {
        if (mounted) {
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(
                content: Text("No se pudo abrir el marcador"),
                backgroundColor: Colors.red),
          );
        }
      }
    } catch (e) {
      debugPrint("Error al intentar llamar: $e");
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      body: IndexedStack(
        index: index,
        children: pages,
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _llamarPeluqueria,
        backgroundColor: AppTheme.primary,
        child: const Icon(Icons.phone, color: Colors.white),
      ),
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: index,
        selectedItemColor: AppTheme.primary,
        onTap: (i) => setState(() => index = i),
        items: const [
          BottomNavigationBarItem(
              icon: Icon(Icons.content_cut_rounded), label: "Servicios"),
          BottomNavigationBarItem(
              icon: Icon(Icons.calendar_month_rounded), label: "Mis Citas"),
          BottomNavigationBarItem(
              icon: Icon(Icons.person_rounded), label: "Mi Perfil"),
        ],
      ),
    );
  }
}
