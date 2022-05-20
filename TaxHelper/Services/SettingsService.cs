using System;
using MonkeyCache.FileStore;
using TaxHelper.Models;
using Xamarin.Essentials;

namespace TaxHelper.Services
{
    public class SettingsService
    {
        public AppSettings Settings
        {
            get
            {
                return Barrel.Current.Get<AppSettings>("settings");
            }
            set
            {
                Barrel.Current.Add<AppSettings>("settings", value, TimeSpan.MaxValue);
            }
        }

        #region Singleton
        private static SettingsService mInstance;
        public static SettingsService Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new SettingsService();
                }
                return mInstance;
            }
        }

        private SettingsService()
        {
            Barrel.ApplicationId = AppInfo.PackageName;
        }
        #endregion
    }
}
