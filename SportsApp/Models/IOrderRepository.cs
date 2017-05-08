using System.Collections.Generic;

namespace SportsApp.Models
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }

        void SaveOrder(Order order);
    }
}