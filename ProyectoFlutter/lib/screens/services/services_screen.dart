import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:peluqueria_aja/providers/servicio_provider.dart';
import 'package:peluqueria_aja/models/servicio.dart';
import '../../utils/theme.dart';

class ServicesScreen extends StatefulWidget {
  const ServicesScreen({super.key});

  @override
  State<ServicesScreen> createState() => _ServicesScreenState();
}

class _ServicesScreenState extends State<ServicesScreen> {
  String _searchQuery = '';
  String? _selectedCategory;
  bool _showCategories = false;

  // Categorías de servicios
  final Map<String, IconData> _categories = {
    'Todos': Icons.grid_view_rounded,
    'Corte Hombre': Icons.face,
    'Corte Mujer': Icons.face_2,
    'Uñas': Icons.back_hand,
    'Tinte': Icons.palette,
    'Tratamientos': Icons.spa,
  };

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<ServicioProvider>().loadServicios();
    });
  }

  List<Servicio> _filterServices(List<Servicio> services) {
    return services.where((servicio) {
      // Filtro por búsqueda
      final matchesSearch = _searchQuery.isEmpty ||
          servicio.nombre.toLowerCase().contains(_searchQuery.toLowerCase()) ||
          servicio.descripcion
              .toLowerCase()
              .contains(_searchQuery.toLowerCase());

      // Filtro por categoría
      final matchesCategory = _selectedCategory == null ||
          _selectedCategory == 'Todos' ||
          servicio.nombre
              .toLowerCase()
              .contains(_selectedCategory!.toLowerCase().split(' ')[1] ?? '');

      return matchesSearch && matchesCategory;
    }).toList();
  }

  @override
  Widget build(BuildContext context) {
    final servicioProv = context.watch<ServicioProvider>();

    return Scaffold(
      backgroundColor: AppTheme.pastelLavender,
      body: CustomScrollView(
        slivers: [
          // App Bar con diseño moderno
          SliverAppBar(
            expandedHeight: 120,
            floating: false,
            pinned: true,
            backgroundColor: AppTheme.primary,
            elevation: 0,
            flexibleSpace: FlexibleSpaceBar(
              title: const Text(
                'Servicios',
                style: TextStyle(
                  fontWeight: FontWeight.bold,
                  fontSize: 24,
                ),
              ),
              centerTitle: false,
              titlePadding: const EdgeInsets.only(left: 20, bottom: 16),
            ),
          ),

          // Buscador
          SliverToBoxAdapter(
            child: Container(
              decoration: const BoxDecoration(
                color: AppTheme.primary,
                borderRadius: BorderRadius.only(
                  bottomLeft: Radius.circular(30),
                  bottomRight: Radius.circular(30),
                ),
              ),
              padding: const EdgeInsets.fromLTRB(20, 0, 20, 24),
              child: Container(
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.circular(16),
                  boxShadow: [
                    BoxShadow(
                      color: Colors.black.withOpacity(0.08),
                      blurRadius: 10,
                      offset: const Offset(0, 4),
                    ),
                  ],
                ),
                child: TextField(
                  onChanged: (value) => setState(() => _searchQuery = value),
                  decoration: InputDecoration(
                    hintText: 'Buscar servicios...',
                    hintStyle: TextStyle(color: Colors.grey[400]),
                    prefixIcon:
                        Icon(Icons.search, color: AppTheme.primary, size: 24),
                    suffixIcon: _searchQuery.isNotEmpty
                        ? IconButton(
                            icon: const Icon(Icons.clear, color: Colors.grey),
                            onPressed: () => setState(() => _searchQuery = ''),
                          )
                        : null,
                    border: InputBorder.none,
                    contentPadding: const EdgeInsets.symmetric(
                        horizontal: 20, vertical: 16),
                  ),
                ),
              ),
            ),
          ),

          // Categorías
          SliverToBoxAdapter(
            child: Padding(
              padding: const EdgeInsets.fromLTRB(20, 20, 20, 12),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      const Text(
                        'Categorías',
                        style: TextStyle(
                          fontSize: 18,
                          fontWeight: FontWeight.bold,
                          color: Colors.black87,
                        ),
                      ),
                      TextButton.icon(
                        onPressed: () =>
                            setState(() => _showCategories = !_showCategories),
                        icon: Icon(
                          _showCategories
                              ? Icons.expand_less
                              : Icons.expand_more,
                          size: 20,
                        ),
                        label: Text(_showCategories ? 'Ocultar' : 'Ver todas'),
                        style: TextButton.styleFrom(
                          foregroundColor: AppTheme.primary,
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 12),
                  _buildCategoryChips(),
                ],
              ),
            ),
          ),

          // Lista de servicios
          SliverPadding(
            padding: const EdgeInsets.symmetric(horizontal: 20),
            sliver: _buildServicesList(servicioProv),
          ),
        ],
      ),
    );
  }

  Widget _buildCategoryChips() {
    final displayCategories = _showCategories
        ? _categories.entries.toList()
        : _categories.entries.take(4).toList();

    return Wrap(
      spacing: 8,
      runSpacing: 8,
      children: displayCategories.map((entry) {
        final isSelected = _selectedCategory == entry.key;
        return FilterChip(
          selected: isSelected,
          label: Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              Icon(
                entry.value,
                size: 18,
                color: isSelected ? Colors.white : AppTheme.primary,
              ),
              const SizedBox(width: 6),
              Text(entry.key),
            ],
          ),
          onSelected: (selected) {
            setState(() {
              _selectedCategory = selected ? entry.key : null;
              if (_selectedCategory == 'Todos') _selectedCategory = null;
            });
          },
          backgroundColor: Colors.white,
          selectedColor: AppTheme.primary,
          checkmarkColor: Colors.white,
          labelStyle: TextStyle(
            color: isSelected ? Colors.white : Colors.black87,
            fontWeight: isSelected ? FontWeight.bold : FontWeight.normal,
          ),
          elevation: isSelected ? 4 : 0,
          shadowColor: AppTheme.primary.withOpacity(0.3),
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(12),
            side: BorderSide(
              color: isSelected ? AppTheme.primary : Colors.grey.shade300,
              width: 1.5,
            ),
          ),
        );
      }).toList(),
    );
  }

  Widget _buildServicesList(ServicioProvider provider) {
    if (provider.loading && provider.servicios.isEmpty) {
      return const SliverFillRemaining(
        child: Center(
          child: CircularProgressIndicator(),
        ),
      );
    }

    if (provider.errorMessage != null) {
      return SliverFillRemaining(
        child: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              const Icon(Icons.error_outline, size: 64, color: Colors.red),
              const SizedBox(height: 16),
              const Text(
                'Error al cargar servicios',
                style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 24),
              ElevatedButton.icon(
                onPressed: () => provider.loadServicios(),
                icon: const Icon(Icons.refresh),
                label: const Text('Reintentar'),
                style: ElevatedButton.styleFrom(
                  backgroundColor: AppTheme.primary,
                  foregroundColor: Colors.white,
                ),
              ),
            ],
          ),
        ),
      );
    }

    final filteredServices = _filterServices(provider.servicios);

    if (filteredServices.isEmpty) {
      return SliverFillRemaining(
        child: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Icon(Icons.search_off, size: 64, color: Colors.grey[300]),
              const SizedBox(height: 16),
              Text(
                'No se encontraron servicios',
                style: TextStyle(color: Colors.grey[600], fontSize: 16),
              ),
              if (_searchQuery.isNotEmpty || _selectedCategory != null) ...[
                const SizedBox(height: 8),
                TextButton(
                  onPressed: () => setState(() {
                    _searchQuery = '';
                    _selectedCategory = null;
                  }),
                  child: const Text('Limpiar filtros'),
                ),
              ],
            ],
          ),
        ),
      );
    }

    return SliverList(
      delegate: SliverChildBuilderDelegate(
        (context, index) {
          final servicio = filteredServices[index];
          return Padding(
            padding: const EdgeInsets.only(bottom: 16),
            child: _ModernServiceCard(
              servicio: servicio,
              onTap: () {
                Navigator.pushNamed(
                  context,
                  "/service-detail",
                  arguments: servicio,
                );
              },
            ),
          );
        },
        childCount: filteredServices.length,
      ),
    );
  }
}

