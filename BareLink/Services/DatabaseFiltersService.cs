using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BareLink.Models;
using Newtonsoft.Json;
using SQLite;

namespace BareLink.Services
{
    public class DatabaseFiltersService : IFiltersService
    {
        private SQLiteConnection _database;

        private SQLiteConnection DatabaseConnection
        {
            get
            {
                if (_database != null) return _database;
                File.Delete(DbPath);
                _database = new SQLiteConnection(DbPath);
                Initialise();

                return _database;
            }
        }
        private static string DbPath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "filters.db3");

        public List<Filter> GetFilters()
        {
            return DatabaseConnection.Table<Filter>().ToList();
        }

        public List<Filter> GetActiveFilters()
        {
            return DatabaseConnection.Table<Filter>().Where(f => f.Active).ToList();
        }

        public Filter GetFilter(int filterId)
        {
            return DatabaseConnection.Table<Filter>().FirstOrDefault(f => f.Id == filterId);
        }

        public int SaveFilter(Filter filter)
        {
            return filter.Id == 0 ? DatabaseConnection.Insert(filter) : DatabaseConnection.Update(filter);
        }

        public void ImportJson(string importJson)
        {
            var import = JsonConvert.DeserializeObject<Filter[]>(importJson);
            if (import != null)
                _database.InsertAll(import);
        }

        public string ExportJson()
        {
            var filters = GetFilters();
            return JsonConvert.SerializeObject(filters);
        }

        private void Initialise()
        {
            if (_database == null) return;
            var result = _database.CreateTable<Filter>();
            if (result != CreateTableResult.Created) return;
            using var resource = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("BareLink.defaultFilters.json");
            if (resource == null) return;
            using var reader = new StreamReader(resource);
            var filters = reader.ReadToEnd();
            ImportJson(filters);
        }
    }
}