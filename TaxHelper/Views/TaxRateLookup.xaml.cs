using Autofac;
using System;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper.Views
{
    public partial class TaxRateLookup : ContentPage
    {
        // todo: force Shell to use Container.Resolve() instead of using the empty constructor
        //public TaxRateLookup(TaxRateLookupViewModel taxRateLookupViewModel)
        public TaxRateLookup()
        {
            InitializeComponent();
            var taxRateLookupViewModel = App.Container?.Resolve<TaxRateLookupViewModel>();
            if(taxRateLookupViewModel != null)
            {
                taxRateLookupViewModel.HandleError += ShowAlert;
                BindingContext = taxRateLookupViewModel;
            }
        }

        private async void ShowAlert(string message)
        {
            await this.DisplayAlert("Error", message, "OK");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((TaxRateLookupViewModel)BindingContext).AppearingCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((TaxRateLookupViewModel)BindingContext).DisappearingCommand.Execute(null);
        }
    }
}
