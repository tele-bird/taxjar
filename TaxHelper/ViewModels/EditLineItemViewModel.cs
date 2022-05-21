using System;
using System.Windows.Input;
using TaxHelper.Models;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class EditLineItemViewModel : BaseViewModel
    {
        public OrderLineItem LineItem { get; set; }

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public Action<OrderLineItem> HandleSubmit;
        public Action<OrderLineItem> HandleDelete;

        public EditLineItemViewModel(INavigation navigation, OrderLineItem lineItem)
            : base(navigation)
        {
            LineItem = (lineItem != null) ? lineItem : new OrderLineItem();
            SubmitCommand = new Command(Submit);
            CancelCommand = new Command(Cancel);
            DeleteCommand = new Command(Delete);
        }

        private async void Delete(object obj)
        {
            await Navigation.PopModalAsync();
            HandleDelete(LineItem);
        }

        private async void Cancel(object obj)
        {
            await Navigation.PopModalAsync();
        }

        private async void Submit()
        {
            string errorMessage = null;
            try
            {
                ThrowIfInvalidValues();
                await Navigation.PopModalAsync();
                HandleSubmit(LineItem);
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

        private void ThrowIfInvalidValues()
        {
            var errors = LineItem.GetErrors();
            if (errors.Count > 0)
            {
                throw new MultipleErrorsException(errors);
            }
        }
    }
}
