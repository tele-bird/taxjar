using System;
using Newtonsoft.Json;
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
                var serializedSettings = JsonConvert.SerializeObject(value);
                Preferences.Set(mKey, serializedSettings);
            }
        }

        public SettingsService()
        {
            mKey = typeof(T).FullName;
        }
    }
}
