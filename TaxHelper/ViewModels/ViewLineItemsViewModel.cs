using Autofac;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TaxHelper.Common.Models;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class ViewLineItemsViewModel : BaseViewModel
    {
        public ICommand LineItemSelectedCommand { get; private set; }
        public ICommand DoneCommand { get; private set; }
        public ICommand AddCommand { get; private set; }

        public Action<OrderLineItem[]> HandleDone { get; set; }

        public ObservableCollection<OrderLineItem> LineItems
        {
            get { return GetValue<ObservableCollection<OrderLineItem>>(); }
            set { SetValue<ObservableCollection<OrderLineItem>>(value); }
        }
        public string Title
        {
            get { return GetValue<string>(); }
            set { SetValue<string>(value); }
        }
        public bool IsRefreshing
        {
            get { return GetValue<bool>(); }
            set { SetValue<bool>(value); }
        }

        public ViewLineItemsViewModel(IAlertHelper alertHelper)
            : base(alertHelper)
        {
            LineItemSelectedCommand = new Command<OrderLineItem>(Edit);
            DoneCommand = new Command(Done);
            AddCommand = new Command(Add);
            LineItems = new ObservableCollection<OrderLineItem>();
        }

        public void SetLineItems(params OrderLineItem[] lineItems)
        {
            LineItems.Clear();
            foreach (var lineItem in lineItems)
            {
                LineItems.Add(lineItem);
            }
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            Title = $"{LineItems.Count} Line Items";
        }

        private async void Done(object obj)
        {
            await NavigatePopModalAsync();
            HandleDone.Invoke(LineItems.ToArray<OrderLineItem>());
        }

        private async void Add(object obj)
        {
            var editLineItemViewModel = App.Container.Resolve<EditLineItemViewModel>();
            editLineItemViewModel.HandleSubmit = OnAdded;
            await NavigatePushModalAsync(editLineItemViewModel);
        }

        private async void Edit(OrderLineItem lineItem)
        {
            if(null != lineItem)
            {
                var editLineItemViewModel = App.Container.Resolve<EditLineItemViewModel>();
                editLineItemViewModel.HandleSubmit = OnEdited;
                editLineItemViewModel.HandleDelete = OnDeleted;
                editLineItemViewModel.LineItem = lineItem;
                await NavigatePushModalAsync(editLineItemViewModel);
            }
        }

        private void OnAdded(OrderLineItem addedLineItem)
        {
            LineItems.Add(addedLineItem);
            UpdateTitle();
        }

        private void OnEdited(OrderLineItem editedLineItem)
        {
            LineItems[LineItems.IndexOf(editedLineItem)] = editedLineItem;
            UpdateTitle();
        }

        private void OnDeleted(OrderLineItem deletedLineItem)
        {
            LineItems.Remove(deletedLineItem);
            UpdateTitle();
        }
    }
}
