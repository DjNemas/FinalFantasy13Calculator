namespace RestAPI.Services
{
    public class UserService : IUserService
    {
        private readonly FFXIIIDbContext _db;

        public UserService(FFXIIIDbContext db)
        {
            _db = db;
        }
        public async Task AddUserAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            await _db.Entry(user).ReloadAsync();
        }

        public async Task<User?> GetUserAsync(string email, bool includeRole = false)
        {
            var user = includeRole?
                await _db.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower()) :
                await _db.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            if(user is not null)
                await _db.Entry(user).ReloadAsync();
            return user;
        }
            

        public async Task<User?> GetUserAsync(uint userId, bool includeRole = false)
        {
            var user = includeRole ?
                await _db.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == userId) :
                await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is not null)
                await _db.Entry(user).ReloadAsync();
            return user;
        }

        public async Task<UserRole> GetUserRoleAsync(Roles role) => await _db.UserRoles.FirstAsync(x => x.Role == role);

        public async Task UpdateUserAsync(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }

        public Task<bool> UsernameExist(string username)
        {
            return _db.Users.AnyAsync(x => x.Username == username);
        }
    }
}
