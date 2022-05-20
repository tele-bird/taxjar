using System;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    public class TaxResultsViewModel : BaseViewModel
    {
        private string mResult;
        public string Result
        {
            get { return mResult; }
            set
            {
                mResult = value;
                OnPropertyChanged();
            }
        }

        public string Title { get; set; }

        public TaxResultsViewModel(INavigation navigation)
            : base(navigation)
        {
        }
    }
}
