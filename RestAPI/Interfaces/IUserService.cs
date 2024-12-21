namespace RestAPI.Interfaces
{
    public interface IUserService
    {
        public Task AddUserAsync(User user);
        public Task<User?> GetUserAsync(string username, bool includeRole = false);
        public Task<User?> GetUserAsync(uint userId, bool includeRole = false);
        public Task<UserRole> GetUserRoleAsync(Roles role);
    }
}
