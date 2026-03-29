using AutoMapper;
using BE_CRUDMascotas.models.DTO;
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
                                   select new { p, m }).ToListAsync();

                return lista.Select(x => new PersonaMascotaListDTO
                {
                    PersonaId = x.p.Id,
                    Nombre = x.p.Nombre,
                    Apellido = x.p.Apellido,
                    Telefono = x.p.Telefono,
                    MascotaId = x.m.ID,
                    NombreMascota = x.m.Nombre,
                    Raza = x.m.Raza,
                    Peso = x.m.Peso
                }).ToList();
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
                .FirstOrDefaultAsync(p => p.Id == personaId);

            if (persona == null)
                throw new Exception("Persona no encontrada");

            _context.Mascota.RemoveRange(persona.ListMascotas);
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateConMascotaAsync(int personaId, PersonaMascotaCreateDTO dto)
        {
            var persona = await _context.Personas
                .Include(p => p.ListMascotas)
                .FirstOrDefaultAsync(p => p.Id == personaId);

            if (persona == null)
                throw new Exception("Persona no encontrada");

            // Actualizar persona
            persona.Nombre = dto.Nombre;
            persona.Apellido = dto.Apellido;
            persona.Telefono = dto.Telefono;
            persona.Edad = dto.Edad;
            persona.Sexo = Enum.Parse<Sexo>(dto.Sexo, true); // 🔹 conversión string -> enum

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
                .FirstOrDefaultAsync(p => p.Id == id);

            if (persona == null) return null;

            var mascota = persona.ListMascotas.FirstOrDefault();

            return new PersonaMascotaCreateDTO
            {
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Telefono = persona.Telefono,
                Edad = persona.Edad,
                Sexo = persona.Sexo.ToString(),
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

    }
}
