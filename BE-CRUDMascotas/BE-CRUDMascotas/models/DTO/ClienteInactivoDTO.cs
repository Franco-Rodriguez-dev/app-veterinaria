namespace BE_CRUDMascotas.models.DTO
{
    public class ClienteInactivoDTO
    {
        public int PersonaId { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Telefono { get; set; } = "";  
        public string Username { get; set; } = "";
        public int CantidadMascotas { get; set; }   
    }
}