class _ModernServiceCard extends StatelessWidget {
  final Servicio servicio;
  final VoidCallback onTap;

  const _ModernServiceCard({
    required this.servicio,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(20),
        boxShadow: [
          BoxShadow(
            color: Colors.black.withOpacity(0.06),
            blurRadius: 12,
            offset: const Offset(0, 4),
          ),
        ],
      ),
      child: Material(
        color: Colors.transparent,
        child: InkWell(
          onTap: onTap,
          borderRadius: BorderRadius.circular(20),
          child: Padding(
            padding: const EdgeInsets.all(16),
            child: Row(
              children: [
                // Imagen del servicio
                Container(
                  height: 80,
                  width: 80,
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(16),
                    gradient: LinearGradient(
                      begin: Alignment.topLeft,
                      end: Alignment.bottomRight,
                      colors: [
                        AppTheme.primary.withOpacity(0.1),
                        AppTheme.primary.withOpacity(0.05),
                      ],
                    ),
                  ),
                  child: servicio.imagen != null && servicio.imagen!.isNotEmpty
                      ? ClipRRect(
                          borderRadius: BorderRadius.circular(16),
                          child: Image.network(
                            servicio.imagen!,
                            fit: BoxFit.cover,
                            errorBuilder: (_, __, ___) => _buildDefaultIcon(),
                          ),
                        )
                      : _buildDefaultIcon(),
                ),

                const SizedBox(width: 16),

                // Información del servicio
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        servicio.nombre,
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.bold,
                          color: Colors.black87,
                        ),
                        maxLines: 1,
                        overflow: TextOverflow.ellipsis,
                      ),
                      const SizedBox(height: 6),
                      Row(
                        children: [
                          Icon(Icons.access_time,
                              size: 14, color: Colors.grey[500]),
                          const SizedBox(width: 4),
                          Text(
                            '${servicio.duracion} min',
                            style: TextStyle(
                                fontSize: 13, color: Colors.grey[600]),
                          ),
                          const SizedBox(width: 12),
                          Icon(Icons.star, size: 14, color: Colors.amber[600]),
                          const SizedBox(width: 4),
                          Text(
                            '4.9',
                            style: TextStyle(
                                fontSize: 13, color: Colors.grey[600]),
                          ),
                        ],
                      ),
                      const SizedBox(height: 8),
                      Row(
                        children: [
                          Container(
                            padding: const EdgeInsets.symmetric(
                                horizontal: 10, vertical: 4),
                            decoration: BoxDecoration(
                              color: AppTheme.primary.withOpacity(0.1),
                              borderRadius: BorderRadius.circular(8),
                            ),
                            child: Text(
                              '${servicio.precio.toStringAsFixed(2)} €',
                              style: const TextStyle(
                                fontSize: 16,
                                fontWeight: FontWeight.bold,
                                color: AppTheme.primary,
                              ),
                            ),
                          ),
                        ],
                      ),
                    ],
                  ),
                ),

                // Flecha
                Container(
                  height: 36,
                  width: 36,
                  decoration: BoxDecoration(
                    color: AppTheme.primary.withOpacity(0.1),
                    borderRadius: BorderRadius.circular(10),
                  ),
                  child: const Icon(
                    Icons.arrow_forward_ios_rounded,
                    size: 16,
                    color: AppTheme.primary,
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildDefaultIcon() {
    return const Center(
      child: Icon(
        Icons.content_cut_rounded,
        color: AppTheme.primary,
        size: 36,
      ),
    );
  }
}
