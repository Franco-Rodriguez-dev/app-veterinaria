using BE_CRUDMascotas.models.DTO;
using BE_CRUDMascotas.models;

namespace BE_CRUDMascotas.models.Repository
{
    public interface IVeterinariaRepository
    {
        Task<List<PersonaMascotaListDTO>> GetListadoGeneralAsync();
        Task CrearConMascotaAsync(PersonaMascotaCreateDTO dto);
        Task DeleteConMascotasAsync(int personaId);
        Task UpdateConMascotaAsync(int personaId, PersonaMascotaCreateDTO dto);
        Task<PersonaMascotaCreateDTO> GetPorIdAsync(int id);
        //nuevos metodos para activos -inactivos 
        Task<List<ClienteInactivoDTO>> GetClientesInactivosAsync();
        Task<ReactivarClienteResultado> ReactivarClienteAsync(int personaId);
    }
}
