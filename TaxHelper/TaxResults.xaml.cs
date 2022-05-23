using System;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper
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
