using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsApp.Infrastructure;
using SportsApp.Models;
using SportsApp.Models.ViewModels;

namespace SportsApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository repository;
        private readonly Cart cart;

        public CartController(IProductRepository repository, Cart cartService)
        {
            this.repository = repository;
            cart = cartService;
        }

        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
                cart.AddItem(product, 1);

            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
                cart.RemoveLine(product);

            return RedirectToAction("Index", new {returnUrl});
        }
    }
}