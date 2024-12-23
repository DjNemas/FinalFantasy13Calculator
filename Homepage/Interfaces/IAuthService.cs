namespace Homepage.Interfaces
{
    public interface IAuthService
    {
        public Task<RequestResponse<LoginResponse>> LoginAsync(string username, string password);

        public Task<RequestResponse<GetUserResponse>> GetUser(string bearerToken);
    }
}
