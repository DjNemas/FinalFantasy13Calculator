using Homepage.Interfaces;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Homepage.Service
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RequestResponse<GetUserResponse>> GetUser(string bearerToken)
        {
            var response = new RequestResponse<GetUserResponse>();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            var result = await _httpClient.GetFromJsonAsync<GetUserResponse>("user");
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

        public async Task<RequestResponse<LoginResponse>> LoginAsync(string username, string password)
        {
            var response = new RequestResponse<LoginResponse>();

            var result = await _httpClient.PostAsJsonAsync("auth/login", new LoginRequest { Username = username, Password = password });
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
    }
}
