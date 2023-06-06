using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RehersalReservation.Controllers;
using RehersalReservation.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RehersalReservation.Tests.Controllers
{
    [TestClass]
    public class OrderControllerTest
    {
        Mock<IOrderService> orderService = new Mock<IOrderService>();
        
        const int IdOne = 1;
        const int IdTwo = 2;
        OrderController controller = null;
        public OrderControllerTest()
        {
            controller = new OrderController(orderService.Object, null, null);
        }

        [TestMethod]
        public async Task Orders()
        {
            // Arrange
            orderService.Setup(order => order.GetOrders()).
                ReturnsAsync(new List<Entity.Order>(){});
            // Act
            ViewResult result = await controller.Orders() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<Order>));
            orderService.VerifyAll();
        }

        [TestMethod]
        public async Task DeleteSuccessful()
        {
            // Arrange   

            // Act
            RedirectToRouteResult result = await controller.Delete(IdOne) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.RouteValues.ContainsKey("controller"));
            Assert.AreEqual("", result.RouteName);
            Assert.IsTrue(result.RouteValues.ContainsKey("action"));
            Assert.AreEqual("Orders", result.RouteValues["action"]);
        }
        [TestMethod]
        public async Task EditSuccessful()
        {
            //Arrange
            Order order = new Order();
            // Act
            RedirectToRouteResult result = await controller.Edit(order) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.RouteValues.ContainsKey("controller"));
            Assert.AreEqual("", result.RouteName);
            Assert.IsTrue(result.RouteValues.ContainsKey("action"));
            Assert.AreEqual("Orders", result.RouteValues["action"]);
        }
        [TestMethod]
        public async Task EditFail()
        {
            //Arrange
            Order order = new Order();
            controller.ModelState.AddModelError("error", "error");
            // Act
            HttpStatusCodeResult result = await controller.Edit(order) as HttpStatusCodeResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
        [TestMethod]
        public async Task AddSuccessful()
        {
            //Arrange
            Order order = new Order();
            // Act
            RedirectToRouteResult result = await controller.Create(order) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.RouteValues.ContainsKey("controller"));
            Assert.AreEqual("", result.RouteName);
            Assert.IsTrue(result.RouteValues.ContainsKey("action"));
            Assert.AreEqual("Orders", result.RouteValues["action"]);
        }
        [TestMethod]
        public async Task AddFail()
        {
            //Arrange
            Order order = new Order();
            controller.ModelState.AddModelError("error", "error");
            // Act
            HttpStatusCodeResult result = await controller.Create(order) as HttpStatusCodeResult; ;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
        [TestMethod]
        public void Create()
        {
            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task EditByIDSuccessful()
        {
            // Arrange
            orderService.Setup(order => order.GetOrderByID(IdOne)).
                ReturnsAsync(new Entity.Order() { });
            // Act
            ViewResult result = await controller.Edit(IdOne) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Order));
            orderService.VerifyAll();
        }
        [TestMethod]
        public async Task EditByIDFail()
        {
            // Arrange
            orderService.Setup(order => order.GetOrderByID(IdOne)).
                ReturnsAsync(new Entity.Order() { });
            // Act
            HttpStatusCodeResult result = await controller.Edit(IdTwo) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}
