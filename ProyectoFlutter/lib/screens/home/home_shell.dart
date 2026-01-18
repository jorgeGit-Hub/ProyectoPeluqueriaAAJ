import 'package:flutter/material.dart';
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

  final List<Widget> pages = const [
    ServicesScreen(),
    MisCitasScreen(),
    AccountScreen(),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      body: IndexedStack(
        index: index,
        children: pages,
      ),
      bottomNavigationBar: Container(
        decoration: BoxDecoration(
          boxShadow: [
            BoxShadow(
              color: Colors.black.withOpacity(0.1),
              blurRadius: 10,
              offset: const Offset(0, -2),
            ),
          ],
        ),
        child: BottomNavigationBar(
          currentIndex: index,
          elevation: 0,
          backgroundColor: Colors.white,
          selectedItemColor: AppTheme.primary,
          unselectedItemColor: Colors.grey.shade500,
          selectedFontSize: 12,
          unselectedFontSize: 12,
          type: BottomNavigationBarType.fixed,
          onTap: (i) => setState(() => index = i),
          items: const [
            BottomNavigationBarItem(
              icon: Padding(
                padding: EdgeInsets.only(bottom: 4),
                child: Icon(Icons.content_cut_rounded),
              ),
              activeIcon: Icon(Icons.content_cut_rounded),
              label: "Servicios",
            ),
            BottomNavigationBarItem(
              icon: Padding(
                padding: EdgeInsets.only(bottom: 4),
                child: Icon(Icons.calendar_month_rounded),
              ),
              activeIcon: Icon(Icons.calendar_month_rounded),
              label: "Mis Citas",
            ),
            BottomNavigationBarItem(
              icon: Padding(
                padding: EdgeInsets.only(bottom: 4),
                child: Icon(Icons.person_rounded),
              ),
              activeIcon: Icon(Icons.person_rounded),
              label: "Mi Perfil",
            ),
          ],
        ),
      ),
    );
  }
}
