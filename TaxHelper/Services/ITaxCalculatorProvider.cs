using System;
namespace TaxHelper.Services
{
    public interface ITaxCalculatorProvider
    {
        ITaxCalculator TaxCalculator { get; }
    }
}
