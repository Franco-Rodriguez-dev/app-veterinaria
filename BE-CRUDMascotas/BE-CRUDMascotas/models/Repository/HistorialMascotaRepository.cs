using BE_CRUDMascotas.models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BE_CRUDMascotas.models.Repository
{
    public class HistorialMascotaRepository : IHistorialMascotaRepository
    {
        private readonly AplicationDbContext _context;

        public HistorialMascotaRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<HistorialMascotaDTO>> GetByMascotaAsync(int mascotaId)
        {
            return await _context.HistorialMascotas
                .Where(h => h.MascotaId == mascotaId && h.Activo)
                .OrderByDescending(h => h.Fecha)
                .Select(h => ToDto(h))
                .ToListAsync();
        }

        public async Task<HistorialMascotaDTO> GetByIdAsync(int id)
        {
            return await _context.HistorialMascotas
                .Where(h => h.Id == id && h.Activo)
                .Select(h => ToDto(h))
                .FirstOrDefaultAsync();
        }

        public async Task<HistorialMascotaDTO> AddAsync(HistorialMascotaCreateDTO dto, int? creadoPorUsuarioId)
        {
            var historial = new HistorialMascota
            {
                MascotaId = dto.MascotaId,
                Fecha = dto.Fecha,
                Tipo = dto.Tipo,
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Observaciones = dto.Observaciones,
                Precio = dto.Precio,
                ProximaVisita = dto.ProximaVisita,
                CreadoPorUsuarioId = creadoPorUsuarioId,
                FechaCreacion = DateTime.Now,
                Activo = true
            };

            _context.HistorialMascotas.Add(historial);
            await _context.SaveChangesAsync();

            return ToDto(historial);
        }

        public async Task<bool> UpdateAsync(int id, HistorialMascotaUpdateDTO dto)
        {
            var historial = await _context.HistorialMascotas
                .FirstOrDefaultAsync(h => h.Id == id && h.Activo);

            if (historial == null)
                return false;

            historial.Fecha = dto.Fecha;
            historial.Tipo = dto.Tipo;
            historial.Titulo = dto.Titulo;
            historial.Descripcion = dto.Descripcion;
            historial.Observaciones = dto.Observaciones;
            historial.Precio = dto.Precio;
            historial.ProximaVisita = dto.ProximaVisita;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var historial = await _context.HistorialMascotas
                .FirstOrDefaultAsync(h => h.Id == id && h.Activo);

            if (historial == null)
                return false;

            historial.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MascotaExistsAsync(int mascotaId)
        {
            return await _context.Mascota.AnyAsync(m => m.ID == mascotaId && m.Activo);
        }

        public async Task<bool> MascotaPerteneceAUsuarioAsync(int mascotaId, int usuarioId)
        {
            var usuario = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario?.PersonaId == null)
                return false;

            return await _context.Mascota
                .AnyAsync(m => m.ID == mascotaId && m.Activo && m.PersonaId == usuario.PersonaId.Value);
        }

        public async Task<int?> GetMascotaIdByHistorialAsync(int historialId)
        {
            return await _context.HistorialMascotas
                .Where(h => h.Id == historialId && h.Activo)
                .Select(h => (int?)h.MascotaId)
                .FirstOrDefaultAsync();
        }

        private static HistorialMascotaDTO ToDto(HistorialMascota historial)
        {
            return new HistorialMascotaDTO
            {
                Id = historial.Id,
                MascotaId = historial.MascotaId,
                Fecha = historial.Fecha,
                Tipo = historial.Tipo,
                Titulo = historial.Titulo,
                Descripcion = historial.Descripcion,
                Observaciones = historial.Observaciones,
                Precio = historial.Precio,
                ProximaVisita = historial.ProximaVisita,
                CreadoPorUsuarioId = historial.CreadoPorUsuarioId,
                FechaCreacion = historial.FechaCreacion,
                Activo = historial.Activo
            };
        }
    }
}
