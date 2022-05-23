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
                return mAppSettingsService.Settings.TaxJarApiKey;
            }
            set
            {
                mAppSettingsService.Settings.TaxJarApiKey = value;
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
