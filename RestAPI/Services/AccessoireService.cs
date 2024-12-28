namespace RestAPI.Services
{
    public class AccessoireService : IAccessoireService
    {
        private readonly FFXIIIDbContext _db;

        public AccessoireService(FFXIIIDbContext db)
        {
            _db = db;
        }

        public async Task<Accessoire> AddAccessoire(Accessoire accessoire)
        {
            _db.Accessoires.Add(accessoire);
            await _db.SaveChangesAsync();
            return accessoire;
        }

        public async ValueTask<Accessoire?> GetAccessoire(uint id, bool includeUpgradeTo = false)
        => includeUpgradeTo ?
            await _db.Accessoires.Include(x => x.UpgradeToAccessoire).FirstOrDefaultAsync(x => x.Id == id) :
            await _db.Accessoires.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Accessoire>> GetAccessoire(bool includeUpgradeTo = false)
        {
            return includeUpgradeTo ?
            await _db.Accessoires.Include(x => x.UpgradeToAccessoire).ToListAsync() :
            await _db.Accessoires.ToListAsync();
        }

        public async Task<bool> AccessoireExist(uint id) => await _db.Accessoires.AnyAsync(x => x.Id == id);
        public async Task<bool> AccessoireExist(string name) => await _db.Accessoires.AnyAsync(x => x.Name == name);

        public async Task<Accessoire> UpdateAccessoire(Accessoire accessoire)
        {
            _db.Accessoires.Update(accessoire);
            await _db.SaveChangesAsync();
            await _db.Entry(accessoire).ReloadAsync();
            return accessoire;
        }
    }
}
