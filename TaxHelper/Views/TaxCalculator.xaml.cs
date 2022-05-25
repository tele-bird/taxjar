using Autofac;
using TaxHelper.ViewModels;

namespace TaxHelper.Views
{
    public partial class TaxCalculator : BaseContentPage<TaxCalculatorViewModel>
    {
        // todo: force Shell to use Container.Resolve() instead of using the empty constructor
        //public TaxCalculator(TaxCalculatorViewModel taxCalculatorViewModel)
        public TaxCalculator()
        {
            InitializeComponent();
            var taxCalculatorViewModel = App.Container.Resolve<TaxCalculatorViewModel>();
            if(taxCalculatorViewModel != null)
            {
                ViewModel = taxCalculatorViewModel;
            }
        }

        private async void ShowAlert(string message)
        {
            await this.DisplayAlert("Error", message, "OK");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.AppearingCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.DisappearingCommand.Execute(null);
        }
    }
}
