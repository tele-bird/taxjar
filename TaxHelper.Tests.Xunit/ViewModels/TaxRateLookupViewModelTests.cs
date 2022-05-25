using System.Threading.Tasks;
using Moq;
using TaxHelper.Common.Models;
using TaxHelper.Services;
using TaxHelper.ViewModels;
using Xunit;

namespace TaxHelper.Tests.Xunit.ViewModels
{
    public class TaxRateLookupViewModelTests
    {
        public readonly TaxLocation TAX_LOCATION = new TaxLocation
        {
            Country = "US",
            Zip = "90002",
            State = "CA",
            County = "Los Angeles",
            City = "Los Angeles",
            Street = "1335 E 103rd St"
        };
        public readonly string EXPECTED_TAX_RATES_JSON = "whatever...";

        private bool mQueryInvoked = false;

        [Fact]
        public void TaxRateQueryInvoked()
        {
            // arrange
            var alertHelperMock = new Mock<IAlertHelper>();
            var taxLocationSettingsServiceMock = new Mock<ITaxLocationSettingsService>();
            taxLocationSettingsServiceMock.SetupProperty(tlss => tlss.Settings, TAX_LOCATION);
            var taxServiceMock = new Mock<ITaxService>();
            taxServiceMock.Setup(ts => ts.GetTaxRatesForLocation(TAX_LOCATION))
                .Callback(GetTaxRatesForLocation_Callback)
                .Returns(Task.FromResult(EXPECTED_TAX_RATES_JSON));
            var taxRateLookupViewModel = new TaxRateLookupViewModel(alertHelperMock.Object, taxLocationSettingsServiceMock.Object, taxServiceMock.Object);

            // act
            taxRateLookupViewModel.LookupTaxRates();

            // assert
            Assert.True(mQueryInvoked);
        }

        private void GetTaxRatesForLocation_Callback()
        {
            mQueryInvoked = true;
        }
    }
}
