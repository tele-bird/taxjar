﻿using Autofac;
using System;
//using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TaxHelper.Common.Models;
using TaxHelper.Services;
using TaxHelper.Views;
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

        public ViewLineItemsViewModel(INavigationProvider navigationProvider, params OrderLineItem[] lineItems)
            : base(navigationProvider)
        {
            LineItemSelectedCommand = new Command<OrderLineItem>(Edit);
            DoneCommand = new Command(Done);
            AddCommand = new Command(Add);
            LineItems = new ObservableCollection<OrderLineItem>();
            foreach (var lineItem in lineItems)
            {
                LineItems.Add(lineItem);
            }
            Title = $"{LineItems.Count} Line Items";
        }

        public void SetLineItems(params OrderLineItem[] lineItems)
        {
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
