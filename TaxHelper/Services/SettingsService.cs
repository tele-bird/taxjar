using System;
using Newtonsoft.Json;
using TaxHelper.Common.Models;
using Xamarin.Essentials;

namespace TaxHelper.Services
{
    public class SettingsService<T> : ISettingsService<T>
    {
        private string mKey;

        public T Settings
        {
            get
            {
                var stringSetting = Preferences.Get(mKey, null);
                return (null != stringSetting) ? JsonConvert.DeserializeObject<T>(stringSetting) : Activator.CreateInstance<T>();
            }
            set
            {
                Preferences.Set(mKey, JsonConvert.SerializeObject(value));
            }
        }

        public SettingsService()
        {
            mKey = typeof(T).FullName;
        }
    }
}
