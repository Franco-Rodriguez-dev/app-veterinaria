using BCrypt.Net;
using BE_CRUDMascotas.models;
using BE_CRUDMascotas.models.DTO;
using BE_CRUDMascotas.models.Enums;
using BE_CRUDMascotas.models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_CRUDMascotas.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(
            AplicationDbContext context,
            IConfiguration configuration,
            IUsuarioRepository usuarioRepository)
        {
            _context = context;
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult> login([FromBody] LoginDTO dto)
        {
         //1.buscar usuario
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                //firstorfaultAsync es una herramienta´para obtener el primer resultado , si no existe devuelve null
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (usuario == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            if (!usuario.Activo)
            {
                return Unauthorized("El usuario esta dado de baja.");
            }

         //2.validar contraseña con BCrypt
            bool passwordValido = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash);

            if (!passwordValido) 
            { 
                return Unauthorized("Usuario o contraseña incorrecto");
            }

            //3. crear claims (info dentro del token)

            var claims = new[]
            {
                  new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                  new Claim(ClaimTypes.Name, usuario.Username),
                  new Claim(ClaimTypes.Role, usuario.Rol.Nombre)
            };

            // 4. clave secreta
            //En esta parte se obtiene la clave secreta desde la configuración
            //y se transforma en bytes para poder usarla en criptografía. Luego se crean las credenciales de firma
            //que indican con qué clave y qué algoritmo se va a firmar el JWT.

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );


            var creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

            //5. crear token
            //En esta parte se construye el JWT indicando quién lo emite, para quién es, qué información contiene,
            //cuándo expira y con qué credenciales se firma.

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: creds   
            );


            //6.respuesta
            //En la respuesta se devuelve un 200 OK con el token JWT serializado y
            //algunos datos del usuario para que el frontend pueda almacenarlos y utilizarlos sin hacer otra consulta.

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                usuario = usuario.Username,
                rol = usuario.Rol.Nombre,
                debeCambiarPassword = usuario.DebeCambiarPassword,

            });






        }

        [Authorize]
        [HttpPut("cambiar-password")]
        public async Task<IActionResult> CambiarPassword(CambiarPasswordDTO dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized("Token invalido.");

            var usuarioId = int.Parse(userIdClaim.Value);
            var resultado = await _usuarioRepository.CambiarPasswordAsync(usuarioId, dto);

            switch (resultado)
            {
                case CambiarPasswordResultado.Cambiada:
                    return Ok("Contraseña actualizada correctamente.");

                case CambiarPasswordResultado.UsuarioNoEncontrado:
                    return Unauthorized("Usuario no encontrado.");

                case CambiarPasswordResultado.UsuarioInactivo:
                    return BadRequest("El usuario esta dado de baja.");

                case CambiarPasswordResultado.PasswordActualIncorrecta:
                    return BadRequest("La contraseña actual es incorrecta.");

                default:
                    return BadRequest("No se pudo cambiar la contraseña.");
            }
        }




    }


}
