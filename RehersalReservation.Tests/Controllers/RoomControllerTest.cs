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
using System.Web.Http.Results;
using System.Web.Mvc;

namespace RehersalReservation.Tests.Controllers
{
    [TestClass]
    public class RoomControllerTest
    {
        Mock<IRoomService> roomService = new Mock<IRoomService>();
        const int IdOne = 1;
        const int IdTwo = 2;
        RoomApiController controller = null;
        
        public RoomControllerTest()
        {
            controller = new RoomApiController(roomService.Object);
        }
        [TestMethod]
        public async Task Rooms()
        {
            // Arrange
            roomService.Setup(room => room.GetRooms()).
            ReturnsAsync(new List<Entity.Room>()
                {
                });
            // Act
            OkNegotiatedContentResult<IEnumerable<Room>> result = await controller.Rooms() as OkNegotiatedContentResult<IEnumerable<Room>>;

            // Assert
            Assert.IsNotNull(result);
            roomService.VerifyAll();
        }
        [TestMethod]
        public async Task DeleteSuccessful()
        {
            //Arrange
            // Act
            OkResult result = await controller.Delete(IdOne) as OkResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task EditSuccessful()
        {
            //Arrange
            Room room = new Room();
            // Act
            OkResult result = await controller.Edit(room) as OkResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task EditFail()
        {
            //Arrange
            Room room = new Room(); 
            controller.ModelState.AddModelError("error", "error");
            // Act
            BadRequestResult result = await controller.Edit(room) as BadRequestResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task AddSuccessful()
        {
            //Arrange
            Room room = new Room();
            // Act
            OkResult result = await controller.Create(room) as OkResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task AddFail()
        {
            //Arrange
            Room room = new Room();
            controller.ModelState.AddModelError("error", "error");
            // Act
            BadRequestResult result = await controller.Create(room) as BadRequestResult; 
            // Assert
            Assert.IsNotNull(result);
        }

  
        [TestMethod]
        public async Task GetRoomByRehersalIDSuccessful()
        {
            // Arrange
            roomService.Setup(room => room.GetRoomByRehersalID(IdOne)).
                  ReturnsAsync(new List<Entity.Room>() { new Entity.Room() { } });
            // Act
            OkNegotiatedContentResult<IEnumerable<Room>> result = await controller.GetRoomByRehersalID(IdOne) as OkNegotiatedContentResult<IEnumerable<Room>>;
            // Assert
            Assert.IsNotNull(result);
            roomService.VerifyAll();
        }
        [TestMethod]
        public async Task GetRoomByRehersalIDFail()
        {
            // Arrange
            roomService.Setup(room => room.GetRoomByRehersalID(IdOne)).
                   ReturnsAsync(new List<Entity.Room>() { new Entity.Room() { } });
            // Act
            NotFoundResult result = await controller.GetRoomByRehersalID(IdTwo) as NotFoundResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }

}
