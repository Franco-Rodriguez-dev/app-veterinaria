using BE_CRUDMascotas.models.DTO;

namespace BE_CRUDMascotas.models.Repository
{
    public interface IClienteRepository
    {
        Task<bool> UsernameExistsAsync(string username);
        Task<ClienteMascotaUsuarioResponseDTO> CrearClienteConMascotaAsync(ClienteMascotaUsuarioCreateDTO dto);
        Task<MiPerfilClienteDTO?> GetMiPerfilAsync(int usuarioId);
    }
}
