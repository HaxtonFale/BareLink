﻿using System;
using System.Diagnostics;
using BareLink.Models;
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
        private bool _active;
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

        public bool Active
        {
            get => _active;
            set => SetProperty(ref _active, value, onChanged: UpdateFilter);
        }

        public int FilterId
        {
            get => _filterId;
            set
            {
                _filterId = value;
                LoadFilterId(value);
            }
        }
        public async void LoadFilterId(int filterId)
        {
            try
            {
                var filter = await FiltersService.GetFilterAsync(filterId);
                Id = filter.Id;
                Name = filter.Name;
                Description = filter.Description;
                Pattern = filter.Pattern;
                Active = filter.Active;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Filter");
            }
        }

        private async void UpdateFilter()
        {
            try
            {
                var filter = new Filter
                {
                    Id = Id,
                    Name = Name,
                    Description = Description,
                    Pattern = Pattern,
                    Active = Active
                };
                await FiltersService.SaveFilterAsync(filter);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Save Filter");
            }
        }
    }
}