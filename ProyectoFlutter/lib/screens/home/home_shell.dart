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

  late final pages = [
    const ServicesScreen(),
    const MisCitasScreen(),
    const AccountScreen(),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      body: pages[index],
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: index,
        selectedItemColor: AppTheme.primary,
        unselectedItemColor: Colors.grey.shade600,
        onTap: (i) => setState(() => index = i),
        items: const [
          BottomNavigationBarItem(
            icon: Icon(Icons.cut),
            label: "Servicios",
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.calendar_month),
            label: "Citas",
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.person),
            label: "Cuenta",
          ),
        ],
      ),
    );
  }
}
