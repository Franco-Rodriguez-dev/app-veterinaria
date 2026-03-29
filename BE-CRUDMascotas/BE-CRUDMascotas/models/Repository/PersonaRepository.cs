
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
                .Include(p => p.ListMascotas)
                .ToListAsync();
        }

        public async Task<Personas?> GetByIdAsync(int id, bool includeMascotas = false)
        {
            IQueryable<Personas> query = _context.Personas.AsNoTracking();

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
            _context.Personas.Remove(persona);
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
