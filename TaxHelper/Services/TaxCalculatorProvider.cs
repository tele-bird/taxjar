using System;
namespace TaxHelper.Services
{
    public class TaxCalculatorProvider : ITaxCalculatorProvider
    {
        public ITaxCalculator TaxCalculator { get; }

        public TaxCalculatorProvider(params ITaxCalculator[] taxCalculators)
        {
            if(taxCalculators.Length < 1)
            {
                throw new Exception("must provide at least one tax calculator");
            }
            TaxCalculator = DecideWhichTaxCalculatorToUse(taxCalculators);
        }

        private ITaxCalculator DecideWhichTaxCalculatorToUse(ITaxCalculator[] taxCalculators)
        {
            // todo: add fancy logic to decide which tax calculator to use...  for now, we have only one:
            return taxCalculators[0];
        }
    }
}
