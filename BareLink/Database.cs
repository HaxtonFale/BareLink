using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using SQLite;

namespace BareLink
{
    public static class Database
    {
        private static SQLiteConnection _database;

        private static SQLiteConnection DatabaseConnection
        {
            get
            {
                if (_database != null) return _database;
                _database = new SQLiteConnection(DbPath);
                Initialise();

                return _database;
            }
        }
        private static string DbPath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "filters.db3");

        public static List<Filter> GetFilters()
        {
            return DatabaseConnection.Table<Filter>().ToList();
        }

        public static List<Filter> GetActiveFilters()
        {
            return DatabaseConnection.Table<Filter>().Where(f => f.Active).ToList();
        }

        public static Filter GetFilter(int filterId)
        {
            return DatabaseConnection.Table<Filter>().FirstOrDefault(f => f.Id == filterId);
        }

        public static int SaveFilter(Filter filter)
        {
            return filter.Id == 0 ? DatabaseConnection.Insert(filter) : DatabaseConnection.Update(filter);
        }

        public static void Import(string importJson)
        {
            var import = JsonConvert.DeserializeObject<Filter[]>(importJson);
            if (import != null)
                _database.InsertAll(import);
        }

        public static string Export()
        {
            var filters = GetFilters();
            return JsonConvert.SerializeObject(filters);
        }

        private static void Initialise()
        {
            if (_database == null) return;
            var result = _database.CreateTable<Filter>();
            if (result != CreateTableResult.Created) return;
            using var resource = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("BareLink.defaultFilters.json");
            if (resource == null) return;
            using var reader = new StreamReader(resource);
            var filters = reader.ReadToEnd();
            Import(filters);
        }
    }
}