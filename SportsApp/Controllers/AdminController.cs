using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsApp.Models;

namespace SportsApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository repository;

        public AdminController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId) =>
            View(repository.Products.
                FirstOrDefault(p => p.ProductId == productId));
    }
}