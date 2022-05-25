using TaxHelper.ViewModels;

namespace TaxHelper.Views
{
    public interface IBaseContentPage
    {
    }

    public interface IBaseContentPage<TViewModel> : IBaseContentPage where TViewModel : BaseViewModel
    {
        TViewModel ViewModel { get; set; }
    }
}
