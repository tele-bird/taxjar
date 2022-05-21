using TaxHelper.Models;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper
{
    public partial class EditLineItem : ContentPage
    {
        public EditLineItem(OrderLineItem lineItem)
        {
            InitializeComponent();
            var viewModel = new EditLineItemViewModel(Navigation, lineItem);
            viewModel.HandleError += ShowAlert;
            BindingContext = viewModel;
        }

        public EditLineItem()
            : this(null)
        {
        }

        private async void ShowAlert(string message)
        {
            await this.DisplayAlert("Error", message, "OK");
        }
    }
}
