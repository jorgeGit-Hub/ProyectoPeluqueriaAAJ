import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../providers/valoracion_provider.dart';
import '../../models/valoracion.dart';
import '../../utils/theme.dart';

class ValoracionesScreen extends StatefulWidget {
  const ValoracionesScreen({super.key});

  @override
  State<ValoracionesScreen> createState() => _ValoracionesScreenState();
}

class _ValoracionesScreenState extends State<ValoracionesScreen> {
  int? _filtroMinPuntuacion;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<ValoracionProvider>().loadValoraciones();
    });
  }

  Future<void> _aplicarFiltro(int? minPuntuacion) async {
    setState(() => _filtroMinPuntuacion = minPuntuacion);
    
    if (minPuntuacion == null) {
      await context.read<ValoracionProvider>().loadValoraciones();
    } else {
      await context.read<ValoracionProvider>().loadValoracionesByMinPuntuacion(minPuntuacion);
    }
  }

  @override
  Widget build(BuildContext context) {
    final valoracionProv = context.watch<ValoracionProvider>();

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      appBar: AppBar(
        title: const Text("Valoraciones", style: TextStyle(fontWeight: FontWeight.w600)),
        backgroundColor: AppTheme.primary,
        foregroundColor: Colors.white,
        centerTitle: true,
        elevation: 0,
        actions: [
          PopupMenuButton<int?>(
            icon: const Icon(Icons.filter_list),
            onSelected: _aplicarFiltro,
            itemBuilder: (context) => [
              const PopupMenuItem(value: null, child: Text("Todas")),
              const PopupMenuItem(value: 5, child: Text("5 estrellas")),
              const PopupMenuItem(value: 4, child: Text("4+ estrellas")),
              const PopupMenuItem(value: 3, child: Text("3+ estrellas")),
            ],
          ),
        ],
      ),
      body: valoracionProv.loading
          ? const Center(child: CircularProgressIndicator())
          : valoracionProv.error != null
              ? _buildErrorState(valoracionProv.error!)
              : valoracionProv.valoraciones.isEmpty
                  ? _buildEmptyState()
                  : RefreshIndicator(
                      onRefresh: () => _filtroMinPuntuacion == null
                          ? valoracionProv.loadValoraciones()
                          : valoracionProv.loadValoracionesByMinPuntuacion(_filtroMinPuntuacion!),
                      child: ListView.builder(
                        padding: const EdgeInsets.all(16),
                        itemCount: valoracionProv.valoraciones.length,
                        itemBuilder: (context, index) {
                          final valoracion = valoracionProv.valoraciones[index];
                          return _buildValoracionCard(valoracion);
                        },
                      ),
                    ),
      floatingActionButton: FloatingActionButton(
        backgroundColor: AppTheme.primary,
        onPressed: () => Navigator.pushNamed(context, '/crear-valoracion'),
        child: const Icon(Icons.add, color: Colors.white),
      ),
    );
  }

  Widget _buildValoracionCard(Valoracion v) {
    return Card(
      margin: const EdgeInsets.only(bottom: 16),
      elevation: 2,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Row(
                  children: List.generate(5, (index) {
                    return Icon(
                      index < v.puntuacion ? Icons.star : Icons.star_border,
                      color: Colors.amber,
                      size: 20,
                    );
                  }),
                ),
                if (v.fechaValoracion != null)
                  Text(
                    v.fechaValoracion!,
                    style: TextStyle(color: Colors.grey[600], fontSize: 12),
                  ),
              ],
            ),
            const SizedBox(height: 12),
            Text(
              v.comentario,
              style: const TextStyle(fontSize: 14, height: 1.5),
            ),
            const SizedBox(height: 8),
            Text(
              'Cita #${v.idCita}',
              style: TextStyle(fontSize: 12, color: Colors.grey[500]),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildEmptyState() {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Icon(Icons.rate_review_outlined, size: 80, color: Colors.grey[400]),
          const SizedBox(height: 16),
          Text("No hay valoraciones", style: TextStyle(color: Colors.grey[600], fontSize: 16)),
        ],
      ),
    );
  }

  Widget _buildErrorState(String error) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          const Icon(Icons.error_outline, size: 80, color: Colors.red),
          const SizedBox(height: 16),
          Text("Error: $error", style: const TextStyle(color: Colors.red)),
          const SizedBox(height: 16),
          ElevatedButton(
            onPressed: () => context.read<ValoracionProvider>().loadValoraciones(),
            child: const Text("Reintentar"),
          ),
        ],
      ),
    );
  }
}