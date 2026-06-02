namespace BE_CRUDMascotas.models.DTO
{
    public class ClienteMascotaUsuarioResponseDTO
    {
        public int PersonaId { get; set; }
        public int UsuarioId { get; set; }
        public int MascotaId { get; set; }
        public string Username { get; set; } = "";
        public string NombreCompleto { get; set; } = "";
        public string NombreMascota { get; set; } = "";
    }
}
