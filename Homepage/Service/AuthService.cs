using System.Net;
using System.Net.Http.Headers;

namespace Homepage.Service
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RequestResponse<UserResponse>> GetUserAsync(string bearerToken)
        {
            var response = new RequestResponse<UserResponse>();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            var result = await _httpClient.GetFromJsonAsync<UserResponse>("user");
            if(result is null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                response.Message = "Unauthorized";
                response.Success = false;
            }
            else
            {
                response.Data = result;
            }
            return response;
        }

        public async Task<RequestResponse<LoginResponse>> LoginAsync(string email, string password)
        {
            var response = new RequestResponse<LoginResponse>();

            var result = await _httpClient.PostAsJsonAsync("auth/login", new LoginRequest { Email  = email, Password = password });
            if(!result.IsSuccessStatusCode)
            {
                response.StatusCode = result.StatusCode;
                response.Message = await result.Content.ReadAsStringAsync();
                response.Success = false;
            }
            else
            {
                response.Data = await result.Content.ReadFromJsonAsync<LoginResponse>();
            }
            return response;
        }

        public async Task<RequestResponse<string>> RegisterAsync(string email, string password)
        {
            var response = new RequestResponse<string>();

            var result = await _httpClient.PostAsJsonAsync("auth/register", new RegisterRequest { Email = email, Password = password });
            if (!result.IsSuccessStatusCode)
            {
                response.StatusCode = result.StatusCode;
                response.Message = await result.Content.ReadAsStringAsync();
                response.Success = false;
            }
            else
            {
                response.Data = await result.Content.ReadAsStringAsync();
            }
            return response;
        }
    }
}
