using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TaxHelper.Models;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class ViewLineItemsViewModel : BaseViewModel
    {
        public ObservableCollection<OrderLineItem> LineItems { get; private set; } = new ObservableCollection<OrderLineItem>();

        private string mTitle;
        public string Title
        {
            get { return mTitle; }
            set
            {
                mTitle = value;
                OnPropertyChanged();
            }
        }

        private bool mIsRefreshing;
        public bool IsRefreshing
        {
            get { return mIsRefreshing; }
            set
            {
                mIsRefreshing = value;
                OnPropertyChanged();
            }
        }

        private OrderLineItem mSelectedItem;
        public OrderLineItem SelectedItem
        {
            get { return mSelectedItem; }
            set
            {
                mSelectedItem = value;
                if(null != mSelectedItem)
                {
                    Edit(mSelectedItem);
                }
            }
        }

        private async Task RefreshLineItemsAsync(params OrderLineItem[] lineItems)
        {
            IsRefreshing = true;
            await Task.Delay(1000);
            LineItems.Clear();
            foreach(var lineItem in lineItems)
            {
                LineItems.Add(lineItem);
            }
            Title = $"{LineItems.Count} Line Items";
            IsRefreshing = false;
        }

        public ICommand AddCommand { get; set; }

        public ViewLineItemsViewModel(INavigation navigation, params OrderLineItem[] lineItems)
            : base(navigation)
        {
            AddCommand = new Command(Add);
            RefreshLineItemsAsync(lineItems);
        }

        private async void Add(object obj)
        {
            var editLineItem = new EditLineItem();
            var editLineItemViewModel = (EditLineItemViewModel)editLineItem.BindingContext;
            editLineItemViewModel.HandleSubmit += OnAdded;
            await Navigation.PushModalAsync(editLineItem);
        }

        private async void Edit(OrderLineItem lineItem)
        {
            var editLineItem = new EditLineItem(lineItem);
            var editLineItemViewModel = (EditLineItemViewModel)editLineItem.BindingContext;
            editLineItemViewModel.HandleSubmit += OnEdited;
            editLineItemViewModel.HandleDelete += OnDeleted;
            await Navigation.PushModalAsync(editLineItem);
        }

        private async void OnAdded(OrderLineItem addedLineItem)
        {
            IsRefreshing = true;
            LineItems.Add(addedLineItem);
            Title = $"{LineItems.Count} Line Items";
            IsRefreshing = false;
            //await AddLineItemAsync(addedLineItem);
        }

        private async void OnEdited(OrderLineItem editedLineItem)
        {
            foreach (var lineItem in LineItems)
            {
                if (lineItem.Id.Equals(editedLineItem.Id))
                {
                    lineItem.Update(editedLineItem);
                    break;
                }
            }
            RefreshLineItemsAsync(LineItems.ToArray<OrderLineItem>());
        }

        private async void OnDeleted(OrderLineItem deletedLineItem)
        {
            LineItems.Remove(deletedLineItem);
            RefreshLineItemsAsync(LineItems.ToArray<OrderLineItem>());
        }
    }
}
