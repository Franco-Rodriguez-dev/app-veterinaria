using BE_CRUDMascotas.models.DTO;
using BE_CRUDMascotas.models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BE_CRUDMascotas.models.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AplicationDbContext _context;

        public UsuarioRepository(AplicationDbContext context)
        {
            _context = context;

        }

        public async Task<RestablecerPasswordResultado> RestablecerPasswordAsync(RestablecerPasswordDTO dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == dto.UsuarioId);

            if (usuario == null)
                return RestablecerPasswordResultado.NoEncontrado;
            if (!usuario.Activo)
                return RestablecerPasswordResultado.UsuarioInactivo;

            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordTemporal);

            usuario.DebeCambiarPassword = true;

            await _context.SaveChangesAsync();
            return RestablecerPasswordResultado.Restablecido;


        }

        public async Task<CambiarPasswordResultado> CambiarPasswordAsync(int usuarioId, CambiarPasswordDTO dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario == null)
                return CambiarPasswordResultado.UsuarioNoEncontrado;
            if (!usuario.Activo)
                return CambiarPasswordResultado.UsuarioInactivo;

            var passwordActualCorrecta = BCrypt.Net.BCrypt.Verify(dto.PasswordActual, usuario.PasswordHash);

            if (!passwordActualCorrecta)
                return CambiarPasswordResultado.PasswordActualIncorrecta;

            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordNueva);
            
            usuario.DebeCambiarPassword = false;
            await _context.SaveChangesAsync();

            return CambiarPasswordResultado.Cambiada;



        }



    }
}
