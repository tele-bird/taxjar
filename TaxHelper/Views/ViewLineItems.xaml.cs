using System;
using TaxHelper.Common.Models;
using TaxHelper.ViewModels;

namespace TaxHelper.Views
{
    public partial class ViewLineItems : BaseContentPage<ViewLineItemsViewModel>
    {
        public Action<OrderLineItem[]> HandleUpdate { get; }

        public ViewLineItems()
        {
            InitializeComponent();
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
