using System;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper.Views
{
    public partial class TaxResults : ContentPage
    {
        public TaxResultsViewModel ViewModel => (TaxResultsViewModel)BindingContext;

        public TaxResults(TaxResultsViewModel taxResultsViewModel)
        {
            InitializeComponent();
            BindingContext = taxResultsViewModel;
        }
    }
}
