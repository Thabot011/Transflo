using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Transflo.Contracts.Contract;

namespace Transflo.Services.Abstractions.Service
{
    public interface IDriverService
    {
        Task<DriverDto> CreateDriver(DriverForCreationDto driverForCreationDto);
        Task<DriverDto> UpdateDriver(int driverId, DriverForUpdateDto driverForCreationDto);
        Task DeleteDriver(int driverId);
        List<DriverDto> GetAllDrivers(DriverGetAllDto driverGetAllDto);

        Task CreateDriversRange();
        Task<DriverDto> GetDriverById(int driverId);
        Task<DriverDto> GetAlphabetizedDriverById(int driverId);
        List<DriverDto> GetAlphabetizedList();

    }
}
