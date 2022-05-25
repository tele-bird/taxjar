using System;
using System.Windows.Input;
using TaxHelper.Common.Exceptions;
using TaxHelper.Common.Models;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class EditLineItemViewModel : BaseViewModel
    {
        public OrderLineItem LineItem
        {
            get { return GetValue<OrderLineItem>(); }
            set { SetValue<OrderLineItem>(value); }
        }
        public bool IsDeleteButtonVisible
        {
            get { return GetValue<bool>(); }
            set { SetValue<bool>(value); }
        }

        public ICommand SubmitCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public Action<OrderLineItem> HandleSubmit { get; set; }
        public Action<OrderLineItem> HandleDelete { get; set; }

        public EditLineItemViewModel(IAlertHelper alertHelper)
            : base(alertHelper)
        {
            PropertyChanged += EditLineItemViewModel_PropertyChanged;
            SubmitCommand = new Command(Submit);
            CancelCommand = new Command(Cancel);
            DeleteCommand = new Command(Delete);
            LineItem = new OrderLineItem();
        }

        private void EditLineItemViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals(nameof(LineItem)))
            {
                IsDeleteButtonVisible = (LineItem != null) && (HandleDelete != null);
            }
        }

        private async void Delete(object obj)
        {
            await NavigatePopModalAsync();
            HandleDelete(LineItem);
        }

        private async void Cancel(object obj)
        {
            await NavigatePopModalAsync();
        }

        private async void Submit()
        {
            string errorMessage = null;
            try
            {
                LineItem.ThrowIfInvalid();
                await NavigatePopModalAsync();
                HandleSubmit(LineItem);
            }
            catch (ModelValidationException exc)
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
