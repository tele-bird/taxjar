using System;
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
    }
}
