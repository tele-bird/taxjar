namespace TaxHelper.Services
{
    public class AlertHelper : IAlertHelper
    {
        private readonly App mApp;

        public AlertHelper(App app)
        {
            mApp = app;
        }

        public async void ShowAlert(string title, string message, string cancel)
        {
            await mApp.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
