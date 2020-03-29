using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Orders;
using WebStore.ViewModels;
using WebStore.ViewModels.Orders;

namespace WebStore.Infrastructure.Interfaces
{
    interface IOrderService
    {
        IEnumerable<Order> GetUserOrders(string Username);

        Order GetOrderById(int id);

        Order CreateOrder(string Username, CartViewModel cart, OrderViewModel orderModel);
    }
}
