using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

        private T GetViewModel<T>(IActionResult result) where T: class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

        private static Mock<IProductRepository> SetupMock()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products)
                .Returns(new Product[]
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