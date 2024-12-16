using RestAPI.Database.Models;

namespace RestAPI.Interfaces
{
    public interface IDatabaseService
    {
        Task<Accessoire> AddAccessoire(Accessoire gear);
        Task<IEnumerable<Accessoire>> GetAccessoire(bool includeUpgradeTo = false);
        ValueTask<Accessoire?> GetAccessoire(uint id, bool includeUpgradeTo = false);
        Task<bool> AccessoireExist(uint id);
        Task<bool> AccessoireExist(string name);
        Task<Accessoire> UpdateAccessoire(Accessoire accessoire);
    }
}
