using CarreraAutonomiaAPI.Data;
using CarreraAutonomiaAPI.Models;
using CarreraAutonomiaAPI.Services;
using Microsoft.EntityFrameworkCore;

public class CategoriaService : ICategoriaService
{
    private readonly ApplicationDbContext _context;
    public CategoriaService(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Categoria>> ObtenerCategoriasActivas()
    {
        return await _context.Categorias
            .Where(c => c.activa)
            .ToListAsync();
    }
}