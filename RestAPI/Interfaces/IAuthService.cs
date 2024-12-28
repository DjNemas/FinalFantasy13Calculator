namespace RestAPI.Interfaces
{
    public interface IAuthService
    {
        public Task AddSessionAsync(Session session);
        public Task RemoveSessionAsync(Session session);
        public Task<Session?> GetSessionByBearerTokenAsync(string bearerToken, bool includeUser = false);
        public Task<Session?> GetSessionByResetTokenAsync(string resetToken, bool includeUser = false);
        public Session GenerateSession(User user);
        public Task UpdateSession(Session session);
        public Task HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
