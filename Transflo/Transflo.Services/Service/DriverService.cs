using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Threading.Tasks;
using Transflo.Contracts.Contract;
using Transflo.Domain.Entities;
using Transflo.Persistence.DBContext;
using Transflo.Services.Abstractions.Service;

namespace Transflo.Services.Service
{
    public class DriverService : IDriverService
    {
        private readonly TransfloDBContext _context;
        public DriverService(TransfloDBContext context)
        {
            _context = context;
        }
        public async Task<DriverDto> CreateDriver(DriverForCreationDto driverForCreationDto)
        {
            var driver = driverForCreationDto.Adapt<Driver>();
            var inserted = await _context.Drivers.AddAsync(driver);
            await _context.SaveChangesAsync();
            var driverDto = inserted.Entity.Adapt<DriverDto>();
            return driverDto;
        }

        public async Task CreateDriversRange()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            IEnumerable<Driver> drivers = Enumerable.Range(0, 100).Select(d => new Driver
            {
                FirstName = new string(Enumerable.Repeat(chars, 10)
        .Select(s => s[random.Next(s.Length)]).ToArray()),
                LastName = new string(Enumerable.Repeat(chars, 10)
        .Select(s => s[random.Next(s.Length)]).ToArray()),
                PhoneNumber = new Random().Next(0, 999999999).ToString("D9")
            });
            await _context.Drivers.AddRangeAsync(drivers);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDriver(int driverId)
        {
            var driver = await _context.Drivers.AsQueryable().FirstOrDefaultAsync(d => d.Id == driverId);
            if (driver == null)
            {
                throw new Exception("Driver not found");
            }

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();

        }

        public List<DriverDto> GetAllDrivers(DriverGetAllDto driverGetAllDto)
        {
            var drivers = _context.Drivers.AsQueryable();
            if (driverGetAllDto.DriverFilter != null)
            {
                drivers = drivers.Where(d => (string.IsNullOrEmpty(driverGetAllDto.DriverFilter.FirstName) || d.FirstName.Contains(driverGetAllDto.DriverFilter.FirstName)) && (string.IsNullOrEmpty(driverGetAllDto.DriverFilter.LastName) || d.LastName.Contains(driverGetAllDto.DriverFilter.LastName)) && (string.IsNullOrEmpty(driverGetAllDto.DriverFilter.PhoneNumber) || d.PhoneNumber.Contains(driverGetAllDto.DriverFilter.PhoneNumber)));
            }
            if (typeof(Driver).GetProperty(driverGetAllDto.SortProperty,
                BindingFlags.Public
                                            | BindingFlags.Instance
                                            | BindingFlags.IgnoreCase) != null)
            {

                if (driverGetAllDto.SortDirection == SortDirection.desc)
                {
                    drivers = drivers.OrderBy(driverGetAllDto.SortProperty + " descending");
                }
                else
                {
                    drivers = drivers.OrderBy(driverGetAllDto.SortProperty);
                }
            }
            else if (!string.IsNullOrEmpty(driverGetAllDto.SortProperty))
            {
                throw new Exception("Sort property doesn't exist");
            }

            var result = drivers.Skip(driverGetAllDto.PageSize * (driverGetAllDto.PageNumber - 1)).Take(driverGetAllDto.PageSize);

            return result.Adapt<List<DriverDto>>();

        }

        public async Task<DriverDto> GetAlphabetizedDriverById(int driverId)
        {
            var driver = await _context.Drivers.AsQueryable().FirstOrDefaultAsync(d => d.Id == driverId);
            if (driver == null)
            {
                throw new Exception("Driver not found");
            }
            driver.FirstName = new string(driver.FirstName.OrderBy(d => d, new CaseInsensitiveComparer()).ToArray());
            driver.LastName = new string(driver.LastName.OrderBy(d => d, new CaseInsensitiveComparer()).ToArray());
            return driver.Adapt<DriverDto>();
        }

        public List<DriverDto> GetAlphabetizedList()
        {
            return _context.Drivers.AsQueryable().OrderBy(c => c.FirstName, StringComparer.OrdinalIgnoreCase).ThenBy(c => c.LastName, StringComparer.OrdinalIgnoreCase).Adapt<List<DriverDto>>();
        }

        public async Task<DriverDto> GetDriverById(int driverId)
        {
            var driver = await _context.Drivers.AsQueryable().FirstOrDefaultAsync(d => d.Id == driverId);
            if (driver == null)
            {
                throw new Exception("Driver not found");
            }
            return driver.Adapt<DriverDto>();
        }

        public async Task<DriverDto> UpdateDriver(int driverId, DriverForUpdateDto driverForUpdateDto)
        {
            var driver = await _context.Drivers.AsQueryable().FirstOrDefaultAsync(d => d.Id == driverId);
            if (driver == null)
            {
                throw new Exception("Driver not found");
            }

            driver.FirstName = driverForUpdateDto.FirstName;
            driver.LastName = driverForUpdateDto.LastName;
            driver.PhoneNumber = driverForUpdateDto.PhoneNumber;


            await _context.SaveChangesAsync();
            return driver.Adapt<DriverDto>();
        }


    }


    public class CaseInsensitiveComparer : IComparer<char>
    {
        public int Compare(char x, char y)
        {
            return string.Compare(x.ToString(), y.ToString(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
