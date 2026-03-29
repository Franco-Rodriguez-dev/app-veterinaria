using AutoMapper;
using BE_CRUDMascotas.models;
using BE_CRUDMascotas.models.DTO;
using BE_CRUDMascotas.models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BE_CRUDMascotas.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IMapper _mapper;

        public PersonaController(IPersonaRepository personaRepository, IMapper mapper)
        {
            _personaRepository = personaRepository;
            _mapper = mapper;
        }

        // ============================================================
        //  GET: api/persona
        // Lista todas las personas (sin sus mascotas)
        // ============================================================
        
        [HttpGet]
        public async Task<ActionResult<List<PersonaListsDTO>>> GetAll()
        {
            var personas = await _personaRepository.GetListAsync();

            // Mapear entidad -> DTO (solo los datos básicos)
            var dto = _mapper.Map<List<PersonaListsDTO>>(personas);

            return Ok(dto); // 200 OK con la lista en formato JSON
        }

        // ============================================================
        //  GET: api/persona/{id}
        // Devuelve una persona por su ID, con su lista de mascotas
        // ============================================================
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaDetalleDTO>> GetById(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id, includeMascotas: true);

            if (persona == null)
                return NotFound($"No se encontró ninguna persona con el Id {id}");

            // Mapear entidad -> DTO de detalle (incluye mascotas)
            var dto = _mapper.Map<PersonaDetalleDTO>(persona);

            return Ok(dto);
        }

        // ============================================================
        //  POST: api/persona
        // Crea una nueva persona en la base de datos
        // ============================================================
        [HttpPost]
        public async Task<ActionResult<PersonaListsDTO>> Create(PersonaCreateDTO dto)
        {
            // Validación automática según las DataAnnotations del DTO
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1️⃣ DTO -> Entidad
            var persona = _mapper.Map<Personas>(dto);

            // 2️⃣ Guardar en base
            var creada = await _personaRepository.AddAsync(persona);

            // 3️⃣ Entidad -> DTO de respuesta
            var result = _mapper.Map<PersonaListsDTO>(creada);

            // 4️⃣ Devuelve 201 Created + la URL de la nueva persona
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // ============================================================
        //  PUT: api/persona/{id}
        // Actualiza una persona existente
        // ============================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PersonaUpdateDTO dto)
        {
            // Verificar si la persona existe antes de actualizar
            if (!await _personaRepository.ExistsAsync(id))
                return NotFound($"No se encontró la persona con ID {id}");

            // Validar modelo (por si algún campo no cumple las reglas)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1️⃣ DTO -> Entidad
            var persona = _mapper.Map<Personas>(dto);
            persona.Id = id; // asignar el ID que viene por ruta

            // 2️⃣ Actualizar en base de datos
            await _personaRepository.UpdateAsync(persona);

            // 3️⃣ No devolvemos datos (solo confirmación)
            return NoContent(); // 204 No Content
        }

        // ============================================================
        //  DELETE: api/persona/{id}
        // Elimina una persona de la base de datos
        // ============================================================
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Buscar la persona
            var persona = await _personaRepository.GetByIdAsync(id);

            if (persona == null)
                return NotFound($"No se encontró la persona con ID {id}");

            // Eliminar
            await _personaRepository.DeleteAsync(persona);

            // Confirmación sin contenido
            return NoContent();
        }

        




    }
}