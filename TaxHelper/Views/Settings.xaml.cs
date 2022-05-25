using Autofac;
using System;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper.Views
{
    public partial class Settings : ContentPage
    {
        // todo: force Shell to use Container.Resolve() instead of using the empty constructor
        //public Settings(SettingsViewModel settingsViewModel)
        public Settings()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<SettingsViewModel>();
        }
    }
}
