using System;
using System.Collections.Generic;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper
{
    public partial class TaxRateLookup : ContentPage
    {
        public TaxRateLookup()
        {
            InitializeComponent();
            var viewModel = new TaxRateLookupViewModel(Navigation);
            viewModel.HandleError += ShowAlert;
            BindingContext = viewModel;
        }

        private async void ShowAlert(string message)
        {
            await this.DisplayAlert("Error", message, "OK");
        }
    }
}
