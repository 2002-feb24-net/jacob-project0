using System;
using Xunit;
using Project0.Library.Model;

namespace Project0.Tests
{
    public class ProductTest
    {
        private readonly Product _product = new Product();

        [Fact]
        public void Product_DefaultValue_NotNull()
        {
            Assert.NotNull(_product);
        }

        [Fact]
        public void ProductEntry()
        {
            var order = _product;
            Assert.Equal(_product, order);
        }
    }
}
