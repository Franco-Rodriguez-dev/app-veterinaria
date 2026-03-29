using BE_CRUDMascotas.models;
using BE_CRUDMascotas.models.DTO;

namespace BE_CRUDMascotas.models.Repository
{
    public interface IPersonaRepository
    {
        Task<List<Personas>> GetListAsync(); // Listar todas las personas
        Task<Personas?> GetByIdAsync(int id, bool includeMascotas = false); // Obtener una persona por Id
        Task<Personas> AddAsync(Personas persona); // Crear una nueva persona
        Task UpdateAsync(Personas persona); // Actualizar
        Task DeleteAsync(Personas persona); // Eliminar
        Task<bool> ExistsAsync(int id); // Verificar si existe
        

    }
}
