using System.Text.Json;
using UniversityBlogApp.Client.Models;

namespace UniversityBlogApp.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private string? _token;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string?> Login(LoginDto dto)
        {
            var res = await _http.PostAsJsonAsync("https://localhost:6001/api/user/login", dto);
            if (!res.IsSuccessStatusCode)
                return null;

            var json = await res.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            _token = doc.RootElement.GetProperty("token").GetString();
            return _token;
        }

        public async Task<bool> Register(RegisterDto dto)
        {
            var res = await _http.PostAsJsonAsync("https://localhost:6001/api/user/register", dto);
            return res.IsSuccessStatusCode;
        }

        public string? GetToken() => _token;
    }
}
