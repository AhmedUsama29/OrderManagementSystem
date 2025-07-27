using Shared.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IOrderService
    {
        public Task<OrderResponse> CreateAsync(OrderRequest orderRequest);

        public Task<IEnumerable<OrderResponse>> GetOrdersByIdAsync(int userId);

        public Task<IEnumerable<OrderResponse>> GetAllAsync();

        public Task UpdateStatusAsync(int orderId, string status);
    }
}
