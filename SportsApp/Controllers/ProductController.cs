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

        public ViewResult List(int page = 1)
        {
            return View(new ProductListViewModel
            {
                Products = repository.Products
                    .OrderBy(p => p.ProductId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            });
        }
    }
}