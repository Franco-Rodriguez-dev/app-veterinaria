using AutoMapper;
using BE_CRUDMascotas.models.DTO;
using BE_CRUDMascotas.models;
using Microsoft.EntityFrameworkCore;


namespace BE_CRUDMascotas.models.Repository
{
    public class VeterinariaRepository : IVeterinariaRepository
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        public VeterinariaRepository(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // 🔹 Obtener listado general (con mapeo)
        public async Task<List<PersonaMascotaListDTO>> GetListadoGeneralAsync()
        {
            try
            {
                var lista = await (from p in _context.Personas
                                   join m in _context.Mascota on p.Id equals m.PersonaId
                                   where p.Activo && m.Activo
                                   select new PersonaMascotaListDTO
                {
                    PersonaId = p.Id,
                    UsuarioId = p.Usuario != null ? p.Usuario.Id : null,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Telefono = p.Telefono,
                    MascotaId = m.ID,
                    NombreMascota = m.Nombre,
                    Raza = m.Raza,
                    Peso = m.Peso
                }).ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrió un error al obtener el listado general de personas con mascotas.", ex);
            }
        }

        // 🔹 Crear persona + mascota
        public async Task CrearConMascotaAsync(PersonaMascotaCreateDTO dto)
        {
            // Automáticamente convierte el DTO en entidades
            var persona = _mapper.Map<Personas>(dto);
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            var mascota = _mapper.Map<Mascota>(dto.Mascota);
            mascota.PersonaId = persona.Id;

            _context.Mascota.Add(mascota);
            await _context.SaveChangesAsync();
        }

        // 🔹 Eliminar persona + mascotas
        public async Task DeleteConMascotasAsync(int personaId)
        {
            var persona = await _context.Personas
                .Include(p => p.ListMascotas)
                    .ThenInclude(m => m.Historiales)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.Id == personaId);

            if (persona == null)
                throw new Exception("Persona no encontrada");

            persona.Activo = false;

            if (persona.Usuario != null)
            {
                persona.Usuario.Activo = false;
            }

            foreach (var mascota in persona.ListMascotas)
            {
                mascota.Activo = false;

                foreach (var historial in mascota.Historiales)
                {
                    historial.Activo = false;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateConMascotaAsync(int personaId, PersonaMascotaCreateDTO dto)
        {
            var persona = await _context.Personas
                .Include(p => p.ListMascotas)
                .FirstOrDefaultAsync(p => p.Id == personaId && p.Activo);

            if (persona == null)
                throw new Exception("Persona no encontrada");

            // Actualizar persona
            persona.Nombre = dto.Nombre;
            persona.Apellido = dto.Apellido;
            persona.Telefono = dto.Telefono;
            persona.Edad = dto.Edad;
            persona.Sexo = dto.Sexo;

            // Actualizar mascota principal (asumimos una)
            var mascota = persona.ListMascotas.FirstOrDefault();
            if (mascota != null)
            {
                mascota.Nombre = dto.Mascota.Nombre;
                mascota.Raza = dto.Mascota.Raza;
                mascota.Color = dto.Mascota.Color;
                mascota.Edad = dto.Mascota.Edad;
                mascota.Peso = dto.Mascota.Peso; //mascota.Peso = (float)dto.Mascota.Peso; // 🔹 conversión double -> float
            }

            await _context.SaveChangesAsync();
        }

        public async Task<PersonaMascotaCreateDTO> GetPorIdAsync(int id)
        {
            var persona = await _context.Personas
                .Include(p => p.ListMascotas)
                .FirstOrDefaultAsync(p => p.Id == id && p.Activo);

            if (persona == null) return null;

            var mascota = persona.ListMascotas.FirstOrDefault();

            return new PersonaMascotaCreateDTO
            {
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Telefono = persona.Telefono,
                Edad = persona.Edad,
                Sexo = persona.Sexo,
                Mascota = new MascotaCreateDTO
                {
                    Nombre = mascota?.Nombre,
                    Raza = mascota?.Raza,
                    Color = mascota?.Color,
                    Edad = mascota?.Edad ?? 0,
                    Peso = mascota?.Peso ?? 0
                }
            };
        }
       public async Task<List<ClienteInactivoDTO>> GetClientesInactivosAsync()
        {
            var ClientesInactivos = await _context.Personas
                .Include(p => p.Usuario)
                .Include(p => p.ListMascotas)
                .Where(p => !p.Activo)
                .Where(p => p.Usuario != null)
                .Select(p => new ClienteInactivoDTO
                {
                    PersonaId = p.Id,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Telefono = p.Telefono,
                    Username = p.Usuario.Username,
                    CantidadMascotas = p.ListMascotas.Count

                })
                .ToListAsync();
            return ClientesInactivos;

                
        }

        public async Task<ReactivarClienteResultado> ReactivarClienteAsync(int personaId)
        {
            var persona = await _context.Personas
                .Include(p => p.Usuario)
                .Include(p => p.ListMascotas)
                   .ThenInclude(m => m.Historiales)
                 .FirstOrDefaultAsync(p => p.Id == personaId);

            if (persona == null)
                return ReactivarClienteResultado.NoEncontrado;

            if (persona.Activo)
                return ReactivarClienteResultado.YaActivo;

            if (persona.Usuario == null)
                return ReactivarClienteResultado.SinUsuario;

            persona.Activo = true;
            persona.Usuario.Activo = true;

            foreach (var mascota in persona.ListMascotas)
            {
                mascota.Activo = true;

                foreach (var historial in mascota.Historiales)
                {
                    historial.Activo = true;
                }
            }

            await _context.SaveChangesAsync();
            return ReactivarClienteResultado.Reactivado;
        }


    }

    
}
