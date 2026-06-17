using BE_CRUDMascotas.models.DTO;
using BE_CRUDMascotas.models.Enums;

namespace BE_CRUDMascotas.models.Repository
{
    public interface IUsuarioRepository
    {
        Task<RestablecerPasswordResultado> RestablecerPasswordAsync(RestablecerPasswordDTO dto);
        Task<CambiarPasswordResultado> CambiarPasswordAsync(int usuarioId, CambiarPasswordDTO dto);
    }
}
