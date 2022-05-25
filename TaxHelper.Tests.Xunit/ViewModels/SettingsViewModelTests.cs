using Moq;
using TaxHelper.Dto;
using TaxHelper.Services;
using TaxHelper.ViewModels;
using Xunit;

namespace TaxHelper.Tests.Xunit.ViewModels
{
    public class SettingsViewModelTests
    {
        public readonly string EXPECTED_TAXJAR_API_KEY = "1234";

        [Fact]
        public void TaxJarApiKeyWasGotten()
        {
            // arrange
            var alertHelperMock = new Mock<IAlertHelper>();
            var appSettingsServiceMock = new Mock<IAppSettingsService>();
            appSettingsServiceMock.Setup(s => s.Settings)
                .Returns(new AppSettingsDto { TaxJarApiKey = EXPECTED_TAXJAR_API_KEY });
            var settingsViewModel = new SettingsViewModel(alertHelperMock.Object, appSettingsServiceMock.Object);
            settingsViewModel.TaxJarApiKey = EXPECTED_TAXJAR_API_KEY;

            // act
            var actualTaxJarApiKey = settingsViewModel.TaxJarApiKey;

            // assert
            Assert.Equal(EXPECTED_TAXJAR_API_KEY, actualTaxJarApiKey);
        }
    }
}
