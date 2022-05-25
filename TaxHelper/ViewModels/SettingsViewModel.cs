using TaxHelper.Services;

namespace TaxHelper.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public string TaxJarApiKey
        {
            get
            {
                return mAppSettingsService.Settings.TaxJarApiKey;
            }
            set
            {
                var appSettingsDto = mAppSettingsService.Settings;
                appSettingsDto.TaxJarApiKey = value;
                mAppSettingsService.Settings = appSettingsDto;
                OnPropertyChanged();
            }
        }

        private IAppSettingsService mAppSettingsService;

        public SettingsViewModel(IAlertHelper alertHelper, IAppSettingsService appSettingsService)
            : base(alertHelper)
        {
            mAppSettingsService = appSettingsService;
        }
    }
}
