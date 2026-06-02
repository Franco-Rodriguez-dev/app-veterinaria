using BE_CRUDMascotas.models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BE_CRUDMascotas.models.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AplicationDbContext _context;

        public ClienteRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            var normalizedUsername = username.Trim().ToLower();

            return await _context.Usuarios
                .AnyAsync(u => u.Username.ToLower() == normalizedUsername);
        }

        public async Task<ClienteMascotaUsuarioResponseDTO> CrearClienteConMascotaAsync(ClienteMascotaUsuarioCreateDTO dto)
        {
            if (await UsernameExistsAsync(dto.Username))
            {
                throw new InvalidOperationException("El nombre de usuario ya existe.");
            }

            var rolCliente = await _context.Roles
                .FirstOrDefaultAsync(r => r.Nombre == "Cliente");

            if (rolCliente == null)
            {
                throw new InvalidOperationException("No existe el rol Cliente.");
            }

            var persona = new Personas
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Edad = dto.Edad,
                Sexo = dto.Sexo,
                Telefono = dto.Telefono
            };

            var usuario = new Usuario
            {
                Username = dto.Username.Trim(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RolId = rolCliente.Id,
                Persona = persona
            };

            var mascota = new Mascota
            {
                Nombre = dto.NombreMascota,
                Raza = dto.Raza,
                Color = dto.Color,
                Edad = dto.EdadMascota,
                Peso = dto.Peso,
                Persona = persona
            };

            _context.Personas.Add(persona);
            _context.Usuarios.Add(usuario);
            _context.Mascota.Add(mascota);

            await _context.SaveChangesAsync();

            return new ClienteMascotaUsuarioResponseDTO
            {
                PersonaId = persona.Id,
                UsuarioId = usuario.Id,
                MascotaId = mascota.ID,
                Username = usuario.Username,
                NombreCompleto = $"{persona.Nombre} {persona.Apellido}",
                NombreMascota = mascota.Nombre
            };
        }
    }
}
