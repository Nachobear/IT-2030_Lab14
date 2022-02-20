using System;
using System.Collections.Generic;
using System.Text;

using Lab14.Controllers;
using Lab14.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;


namespace Lab14.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            //Arrange
            var rep = new Mock<IRepository<Contact>>();
            var controller = new HomeController(rep.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }
        

        [Fact]
        public void Index_ModelIsACollectionOfContactObjects()
        {
            //Arrange
            var rep = new Mock<IRepository<Contact>>();
            rep.Setup(m => m.List(It.IsAny<QueryOptions<Contact>>())).Returns(new List<Contact>());
            //                    ^List parameter can only be QueryOptions<Contact>   ^List can only return a list of Contact objects
            var controller = new HomeController(rep.Object);


            //Act
            var viewResult = (ViewResult)controller.Index();
            var model = viewResult.ViewData.Model as List<Contact>;


            //Assert
            Assert.IsType<List<Contact>>(model);


        }

    }
}
