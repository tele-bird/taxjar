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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((TaxRateLookupViewModel)BindingContext).AppearingCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((TaxRateLookupViewModel)BindingContext).DisappearingCommand.Execute(null);
        }
    }
}
