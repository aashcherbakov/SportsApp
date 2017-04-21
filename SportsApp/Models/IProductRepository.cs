using System.Collections.Generic;

namespace SportsApp.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
}