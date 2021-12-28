using System.Collections.Generic;
using BareLink.Models;

namespace BareLink.Services
{
    public interface IFiltersService
    {
        List<Filter> GetFilters();
        List<Filter> GetActiveFilters();
        Filter GetFilter(int filterId);
        int SaveFilter(Filter filter);
        void ImportJson(string importJson);
        string ExportJson();
    }
}