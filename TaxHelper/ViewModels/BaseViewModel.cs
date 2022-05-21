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

        public Action<string> HandleError;

        protected BaseViewModel(INavigation navigation)
        {
            Navigation = navigation;
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
