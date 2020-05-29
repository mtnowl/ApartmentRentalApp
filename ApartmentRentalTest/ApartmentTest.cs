using ApartmentRental.Controllers;
using ApartmentRental.Helpers;
using ApartmentRental.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Xunit;

namespace ApartmentRentalTest
{
    public class ApartmentTest : IDisposable
    {
        private ApartmentController _controller;
        public ApartmentTest()
        {
            _controller = SetupDefaultController();
        }
        public void Dispose()
        {
            _controller = null;
        }

        [Theory]
        [MemberData(nameof(GetApartmentRoleTestParams))]
        public async void GetApartmentRoleTest(string role, int expectedCount)
        {
            //Arrange
            _controller.ControllerContext = GetMockedControllerContext(role);
            var aptParams = new ApartmentParameters();

            //Act
            var response = await _controller.GetApartment(aptParams);

            //Assert
            Assert.Equal(expectedCount, response.Value.Count());
        }
        public static TheoryData<string, int> GetApartmentRoleTestParams =>
            new TheoryData<string, int>
            {
                { Role.Client, 2 },
                { Role.Admin, 3 },
                { Role.Realtor, 3 }
            };

        [Theory]
        [MemberData(nameof(GetApartmentFilterTestParams))]
        public async void GetApartmentFilterTest(ApartmentParameters aptParams, int[] expectedIds)
        {
            //Arrange
            _controller.ControllerContext = GetMockedControllerContext(Role.Admin);

            //Act
            var response = await _controller.GetApartment(aptParams);

            //Assert
            Assert.Equal(expectedIds.Length, response.Value.Count());
            foreach(int id in expectedIds)
            {
                Assert.Contains(response.Value, x => x.Id == id);
            }
        }
        public static TheoryData<ApartmentParameters, int[]> GetApartmentFilterTestParams =>
            new TheoryData<ApartmentParameters, int[]>
            {
                { new ApartmentParameters { MaxPrice = 2000 }, new int[] { 2, 3 } },
                { new ApartmentParameters { MinPrice = 2000 }, new int[] { 1 } },
                { new ApartmentParameters { MaxArea = 2000 }, new int[] { 1, 3 } },
                { new ApartmentParameters { MinArea = 2000 }, new int[] { 1, 2 } },
                { new ApartmentParameters { MaxRooms = 5 }, new int[] { 1, 2, 3 } },
                { new ApartmentParameters { MinRooms = 5 }, new int[] { 1 } },
                { new ApartmentParameters { MaxPrice = 2000, MinArea = 2000 }, new int[] { 2 } }
            };

        [Theory]
        [MemberData(nameof(GetSingleApartmentParams))]
        public async void GetSingleApartment(string role, int id, Type expectedType)
        {
            //Arrange
            _controller.ControllerContext = GetMockedControllerContext(role);

            //Act
            var response = await _controller.GetApartment(id);

            //Assert
            if (expectedType == null)
                Assert.Equal(id, response.Value.Id);
            else
                Assert.IsType(expectedType, response.Result);
        }
        public static TheoryData<string, int, Type> GetSingleApartmentParams =>
            new TheoryData<string, int, Type>
            {
                { Role.Client, 2, typeof(NotFoundResult) },
                { Role.Client, 3, null },
                { Role.Realtor, 2, null },
                { Role.Admin, 2, null },
                { Role.Admin, 4, typeof(NotFoundResult)  }
            };

        private ControllerContext GetMockedControllerContext(string role)
        {
            var mockContext = new Mock<HttpContext>(MockBehavior.Strict);

            mockContext.Setup(hc => hc.User.IsInRole(
                It.Is<string>(s => s.Equals(Role.Admin))))
                .Returns(role == Role.Admin);
            mockContext.Setup(hc => hc.User.IsInRole(
                It.Is<string>(s => s.Equals(Role.Realtor))))
                .Returns(role == Role.Realtor);
            mockContext.Setup(hc => hc.User.IsInRole(
                It.Is<string>(s => s.Equals(Role.Client))))
                .Returns(role == Role.Client);

            return new ControllerContext
            {
                HttpContext = mockContext.Object
            };
        }

        private ApartmentController SetupDefaultController()
        {
            var options = new DbContextOptionsBuilder<ApartmentContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApartmentContext(options))
            {
                context.Users.Add(new User
                {
                    Id = 2,
                    Username = "realtor",
                    Password = "1zjqPT0Db2SkhOS4OTS2/37QRQwe/ezFkDN+D47QIO4=",
                    PasswordSalt = Convert.FromBase64String("tYwvfcbNoAFjLOgwhYpGaQ=="),
                    Role = Role.Realtor
                });
                context.Apartments.Add(new Apartment
                {
                    Id = 1,
                    Name = "High living",
                    Description = "Come see the sights from up on high!",
                    Rooms = 5,
                    Area = 2000,
                    MonthlyPrice = 3540,
                    Latitude = 30,
                    Longitude = -95,
                    RealtorUserId = 2,
                    IsRented = false,
                    DateAdded = new DateTime(2020, 05, 31)
                });
                context.Apartments.Add(new Apartment
                {
                    Id = 2,
                    Name = "Next to the water",
                    Description = "Relax with your own private dock.",
                    Rooms = 2,
                    Area = 2500,
                    MonthlyPrice = 1750,
                    Latitude = 28,
                    Longitude = -105,
                    RealtorUserId = 2,
                    IsRented = true,
                    DateAdded = new DateTime(2020, 02, 20)
                });
                context.Apartments.Add(new Apartment
                {
                    Id = 3,
                    Name = "Low living",
                    Description = "Stay close to the pulse!",
                    Rooms = 1,
                    Area = 500,
                    MonthlyPrice = 999.99M,
                    Latitude = 45,
                    Longitude = -110,
                    RealtorUserId = 2,
                    IsRented = false,
                    DateAdded = new DateTime(2020, 03, 01)
                });
                context.SaveChanges();
            }

            var appSettings = new AppSettings { Secret = "This is a test Secret string" };
            var appSettingOptions = new Mock<IOptions<AppSettings>>();
            appSettingOptions.SetupGet(a => a.Value).Returns(appSettings);
            var controller = new ApartmentController(
                new ApartmentContext(options));

            return controller;
        }

    }
}
