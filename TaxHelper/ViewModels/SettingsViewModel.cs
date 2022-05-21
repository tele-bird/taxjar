using TaxHelper.Dto;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private AppSettingsDto mSettings;

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
                    mSettings = new AppSettingsDto();
                }
                mSettings.TaxJarApiKey = value;
                SettingsService<AppSettingsDto>.Instance.Settings = mSettings;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel(INavigation navigation)
            : base(navigation)
        {
            mSettings = SettingsService<AppSettingsDto>.Instance.Settings;
        }
    }
}
