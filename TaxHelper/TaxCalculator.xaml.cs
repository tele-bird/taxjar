using Autofac;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper
{
    public partial class TaxCalculator : ContentPage
    {
        // todo: force Shell to use Container.Resolve() instead of using the empty constructor
        //public TaxCalculator(TaxCalculatorViewModel taxCalculatorViewModel)
        public TaxCalculator()
        {
            InitializeComponent();
            var taxCalculatorViewModel = App.Container?.Resolve<TaxCalculatorViewModel>();
            if(taxCalculatorViewModel != null)
            {
                taxCalculatorViewModel.HandleError = ShowAlert;
                BindingContext = taxCalculatorViewModel;
            }
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
