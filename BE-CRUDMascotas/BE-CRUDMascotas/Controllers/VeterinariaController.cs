using BE_CRUDMascotas.models;
using BE_CRUDMascotas.models.DTO;
using BE_CRUDMascotas.models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class VeterinariaController : ControllerBase
{
    private readonly IVeterinariaRepository _repo;

    public VeterinariaController(IVeterinariaRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("listadoGeneral")]
    public async Task<ActionResult> GetListadoGeneral()
    {
        var lista = await _repo.GetListadoGeneralAsync();
        return Ok(lista);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost("crearConMascota")]
    public async Task<IActionResult> CrearConMascota(PersonaMascotaCreateDTO dto)
    {
        await _repo.CrearConMascotaAsync(dto);
        return Ok(new { message = "Persona y mascota creadas correctamente" });
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("eliminarConMascotas/{id}")]
    public async Task<IActionResult> DeleteConMascotas(int id)
    {
        await _repo.DeleteConMascotasAsync(id);
        return NoContent();
    }


    [Authorize(Roles = "Administrador")]
    [HttpPut("actualizarConMascota/{id}")]
    public async Task<IActionResult> UpdateConMascota(int id, PersonaMascotaCreateDTO dto)
    {
        // Obtener ID del usuario desde el token
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized("Token inválido");

        var userId = int.Parse(userIdClaim.Value);

        // Obtener rol
        var rol = User.FindFirst(ClaimTypes.Role)?.Value;

        // Si no es admin y quiere editar otra persona → prohibido
        if (rol != "Administrador" && userId != id)
            return Forbid();

        // Verificar que exista la persona antes de actualizar
        var existe = await _repo.GetPorIdAsync(id);
        if (existe == null)
            return NotFound("No se encontró la persona.");



        await _repo.UpdateConMascotaAsync(id, dto);

        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonaMascotaCreateDTO>> GetPorId(int id)
    {
        // Obtener el claim del usuario desde el token
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized("Token inválido");

        // Convertir el valor del claim a int
        var userId = int.Parse(userIdClaim.Value);

        // Obtener rol
        var rol = User.FindFirst(ClaimTypes.Role)?.Value;

        // Si no es admin y quiere ver otra persona → prohibido
        if (rol != "Administrador" && userId != id)
            return Forbid();

        var data = await _repo.GetPorIdAsync(id);

        if (data == null)
            return NotFound("No se encontró la persona.");

        return Ok(data);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut("reactivar-cliente/{personaId}")]
    public async Task<IActionResult> ReactivarClienteAsync(int personaId)
    {
        var resultado = await _repo.ReactivarClienteAsync(personaId);

        switch (resultado)
        {
            case ReactivarClienteResultado.Reactivado:
                return Ok("Cliente reactivado correctamente.");

            case ReactivarClienteResultado.NoEncontrado:
                return NotFound("No se encontro el cliente.");

            case ReactivarClienteResultado.YaActivo:
                return BadRequest("El cliente ya se encuentra activo.");

            case ReactivarClienteResultado.SinUsuario:
                return BadRequest("No se puede reactivar porque el cliente no tiene usuario asociado.");

            default:
                return BadRequest("No se pudo reactivar el cliente.");
        }

    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("clientes-inactivos")]
    public async Task<ActionResult<List<ClienteInactivoDTO>>> GetClientesInactivosAsync()
    {
        var clientesInactivos = await _repo.GetClientesInactivosAsync();

        return Ok(clientesInactivos);
    }

    [AllowAnonymous]
    [HttpGet("probar-error")]
    public IActionResult ProbarError()
    {
        throw new Exception("Esto es una prueba");
    }

}
