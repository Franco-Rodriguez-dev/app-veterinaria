using Microsoft.EntityFrameworkCore;
// Entity Framework Core (EF Core):
// El DbContext es la clase principal que representa la conexión a la base de datos 
// y permite interactuar con ella.

namespace BE_CRUDMascotas.models
{
    public class AplicationDbContext : DbContext // ApplicationDbContext hereda de DbContext
    {
        // Constructor que recibe opciones (cadena de conexión, etc.)
        // EF Core necesita estas opciones para saber cómo conectarse a la base de datos.
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
        }


        // Representa las tablas en la base de datos
        public DbSet<Mascota> Mascota { get; set; }
        public DbSet<Personas> Personas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }




        // Configuración del modelo (relaciones, restricciones, etc.)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Guardar el enum Sexo como texto
            modelBuilder.Entity<Personas>()
             .Property(p => p.Sexo)
             .HasConversion<string>()
             .HasColumnType("nvarchar(20)");

            modelBuilder.Entity<Personas>()
                .Property(p => p.Telefono)
                .HasColumnType("nvarchar(20)");

            // Acá podrías agregar configuración adicional si lo necesitás
            // Por ejemplo: modelBuilder.Entity<Mascota>().ToTable("Mascotas");

            modelBuilder.Entity<Personas>().ToTable("Personas");

            modelBuilder.Entity<Mascota>()  //“Voy a configurar la entidad Mascota.”
                .HasOne(m => m.Persona) //“Cada Mascota tiene una Persona asociada.” gracias a esto ->public Personas Persona { get; set; }
                .WithMany( p => p.ListMascotas) //“Cada Persona puede tener muchas Mascotas.” gracias a esto ->public List<Mascota>? Mascotas { get; set; }
                .HasForeignKey(m => m.PersonaId) //Esto le dice a EF Core cuál es la clave foránea (FK) en la tabla Mascota.
                .OnDelete(DeleteBehavior.Restrict); //Esto configura qué pasa cuando se elimina una Persona en la base de datos. (Cascade,Restrict,SetNull)

            //“Si intento eliminar una Persona que aún tiene Mascotas registradas, el sistema me bloquea el borrado.”


            modelBuilder.Entity<Rol>().HasData(
        new Rol { Id = 1, Nombre = "Administrador" },
        new Rol { Id = 2, Nombre = "Cliente" }
            );


            modelBuilder.Entity<Usuario>().HasData(
               new Usuario
               {
                   Id = 1,
                   Username = "admin",
                   PasswordHash = "$2a$11$7g9hjhGe95vuvupJeCA4OemI8inSrfFFatgDwEoyxduWQJgyk3k6q", // hash de admin123
                   RolId = 1, // 1 = Administrador
                   FechaCreacion = DateTime.Now
               },
               new Usuario
               {
                   Id = 2,
                   Username = "user1",
                   PasswordHash = "$2a$11$3fDFszUwJ.lHFptWqc85Eu2JD8sxTEeYhawHeh9GiOcydUAReMkAe", // ← pegás el hash
                   RolId = 2,
                   FechaCreacion = DateTime.Now
               }
            );
        }
    }
}







