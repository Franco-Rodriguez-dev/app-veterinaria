using BE_CRUDMascotas.models.DTO;
using BE_CRUDMascotas.models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BE_CRUDMascotas.models.Enums;

namespace BE_CRUDMascotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController (IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository; 
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("restablecer-password")]
        public async Task<IActionResult> RestablecerPasswordAsync(RestablecerPasswordDTO dto)
        {
            var resultado = await _usuarioRepository.RestablecerPasswordAsync(dto);

            switch (resultado)
            {
                case RestablecerPasswordResultado.Restablecido:
                    return Ok("Contraseña restablecida correctamente.");

                case RestablecerPasswordResultado.NoEncontrado:
                    return NotFound("No se encontro el usuario.");

                case RestablecerPasswordResultado.UsuarioInactivo:
                    return BadRequest("No se puede restablecer la contraseña de un usuario dado de baja.");

                default:
                    return BadRequest("No se pudo restablecer la contraseña ");
            }

        }










    }
}
