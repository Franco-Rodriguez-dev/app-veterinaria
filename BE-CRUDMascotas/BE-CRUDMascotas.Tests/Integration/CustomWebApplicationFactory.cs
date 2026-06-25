using BE_CRUDMascotas.models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BE_CRUDMascotas.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private const string AdminPassword = "Admin123!";
    private const string ClientePassword = "Cliente123!";
    private const string InactivoPassword = "Inactivo123!";

    private readonly SqliteConnection _connection = new("DataSource=:memory:");

    public int AdminMascotaId => 400;
    public int ClienteMascotaId => 400;
    public int MascotaAjenaId => 401;
    public int ClientePersonaId => 300;

    public string AdminUsername => "admin.test";
    public string AdminPlainPassword => AdminPassword;
    public string ClienteUsername => "cliente.a";
    public string ClientePlainPassword => ClientePassword;
    public string InactivoUsername => "cliente.inactivo";
    public string InactivoPlainPassword => InactivoPassword;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Key"] = "ClaveSuperSecreta123456789ABCDEF",
                ["Jwt:Issuer"] = "BE_CRUDMascotas",
                ["Jwt:Audience"] = "BE_CRUDMascotasUsers",
                ["Jwt:ExpireMinutes"] = "60"
            });
        });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<AplicationDbContext>>();
            services.AddSingleton(_connection);
            services.AddDbContext<AplicationDbContext>(options => options.UseSqlite(_connection));

            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();

            _connection.Open();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Seed(context);
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _connection.Dispose();
    }

    private static void Seed(AplicationDbContext context)
    {
        context.HistorialMascotas.RemoveRange(context.HistorialMascotas);
        context.Mascota.RemoveRange(context.Mascota);
        context.Usuarios.RemoveRange(context.Usuarios);
        context.Personas.RemoveRange(context.Personas);
        context.Roles.RemoveRange(context.Roles);
        context.SaveChanges();

        var adminRol = new Rol { Id = 1, Nombre = "Administrador" };
        var clienteRol = new Rol { Id = 2, Nombre = "Cliente" };

        var personaCliente = new Personas
        {
            Id = 300,
            Nombre = "Cliente",
            Apellido = "Uno",
            Edad = 30,
            Sexo = Sexo.Masculino,
            Telefono = "1122334455",
            Activo = true
        };

        var personaAjena = new Personas
        {
            Id = 301,
            Nombre = "Cliente",
            Apellido = "Dos",
            Edad = 28,
            Sexo = Sexo.Femenino,
            Telefono = "1199887766",
            Activo = true
        };

        var personaInactiva = new Personas
        {
            Id = 302,
            Nombre = "Cliente",
            Apellido = "Inactivo",
            Edad = 40,
            Sexo = Sexo.Otro,
            Telefono = "1155667788",
            Activo = false
        };

        var admin = new Usuario
        {
            Id = 200,
            Username = "admin.test",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(AdminPassword),
            RolId = adminRol.Id,
            Activo = true
        };

        var cliente = new Usuario
        {
            Id = 201,
            Username = "cliente.a",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(ClientePassword),
            RolId = clienteRol.Id,
            PersonaId = personaCliente.Id,
            Activo = true
        };

        var clienteAjeno = new Usuario
        {
            Id = 202,
            Username = "cliente.b",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(ClientePassword),
            RolId = clienteRol.Id,
            PersonaId = personaAjena.Id,
            Activo = true
        };

        var usuarioInactivo = new Usuario
        {
            Id = 203,
            Username = "cliente.inactivo",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(InactivoPassword),
            RolId = clienteRol.Id,
            PersonaId = personaInactiva.Id,
            Activo = false
        };

        var mascotaCliente = new Mascota
        {
            ID = 400,
            Nombre = "Toby",
            Raza = "Caniche",
            Color = "Blanco",
            Edad = 4,
            Peso = 8.5f,
            PersonaId = personaCliente.Id,
            Activo = true
        };

        var mascotaAjena = new Mascota
        {
            ID = 401,
            Nombre = "Luna",
            Raza = "Mestiza",
            Color = "Negro",
            Edad = 6,
            Peso = 12.3f,
            PersonaId = personaAjena.Id,
            Activo = true
        };

        var historialCliente = new HistorialMascota
        {
            Id = 500,
            MascotaId = mascotaCliente.ID,
            Fecha = new DateTime(2026, 6, 1),
            Tipo = TipoHistorialMascota.Consulta,
            Titulo = "Control general",
            Descripcion = "Control anual",
            Observaciones = "Sin novedades",
            Precio = 1500,
            Activo = true
        };

        var historialAjeno = new HistorialMascota
        {
            Id = 501,
            MascotaId = mascotaAjena.ID,
            Fecha = new DateTime(2026, 6, 2),
            Tipo = TipoHistorialMascota.Vacuna,
            Titulo = "Vacuna",
            Descripcion = "Vacuna anual",
            Observaciones = "Aplicada",
            Precio = 2500,
            Activo = true
        };

        context.Roles.AddRange(adminRol, clienteRol);
        context.Personas.AddRange(personaCliente, personaAjena, personaInactiva);
        context.Usuarios.AddRange(admin, cliente, clienteAjeno, usuarioInactivo);
        context.Mascota.AddRange(mascotaCliente, mascotaAjena);
        context.HistorialMascotas.AddRange(historialCliente, historialAjeno);
        context.SaveChanges();
    }
}
