using System.Collections.Generic;
using System.Linq;
using Moq;
using SportsApp.Controllers;
using SportsApp.Models;
using SportsApp.Models.ViewModels;
using Xunit;

namespace SportsApp.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products)
                .Returns(DummyProducts);

            var controller = new ProductController(mock.Object) {PageSize = 3};

            // Act
            var result = controller.List(2).ViewData.Model as ProductListViewModel;

            // Assert
            var productArray = result.Products.ToArray();
            Assert.True(productArray.Length == 2);
            Assert.Equal("P4", productArray[0].Name);
            Assert.Equal("P5", productArray[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
             // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(DummyProducts());
            var controller = new ProductController(mock.Object) {PageSize = 3};

            // Act
            var result = controller.List(2).ViewData.Model as ProductListViewModel;

            // Assert
            var pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        private static IEnumerable<Product> DummyProducts()
        {
            return new Product[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"}
            };
        }
    }
}