using System;
using System.Collections.Generic;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper
{
    public partial class TaxResults : ContentPage
    {
        public TaxResults()
        {
            InitializeComponent();
            var viewModel = new TaxResultsViewModel(Navigation);
            BindingContext = viewModel;
        }
    }
}
