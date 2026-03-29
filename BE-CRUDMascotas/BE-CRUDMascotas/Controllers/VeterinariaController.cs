using BE_CRUDMascotas.models.DTO;
using BE_CRUDMascotas.models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPut("actualizarConMascota/{id}")]
    public async Task<IActionResult> UpdateConMascota(int id, PersonaMascotaCreateDTO dto)
    {
        var userId = int.Parse(User.FindFirst("Usuario")?.Value);
        var rol = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        // Si no es admin y quiere editar otra persona → prohibido
        if (rol != "Administrador" && userId != id)
        {
            return Forbid();
        }

        await _repo.UpdateConMascotaAsync(id, dto);
        return NoContent();
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonaMascotaCreateDTO>> GetPorId(int id)
    {
        if (!int.TryParse(User.FindFirst("Usuario")?.Value, out int userId))
        {
            return Unauthorized("Token no contiene Usuario válido");
        }
        var rol = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        if (rol != "Administrador" && userId != id)
            return Forbid();

        var data = await _repo.GetPorIdAsync(id);
        if (data == null) return NotFound("No se encontró la persona.");
        return Ok(data);
    }

    [AllowAnonymous]
    [HttpGet("probar-error")]
    public IActionResult ProbarError()
    {
        throw new Exception("Esto es una prueba");
    }

}