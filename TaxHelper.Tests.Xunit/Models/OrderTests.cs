using System;
using System.Collections;
using System.Collections.Generic;
using TaxHelper.Common.Models;
using TaxHelper.Tests.Xunit.Models.TestData;
using Xunit;

namespace TaxHelper.Tests.Xunit.Models
{
    public class OrderTests
    {
        [Theory]
        [ClassData(typeof(ValidOrderTestData))]
        [ClassData(typeof(InvalidOrderTestDataMissingAmountOrLineItems))]
        public void TestOrder(
            Order order,
            float expectedLineItemsTotal,
            float expectedGrandTotal,
            int expectedErrors)
        {
            // test calculated values:
            Assert.Equal(expectedLineItemsTotal, order.LineItemsTotalFloat);
            Assert.Equal(expectedGrandTotal, order.GrandTotalFloat);

            // test validation:
            Assert.Equal(expectedErrors, order.GetErrors().Count);
        }
    }
}
