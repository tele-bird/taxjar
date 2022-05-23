using System;
using Autofac;
using TaxHelper.Services;
using TaxHelper.ViewModels;
using Xamarin.Forms;

namespace TaxHelper.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap(INavigationProvider navigationProvider)
        {
            var builder = new ContainerBuilder();

            // register services:
            builder.RegisterInstance(navigationProvider).As<INavigationProvider>();
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

            // register views:
            builder.RegisterType<TaxRateLookup>().AsSelf();
            builder.RegisterType<TaxResults>().AsSelf();
            builder.RegisterType<TaxCalculator>().AsSelf();
            builder.RegisterType<ViewLineItems>().AsSelf();
            builder.RegisterType<EditLineItem>().AsSelf();
            builder.RegisterType<Settings>().AsSelf();

            return builder.Build();
        }
    }
}
