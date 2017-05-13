using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportsApp.Controllers;
using SportsApp.Models;
using Xunit;

namespace SportsApp.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            // Arrange
            var mock = SetupMock();
            var target = new AdminController(mock.Object);

            // Action
            var result = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();

            // Assert
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }

        [Fact]
        public void Can_Edit_Product()
        {
            var mock = SetupMock();
            var target = new AdminController(mock.Object);

            var p1 = GetViewModel<Product>(target.Edit(1));
            var p2 = GetViewModel<Product>(target.Edit(2));
            var p3 = GetViewModel<Product>(target.Edit(3));

            Assert.Equal(1, p1.ProductId);
            Assert.Equal(2, p2.ProductId);
            Assert.Equal(3, p3.ProductId);
        }

        [Fact]
        public void Cannot_Edit_Nonexistent_Product()
        {
            var mock = SetupMock();
            var target = new AdminController(mock.Object);

            var result = GetViewModel<Product>(target.Edit(4));

            Assert.Null(result);
        }

        [Fact]
        public void Can_Save_Valid_Changes()
        {
            var mock = new Mock<IProductRepository>();
            var tempData = new Mock<ITempDataDictionary>();
            var target = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };
            var product = new Product {Name = "Test"};

            // Act
            var result = target.Edit(product);

            // Assert
            mock.Verify(m => m.SaveProduct(product));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            var mock = new Mock<IProductRepository>();
            var tempData = new Mock<ITempDataDictionary>();
            var target = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };
            var product = new Product { Name = "Test" };
            target.ModelState.AddModelError("error", "error");

            var result = target.Edit(product);

            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Can_Delete_Valid_Products()
        {
            // Arrange 
            var product = new Product {ProductId = 2, Name = "Test"};
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, Name = "P1"},
                product,
                new Product {ProductId = 3, Name = "P3"}
            });

            var target = new AdminController(mock.Object);
            target.Delete(product.ProductId);

            mock.Verify(m => m.DeleteProduct(product.ProductId));
        }

        private T GetViewModel<T>(IActionResult result) where T: class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

        private static Mock<IProductRepository> SetupMock()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products)
                .Returns(new[]
                    {
                        new Product {ProductId = 1, Name = "P1"},
                        new Product {ProductId = 2, Name = "P2"},
                        new Product {ProductId = 3, Name = "P3"},
                    }
                );
            return mock;
        }
    }
}