using System;
using TaxHelper.Dto;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public string TaxJarApiKey
        {
            get
            {
                var appSettingsDto = mAppSettingsService.Settings;
                return appSettingsDto.TaxJarApiKey;
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

        public SettingsViewModel(INavigationProvider navigationProvider, IAppSettingsService appSettingsService)
            : base(navigationProvider)
        {
            mAppSettingsService = appSettingsService;
        }
    }
}
