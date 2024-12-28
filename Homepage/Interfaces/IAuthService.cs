namespace Homepage.Interfaces
{
    public interface IAuthService
    {
        public Task<RequestResponse<LoginResponse>> LoginAsync(string email, string password);

        public Task<RequestResponse<UserResponse>> GetUserAsync(string bearerToken);

        public Task<RequestResponse<string>> RegisterAsync(string email, string password);
    }
}
