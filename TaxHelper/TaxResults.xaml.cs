using System;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper
{
    public partial class TaxResults : ContentPage
    {
        public TaxResults()
        {
            InitializeComponent();
            var viewModel = new TaxResultsViewModel(Navigation, null);
            BindingContext = viewModel;
        }
    }
}
