using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace BlazorAppUni.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private string _jwtToken = null;
        private string _username = null;
        public event Action? OnAuthStateChanged;
        
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string JwtToken => _jwtToken;
        public string Username => _username;
        public bool IsAuthenticated => !string.IsNullOrEmpty(_jwtToken);

        public async Task<bool> LoginAsync(string username, string password)
        {
            var loginModel = new { Username = username, Password = password };

            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                _jwtToken = result.token;
                _username = username;
                
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
                OnAuthStateChanged?.Invoke();
                return true;
            }

            return false;
        }

        public async Task<bool> RegisterAsync(string username, string email, string password, string role)
        {
            var registerModel = new { Username = username, Email = email, Password = password, Role = role };

            var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerModel);
            return response.IsSuccessStatusCode;
        }

        public void Logout()
        {
            _jwtToken = null;
            _username = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
            OnAuthStateChanged?.Invoke();
        }

        private class LoginResponse
        {
            public string token { get; set; }
            public DateTime expiration { get; set; }
        }
    }
}
