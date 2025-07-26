using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{

    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

    public enum PaymentMethod
    {
        CreditCard,
        PayPal,
        CashOnDelivery
    }
    public class Order
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } // M-1
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; } // 1-M
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
    }
}
