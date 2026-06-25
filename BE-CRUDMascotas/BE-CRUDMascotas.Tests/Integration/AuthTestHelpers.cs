using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BE_CRUDMascotas.Tests.Integration;

public static class AuthTestHelpers
{
    public static async Task<string> LoginAndGetTokenAsync(
        this HttpClient client,
        string username,
        string password)
    {
        var response = await client.PostAsJsonAsync("/api/Auth/login", new
        {
            username,
            password
        });

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(json);

        return document.RootElement.GetProperty("token").GetString()
            ?? throw new InvalidOperationException("La respuesta de login no devolvio token.");
    }

    public static void UseBearerToken(this HttpClient client, string token)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
