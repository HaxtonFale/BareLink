﻿using System.Threading.Tasks;
using BareLink.Models;

namespace BareLink.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        private readonly Filter _filter;

        public FilterViewModel(Filter filter)
        {
            _filter = filter;
        }

        public int Id => _filter.Id;

        public string Name => _filter.Name;

        public string Description => _filter.Description;

        public string ActiveGlyph => _filter.Active ? "\uef4f" : "\ueb32";

        public async Task<int> SaveAsync()
        {
            return await FiltersService.SaveFilterAsync(_filter);
        }

        public void Toggle()
        {
            _filter.Active = !_filter.Active;
            OnPropertyChanged(nameof(ActiveGlyph));
        }
    }
}