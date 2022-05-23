using System.Collections.Generic;
using System.Threading.Tasks;
using TaxHelper.Dto;
using TaxHelper.Common.Models;

namespace TaxHelper.Services
{
    public class TaxService : ITaxService
    {
        private readonly ITaxCalculator mTaxCalculator;

        public TaxService(ITaxCalculatorProvider taxCalculatorProvider)
        {
            mTaxCalculator = taxCalculatorProvider.TaxCalculator;
        }

        public async Task<string> GetTaxRatesForLocation(TaxLocation taxLocation)
        {
            return await mTaxCalculator.GetTaxRatesForLocation(taxLocation);
        }

        public async Task<string> GetTaxesForOrder(Order order)
        {
            return await mTaxCalculator.GetTaxesForOrder(order);
        }
    }
}
