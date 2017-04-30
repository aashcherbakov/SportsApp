using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsApp.Models;
using SportsApp.Models.ViewModels;

namespace SportsApp.Controllers
{
    public class ProductController: Controller
    {
        private readonly IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            return View(new ProductListViewModel
            {
                Products = repository.Products
                    .Where(product => category == null || product.Category == category)
                    .OrderBy(p => p.ProductId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        repository.Products.Count() :
                        repository.Products.Count(e => e.Category == category)
                },

                CurrentCategory = category
            });
        }
    }
}