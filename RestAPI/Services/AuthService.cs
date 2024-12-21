using RestAPI.Database.Models;
using System.Security.Cryptography;

namespace RestAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly FFXIIIDbContext _db;

        public AuthService(FFXIIIDbContext db)
        {
            _db = db;
        }

        public async Task AddSessionAsync(Session session)
        {
            _db.Sessions.Add(session);
            await _db.SaveChangesAsync();
        }

        public async Task<Session?> GetSessionByBearerTokenAsync(string bearerToken, bool includeUser = false)
        {
            var session = includeUser ?
                await _db.Sessions.Include(x => x.User).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.BearerToken == bearerToken) :
                await _db.Sessions.FirstOrDefaultAsync(x => x.BearerToken == bearerToken);
            if (session is not null)
                await _db.Entry(session).ReloadAsync();
            return session;
        }

        public async Task<Session?> GetSessionByResetTokenAsync(string resetToken, bool includeUser = false)
        {
            var session = includeUser ?
                await _db.Sessions.Include(x => x.User).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.ResetToken == resetToken) :
                await _db.Sessions.FirstOrDefaultAsync(x => x.ResetToken == resetToken);
            if (session is not null)
                await _db.Entry(session).ReloadAsync();
            return session;
        }

        public Session GenerateSession(User user)
            => new Session()
                {
                    User = user,
                    BearerToken = Generator.GenerateRandomString(512),
                    ResetToken = Generator.GenerateRandomString(512)
                };

        public Task HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            return Task.CompletedTask;
        }

        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var compareHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return compareHash.SequenceEqual(passwordHash);
            }
        }

        public async Task RemoveSessionAsync(Session session)
        {
            _db.Sessions.Remove(session);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateSession(Session session)
        {
            _db.Update(session);
            await _db.SaveChangesAsync();
        }
    }
}
