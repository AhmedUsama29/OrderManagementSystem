﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderItem
    {

        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } // M-1
        public int ProductId { get; set; }
        public Product Product { get; set; } // M-1
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }

    }
}
