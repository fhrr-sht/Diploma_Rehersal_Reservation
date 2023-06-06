using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RehersalReservation;
using System.Threading.Tasks;
using RehersalReservation.Controllers;
using RehersalReservation.Models;
using Services;
using Entity;
using System.Web.Http.Results;
using System.Web.Http;

namespace RehersalReservation.Tests.Controllers
{
    [TestClass]
    public class RehersalControllerTest
    {

        Mock<IRehersalService> rehersalService = new Mock<IRehersalService>();
        Mock<IRoomService> roomService = new Mock<IRoomService>();
        RehersalWebApiController controller = null;
        const int IdOne = 1;
        const int IdTwo = 2;
        public RehersalControllerTest()
        {
            controller = new RehersalWebApiController(rehersalService.Object, roomService.Object);
        }
        [TestMethod]
        public async Task Rehersals()
        {
            // Act
            OkNegotiatedContentResult<IEnumerable<RehersalSpace>> result = await controller.Rehersals() as OkNegotiatedContentResult<IEnumerable<RehersalSpace>>;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Content, typeof(IEnumerable<RehersalSpace>));
            rehersalService.VerifyAll();
        }

        [TestMethod]
        public async Task DeleteSuccessful()
        {
            // Arrange
            roomService.Setup(room => room.GetRoomByRehersalID(IdOne)).
                ReturnsAsync(new List<Entity.Room>()
                {
                });
            // Act
            OkResult result = await controller.Delete(IdOne) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            roomService.VerifyAll();
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            // Arrange
            roomService.Setup(room => room.GetRoomByRehersalID(IdOne)).
                ReturnsAsync(new List<Entity.Room>() {
                new Entity.Room()
                {
                    RehersalSpaceID = IdOne
                }
            });
            // Act
            IHttpActionResult result = await controller.Delete(IdOne) ;

            // Assert
            Assert.IsNotNull(result as InternalServerErrorResult);
            roomService.VerifyAll();
        }
        [TestMethod]
        public async Task DeleteNotFound()
        {
            // Arrange
            roomService.Setup(room => room.GetRoomByRehersalID(IdOne)).
                ReturnsAsync(new List<Entity.Room>() {
                new Entity.Room()
                {
                    RoomId = IdOne
                }
            });
            // Act
            IHttpActionResult result = await controller.Delete(IdTwo);

            // Assert
            Assert.IsNotNull(result as NotFoundResult);
        }
        [TestMethod]
        public async Task EditSuccessful()
        {
            //Arrange
            RehersalSpace rehersalSpace = new RehersalSpace();
            // Act
            IHttpActionResult result = await controller.Edit(rehersalSpace);

            // Assert
            Assert.IsNotNull(result as OkResult);
        }
        [TestMethod]
        public async Task EditFail()
        {
            //Arrange
            RehersalSpace rehersalSpace = new RehersalSpace();
            controller.ModelState.AddModelError("error", "error");
            // Act
            IHttpActionResult result = await controller.Edit(rehersalSpace);
            // Assert
            Assert.IsNotNull(result as BadRequestResult);
        }
        [TestMethod]
        public async Task AddSuccessful()
        {
            //Arrange
            RehersalSpace rehersalSpace = new RehersalSpace();
            // Act
            IHttpActionResult result = await controller.Add(rehersalSpace);

            // Assert
            Assert.IsNotNull(result as OkResult);
        }
        [TestMethod]
        public async Task AddFail()
        {
            //Arrange
            RehersalSpace rehersalSpace = new RehersalSpace();
            controller.ModelState.AddModelError("error", "error");
            // Act
            IHttpActionResult result = await controller.Add(rehersalSpace);
            // Assert
            Assert.IsNotNull(result as BadRequestResult);
        }
        [TestMethod]
        public async Task GetRehersalByCityIDSuccessful()
        {
            // Arrange
            rehersalService.Setup(rehersal => rehersal.GetRehersalByCityID(IdOne)).
                  ReturnsAsync(new List<RehersalSpase>() { new RehersalSpase() { } });
            // Act
            OkNegotiatedContentResult<IEnumerable<RehersalSpace>> result = await controller.GetRehersalByCityID(IdOne) as OkNegotiatedContentResult<IEnumerable<RehersalSpace>>;
            // Assert
            Assert.IsNotNull(result);
            rehersalService.VerifyAll();
        }
        [TestMethod]
        public async Task GetRehersalByCityIDFail()
        {
            // Arrange
            rehersalService.Setup(rehersal => rehersal.GetRehersalByCityID(IdOne)).
                  ReturnsAsync(new List<RehersalSpase>() { new RehersalSpase() { } });
            // Act
            IHttpActionResult result = await controller.GetRehersalByCityID(IdTwo);
            // Assert
            Assert.IsNotNull(result as NotFoundResult);
        }
    }
}
