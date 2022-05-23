using Autofac;
using System;
using System.Linq;
using System.Windows.Input;
using TaxHelper.Common.Models;
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

        private ITaxService mTaxService { get; }

        public TaxCalculatorViewModel(INavigationProvider navigationProvider, IOrderSettingsService orderSettingsService, ITaxService taxService)
            : base(navigationProvider, orderSettingsService)
        {
            ViewLineItemsCommand = new Command(ViewLineItems);
            ViewNexusAddressesCommand = new Command(ViewNexusAddresses);
            GetTaxCommand = new Command(GetTax);
            mTaxService = taxService;
        }

        private async void ViewLineItems()
        {
            var viewLineItems = App.Container.Resolve<ViewLineItems>();
            viewLineItems.ViewModel.SetLineItems(StickyDto.LineItems.ToArray());
            viewLineItems.ViewModel.HandleDone += OnLineItemsUpdated;
            await NavigationProvider.Navigation.PushModalAsync(viewLineItems);
        }

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
            //StickyDto.Amount = StickyDto.LineItemsTotalFloat;
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
                var result = await mTaxService.GetTaxesForOrder(StickyDto);

                // navigate to results page:
                var taxResults = App.Container.Resolve<TaxResults>();
                taxResults.ViewModel.Result = result;
                taxResults.ViewModel.Title = "Tax Calculation";
                await NavigationProvider.Navigation.PushAsync(taxResults);
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
