using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaxHelper.Models;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class TaxCalculatorViewModel : BaseViewModel
    {
        public Order Order { get; private set; }

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

        public ICommand AddLineItemCommand { get; set; }

        public ICommand ViewLineItemsCommand { get; set; }

        public ICommand ViewNexusAddressesCommand { get; set; }

        public ICommand GetTaxCommand { get; set; }

        public TaxCalculatorViewModel(INavigation navigation)
            : base(navigation)
        {
            Navigation = navigation;
            Order = new Order();
            AddLineItemCommand = new Command(AddLineItem);
            ViewLineItemsCommand = new Command(ViewLineItems);
            ViewNexusAddressesCommand = new Command(ViewNexusAddresses);
            GetTaxCommand = new Command(GetTax);
            //LineItems.Add(new OrderLineItem { Id = "1", Quantity = 5, ProductTaxCode = "abc", UnitPrice = 3.2f, Discount = 1.0f });
            //LineItems.Add(new OrderLineItem { Id = "2", Quantity = 5, ProductTaxCode = "abc", UnitPrice = 3.2f, Discount = 1.0f });
            //LineItems.Add(new OrderLineItem { Id = "3", Quantity = 5, ProductTaxCode = "abc", UnitPrice = 3.2f, Discount = 1.0f });
            //LineItems.Add(new OrderLineItem { Id = "4", Quantity = 5, ProductTaxCode = "abc", UnitPrice = 3.2f, Discount = 1.0f });
            //LineItems.Add(new OrderLineItem { Id = "5", Quantity = 5, ProductTaxCode = "abc", UnitPrice = 3.2f, Discount = 1.0f });
        }

        private async void AddLineItem()
        {
            var editLineItem = new EditLineItem();
            var editLineItemViewModel = (EditLineItemViewModel)editLineItem.BindingContext;
            await Navigation.PushAsync(editLineItem);
            if(editLineItemViewModel.WasSubmitted)
            {
                LineItems.Add(editLineItemViewModel.LineItem);
            }
        }

        private async void ViewLineItems()
        {
            HandleError("View line items is not plugged in yet.");
            //var viewLineItems = new ViewLineItems();
            //var viewLineItemsViewModel = (ViewLineItemsViewModel)viewLineItems.BindingContext;
            //await Navigation.PushAsync(viewLineItems);
            //LineItems.Clear();
            //foreach(var lineItem in viewLineItemsViewModel.LineItems)
            //{
            //    LineItems.Add(lineItem);
            //}
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
                var orderErrors = Order.GetErrors();
                if(orderErrors.Count > 0)
                {
                    throw new MultipleErrorsException(orderErrors);
                }

                // calculate the tax:
                var result = await TaxService.Instance.GetTaxesForOrder(Order);

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
