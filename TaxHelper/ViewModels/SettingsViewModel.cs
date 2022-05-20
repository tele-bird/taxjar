using System;
using System.Collections.Generic;
using TaxHelper.Models;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private TaxHelper.Models.AppSettings mSettings;

        public string TaxJarApiKey
        {
            get
            {
                return mSettings?.TaxJarApiKey;
            }
            set
            {
                if(mSettings == null)
                {
                    mSettings = new AppSettings();
                }
                mSettings.TaxJarApiKey = value;
                SettingsService.Instance.Settings = mSettings;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel(INavigation navigation)
            : base(navigation)
        {
            mSettings = SettingsService.Instance.Settings;
        }
    }
}
