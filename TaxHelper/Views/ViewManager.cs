using System;
using System.Reflection;
using System.Linq;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper.Views
{
    public static class ViewManager
    {
        public static ContentPage GetView<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
        {
            var viewName = viewModel.GetType().Name.Replace("ViewModel", "");
            var types = viewModel.GetType().GetTypeInfo().Assembly.DefinedTypes;
            var typeToReturn = types.FirstOrDefault(t => t.Name == viewName);
            return Activator.CreateInstance(typeToReturn.AsType()) as ContentPage;
        }
    }
}
