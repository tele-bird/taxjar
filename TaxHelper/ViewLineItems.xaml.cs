using System.Collections.Generic;
using TaxHelper.Models;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper
{
    public partial class ViewLineItems : ContentPage
    {
        public ViewLineItems(params OrderLineItem[] lineItems)
        {
            InitializeComponent();
            var viewModel = new ViewLineItemsViewModel(Navigation, lineItems);
            viewModel.HandleError += ShowAlert;
            BindingContext = viewModel;
        }

        private async void ShowAlert(string message)
        {
            await this.DisplayAlert("Error", message, "OK");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            mLineItemsListView.SelectedItem = null;
        }
    }
}
