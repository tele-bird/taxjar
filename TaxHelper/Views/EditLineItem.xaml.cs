using System;
using TaxHelper.Common.Models;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper.Views
{
    public partial class EditLineItem : ContentPage
    {
        public EditLineItemViewModel ViewModel => (EditLineItemViewModel)BindingContext;

        public EditLineItem(EditLineItemViewModel editLineItemViewModel)
        {
            InitializeComponent();
            editLineItemViewModel.HandleError += ShowAlert;
            BindingContext = editLineItemViewModel;
        }

        private async void ShowAlert(string message)
        {
            await this.DisplayAlert("Error", message, "OK");
        }
    }
}
