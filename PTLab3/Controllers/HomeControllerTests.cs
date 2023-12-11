using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PTLab2_testASP.Controllers;
using PTLab2_testASP.Interfaces_;
using PTLab2_testASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTLab3.Controllers
{
    public class HomeControllerTests
    {
        private IHomeRepository _homeRepository;
        private HomeController _homeController;

        public HomeControllerTests()
        {
            _homeRepository = A.Fake<IHomeRepository>();
            _homeController = new HomeController(_homeRepository);
        }

        [Fact]
        public void HomeController_Index_ReturnSuccess()
        {
            var products = A.Fake<List<Product>>();
            A.CallTo(()=> _homeRepository.GetAll()).Returns(products);

            var result = _homeController.Index();

            result.Should().BeOfType<ViewResult>();
        }
    }
}
