using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.RequestHelpers.Extensions
{
    public static class OrderExtension
    {
        public static IQueryable<OrderDto> ProjectOrderToOrderDto(this IQueryable<Order> query)
        { 
            return query
                .Select(order => new OrderDto
                {
                    OrderId = order.OrderId,
                    BuyerId = order.BuyerId,
                    ShippingAddress = order.ShippingAddress,
                    Subtotal = order.Subtotal,
                    AdditionalExpenses = order.AdditionalExpenses,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus.ToString(),
                    Total = order.GetTotal(),
                    OrderItems = order.OrderItems!.Select(item => new OrderItemDto
                    {
                        ProductId = item.ItemOrdered!.ProductId,
                        Name = item.ItemOrdered.Name,
                        ImageUrl = item.ItemOrdered.ImageUrl,
                        Price = item.Price,
                        Size = item.Size,
                        Quantity = item.Quantity
                    })
                    .ToList()
                }).AsNoTracking();
        }
    }
}