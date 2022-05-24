using Autofac;
using System;
//using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TaxHelper.Common.Models;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class ViewLineItemsViewModel : BaseViewModel
    {
        public ObservableCollection<OrderLineItem> LineItems { get; private set; } = new ObservableCollection<OrderLineItem>();

        public ICommand LineItemSelectedCommand { get; private set; }
        public ICommand DoneCommand { get; private set; }

        public Action<OrderLineItem[]> HandleDone { get; set; }

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

        public void SetLineItems(params OrderLineItem[] lineItems)
        {
            LineItems.Clear();
            foreach(var lineItem in lineItems)
            {
                LineItems.Add(lineItem);
            }
            Title = $"{LineItems.Count} Line Items";
        }

        public ICommand AddCommand { get; set; }

        public ViewLineItemsViewModel(INavigationProvider navigationProvider, params OrderLineItem[] lineItems)
            : base(navigationProvider)
        {
            LineItemSelectedCommand = new Command<OrderLineItem>(Edit);
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
            await NavigationProvider.Navigation.PopModalAsync();
            HandleDone.Invoke(LineItems.ToArray<OrderLineItem>());
        }

        private async void Add(object obj)
        {
            var editLineItem = App.Container?.Resolve<EditLineItem>();
            if(editLineItem != null)
            {
                editLineItem.ViewModel.HandleSubmit += OnAdded;
                await NavigationProvider.Navigation.PushModalAsync(editLineItem);
            }
        }

        private async void Edit(OrderLineItem lineItem)
        {
            if(null != lineItem)
            {
                var editLineItem = App.Container?.Resolve<EditLineItem>();
                if(editLineItem != null)
                {
                    editLineItem.ViewModel.HandleSubmit += OnEdited;
                    editLineItem.ViewModel.HandleDelete += OnDeleted;
                    editLineItem.ViewModel.LineItem = lineItem;
                    await NavigationProvider.Navigation.PushModalAsync(editLineItem);
                }
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
