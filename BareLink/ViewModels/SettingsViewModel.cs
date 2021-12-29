namespace BareLink.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private string _appTheme;

        public enum Theme
        {
            DeviceDefault,
            Light,
            Dark
        }

        public string AppTheme
        {
            get => _appTheme;
            set => SetProperty(ref _appTheme, value, onChanged: UpdateTheme);
        }

        private void UpdateTheme()
        {
            
        }
    }
}