using BE_CRUDMascotas.models;

namespace BE_CRUDMascotas.models.DTO
{
    public class HistorialMascotaDTO
    {
        public int Id { get; set; }
        public int MascotaId { get; set; }
        public DateTime Fecha { get; set; }
        public TipoHistorialMascota Tipo { get; set; }
        public string Titulo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Observaciones { get; set; }
        public decimal? Precio { get; set; }
        public DateTime? ProximaVisita { get; set; }
        public int? CreadoPorUsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}
