﻿using System;
using System.Collections.Generic;

namespace Project0.Library.Model
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public int StoreLocationId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public virtual StoreLocation StoreLocation { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
