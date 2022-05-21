using System;
using Newtonsoft.Json;
using TaxHelper.Models;
using Xamarin.Essentials;

namespace TaxHelper.Services
{
    public class SettingsService<T> : ISettingsService<T>
    {
        private string mKey;
        //private string mDefaultValue;

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

        #region Singleton
        private static SettingsService<T> mInstance;
        public static SettingsService<T> Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new SettingsService<T>();
                }
                return mInstance;
            }
        }

        private SettingsService()
        {
            mKey = typeof(T).FullName;
            //var defaultInstance = Activator.CreateInstance<T>();
            //mDefaultValue = JsonConvert.SerializeObject(defaultInstance, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
        #endregion
    }
}
