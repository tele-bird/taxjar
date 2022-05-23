using System;
using TaxHelper.Common.Models;
using Xunit;
namespace TaxHelper.Tests.Xunit.Models.TestData
{
    public class ValidOrderTestData : TheoryData<Order, float, float, Exception>
    {
        private Order mOrder = new Order
        {
            FromCountry = "us",
            FromZip = "92093",
            FromState = "ca",
            FromCity = "La Jolla",
            FromStreet = "9500 Gilman Drive",
            ToCountry = "us",
            ToZip = "90002",
            ToState = "ca",
            ToCity = "Los Angeles",
            ToStreet = "1335 E 103rd St",
            Amount = 15f,
            Shipping = 1.5f,
            CustomerId = "",
            ExemptionType = "",
            Addresses = new[]
                {
                    new NexusAddress
                    {
                        Id = "Main Location",
                        Country = "us",
                        Zip = "922093",
                        State = "ca",
                        City = "La Jolla",
                        Street = "9500 Gilman Drive",
                    }
                },
            LineItems = new[]
                {
                    new OrderLineItem
                    {
                        Id = "1",
                        Quantity = 1,
                        ProductTaxCode = "20010",
                        UnitPrice = 15f,
                        Discount = 0f,
                    }
                }
        };
        private float mExpectedLineItemsTotal = 15f;
        private float mExpectedGrandTotal = 16.5f;
        private Exception mExpectedException = null;

        public ValidOrderTestData()
        {
            Add(mOrder,
                mExpectedLineItemsTotal,
                mExpectedGrandTotal,
                mExpectedException);
        }
    }
}
