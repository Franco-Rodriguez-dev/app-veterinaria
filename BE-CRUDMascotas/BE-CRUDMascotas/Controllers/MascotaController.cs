using AutoMapper;
using BE_CRUDMascotas.models;
using BE_CRUDMascotas.models.DTO;
using BE_CRUDMascotas.models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_CRUDMascotas.Controllers
{
    [Route("api/[controller]")] //El token [controller] se reemplaza por el nombre del controlador sin la palabra Controller.
    [ApiController]//Hereda funcionalidades básicas para API, como devolver JSON, códigos HTTP (Ok(), NotFound(), BadRequest(), etc.)
    public class MascotaController : ControllerBase//La clase hereda de ControllerBase.
    {
       
        private readonly IMapper _mapper;
        private readonly IMascotaRepository _mascotaRepository; 


        /*el constructor:ASP.NET Core, gracias a Dependency Injection (DI), automáticamente crea un AppDbContext y lo pasa como parámetro.
        //Vos lo guardás en _context para poder usarlo en los métodos del controlador.
        //EN OTRAS PALABRAS ESTO ES UN CONTRUCTOR DE LA CLASS MascotaController*/
        public MascotaController( IMapper mapper , IMascotaRepository mascotaRepository )
        {
            
            _mapper = mapper;   
            _mascotaRepository = mascotaRepository; 
        }

        /*¿Por qué casi siempre se usa async Task<IActionResult>?

async + Task

Hoy en día, la mayoría de las APIs trabajan con bases de datos, archivos o servicios externos.

Esas operaciones tardan tiempo, y async/await permite que el servidor no se bloquee mientras espera.

Por eso, la convención es usar métodos asíncronos (async Task<...>).

IActionResult

Te permite devolver diferentes respuestas HTTP (Ok, NotFound, BadRequest, etc.).

Es flexible porque un mismo método puede devolver distintos resultados según lo que pase.*/
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                
                var LisMascota = await _mascotaRepository.GetListMascota();

                var LisMascotaDto = _mapper.Map<IEnumerable<MascotaDTO>>(LisMascota);

                return Ok(LisMascota);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var mascota = await _mascotaRepository.GetMascota(id);

                if (mascota == null)
                {
                    return NotFound();
                }

                var mascotaDto = _mapper.Map<MascotaDTO>(mascota);


                return Ok(mascotaDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var mascota = await _mascotaRepository.GetMascota(id);
                if (mascota == null)
                {
                    return NotFound();
                }


                await _mascotaRepository.DeleteMascota(mascota);

                return NoContent();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }



        }

        [HttpPost]
        public async Task<IActionResult> post(MascotaDTO mascotaDTO)
        {
            try
            {
                var mascota = _mapper.Map<Mascota>(mascotaDTO);

                mascota.FechaCreacion = DateTime.Now;
                
                 mascota = await _mascotaRepository.AddMascota(mascota);

                var mascotaItemDto = _mapper.Map<MascotaDTO>(mascota);

                return CreatedAtAction("Get", new { id = mascotaItemDto.ID }, mascotaDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> put(int id, MascotaDTO mascotaDTO)
        {
            try
            {
                var mascota = _mapper.Map<Mascota>(mascotaDTO);

                if (id != mascota.ID)
                {
                    return BadRequest();
                }

                var mascotaItem = await _mascotaRepository.GetMascota(id);

                if (mascotaItem == null)
                {
                    return NotFound();
                }


                await _mascotaRepository.UpdateMascota(mascota);

                return NoContent();



            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }











    }







}
