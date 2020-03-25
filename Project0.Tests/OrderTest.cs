using System;
using Xunit;
using Project0.Library.Model;

namespace Project0.Tests
{
    public class OrderTest
    {

        private readonly Orders _order = new Orders();

        [Fact]
        public void Orders_DefaultValue_NotNull()
        {
            Assert.NotNull(_order);
        }

        [Fact]
        public void OrderEntry()
        {
            var order = _order;
            Assert.Equal(_order,order);
        }
    }
}
