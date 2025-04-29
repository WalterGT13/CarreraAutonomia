using CarreraAutonomiaAPI.Models;

namespace CarreraAutonomiaAPI.Services
{
    public interface IRegistroService
    {
        Task<int> ContarRegistros();
        Task<int> ObtenerLimiteCamisas();
        Task<string> Registrar(Registro registro);
        Task<IEnumerable<Registro>> ObtenerTodos();


    }
}
