using AliExpress.Application.Contract;
using AliExpress.Application.IServices;
using AliExpress.Dtos.Order;
using AliExpress.Models;
using AliExpress.Models.Orders;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AliExpress.Application.Services
{
    public class OrderService :IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository ,IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderReturnDto> CreateOrderAsync(int cartId, int deliveryMethodId, string userId)
        {
            var appUser = await _orderRepository.GetAppUserAsync(userId);
            var order = await _orderRepository.CreateOrderAsync(cartId, deliveryMethodId, appUser);
            var mappedOrder=_mapper.Map<Order,OrderReturnDto>(order);
            return mappedOrder;
        }

        public async Task DeleteOrderAsync(int orderId)
        {
           var order=await _orderRepository.GetOrderByIdAsync(orderId);
            await _orderRepository.DeleteAsync(order);
         }

        public async Task<IEnumerable<OrderReturnDto>> GetAllOrdersAsync()
        {
           var orders=await _orderRepository.GetAllAsync();
           var mappedOrders=_mapper.Map<IEnumerable<Order>,IEnumerable<OrderReturnDto>>(orders);
            return mappedOrders;
        }

        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderRepository.GetDeliveryMethods();
            return deliveryMethods;
        }

        public async Task<OrderReturnDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            var mappedOrder = _mapper.Map<Order, OrderReturnDto>(order);
            return mappedOrder;
        }

        public async Task<OrderReturnDto> GetOrderByUserIdAsync(string userId)
        {
            var order=await _orderRepository.GetByUserIdAsync(userId);
            var mappedOrder = _mapper.Map<Order, OrderReturnDto>(order);
            
            return mappedOrder;
        }

        public async Task UpdateOrderAsync(int orderId, OrderReturnDto orderReturnDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order.Id == orderReturnDto.Id)
            {
                var mappedOrder = _mapper.Map<OrderReturnDto, Order>(orderReturnDto);
                await _orderRepository.UpdateAsync(mappedOrder);
            }
        }

        public async Task UpdateOrderMvcAsync(int orderId, OrderReturnDto orderReturnDto)
        {
            var existingOrder = await _orderRepository.GetOrderByIdAsync(orderId);

            if (existingOrder != null)
            {
                if (orderReturnDto.Status== "Payment Received")
                {
                    existingOrder.Status = OrderStatus.PaymentRecieved;
                }
                else if(orderReturnDto.Status == "Payment Failed") {
                    existingOrder.Status = OrderStatus.PaymentFailed;
                }
                else
                {
                    existingOrder.Status = OrderStatus.Pending;
                }


                await _orderRepository.UpdateAsync(existingOrder);

              
            }
        }


        //public async Task UpdateOrderByAdminAsync(int orderId, OrderStatusDto OrderStatusDto)
        //{
        //    var order = await _orderRepository.GetOrderByIdAsync(orderId);
        //    //order.Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderStatusDto.Status);
        //    var mappedOrder = _mapper.Map<OrderStatusDto, Order>(OrderStatusDto);
        //    await _orderRepository.UpdateAsync(mappedOrder);
        //    //await _orderRepository.UpdateAsync(order);
        //}

        //public async Task UpdateOrderByAdminAsync(int orderId, OrderReturnDto orderReturnDto)
        //{
        //    var order = await _orderRepository.GetOrderByIdAsync(orderId);
        //    order.Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderReturnDto.Status);

        //    await _orderRepository.UpdateAsync(order);
        //}
    }
}
