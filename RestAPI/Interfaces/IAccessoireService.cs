namespace RestAPI.Interfaces
{
    public interface IAccessoireService
    {
        public Task<Accessoire> AddAccessoire(Accessoire accessoire);

        public ValueTask<Accessoire?> GetAccessoire(uint id, bool includeUpgradeTo = false);

        public Task<IEnumerable<Accessoire>> GetAccessoire(bool includeUpgradeTo = false);

        public Task<bool> AccessoireExist(uint id);
        public Task<bool> AccessoireExist(string name);

        public Task<Accessoire> UpdateAccessoire(Accessoire accessoire);
    }
}
