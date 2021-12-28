using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace BareLink.ViewModels
{
    [QueryProperty(nameof(FilterId), nameof(FilterId))]
    public class FilterDetailViewModel : BaseViewModel
    {
        private int _filterId;
        private string _name;
        private string _description;
        private string _pattern;
        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Pattern
        {
            get => _pattern;
            set => SetProperty(ref _pattern, value);
        }

        public int FilterId
        {
            get => _filterId;
            set
            {
                _filterId = value;
                LoadItemId(value);
            }
        }
        public async void LoadItemId(int filterId)
        {
            try
            {
                var filter = await FiltersService.GetFilterAsync(filterId);
                Id = filter.Id;
                Name = filter.Name;
                Description = filter.Description;
                Pattern = filter.Pattern;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}