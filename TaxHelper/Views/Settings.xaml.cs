using Autofac;
using TaxHelper.ViewModels;

namespace TaxHelper.Views
{
    public partial class Settings : BaseContentPage<SettingsViewModel>
    {
        // todo: force Shell to use Container.Resolve() instead of using the empty constructor
        //public Settings(SettingsViewModel settingsViewModel)
        public Settings()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<SettingsViewModel>();
        }
    }
}
