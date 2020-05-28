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
using System.Linq;
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

        [Theory]
        [MemberData(nameof(BadUserTheoryData))]
        public async void LoginNonExistingUser(AuthenticateModel model)
        {
            var result = await _controller.Authenticate(model);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [MemberData(nameof(UserTheoryData))]
        public async void LoginExistingUsers(AuthenticateModel model)
        {
            var result = await _controller.Authenticate(model);

            Assert.IsType<OkObjectResult>(result);
        }

        public static TheoryData<AuthenticateModel> UserTheoryData =>
            new TheoryData<AuthenticateModel>
            {
                { new AuthenticateModel { Username = "admin", Password = "admin"} },
                { new AuthenticateModel { Username = "realtor", Password = "realtor"} },
                { new AuthenticateModel { Username = "client", Password = "client"} }
            };

        public static TheoryData<AuthenticateModel> BadUserTheoryData =>
            new TheoryData<AuthenticateModel>
            {
                { new AuthenticateModel { Username = "asdf", Password = "1234"} },
                { new AuthenticateModel { Username = "realtor", Password = "admin"} }
            };

        private UserController SetupDefaultController()
        {
            var options = new DbContextOptionsBuilder<ApartmentContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApartmentContext(options))
            {
                context.Users.Add(new User { 
                    Id = 1, 
                    Username = "admin", 
                    Password = "OT672nTS108fBgHT9JWFEucWAv+dGEiS0X5E2G65TWI=", 
                    PasswordSalt = Convert.FromBase64String("SEfcsflTemzwn2t/MP00Mw=="), 
                    Role = Role.Admin });
                context.Users.Add(new User { 
                    Id = 2, 
                    Username = "realtor", 
                    Password = "1zjqPT0Db2SkhOS4OTS2/37QRQwe/ezFkDN+D47QIO4=", 
                    PasswordSalt = Convert.FromBase64String("tYwvfcbNoAFjLOgwhYpGaQ=="), 
                    Role = Role.Realtor });
                context.Users.Add(new User { 
                    Id = 3, 
                    Username = "client", 
                    Password = "0BBnc7tkaLphnwl207AJTLjQwrzV/sFftfHVvTv8GPw=", 
                    PasswordSalt = Convert.FromBase64String("BAV88E56WMdNcAU5y7ZDfw=="), 
                    Role = Role.Client });
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
