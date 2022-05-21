using System;
using TaxHelper.Models;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper
{
    public partial class EditLineItem : ContentPage
    {
        public EditLineItem(OrderLineItem lineItem, Action<OrderLineItem> handleSubmit, Action<OrderLineItem> handleDelete)
        {
            InitializeComponent();
            var viewModel = new EditLineItemViewModel(Navigation, ShowAlert, lineItem, handleSubmit, handleDelete);
            BindingContext = viewModel;
        }

        public EditLineItem(Action<OrderLineItem> handleSubmit)
            : this(null, handleSubmit, null)
        {
        }

        private async void ShowAlert(string message)
        {
            await this.DisplayAlert("Error", message, "OK");
        }
    }
}
