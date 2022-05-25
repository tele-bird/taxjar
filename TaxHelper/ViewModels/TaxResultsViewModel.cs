using TaxHelper.Services;

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

        public TaxResultsViewModel(IAlertHelper alertHelper)
            : base(alertHelper)
        {
        }
    }
}
