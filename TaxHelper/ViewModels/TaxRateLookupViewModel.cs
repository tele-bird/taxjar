using System;
using System.Windows.Input;
using Newtonsoft.Json;
using TaxHelper.Models;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class TaxRateLookupViewModel : BaseViewModel
    {
        public TaxLocation Location { get; private set; }

        public ICommand GetTaxRatesCommand { get; set; }

        public TaxRateLookupViewModel(INavigation navigation)
            : base(navigation)
        {
            Location = new TaxLocation();
            GetTaxRatesCommand = new Command(LookupTaxRates);
        }

        private async void LookupTaxRates()
        {
            string errorMessage = null;
            try
            {
                // let the TaxLocation tell us whether it is valid:
                var orderErrors = Location.GetErrors();
                if (orderErrors.Count > 0)
                {
                    throw new MultipleErrorsException(orderErrors);
                }

                // look up the tax rate:
                var result = await TaxService.Instance.GetTaxRatesForLocation(Location);

                // navigate to results page:
                var taxResults = new TaxResults();
                var taxResultsViewModel = (TaxResultsViewModel)taxResults.BindingContext;
                taxResultsViewModel.Result = result;
                taxResultsViewModel.Title = "Tax Rates";
                await Navigation.PushAsync(taxResults);
            }
            catch (Exception exc)
            {
                errorMessage = exc.Message;
            }
            finally
            {
                if (null != errorMessage)
                {
                    HandleError(errorMessage);
                }
            }
        }
    }
}
