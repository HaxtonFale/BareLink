using System;
using Xamarin.Forms;

namespace BareLink.Models
{
    public struct ThemeItem
    {
        public OSAppTheme Theme { get; }
        public string Name { get; }

        public ThemeItem(OSAppTheme theme)
        {
            Theme = theme;
            Name = theme switch
            {
                OSAppTheme.Unspecified => "Device Default",
                OSAppTheme.Light => "Light",
                OSAppTheme.Dark => "Dark",
                _ => throw new ArgumentOutOfRangeException(nameof(theme), theme, null)
            };
        }
    }
}