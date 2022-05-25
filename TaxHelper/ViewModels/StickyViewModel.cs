using System;
using System.Windows.Input;
using TaxHelper.Services;
using Xamarin.Forms;

namespace TaxHelper.ViewModels
{
    /// <summary>
    /// A ViewModel that remembers and pre-populates the last seen values in the view.
    /// To survive across app launches, the values in the view are stored in the StickyDto property.
    /// The StickyDto value is written to Settings in JSON format.
    /// This works in conjunction with the BaseViewModel that provides to two-way binding to the View.
    /// Subclasses can optionally override OnDisappearing and OnAppearing for other purposes, but
    /// just be sure to invoke base.OnDisappearing() and base.OnAppearing() in your override.
    /// To use this as your ViewModel subclass, just the following is needed:
    ///    1) Inherit from this class, assigning your Dto type to T.
    ///    2) The View needs to override OnAppearing() and OnDisappearing() and invoke AppearingCommand and DisappearingCommand.
    ///    2) Bind to StickyDto property as needed in your XAML markup: {Binding StickyDto}.
    ///    3) Refer to StickyDto from your subclass when you need to access the current bound value.
    /// </summary>
    /// <typeparam name="T">The Type of Dto that is bound to the View.</typeparam>
    public class StickyViewModel<T> : BaseViewModel
    {
        private bool mAppeared;

        public T StickyDto
        {
            get { return GetValue<T>(); }
            protected set { SetValue<T>(value); }
        }

        public ICommand AppearingCommand { get; set; }
        public ICommand DisappearingCommand { get; set; }

        private ISettingsService<T> mSettingsService;

        protected StickyViewModel(INavigationProvider navigationProvider, ISettingsService<T> settingsService)
            : base(navigationProvider)
        {
            mAppeared = false;
            AppearingCommand = new Command(OnAppearing);
            DisappearingCommand = new Command(OnDisppearing);
            mSettingsService = settingsService;
            StickyDto = mSettingsService.Settings;
        }

        protected virtual void OnDisppearing(object obj)
        {
            mSettingsService.Settings = StickyDto;
        }

        protected virtual void OnAppearing(object obj)
        {
            if (!mAppeared)
            {
                mAppeared = true;
                OnAppearingFirstTime(obj);
            }
        }

        protected virtual void OnAppearingFirstTime(object obj)
        {
        }
    }
}
