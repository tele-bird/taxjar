using System;
using Xamarin.Forms;

namespace TaxHelper.Services
{
    public class NavigationProvider : INavigationProvider
    {
        public INavigation Navigation { get; private set; }

        public NavigationProvider(INavigation navigation)
        {
            Navigation = navigation;
        }
    }
}
