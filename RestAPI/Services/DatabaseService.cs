using RestAPI.Database;
using RestAPI.Database.Models;
using RestAPI.Interfaces;

namespace RestAPI.Services
{
    public partial class DatabaseService : IDatabaseService
    {
        private FFXIIIDbContext _dbContext;

        public DatabaseService(FFXIIIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
