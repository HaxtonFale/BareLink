﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BareLink.Models;

namespace BareLink.Services
{
    public interface IFiltersService
    {
        Task<List<Filter>> GetFiltersAsync();
        Task<List<Filter>> GetEnabledFiltersAsync();
        Task<Filter> GetFilterAsync(int filterId);
        Task<int> SaveFilterAsync(Filter filter);
        Task<int> ImportJsonAsync(string importJson);
        Task<string> ExportJsonAsync();
        Task<int> DeleteFilterAsync(Filter filter);
        Task DeleteAllFiltersAsync();
    }
}