using System;
using System.Collections.Generic;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper
{
    public partial class TaxCalculator : ContentPage
    {
        public TaxCalculator()
        {
            InitializeComponent();
            var viewModel = new TaxCalculatorViewModel(Navigation);
            viewModel.HandleError += ShowAlert;
            BindingContext = viewModel;
        }

        private async void ShowAlert(string message)
        {
            await this.DisplayAlert("Error", message, "OK");
        }
    }
}
