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
        public string LineItemsDescription
        {
            get { return GetValue<string>(); }
            set { SetValue<string>(value); }
        }
        public string AddressesDescription
        {
            get { return GetValue<string>(); }
            set { SetValue<string>(value); }
        }

        public ICommand ViewLineItemsCommand { get; private set; }
        public ICommand ViewNexusAddressesCommand { get; private set; }
        public ICommand GetTaxCommand { get; private set; }

        private readonly ITaxService mTaxService;

        public TaxCalculatorViewModel(IAlertHelper alertHelper, IOrderSettingsService orderSettingsService, ITaxService taxService)
            : base(alertHelper, orderSettingsService)
        {
            ViewLineItemsCommand = new Command(ViewLineItems);
            ViewNexusAddressesCommand = new Command(ViewNexusAddresses);
            GetTaxCommand = new Command(GetTax);
            mTaxService = taxService;
        }

        private async void ViewLineItems()
        {
            var viewLineItemsViewModel = App.Container.Resolve<ViewLineItemsViewModel>();
            viewLineItemsViewModel.HandleDone += OnLineItemsUpdated;
            viewLineItemsViewModel.SetLineItems(StickyDto.LineItems.ToArray());
            await NavigatePushModalAsync(viewLineItemsViewModel);
        }

        private void ViewNexusAddresses()
        {
            ShowAlert("Error", "View addresses is not plugged in yet.", "OK");
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
            LineItemsDescription = $"{StickyDto.LineItems.Length} totaling {StickyDto.LineItemsTotalFloat:C}";
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
                StickyDto.ThrowIfInvalid();

                // calculate the tax:
                var result = await mTaxService.GetTaxesForOrder(StickyDto);

                // navigate to results page:
                var taxResultsViewModel = App.Container.Resolve<TaxResultsViewModel>();
                taxResultsViewModel.Result = result;
                taxResultsViewModel.Title = "Tax Calculator";
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
