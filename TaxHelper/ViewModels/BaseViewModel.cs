using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigation Navigation { get; set; }

        protected Action<string> HandleError { get; private set; }

        protected BaseViewModel(INavigation navigation, Action<string> handleError)
        {
            Navigation = navigation;
            HandleError = handleError;
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
