using System.ComponentModel.DataAnnotations;
/*te permite agregar reglas o metadatos a las propiedades de una clase usando atributos entre corchetes ([ ]).
Por ejemplo, sirve para:

Validar datos automáticamente.

Indicar que un campo es obligatorio.

Definir límites de longitud.

Especificar cómo mostrar un campo en formularios o vistas.*/
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_CRUDMascotas.models
{
   

    public class Personas
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(20, ErrorMessage = "El nombre no puede superar los 20 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(20, ErrorMessage = "El apellido no puede superar los 20 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios")]
        public string Apellido { get; set; } = "";

        [Range(1, 100, ErrorMessage = "La edad debe estar entre 1 y 100")]
        public int Edad { get; set; }

        [Required]
        public Sexo Sexo { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression(@"^(\+54|0)?[0-9]{8,15}$", ErrorMessage = "Ingrese un número de teléfono válido (solo dígitos, puede incluir +54 o 0)")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = "";

        // Relación 1:N con Mascotas
        [InverseProperty("Persona")]
        public List<Mascota> ListMascotas { get; set; } = new(); // inicializada para evitar null
    }
}