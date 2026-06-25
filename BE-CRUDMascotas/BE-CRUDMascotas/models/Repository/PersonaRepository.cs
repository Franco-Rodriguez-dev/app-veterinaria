
using BE_CRUDMascotas.models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BE_CRUDMascotas.models.Repository
{
    public class PersonaRepository : IPersonaRepository   
    {
        private readonly AplicationDbContext _context;

        public PersonaRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Personas>> GetListAsync()
        {
            return await _context.Personas
                .AsNoTracking()
                .Where(p => p.Activo)
                .Include(p => p.ListMascotas)
                .ToListAsync();
        }

        public async Task<Personas?> GetByIdAsync(int id, bool includeMascotas = false)
        {
            IQueryable<Personas> query = _context.Personas
                .AsNoTracking()
                .Where(p => p.Activo);

            if (includeMascotas)
                query = query.Include(per => per.ListMascotas);
                    
            return await query.FirstOrDefaultAsync(per => per.Id == id);    

        }


        public async Task<Personas> AddAsync(Personas persona)
        {
            _context.Personas.Add(persona); 
            await _context.SaveChangesAsync();
            return persona;
        }

        public async Task UpdateAsync(Personas persona)
        {
            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Personas persona)
        {
            var personaItem = await _context.Personas
                .Include(p => p.Usuario)
                .Include(p => p.ListMascotas)
                .FirstOrDefaultAsync(p => p.Id == persona.Id);

            if (personaItem == null)
                return;

            personaItem.Activo = false;

            if (personaItem.Usuario != null)
            {
                personaItem.Usuario.Activo = false;
            }

            foreach (var mascota in personaItem.ListMascotas)
            {
                mascota.Activo = false;
            }

            await _context.SaveChangesAsync();
        }

        // ============================
        // Métodos utilitarios
        // ============================

        public async Task<bool> ExistsAsync(int id)
        {
           return await _context.Personas.AnyAsync(p => p.Id == id);
            
        }


        





    }
}
