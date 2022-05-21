using System;
using System.Collections.Generic;
using System.Linq;
using TaxHelper.Models;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper
{
    public partial class ViewLineItems : ContentPage
    {
        private Action<OrderLineItem[]> mHandleUpdate;

        public ViewLineItems(Action<string> handleError, Action<OrderLineItem[]> handleDone, params OrderLineItem[] lineItems)
        {
            InitializeComponent();
            var viewModel = new ViewLineItemsViewModel(Navigation, handleError, handleDone, lineItems);
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
