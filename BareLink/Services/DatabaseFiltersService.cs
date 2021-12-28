using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using BareLink.Models;
using Newtonsoft.Json;
using SQLite;

namespace BareLink.Services
{
    public class DatabaseFiltersService : IFiltersService
    {
        private SQLiteAsyncConnection _database;

        private SQLiteAsyncConnection DatabaseConnection
        {
            get
            {
                if (_database != null) return _database;
                File.Delete(DbPath);
                _database = new SQLiteAsyncConnection(DbPath);
                _ = Initialise();

                return _database;
            }
        }
        private static string DbPath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "filters.db3");

        public Task<List<Filter>> GetFiltersAsync()
        {
            return DatabaseConnection.Table<Filter>().ToListAsync();
        }

        public Task<List<Filter>> GetActiveFiltersAsync()
        {
            return DatabaseConnection.Table<Filter>().Where(f => f.Active).ToListAsync();
        }

        public Task<Filter> GetFilterAsync(int filterId)
        {
            return DatabaseConnection.Table<Filter>().FirstOrDefaultAsync(f => f.Id == filterId);
        }

        public Task<int> SaveFilterAsync(Filter filter)
        {
            return filter.Id == 0 ? DatabaseConnection.InsertAsync(filter) : DatabaseConnection.UpdateAsync(filter);
        }

        public Task<int> ImportJsonAsync(string importJson)
        {
            var import = JsonConvert.DeserializeObject<Filter[]>(importJson);
            if (import == null) return new Task<int>(() => 0);
            return _database.InsertAllAsync(import);
        }

        public async Task<string> ExportJsonAsync()
        {
            var filters = await GetFiltersAsync();
            return JsonConvert.SerializeObject(filters);
        }

        private async Task Initialise()
        {
            if (_database == null) return;
            var result = await _database.CreateTableAsync<Filter>();
            if (result != CreateTableResult.Created) return;
            using var resource = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("BareLink.Models.filters.default.json");
            if (resource == null) return;
            using var reader = new StreamReader(resource);
            var filters = await reader.ReadToEndAsync();
            await ImportJsonAsync(filters);
        }
    }
}