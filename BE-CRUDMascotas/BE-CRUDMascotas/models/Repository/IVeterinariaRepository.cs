using BE_CRUDMascotas.models.DTO;

namespace BE_CRUDMascotas.models.Repository
{
    public interface IVeterinariaRepository
    {
        Task<List<PersonaMascotaListDTO>> GetListadoGeneralAsync();
        Task CrearConMascotaAsync(PersonaMascotaCreateDTO dto);
        Task DeleteConMascotasAsync(int personaId);
        Task UpdateConMascotaAsync(int personaId, PersonaMascotaCreateDTO dto);
        Task<PersonaMascotaCreateDTO> GetPorIdAsync(int id);
    }
}
