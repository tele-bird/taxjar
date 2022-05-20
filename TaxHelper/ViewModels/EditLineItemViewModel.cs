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

        public bool WasSubmitted { get; set; }

        public ICommand SubmitCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public EditLineItemViewModel(INavigation navigation)
            : base(navigation)
        {
            SubmitCommand = new Command(Submit);
            CancelCommand = new Command(Cancel);
            LineItem = new OrderLineItem();
        }

        private async void Cancel()
        {
            await Navigation.PopAsync();
        }

        private async void Submit()
        {
            string errorMessage = null;
            try
            {
                ThrowIfInvalidValues();
                WasSubmitted = true;
                await Navigation.PopAsync();
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
