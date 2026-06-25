using System.Net;
using System.Net.Http.Json;
using BE_CRUDMascotas.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BE_CRUDMascotas.Tests.Integration;

public class SecurityAndBusinessRulesTests : IDisposable
{
    private readonly CustomWebApplicationFactory _factory = new();
    private readonly HttpClient _client;

    public SecurityAndBusinessRulesTests()
    {
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Login_UsuarioInactivo_DevuelveUnauthorized()
    {
        // Arrange
        var request = new
        {
            username = _factory.InactivoUsername,
            password = _factory.InactivoPlainPassword
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Auth/login", request);
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.Contains("dado de baja", body, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task MiPerfil_SinToken_DevuelveUnauthorized()
    {
        // Arrange & Act
        var response = await _client.GetAsync("/api/Cliente/mi-perfil");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task MascotaGeneral_ConTokenCliente_DevuelveForbidden()
    {
        // Arrange
        var token = await _client.LoginAndGetTokenAsync(_factory.ClienteUsername, _factory.ClientePlainPassword);
        _client.UseBearerToken(token);

        // Act
        var response = await _client.GetAsync("/api/Mascota");

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task MiPerfil_ConTokenCliente_DevuelvePerfilPropio()
    {
        // Arrange
        var token = await _client.LoginAndGetTokenAsync(_factory.ClienteUsername, _factory.ClientePlainPassword);
        _client.UseBearerToken(token);

        // Act
        var response = await _client.GetAsync("/api/Cliente/mi-perfil");
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Cliente", body);
        Assert.Contains("Toby", body);
    }

    [Fact]
    public async Task MiMascota_ConTokenClienteYMascotaAjena_DevuelveNotFound()
    {
        // Arrange
        var token = await _client.LoginAndGetTokenAsync(_factory.ClienteUsername, _factory.ClientePlainPassword);
        _client.UseBearerToken(token);

        // Act
        var response = await _client.GetAsync($"/api/Cliente/mi-mascota/{_factory.MascotaAjenaId}");
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.DoesNotContain("Luna", body);
    }

    [Fact]
    public async Task HistorialMascota_ConTokenClienteYMascotaAjena_DevuelveForbidden()
    {
        // Arrange
        var token = await _client.LoginAndGetTokenAsync(_factory.ClienteUsername, _factory.ClientePlainPassword);
        _client.UseBearerToken(token);

        // Act
        var response = await _client.GetAsync($"/api/HistorialMascota/mascota/{_factory.MascotaAjenaId}");
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        Assert.DoesNotContain("Vacuna", body);
    }

    [Fact]
    public async Task MascotaGeneral_ConTokenAdministrador_DevuelveMascota()
    {
        // Arrange
        var token = await _client.LoginAndGetTokenAsync(_factory.AdminUsername, _factory.AdminPlainPassword);
        _client.UseBearerToken(token);

        // Act
        var response = await _client.GetAsync($"/api/Mascota/{_factory.AdminMascotaId}");
        var body = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Toby", body);
    }

    [Fact]
    public async Task EliminarCliente_DesactivaPersonaUsuarioYMascotas_PeroMantieneHistoriales()
    {
        // Arrange
        var token = await _client.LoginAndGetTokenAsync(_factory.AdminUsername, _factory.AdminPlainPassword);
        _client.UseBearerToken(token);

        // Act
        var response = await _client.DeleteAsync($"/api/Veterinaria/eliminarConMascotas/{_factory.ClientePersonaId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();

        var persona = await context.Personas
            .Include(p => p.Usuario)
            .Include(p => p.ListMascotas)
            .FirstAsync(p => p.Id == _factory.ClientePersonaId);

        var historial = await context.HistorialMascotas
            .FirstAsync(h => h.MascotaId == _factory.ClienteMascotaId);

        Assert.False(persona.Activo);
        Assert.NotNull(persona.Usuario);
        Assert.False(persona.Usuario.Activo);
        Assert.All(persona.ListMascotas, mascota => Assert.False(mascota.Activo));
        Assert.True(historial.Activo);
    }

    public void Dispose()
    {
        _client.Dispose();
        _factory.Dispose();
    }
}
