using Autofac;
using TaxHelper.Dto;
using TaxHelper.Services;
using TaxHelper.Startup;
using Xamarin.Forms;

namespace TaxHelper
{
    public partial class App : Application
    {
        public static IContainer Container = null;

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            var bootStrapper = new Bootstrapper();
            var navigationProvider = new NavigationProvider(this.MainPage.Navigation);
            Container = bootStrapper.Bootstrap(navigationProvider);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
