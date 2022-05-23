using System;
using System.Collections;
using System.Collections.Generic;
using TaxHelper.Common.Exceptions;
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
            Exception expectedException)
        {
            // arrange
            // nothing to do here - ClassData arranges the state

            // act
            var actualLineItemsTotal = order.LineItemsTotalFloat;
            var actualGrandTotalFloat = order.GrandTotalFloat;
            var actualException = Record.Exception(order.ThrowIfInvalid);

            // assert
            Assert.Equal(expectedLineItemsTotal, actualLineItemsTotal);
            Assert.Equal(expectedGrandTotal, actualGrandTotalFloat);
            Assert.Equal(expectedException, actualException);
        }
    }
}
