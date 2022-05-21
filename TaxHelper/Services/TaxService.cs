using System.Collections.Generic;
using System.Threading.Tasks;
using TaxHelper.Dto;
using TaxHelper.Models;

namespace TaxHelper.Services
{
    public class TaxService : ITaxService
    {
        private readonly List<ITaxCalculator> mTaxCalculators;

        #region singleton pattern
        private static TaxService mInstance;
        public static TaxService Instance
        {
            get
            {
                if(mInstance == null)
                {
                    var settings = SettingsService<AppSettingsDto>.Instance.Settings;
                    mInstance = new TaxService(new TaxJarTaxCalculator(settings.TaxJarApiKey));
                }
                return mInstance;
            }
        }

        private TaxService(params ITaxCalculator[] taxCalculators)
        {
            if(taxCalculators.Length < 1)
            {
                throw new MissingTaxCalculatorException("The TaxService requires at least one TaxCalculator.");
            }
            mTaxCalculators = new List<ITaxCalculator>(taxCalculators);
        }
        #endregion

        public async Task<string> GetTaxRatesForLocation(TaxLocation taxLocation)
        {
            // future todo: decide which tax calculator to use:
            // for now, we'll use the only one
            ITaxCalculator taxCalculatorToUse = mTaxCalculators[0];

            return await taxCalculatorToUse.GetTaxRatesForLocation(taxLocation);
        }

        public async Task<string> GetTaxesForOrder(Order order)
        {
            // future todo: decide which tax calculator to use:
            // for now, we'll use the only one
            ITaxCalculator taxCalculatorToUse = mTaxCalculators[0];

            return await taxCalculatorToUse.GetTaxesForOrder(order);
        }
    }
}
