using BE_CRUDMascotas.models;
using BE_CRUDMascotas.models.DTO;
using BE_CRUDMascotas.models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BE_CRUDMascotas.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialMascotaController : ControllerBase
    {
        private readonly IHistorialMascotaRepository _repo;

        public HistorialMascotaController(IHistorialMascotaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("tipos")]
        public ActionResult<IEnumerable<string>> GetTipos()
        {
            return Ok(Enum.GetNames<TipoHistorialMascota>());
        }

        [HttpGet("mascota/{mascotaId}")]
        public async Task<ActionResult<List<HistorialMascotaDTO>>> GetByMascota(int mascotaId)
        {
            if (!await _repo.MascotaExistsAsync(mascotaId))
                return NotFound("No se encontro la mascota.");

            if (!UserIsAdmin() && !await UsuarioPuedeVerMascota(mascotaId))
                return Forbid();

            var historial = await _repo.GetByMascotaAsync(mascotaId);
            return Ok(historial);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialMascotaDTO>> GetById(int id)
        {
            var mascotaId = await _repo.GetMascotaIdByHistorialAsync(id);

            if (mascotaId == null)
                return NotFound("No se encontro el historial.");

            if (!UserIsAdmin() && !await UsuarioPuedeVerMascota(mascotaId.Value))
                return Forbid();

            var historial = await _repo.GetByIdAsync(id);
            return Ok(historial);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult<HistorialMascotaDTO>> Create(HistorialMascotaCreateDTO dto)
        {
            if (!await _repo.MascotaExistsAsync(dto.MascotaId))
                return NotFound("No se encontro la mascota.");

            var userId = GetUserId();
            var historial = await _repo.AddAsync(dto, userId);

            return CreatedAtAction(nameof(GetById), new { id = historial.Id }, historial);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HistorialMascotaUpdateDTO dto)
        {
            var updated = await _repo.UpdateAsync(id, dto);

            if (!updated)
                return NotFound("No se encontro el historial.");

            return NoContent();
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.SoftDeleteAsync(id);

            if (!deleted)
                return NotFound("No se encontro el historial.");

            return NoContent();
        }

        private bool UserIsAdmin()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value == "Administrador";
        }

        private int? GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(claim, out var userId) ? userId : null;
        }

        private async Task<bool> UsuarioPuedeVerMascota(int mascotaId)
        {
            var userId = GetUserId();

            if (userId == null)
                return false;

            return await _repo.MascotaPerteneceAUsuarioAsync(mascotaId, userId.Value);
        }
    }
}
