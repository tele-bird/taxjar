using System;
using System.Linq;
using System.Windows.Input;
using TaxHelper.Models;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class TaxCalculatorViewModel : StickyViewModel<Order>
    {
        private string mLineItemsDescription;
        public string LineItemsDescription
        {
            get { return mLineItemsDescription; }
            set
            {
                mLineItemsDescription = value;
                OnPropertyChanged();
            }
        }

        private string mAddressesDescription;
        public string AddressesDescription
        {
            get { return mAddressesDescription; }
            set
            {
                mAddressesDescription = value;
                OnPropertyChanged();
            }
        }

        public ICommand ViewLineItemsCommand { get; set; }

        public ICommand ViewNexusAddressesCommand { get; set; }

        public ICommand GetTaxCommand { get; set; }

        public TaxCalculatorViewModel(INavigation navigation, Action<string> handleError)
            : base(navigation, handleError)
        {
            Navigation = navigation;
            ViewLineItemsCommand = new Command(ViewLineItems);
            ViewNexusAddressesCommand = new Command(ViewNexusAddresses);
            GetTaxCommand = new Command(GetTax);
        }

        private async void ViewLineItems()
        {
            var viewLineItems = new ViewLineItems(null, OnLineItemsUpdated, StickyDto.LineItems.ToArray<OrderLineItem>());
            await Navigation.PushModalAsync(viewLineItems);
        }

        //private async void ViewAddresses()
        //{
        //    var viewAddresses = new ViewAddresses(null, OnAddressesUpdated, StickyDto.Addresses.ToArray<NexusAddress>());
        //    await Navigation.PushModalAsync(viewAddresses);
        //}

        private void ViewNexusAddresses()
        {
            HandleError("View addresses is not plugged in yet.");
        }

        private void OnLineItemsUpdated(OrderLineItem[] updatedLineItems)
        {
            StickyDto.LineItems = updatedLineItems;
            UpdateOrderWithLineItems();
        }

        private void OnAddressesUpdated(NexusAddress[] updatedAddresses)
        {
            StickyDto.Addresses = updatedAddresses;
            UpdateOrderWithAddresses();
        }

        private void UpdateOrderWithLineItems()
        {
            LineItemsDescription = $"{StickyDto.LineItems.Length} totaling {StickyDto.GrandTotalFloat:C}";
            StickyDto.Amount = StickyDto.LineItemsTotalFloat;
            OnPropertyChanged(nameof(StickyDto));
        }

        private void UpdateOrderWithAddresses()
        {
            AddressesDescription = $"{StickyDto.Addresses.Length} addresses";
            OnPropertyChanged(nameof(StickyDto));
        }

        protected override void OnAppearingFirstTime(object obj)
        {
            base.OnAppearingFirstTime(obj); // StickyDto is set in the base class implementation
            UpdateOrderWithLineItems();
            UpdateOrderWithAddresses();
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
