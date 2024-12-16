using Microsoft.EntityFrameworkCore;
using RestAPI.Database.Models;
using RestAPI.Interfaces;

namespace RestAPI.Services
{
    public partial class DatabaseService : IDatabaseService
    {
        public async Task<Accessoire> AddAccessoire(Accessoire accessoire)
        {
            _dbContext.Accessoires.Add(accessoire);
            await _dbContext.SaveChangesAsync();
            return accessoire;
        }

        public async ValueTask<Accessoire?> GetAccessoire(uint id, bool includeUpgradeTo = false) 
            => includeUpgradeTo ? 
            await _dbContext.Accessoires.Include(x => x.UpgradeToAccessoire).FirstOrDefaultAsync(x => x.Id == id) :
            await _dbContext.Accessoires.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Accessoire>> GetAccessoire(bool includeUpgradeTo = false) 
            => includeUpgradeTo ?
            await _dbContext.Accessoires.Include(x => x.UpgradeToAccessoire).ToListAsync() :
            await _dbContext.Accessoires.ToListAsync();

        public async Task<bool> AccessoireExist(uint id) => await _dbContext.Accessoires.AnyAsync(x => x.Id == id);
        public async Task<bool> AccessoireExist(string name) => await _dbContext.Accessoires.AnyAsync(x => x.Name == name);

        public async Task<Accessoire> UpdateAccessoire(Accessoire accessoire)
        {
            _dbContext.Accessoires.Update(accessoire);
            await _dbContext.SaveChangesAsync();
            await _dbContext.Entry(accessoire).ReloadAsync();
            return accessoire;
        }


    }
}
