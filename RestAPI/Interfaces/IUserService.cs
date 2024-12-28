namespace RestAPI.Interfaces
{
    public interface IUserService
    {
        public Task AddUserAsync(User user);
        public Task<User?> GetUserAsync(string email, bool includeRole = false);
        public Task<User?> GetUserAsync(uint userId, bool includeRole = false);
        public Task<bool> UsernameExist(string username);        
        public Task<UserRole> GetUserRoleAsync(Roles role);

        public Task UpdateUserAsync(User user);
    }
}
