using PTLab2_testASP.Controllers;
using PTLab2_testASP.Interfaces_;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PTLab2_testASP.Models;
using PTLab2_testASP.Repositories;

namespace PTLab3.Controllers
{
    public class CartControllerTests
    {
        private CartController _cartController;
        private ICartRepository _cartRepository;

        public CartControllerTests()
        {
            _cartRepository = A.Fake<ICartRepository>();

            _cartController = new CartController(_cartRepository);
        }

        [Fact]
        public void CartController_Index_ReturnSuccess()
        {
            var products = A.Fake<List<Product>>();
            A.CallTo(() => _cartRepository.GetAllProduct()).Returns(products);

            var result = _cartController.Index();

            result.Should().BeOfType<ViewResult>();
        }
    }
}
