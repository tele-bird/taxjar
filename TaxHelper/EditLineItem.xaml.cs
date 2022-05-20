using System;
using System.Collections.Generic;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper
{
    public partial class EditLineItem : ContentPage
    {
        public EditLineItem()
        {
            InitializeComponent();
            var viewModel = new EditLineItemViewModel(Navigation);
            viewModel.HandleError += ShowAlert;
            BindingContext = viewModel;
        }

        private async void ShowAlert(string message)
        {
            await this.DisplayAlert("Error", message, "OK");
        }
    }
}
