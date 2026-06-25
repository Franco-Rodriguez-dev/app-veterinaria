using BE_CRUDMascotas.models.DTO;
using BE_CRUDMascotas.models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BE_CRUDMascotas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("crear-usuario-con-mascota")]
        public async Task<ActionResult<ClienteMascotaUsuarioResponseDTO>> CrearUsuarioConMascota(
            [FromBody] ClienteMascotaUsuarioCreateDTO dto)
        {
            if (await _clienteRepository.UsernameExistsAsync(dto.Username))
                return BadRequest("El nombre de usuario ya existe.");

            try
            {
                var cliente = await _clienteRepository.CrearClienteConMascotaAsync(dto);
                return Ok(cliente);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("mi-perfil")]
        public async Task<ActionResult<MiPerfilClienteDTO>> GetMiPerfil()
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized("Token invalido.");

            var perfil = await _clienteRepository.GetMiPerfilAsync(userId.Value);

            if (perfil == null)
                return NotFound("No se encontro un perfil asociado al usuario logueado.");

            return Ok(perfil);
        }

        [Authorize(Roles = "Cliente")]
        [HttpGet("mi-mascota/{mascotaId}")]
        public async Task<ActionResult<MiPerfilMascotaDTO>> GetMiMascota(int mascotaId)
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized("Token invalido.");

            var mascota = await _clienteRepository.GetMiMascotaAsync(userId.Value, mascotaId);

            if (mascota == null)
                return NotFound("No se encontro la mascota asociada al cliente.");

            return Ok(mascota);
        }

        private int? GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(claim, out var userId) ? userId : null;
        }
    }
}
