using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using ServicesAbstraction;
using Shared.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IUnitOfWork _unitOfWork, IMapper _mapper) : IOrderService
    {

        public async Task<OrderResponse> CreateAsync(OrderRequest orderRequest)
        {
            var orderItems = new List<OrderItem>();

            foreach (var item in orderRequest.OrderItems)
            {
                var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(item.ProductId)
                               ?? throw new ProductNotFoundException(item.ProductId);

                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount
                };

                orderItems.Add(orderItem);
            }

            var order = new Order
            {
                CustomerId = orderRequest.CustomerId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = orderRequest.TotalAmount,
                OrderItems = orderItems,
                PaymentMethod = Enum.Parse<PaymentMethod>(orderRequest.PaymentMethod),
                Status = Enum.Parse<OrderStatus>(orderRequest.Status)
            };

            var orderRepo = _unitOfWork.GetRepository<Order>();
            orderRepo.Add(order);

            await _unitOfWork.SaveChanges();

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<OrderResponse>> GetAllAsync()
        {
            var orders = await _unitOfWork.GetRepository<Order>().GetAllAsync();
            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Order>();

            var orders = (await repo.GetAllAsync()
                                    ).Where(o => o.Customer.Id == id);

            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }
        public async Task UpdateStatusAsync(int orderId, string status)
        {
            var repo = _unitOfWork.GetRepository<Order>();
            var order = await repo.GetByIdAsync(orderId)
                        ?? throw new Exception($"No order with this id : {orderId} was found");

            order.Status = Enum.Parse<OrderStatus>(status);
            await _unitOfWork.SaveChanges();
        }
    }
}
