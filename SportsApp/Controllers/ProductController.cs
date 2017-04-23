using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsApp.Models;

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
            return View(repository.Products
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize));
        }
    }
}