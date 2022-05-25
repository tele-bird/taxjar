using System;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class TaxResultsViewModel : BaseViewModel
    {
        public string Result
        {
            get { return GetValue<string>(); }
            set { SetValue<string>(value); }
        }

        public string Title
        {
            get { return GetValue<string>(); }
            set { SetValue<string>(value); }
        }

        public TaxResultsViewModel(INavigationProvider navigationProvider)
            : base(navigationProvider)
        {
        }
    }
}
