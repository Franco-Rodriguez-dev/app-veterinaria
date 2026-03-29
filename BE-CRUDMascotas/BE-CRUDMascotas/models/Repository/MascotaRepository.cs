
using Microsoft.EntityFrameworkCore;

namespace BE_CRUDMascotas.models.Repository
{
    public class MascotaRepository: IMascotaRepository
    {
        private readonly AplicationDbContext _context;//Eso significa: “quiero guardar una referencia al AplicationDbContext dentro de mi controlador, y que sea solo de lectura (readonly)”

        public MascotaRepository(AplicationDbContext context)
        {
            _context = context;

        }

        public async Task<Mascota> AddMascota(Mascota mascota)
        {
            _context.Add(mascota);
            await _context.SaveChangesAsync();
            return mascota;
        }

        public async Task DeleteMascota(Mascota mascota)
        {
            _context.Mascota.Remove(mascota);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Mascota>> GetListMascota()
        {
           return await _context.Mascota.ToListAsync();
        }

        public async Task<Mascota> GetMascota(int id)
        {
           return await _context.Mascota.FindAsync(id);

           
        }

        public async Task UpdateMascota(Mascota mascota)
        {
            var mascotaItem = await _context.Mascota.FirstOrDefaultAsync(x => x.ID == mascota.ID);

            if (mascotaItem != null)
            {

                mascotaItem.Nombre = mascota.Nombre;
                mascotaItem.Edad = mascota.Edad;
                mascotaItem.Raza = mascota.Raza;
                mascotaItem.Peso = mascota.Peso;
                mascotaItem.Color = mascota.Color;

                await _context.SaveChangesAsync();
            }
        }
    }
}
