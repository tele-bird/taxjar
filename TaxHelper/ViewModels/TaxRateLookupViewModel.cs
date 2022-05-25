using Autofac;
using System;
using System.Windows.Input;
using TaxHelper.Common.Exceptions;
using TaxHelper.Common.Models;
using TaxHelper.Services;
using TaxHelper.Views;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class TaxRateLookupViewModel : StickyViewModel<TaxLocation>
    {
        public ICommand GetTaxRatesCommand { get; private set; }

        public readonly ITaxService mTaxService;

        public TaxRateLookupViewModel(INavigationProvider navigationProvider, ITaxLocationSettingsService taxLocationSettingsService, ITaxService taxService)
            : base(navigationProvider, taxLocationSettingsService)
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
                var taxResults = App.Container?.Resolve<TaxResults>();
                if(taxResults != null)
                {
                    taxResults.ViewModel.Result = result;
                    taxResults.ViewModel.Title = "Tax Rates";
                    await NavigationProvider.Navigation?.PushAsync(taxResults);
                }
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
