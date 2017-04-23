using System.Collections.Generic;

namespace SportsApp.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// List of all products
        /// </summary>
        public IEnumerable<Product> Products => context.Products;

        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

    }
}