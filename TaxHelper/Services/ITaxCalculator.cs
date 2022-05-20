using System;
using System.Threading.Tasks;
using TaxHelper.Models;

namespace TaxHelper.Services
{
    public interface ITaxCalculator
    {
        Task<string> GetTaxRatesForLocation(TaxLocation taxLocation);
        Task<string> GetTaxesForOrder(Order order);
    }
}
