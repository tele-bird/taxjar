using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigationProvider NavigationProvider { get; set; }

        public Action<string> HandleError { get; set; }

        protected BaseViewModel(INavigationProvider navigationProvider)
        {
            NavigationProvider = navigationProvider;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

		#endregion

		Dictionary<string, object> properties = new Dictionary<string, object>();

		protected void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
		{
			if (!properties.ContainsKey(propertyName))
			{
				properties.Add(propertyName, default(T));
			}

			var oldValue = GetValue<T>(propertyName);
			if (!EqualityComparer<T>.Default.Equals(oldValue, value))
			{
				properties[propertyName] = value;
				OnPropertyChanged(propertyName);
			}
		}

		protected T GetValue<T>([CallerMemberName] string propertyName = null)
		{
			if (!properties.ContainsKey(propertyName))
			{
				return default(T);
			}
			else
			{
				return (T)properties[propertyName];
			}
		}
	}
}
