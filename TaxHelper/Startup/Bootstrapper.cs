using Autofac;
using TaxHelper.Services;
using TaxHelper.ViewModels;

namespace TaxHelper.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap(App app)
        {
            var builder = new ContainerBuilder();

            // register services:
            builder.RegisterInstance<IAlertHelper>(new AlertHelper(app));
            builder.RegisterType<AppSettingsService>().As<IAppSettingsService>().SingleInstance();
            builder.RegisterType<OrderSettingsService>().As<IOrderSettingsService>().SingleInstance();
            builder.RegisterType<TaxService>().As<ITaxService>().SingleInstance();
            builder.RegisterType<TaxCalculatorProvider>().As<ITaxCalculatorProvider>().SingleInstance();
            builder.RegisterType<TaxJarTaxCalculator>().As<ITaxCalculator>().SingleInstance();
            builder.RegisterType<TaxLocationSettingsService>().As<ITaxLocationSettingsService>().SingleInstance();

            // register view models:
            builder.RegisterType<EditLineItemViewModel>().AsSelf();
            builder.RegisterType<SettingsViewModel>().AsSelf();
            builder.RegisterType<TaxCalculatorViewModel>().AsSelf();
            builder.RegisterType<TaxRateLookupViewModel>().AsSelf();
            builder.RegisterType<TaxResultsViewModel>().AsSelf();
            builder.RegisterType<ViewLineItemsViewModel>().AsSelf();

            return builder.Build();
        }
    }
}
