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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((TaxCalculatorViewModel)BindingContext).AppearingCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((TaxCalculatorViewModel)BindingContext).DisappearingCommand.Execute(null);
        }
    }
}
