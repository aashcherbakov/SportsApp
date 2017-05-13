using System.Collections.Generic;
using System.Linq;

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

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                var dbEntity = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (dbEntity != null)
                {
                    dbEntity.Name = product.Name;
                    dbEntity.Description = product.Description;
                    dbEntity.Price = product.Price;
                    dbEntity.Category = product.Category;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            var dbEntry = context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (dbEntry != null)
            {   
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}