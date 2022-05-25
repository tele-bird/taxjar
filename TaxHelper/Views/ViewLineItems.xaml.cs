using System;
using System.Collections.Generic;
using System.Linq;
using TaxHelper.Common.Models;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper.Views
{
    public partial class ViewLineItems : ContentPage
    {
        public ViewLineItemsViewModel ViewModel => (ViewLineItemsViewModel)BindingContext;
        public Action<OrderLineItem[]> HandleUpdate { get; }

        public ViewLineItems(ViewLineItemsViewModel viewLineItemsViewModel)
        {
            InitializeComponent();
            viewLineItemsViewModel.HandleError += ShowAlert;
            BindingContext = viewLineItemsViewModel;
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
