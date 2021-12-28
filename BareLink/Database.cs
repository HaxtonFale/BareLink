using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace BareLink
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Filter>().Wait();
        }

        public Task<List<Filter>> GetFiltersAsync()
        {
            return _database.Table<Filter>().ToListAsync();
        }

        public Task<int> SaveFilterAsync(Filter filter)
        {
            return _database.InsertAsync(filter);
        }
    }
}