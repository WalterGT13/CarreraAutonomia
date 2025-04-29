using CarreraAutonomiaAPI.Models;

namespace CarreraAutonomiaAPI.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> ObtenerCategoriasActivas();
    }

}
