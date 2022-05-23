using System;
using TaxHelper.Common.Exceptions;
using TaxHelper.Common.Models;
using Xunit;

namespace TaxHelper.Tests.Xunit.Models.TestData
{
    public class InvalidOrderTestDataMissingAmountOrLineItems : TheoryData<Order, float, float, Exception>
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
            Amount = null,
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
            LineItems = new OrderLineItem[] { },
        };
        private float mExpectedLineItemsTotal = 0f;
        private float mExpectedGrandTotal = 1.5f;
        private Exception mExpectedException = new ModelValidationException
        (
            new[]
            {
                ModelValidationErrors.ORDER_AMOUNT_OR_LINE_ITEMS_REQUIRED
            }
        );

        public InvalidOrderTestDataMissingAmountOrLineItems()
        {
            Add(mOrder,
                mExpectedLineItemsTotal,
                mExpectedGrandTotal,
                mExpectedException);
        }
    }
}
