using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsApp.Components;
using SportsApp.Models;
using Xunit;

namespace SportsApp.Tests
{
    public class NavigationMenuComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products)
                .Returns(new Product[]
                {
                    new Product {ProductId = 1, Name = "P1", Category = "Apples"},
                    new Product {ProductId = 2, Name = "P2", Category = "Apples"},
                    new Product {ProductId = 3, Name = "P3", Category = "Plums"},
                    new Product {ProductId = 4, Name = "P4", Category = "Oranges"},
                });

            var target = new NavigationMenuViewComponent(mock.Object);

            // Act
            var results = ((IEnumerable<string>) (target.Invoke() as ViewViewComponentResult)?.ViewData.Model).ToArray();

            // Assert
            Assert.True(new string[] {"Apples", "Oranges", "Plums"}.SequenceEqual(results));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            // Arrange
            const string categoryToSelect = "Apples";
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products)
                .Returns(new Product[]
                {
                    new Product {ProductId = 1, Name = "P1", Category = "Apples"},
                    new Product {ProductId = 2, Name = "P2", Category = "Oranges"}
                });
            var target = new NavigationMenuViewComponent(mock.Object)
            {
                ViewComponentContext = new ViewComponentContext
                {
                    ViewContext = new ViewContext
                    {
                        RouteData = new RouteData()
                    }
                }
            };

            target.RouteData.Values["category"] = categoryToSelect;

            // Action
            var result = (string) (target.Invoke() as ViewViewComponentResult)?.ViewData["SelectedCategory"];

            // Assert
            Assert.Equal(categoryToSelect, result);
        }
    }
}