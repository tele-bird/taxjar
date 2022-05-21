using System;
//using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TaxHelper.Models;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class ViewLineItemsViewModel : BaseViewModel
    {
        public ObservableCollection<OrderLineItem> LineItems { get; private set; } = new ObservableCollection<OrderLineItem>();

        public ICommand LineItemSelectedCommand { get; private set; }
        public ICommand DoneCommand { get; private set; }

        private Action<OrderLineItem[]> mHandleDone;

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

        public ViewLineItemsViewModel(INavigation navigation, Action<string> handleError, Action<OrderLineItem[]> handleDone, params OrderLineItem[] lineItems)
            : base(navigation, handleError)
        {
            LineItemSelectedCommand = new Command<OrderLineItem>(Edit);
            mHandleDone = handleDone;
            DoneCommand = new Command(Done);
            AddCommand = new Command(Add);
            LineItems.Clear();
            foreach (var lineItem in lineItems)
            {
                LineItems.Add(lineItem);
            }
            Title = $"{LineItems.Count} Line Items";
        }

        private async void Done(object obj)
        {
            await Navigation.PopModalAsync();
            mHandleDone.Invoke(LineItems.ToArray<OrderLineItem>());
        }

        private async void Add(object obj)
        {
            var editLineItem = new EditLineItem(OnAdded);
            await Navigation.PushModalAsync(editLineItem);
        }

        private async void Edit(OrderLineItem lineItem)
        {
            if(null != lineItem)
            {
                var editLineItem = new EditLineItem(lineItem, OnEdited, OnDeleted);
                await Navigation.PushModalAsync(editLineItem);
            }
        }

        private void OnAdded(OrderLineItem addedLineItem)
        {
            LineItems.Add(addedLineItem);
            Title = $"{LineItems.Count} Line Items";
        }

        private void OnEdited(OrderLineItem editedLineItem)
        {
            LineItems[LineItems.IndexOf(editedLineItem)] = editedLineItem;
        }

        private void OnDeleted(OrderLineItem deletedLineItem)
        {
            LineItems.Remove(deletedLineItem);
        }
    }
}
