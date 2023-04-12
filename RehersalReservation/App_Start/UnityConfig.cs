using RehersalReservation.DataAccessLayer;
using RehersalReservation.DataAccessLayer.Contracts;
using Services;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace RehersalReservation
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IConnection, BaseRepository>();
            container.RegisterType<IRehersalRepository, RehersalRepository>();
            container.RegisterType<IRehersalService, RehersalService>();
            container.RegisterType<ICityRepository, CityRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ICityService, CityService>();
            container.RegisterType<IRoomRepository, RoomRepository>();
            container.RegisterType<IRoomService, RoomService>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<ICalendarRepository, CalendarRepository>();
            container.RegisterType<ICalendarService, CalendarService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}