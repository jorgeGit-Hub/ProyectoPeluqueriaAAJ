import 'package:flutter/material.dart';
import '../../utils/theme.dart';

class ReservationConfirmedScreen extends StatelessWidget {
  const ReservationConfirmedScreen({super.key});

  @override
  Widget build(BuildContext context) {
    // PopScope asegura que el usuario no pueda volver "atrás" al formulario de reserva
    // una vez que la cita ya ha sido creada en el backend de Spring Boot.
    return PopScope(
      canPop: false,
      onPopInvokedWithResult: (didPop, result) {
        if (didPop) return;
        // Si intenta ir atrás, limpiamos el historial y vamos al Home
        Navigator.pushNamedAndRemoveUntil(context, "/home", (route) => false);
      },
      child: Scaffold(
        backgroundColor: AppTheme.pastelLavender,
        body: Center(
          child: SingleChildScrollView(
            padding: const EdgeInsets.all(24),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                // -----------------------------
                // TARJETA DE ÉXITO
                // -----------------------------
                Container(
                  padding:
                      const EdgeInsets.symmetric(horizontal: 32, vertical: 48),
                  decoration: BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.circular(30),
                    boxShadow: [
                      BoxShadow(
                        color: Colors.black.withOpacity(0.05),
                        blurRadius: 20,
                        offset: const Offset(0, 10),
                      ),
                    ],
                  ),
                  child: Column(
                    children: [
                      // ANIMACIÓN DE ICONO (Efecto Rebote)
                      TweenAnimationBuilder(
                        duration: const Duration(milliseconds: 800),
                        curve: Curves.elasticOut,
                        tween: Tween<double>(begin: 0.0, end: 1.0),
                        builder: (context, double value, child) {
                          return Transform.scale(
                            scale: value,
                            child: child,
                          );
                        },
                        child: Container(
                          height: 100,
                          width: 100,
                          decoration: BoxDecoration(
                            color: Colors.green.shade50,
                            shape: BoxShape.circle,
                          ),
                          child: Icon(
                            Icons.check_rounded,
                            color: Colors.green.shade600,
                            size: 60,
                          ),
                        ),
                      ),

                      const SizedBox(height: 30),

                      const Text(
                        "¡Reserva Exitosa!",
                        textAlign: TextAlign.center,
                        style: TextStyle(
                          fontSize: 26,
                          fontWeight: FontWeight.bold,
                          color: Colors.black87,
                        ),
                      ),
                      const SizedBox(height: 12),
                      Text(
                        "Tu cita ha sido agendada correctamente en nuestro sistema. Te esperamos en el salón.",
                        textAlign: TextAlign.center,
                        style: TextStyle(
                          fontSize: 16,
                          color: Colors.grey[600],
                          height: 1.5,
                        ),
                      ),

                      const SizedBox(height: 40),

                      // -----------------------------
                      // BOTÓN: VOLVER AL INICIO
                      // -----------------------------
                      SizedBox(
                        width: double.infinity,
                        height: 52,
                        child: ElevatedButton(
                          onPressed: () {
                            // pushNamedAndRemoveUntil es clave para reiniciar el flujo de la app
                            Navigator.pushNamedAndRemoveUntil(
                                context, "/home", (route) => false);
                          },
                          style: ElevatedButton.styleFrom(
                            backgroundColor: AppTheme.primary,
                            foregroundColor: Colors.white,
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(14),
                            ),
                            elevation: 0,
                          ),
                          child: const Text(
                            "Volver al Inicio",
                            style: TextStyle(
                                fontSize: 16, fontWeight: FontWeight.bold),
                          ),
                        ),
                      ),

                      const SizedBox(height: 16),

                      // -----------------------------
                      // BOTÓN: VER MIS CITAS
                      // -----------------------------
                      SizedBox(
                        width: double.infinity,
                        height: 52,
                        child: OutlinedButton(
                          onPressed: () {
                            // Navegamos a la pantalla de Mis Citas que corregimos antes
                            Navigator.pushNamedAndRemoveUntil(context,
                                "/mis-citas", ModalRoute.withName("/home"));
                          },
                          style: OutlinedButton.styleFrom(
                            foregroundColor: AppTheme.primary,
                            side: const BorderSide(
                                color: AppTheme.primary, width: 1.5),
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(14),
                            ),
                          ),
                          child: const Text(
                            "Ver Mis Citas",
                            style: TextStyle(
                                fontSize: 16, fontWeight: FontWeight.bold),
                          ),
                        ),
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
