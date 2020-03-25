using System;
using System.Collections.Generic;

namespace Project0.Library.Model
{
    public partial class StoreLocation
    {
        public StoreLocation()
        {
            Orders = new HashSet<Orders>();
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
