using System;
using System.Windows.Input;
using TaxHelper.Common.Models;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class EditLineItemViewModel : BaseViewModel
    {
        private OrderLineItem mLineItem;
        public OrderLineItem LineItem
        {
            get { return mLineItem; }
            set
            {
                mLineItem = value;
                OnPropertyChanged();
                IsDeleteButtonVisible = (value != null) && (HandleDelete != null);
            }
        }

        private bool mIsDeleteButtonVisible;
        public bool IsDeleteButtonVisible
        {
            get { return mIsDeleteButtonVisible; }
            set
            {
                mIsDeleteButtonVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand SubmitCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public Action<OrderLineItem> HandleSubmit { get; set; }
        public Action<OrderLineItem> HandleDelete { get; set; }

        public EditLineItemViewModel(INavigationProvider navigationProvider)
            : base(navigationProvider)
        {
            SubmitCommand = new Command(Submit);
            CancelCommand = new Command(Cancel);
            DeleteCommand = new Command(Delete);
            LineItem = new OrderLineItem();
        }

        private async void Delete(object obj)
        {
            await NavigationProvider.Navigation.PopModalAsync();
            HandleDelete(LineItem);
        }

        private async void Cancel(object obj)
        {
            await NavigationProvider.Navigation.PopModalAsync();
        }

        private async void Submit()
        {
            string errorMessage = null;
            try
            {
                ThrowIfInvalidValues();
                await NavigationProvider.Navigation.PopModalAsync();
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
