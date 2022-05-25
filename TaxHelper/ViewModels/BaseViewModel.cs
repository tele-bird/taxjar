using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TaxHelper.Services;

namespace TaxHelper.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private readonly IAlertHelper mAlertHelper;

        protected BaseViewModel(IAlertHelper alertHelper)
        {
            mAlertHelper = alertHelper;
        }

        protected void ShowAlert(string title, string message, string cancel)
        {
            mAlertHelper.ShowAlert(title, message, cancel);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region property storage in Dictionary

        private Dictionary<string, object> mPropertyValuesByName = new Dictionary<string, object>();

		protected void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
		{
			if (!mPropertyValuesByName.ContainsKey(propertyName))
			{
				mPropertyValuesByName.Add(propertyName, default(T));
			}

			var oldValue = GetValue<T>(propertyName);
			if (!EqualityComparer<T>.Default.Equals(oldValue, value))
			{
				mPropertyValuesByName[propertyName] = value;
				OnPropertyChanged(propertyName);
			}
		}

		protected T GetValue<T>([CallerMemberName] string propertyName = null)
		{
			if (!mPropertyValuesByName.ContainsKey(propertyName))
			{
				return default(T);
			}
			else
			{
				return (T)mPropertyValuesByName[propertyName];
			}
		}

        #endregion

        #region Navigation

        public Func<BaseViewModel,Task> NavigationPushDelegate;
        public Func<BaseViewModel,Task> NavigationPushModalDelegate;
        public Func<Task> NavigationPopDelegate;
        public Func<Task> NavigationPopModalDelegate;

        public async Task NavigatePushAsync<TViewModel>(TViewModel targetViewModel) where TViewModel : BaseViewModel
        {
            await NavigationPushDelegate?.Invoke(targetViewModel);
        }

        public async Task NavigatePushModalAsync<TViewModel>(TViewModel targetViewModel) where TViewModel : BaseViewModel
        {
            await NavigationPushModalDelegate?.Invoke(targetViewModel);
        }

        public async Task NavigatePopAsync()
        {
            await NavigationPopDelegate?.Invoke();
        }

        public async Task NavigatePopModalAsync()
        {
            await NavigationPopModalDelegate?.Invoke();
        }


        #endregion
    }
}
