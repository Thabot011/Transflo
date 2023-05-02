using Moq;
using System;
using System.Threading.Tasks;
using Transflo.Contracts.Contract;
using Transflo.Services.Abstractions.Service;
using Transflo.Services.Service;
using Transflo.Test.Helper;
using Xunit;

namespace Transflo.Test.Service
{
    public class DriverServiceTest
    {
        private readonly IDriverService _driverService;
        public DriverServiceTest()
        {
            var dbContextMock = DBContextMockHelper.GetDBContextMock();
            _driverService = new DriverService(dbContextMock.Object);
        }

        [Fact]
        public void GetAllDrivers_WhenCalledWithFilter_ReturnsFilteredDrivers()
        {
            //Arange
            var filteredDriversCount = 2;
            var driverdto = new DriverGetAllDto
            {
                PageNumber = 1,
                PageSize = 5,
                SortProperty = "",
                DriverFilter = new DriverForFilterDto
                {
                    FirstName = "Ahmed",
                    LastName = "",
                    PhoneNumber = ""
                }
            };

            //Act
            var drivers = _driverService.GetAllDrivers(driverdto);

            //Assert
            Assert.NotNull(drivers);
            Assert.NotEmpty(drivers);
            Assert.Equal(filteredDriversCount, drivers.Count);
            Assert.All(drivers, d => Assert.Equal(driverdto.DriverFilter.FirstName, d.FirstName));
        }



        [Fact]
        public void GetAllDrivers_WhenCalledWithoutFilter_ReturnsAllDrivers()
        {
            //Arange
            int allDriversCount = 4;
            var driverdto = new DriverGetAllDto
            {
                PageNumber = 1,
                PageSize = 5,
                SortProperty = "",
            };

            //Act
            var drivers = _driverService.GetAllDrivers(driverdto);

            //Assert
            Assert.NotNull(drivers);
            Assert.NotEmpty(drivers);
            Assert.Equal(allDriversCount, drivers.Count);

        }


        [Fact]
        public void GetAllDrivers_WhenCalledWithInvalidSortProperty_ThrowsException()
        {
            //Arange
            var driverdto = new DriverGetAllDto
            {
                PageNumber = 1,
                PageSize = 5,
                SortProperty = "Test",
            };

            //Act

            //Assert
            Assert.Throws<Exception>(() => _driverService.GetAllDrivers(driverdto));

        }


        [Fact]
        public void GetAllDrivers_WhenCalledWithPageSize_ReturnsCorrectCount()
        {
            //Arange
            var driverdto = new DriverGetAllDto
            {
                PageNumber = 1,
                PageSize = 3,
                SortProperty = "",
            };

            //Act
            var drivers = _driverService.GetAllDrivers(driverdto);

            //Assert
            Assert.Equal(driverdto.PageSize, drivers.Count);

        }



        [Fact]
        public async Task GetAlphabetizedDriverById_WhenCalled_ReturnsAlphabetizedDriverFirstNameAndLastNameAsync()
        {
            //Arange
            var driverId = 1;
            var sortedFirstName = "Adehm";
            var sortedLastName = "abehTt";

            //Act
            var driver = await _driverService.GetAlphabetizedDriverById(driverId);

            //Assert
            Assert.Equal(sortedFirstName, driver.FirstName);
            Assert.Equal(sortedLastName, driver.LastName);

        }

    }
}
