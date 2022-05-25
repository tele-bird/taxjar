using Autofac;
using TaxHelper.ViewModels;

namespace TaxHelper.Views
{
    public partial class TaxRateLookup : BaseContentPage<TaxRateLookupViewModel>
    {
        // todo: force Shell to use Container.Resolve() instead of using the empty constructor
        //public TaxRateLookup(TaxRateLookupViewModel taxRateLookupViewModel)
        public TaxRateLookup()
        {
            InitializeComponent();
            var taxRateLookupViewModel = App.Container?.Resolve<TaxRateLookupViewModel>();
            if(taxRateLookupViewModel != null)
            {
                ViewModel = taxRateLookupViewModel;
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
