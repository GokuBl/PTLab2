using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTLab2_testASP.Models;
using FakeItEasy;
using FluentAssertions;

namespace PTLab3
{
    public class ModelTests
    {
        [Fact]
        public void ProductModel_Test()
        {
            List<Product> products = A.Fake<List<Product>>();

            products.Select(p=>p.ID).ToList().Should().BeOfType<List<int>>();
            products.Select(p => p.Name).ToList().Should().BeOfType<List<string>>();
            products.Select(p => p.Price).ToList().Should().BeOfType<List<int>>();
        }

        [Fact]
        public void PurchaseModel_Test()
        {
            List<Purchase> products = A.Fake<List<Purchase>>();

            products.Select(p => p.ID).ToList().Should().BeOfType<List<int>>();
            products.Select(p => p.Person).ToList().Should().BeOfType<List<string>>();
            products.Select(p => p.Address).ToList().Should().BeOfType<List<string>>();
        }

        [Fact]
        public void ShopCartModel_Test()
        {
            List<ShopCart> products = A.Fake<List<ShopCart>>();

            products.Select(p => p.ID).ToList().Should().BeOfType<List<int>>();
            products.Select(p => p.ProductId).ToList().Should().BeOfType<List<int>>();
            products.Select(p => p.PurchaseId).ToList().Should().BeOfType<List<int>>();
        }
    }
}
