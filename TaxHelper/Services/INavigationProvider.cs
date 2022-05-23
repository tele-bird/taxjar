using System;
using Xamarin.Forms;

namespace TaxHelper.Services
{
    public interface INavigationProvider
    {
        INavigation Navigation { get; }
    }
}
