using BE_CRUDMascotas.models.DTO;
using BE_CRUDMascotas.models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
