using CarreraAutonomiaAPI.Data;
using CarreraAutonomiaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarreraAutonomiaAPI.Services
{
    public class RegistroService : IRegistroService
    {
        private readonly ApplicationDbContext _context;
        public RegistroService(ApplicationDbContext context) => _context = context;

        public async Task<int> ContarRegistros() =>
            await _context.Registros.CountAsync();

        public async Task<int> ObtenerLimiteCamisas()
        {
            var config = await _context.Configuraciones.FindAsync(1);
            return config?.limite_camisas ?? 0;
        }

        public async Task<string> Registrar(Registro registro)
        {
            try
            {
                if (await _context.Registros.AnyAsync(r => r.Identificacion == registro.Identificacion))
                    return "Este CUI o Pasaporte ya está registrado.";

                var config = await _context.Configuraciones.FindAsync(1);
                if (config == null)
                    return "No se ha configurado el límite de camisas.";

                int total = await ContarRegistros();
                if (total >= config.limite_camisas)
                    return "Ya no hay camisas disponibles.";

                _context.Registros.Add(registro);
                await _context.SaveChangesAsync();
                return "Registro exitoso";
            }
            catch (Exception ex)
            {
                // Mostramos el mensaje más específico
                return $"Error interno: {ex.InnerException?.Message ?? ex.Message}";
            }
        }

        public async Task<IEnumerable<Registro>> ObtenerTodos()
        {
            return await _context.Registros.ToListAsync();
        }


    }
}
