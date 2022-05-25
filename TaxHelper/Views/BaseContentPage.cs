using System.Threading.Tasks;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper.Views
{
    public class BaseContentPage<TViewModel> : ContentPage, IBaseContentPage<TViewModel> where TViewModel : BaseViewModel
    {
        public TViewModel ViewModel
        {
            get { return (TViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.NavigationPushDelegate = HandleNavigationPush;
            ViewModel.NavigationPushModalDelegate = HandleNavigationPushModal;
            ViewModel.NavigationPopDelegate = HandleNavigationPop;
            ViewModel.NavigationPopModalDelegate = HandleNavigationPopModal;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel.NavigationPushDelegate = null;
            ViewModel.NavigationPushModalDelegate = null;
            ViewModel.NavigationPopDelegate = null;
            ViewModel.NavigationPopModalDelegate = null;
        }

        async Task HandleNavigationPush(BaseViewModel targetViewModel)
        {
            var targetView = ViewManager.GetView(targetViewModel);
            targetView.BindingContext = targetViewModel;
            await Navigation.PushAsync(targetView);
        }

        async Task HandleNavigationPushModal(BaseViewModel targetViewModel)
        {
            var targetView = ViewManager.GetView(targetViewModel);
            targetView.BindingContext = targetViewModel;
            await Navigation.PushModalAsync(targetView);
        }

        async Task HandleNavigationPop()
        {
            await Navigation.PopAsync();
        }

        async Task HandleNavigationPopModal()
        {
            await Navigation.PopModalAsync(true);
        }
    }
}
