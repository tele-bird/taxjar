using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TaxHelper.Models;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class TaxCalculatorViewModel : StickyViewModel<Order>
    {
        public ObservableCollection<OrderLineItem> LineItems { get; private set; } = new ObservableCollection<OrderLineItem>();

        public string LineItemsDescription => $"{LineItems.Count} line items totaling {GetLineItemsTotal()}";

        public ObservableCollection<NexusAddress> Addresses { get; private set; } = new ObservableCollection<NexusAddress>();

        public string NexusAddressesDescription => $"{Addresses.Count} addresses";

        private float? GetLineItemsTotal()
        {
            float? total = 0;
            foreach (var lineItem in LineItems)
            {
                total += lineItem.Quantity * lineItem.UnitPrice;
            }
            return total;
        }

        public ICommand ViewLineItemsCommand { get; set; }

        public ICommand ViewNexusAddressesCommand { get; set; }

        public ICommand GetTaxCommand { get; set; }

        public TaxCalculatorViewModel(INavigation navigation)
            : base(navigation)
        {
            Navigation = navigation;
            ViewLineItemsCommand = new Command(ViewLineItems);
            ViewNexusAddressesCommand = new Command(ViewNexusAddresses);
            GetTaxCommand = new Command(GetTax);
        }

        private async void ViewLineItems()
        {
            var viewLineItems = new ViewLineItems(this.LineItems.ToArray<OrderLineItem>());
            var viewLineItemsViewModel = (ViewLineItemsViewModel)viewLineItems.BindingContext;
            await Navigation.PushAsync(viewLineItems);
        }

        private async void ViewNexusAddresses()
        {
            HandleError("View addresses is not plugged in yet.");
        }

        private async void GetTax()
        {
            string errorMessage = null;
            try
            {
                // let the Order tell us whether it is valid:
                var orderErrors = StickyDto.GetErrors();
                if(orderErrors.Count > 0)
                {
                    throw new MultipleErrorsException(orderErrors);
                }

                // calculate the tax:
                var result = await TaxService.Instance.GetTaxesForOrder(StickyDto);

                // navigate to results page:
                var taxResults = new TaxResults();
                var taxResultsViewModel = (TaxResultsViewModel)taxResults.BindingContext;
                taxResultsViewModel.Result = result;
                taxResultsViewModel.Title = "Tax Calculation";
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
