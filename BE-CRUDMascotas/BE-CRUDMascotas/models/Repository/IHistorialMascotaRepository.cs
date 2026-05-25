using BE_CRUDMascotas.models.DTO;

namespace BE_CRUDMascotas.models.Repository
{
    public interface IHistorialMascotaRepository
    {
        Task<List<HistorialMascotaDTO>> GetByMascotaAsync(int mascotaId);
        Task<HistorialMascotaDTO> GetByIdAsync(int id);
        Task<HistorialMascotaDTO> AddAsync(HistorialMascotaCreateDTO dto, int? creadoPorUsuarioId);
        Task<bool> UpdateAsync(int id, HistorialMascotaUpdateDTO dto);
        Task<bool> SoftDeleteAsync(int id);
        Task<bool> MascotaExistsAsync(int mascotaId);
        Task<bool> MascotaPerteneceAUsuarioAsync(int mascotaId, int usuarioId);
        Task<int?> GetMascotaIdByHistorialAsync(int historialId);
    }
}
