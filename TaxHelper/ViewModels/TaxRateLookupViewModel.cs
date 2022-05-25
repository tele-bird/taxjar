using Autofac;
using System;
using System.Windows.Input;
using TaxHelper.Common.Models;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class TaxRateLookupViewModel : StickyViewModel<TaxLocation>
    {
        public ICommand GetTaxRatesCommand { get; private set; }

        public readonly ITaxService mTaxService;

        public TaxRateLookupViewModel(IAlertHelper alertHelper, ITaxLocationSettingsService taxLocationSettingsService, ITaxService taxService)
            : base(alertHelper, taxLocationSettingsService)
        {
            GetTaxRatesCommand = new Command(LookupTaxRates);
            mTaxService = taxService;
        }

        public async void LookupTaxRates()
        {
            string errorMessage = null;
            try
            {
                // let the TaxLocation tell us whether it is valid:
                StickyDto.ThrowIfInvalid();

                // look up the tax rate:
                var result = await mTaxService.GetTaxRatesForLocation(StickyDto);

                // navigate to results page:
                var taxResultsViewModel = App.Container.Resolve<TaxResultsViewModel>();
                taxResultsViewModel.Result = result;
                taxResultsViewModel.Title = "Tax Rates";
                await NavigatePushAsync(taxResultsViewModel);
            }
            catch (Exception exc)
            {
                errorMessage = exc.Message;
            }
            finally
            {
                if (null != errorMessage)
                {
                    ShowAlert("Error", errorMessage, "OK");
                }
            }
        }
    }
}
