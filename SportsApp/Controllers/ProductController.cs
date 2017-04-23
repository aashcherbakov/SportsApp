using Microsoft.AspNetCore.Mvc;
using SportsApp.Models;

namespace SportsApp.Controllers
{
    public class ProductController: Controller
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List() => View(repository.Products);
    }
}