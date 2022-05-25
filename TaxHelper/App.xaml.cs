using Autofac;
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
            Container = bootStrapper.Bootstrap(this);
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
