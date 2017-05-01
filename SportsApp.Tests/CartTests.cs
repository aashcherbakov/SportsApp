﻿using System.Linq;
using SportsApp.Models;
using Xunit;

namespace SportsApp.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            var product1 = new Product {ProductId = 1, Name = "P1"};
            var product2 = new Product {ProductId = 2, Name = "P2"};

            var target = new Cart();

            target.AddItem(product1, 1);
            target.AddItem(product2, 1);
            var results = target.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(product1, results[0].Product);
            Assert.Equal(product2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            var product1 = new Product {ProductId = 1, Name = "P1"};
            var product2 = new Product {ProductId = 2, Name = "P2"};

            var target = new Cart();

            target.AddItem(product1, 1);
            target.AddItem(product2, 1);
            target.AddItem(product1, 10);
            var results = target.Lines.OrderBy(c => c.Product.ProductId).ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            var product1 = new Product {ProductId = 1, Name = "P1"};
            var product2 = new Product {ProductId = 2, Name = "P2"};
            var product3 = new Product {ProductId = 3, Name = "P3"};

            var target = new Cart();

            target.AddItem(product1, 1);
            target.AddItem(product2, 3);
            target.AddItem(product3, 5);
            target.AddItem(product2, 1);

            target.RemoveLine(product2);

            Assert.Equal(0, target.Lines.Count(c => c.Product == product2));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            var product1 = new Product {ProductId = 1, Name = "P1", Price = 100M};
            var product2 = new Product {ProductId = 2, Name = "P2", Price = 50M};
            var target = new Cart();

            target.AddItem(product1, 1);
            target.AddItem(product2, 1);
            target.AddItem(product1, 3);
            var result = target.ComputeTotalValue();

            Assert.Equal(450M, result);
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            var product1 = new Product {ProductId = 1, Name = "P1", Price = 100M};
            var product2 = new Product {ProductId = 2, Name = "P2", Price = 50M};
            var target = new Cart();

            target.AddItem(product1, 1);
            target.AddItem(product2, 1);

            target.Clear();

            Assert.Equal(0, target.Lines.Count());
        }
    }
}