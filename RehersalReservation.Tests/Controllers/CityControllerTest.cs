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
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace RehersalReservation.Tests.Controllers
{
    [TestClass]
    public class CityControllerTest
    {
        Mock<IRehersalService> rehersalService = new Mock<IRehersalService>();
        Mock<ICityService> cityService = new Mock<ICityService>();
        const int IdOne = 1;
        const int IdTwo = 2;
        CityWebApiController controller = null;
        public CityControllerTest()
        {
            controller = new CityWebApiController(cityService.Object, rehersalService.Object);
        }
        [TestMethod]
        public async Task Cities()
        {
            // Arrange
            cityService.Setup(city => city.GetCities()).
                ReturnsAsync(new List<Entity.City>()
        {
        });
            // Act
            OkNegotiatedContentResult<IEnumerable<City>> result = await controller.GetCities() as OkNegotiatedContentResult<IEnumerable<City>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Content, typeof(IEnumerable < City >));
            cityService.VerifyAll();
        }
        [TestMethod]
        public async Task DeleteSuccessful()
        {
            // Arrange
            rehersalService.Setup(rehersal => rehersal.GetRehersalByCityID(IdOne)).
                ReturnsAsync(new List<Entity.RehersalSpase>()
                {
                });


            // Act
            OkResult result = await controller.Delete(IdOne) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            rehersalService.VerifyAll();
        }
        [TestMethod]
        public async Task DeleteFail()
        {
            // Arrange
            rehersalService.Setup(rehersal => rehersal.GetRehersalByCityID(IdOne)).
                ReturnsAsync(new List<Entity.RehersalSpase>() {
                new Entity.RehersalSpase()
                {
                    RehersalSpaseID = IdOne
                }
            });
            // Act
            IHttpActionResult result = await controller.Delete(IdOne) as IHttpActionResult;

            // Assert
            Assert.IsNotNull(result as InternalServerErrorResult);
            rehersalService.VerifyAll();
        }
        [TestMethod]
        public async Task DeleteNotFound()
        {
            // Arrange
            rehersalService.Setup(rehersal => rehersal.GetRehersalByCityID(IdOne)).
                ReturnsAsync(new List<Entity.RehersalSpase>() {
                new Entity.RehersalSpase()
                {
                    RehersalSpaseID = IdOne
                }
            });
            // Act
            NotFoundResult result = await controller.Delete(IdTwo) as NotFoundResult; ;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task EditSuccessful()
        {
            //Arrange
            City city = new City();
            // Act
            IHttpActionResult result = await controller.Edit(city);

            // Assert
            Assert.IsNotNull(result as OkResult);
        }
        [TestMethod]
        public async Task EditFail()
        {
            //Arrange
            City city = new City();
            controller.ModelState.AddModelError("error", "error");
            // Act
            IHttpActionResult result = await controller.Edit(city) as IHttpActionResult;
            // Assert
            Assert.IsNotNull(result as BadRequestResult);
        }
        [TestMethod]
        public async Task AddSuccessful()
        {
            //Arrange
            City city = new City();
            // Act
            IHttpActionResult result = await controller.Create(city) ;

            // Assert
            Assert.IsNotNull(result as OkResult);
        }
        [TestMethod]
        public async Task AddFail()
        {
            //Arrange
            City city = new City();
            controller.ModelState.AddModelError("error", "error");
            // Act
            IHttpActionResult result = await controller.Create(city) as IHttpActionResult; ;
            // Assert
            Assert.IsNotNull(result as BadRequestResult);
        }

    }
}
