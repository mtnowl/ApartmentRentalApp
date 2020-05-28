using ApartmentRental.Controllers;
using ApartmentRental.Helpers;
using ApartmentRental.Models;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace ApartmentRentalTest
{
    public class UserTest : IDisposable
    {
        private UserController _controller;
        public UserTest()
        {
            _controller = SetupDefaultController();
        }

        public void Dispose()
        {
            _controller = null;
        }

        [Fact]
        public async void LoginNonExistingUser()
        {
            var controller = _controller;
            var result = await controller.Authenticate(new AuthenticateModel
            {
                Username = "asdf",
                Password = "1234"
            });

            var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [MemberData(nameof(UserData))]
        public async void LoginExistingUsers(AuthenticateModel model)
        {
            var controller = _controller;
            var result = await controller.Authenticate(model);

            var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
            Assert.IsType<OkObjectResult>(result);
        }

        public static IEnumerable<object[]> UserData =>
            new List<object[]>
            {
                new object[] { new AuthenticateModel { Username = "admin", Password = "admin"} },
                new object[] { new AuthenticateModel { Username = "realtor", Password = "realtor"} },
                new object[] { new AuthenticateModel { Username = "client", Password = "client"} }
            };

        private UserController SetupDefaultController()
        {
            var options = new DbContextOptionsBuilder<ApartmentContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApartmentContext(options))
            {
                context.Users.Add(new User { Id = 1, Username = "admin", Password = "admin", Role = Role.Admin });
                context.Users.Add(new User { Id = 2, Username = "realtor", Password = "realtor", Role = Role.Realtor });
                context.Users.Add(new User { Id = 3, Username = "client", Password = "client", Role = Role.Client });
                context.SaveChanges();
            }

            var appSettings = new AppSettings { Secret = "This is a test Secret string" };
            var appSettingOptions = new Mock<IOptions<AppSettings>>();
            appSettingOptions.SetupGet(a => a.Value).Returns(appSettings);
            var mapper = new Mock<IMapper>();
            var controller = new UserController(
                new ApartmentContext(options),
                appSettingOptions.Object,
                mapper.Object);


            return controller;
        }
    }
}
