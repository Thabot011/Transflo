using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transflo.Contracts.Contract;
using Transflo.Services.Abstractions.Service;

namespace Transflo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }


        [HttpGet]
        [Route("getDriverById/{driverId}")]
        public async Task<ActionResult<DriverDto>> GetDriverById(int driverId)
        {
            var result = await _driverService.GetDriverById(driverId);
            return Ok(result);
        }


        [HttpGet]
        [Route("getAlphabetizedDriverById/{driverId}")]
        public async Task<ActionResult<DriverDto>> GetAlphabetizedDriverById([FromRoute] int driverId)
        {
            var result = await _driverService.GetAlphabetizedDriverById(driverId);
            return Ok(result);
        }

        [HttpGet]
        [Route("getAllDriversAlphabetized")]
        public ActionResult<List<DriverDto>> GetAllDriversAlphabetized()
        {
            var result = _driverService.GetAlphabetizedList();
            return Ok(result);
        }

        [HttpPost]
        [Route("createDriver")]
        public async Task<ActionResult<DriverDto>> CreateDriver([FromBody] DriverForCreationDto driverForCreation)
        {
            var result = await _driverService.CreateDriver(driverForCreation);
            return Ok(result);
        }

        [HttpPost]
        [Route("getAllDrivers")]
        public ActionResult<DriverDto> GetAllDrivers([FromBody] DriverGetAllDto driverGetAll)
        {
            var result = _driverService.GetAllDrivers(driverGetAll);
            return Ok(result);
        }

        [HttpPost]
        [Route("createDriverRange")]
        public async Task<ActionResult<DriverDto>> CreateDriverRange()
        {
            await _driverService.CreateDriversRange();
            return Ok();
        }


        [HttpPut]
        [Route("updateDriver/{driverId}")]
        public async Task<ActionResult<DriverDto>> UpdateDriver([FromRoute] int driverId, [FromBody] DriverForUpdateDto driverForUpdate)
        {
            var result = await _driverService.UpdateDriver(driverId, driverForUpdate);
            return Ok(result);
        }


        [HttpDelete]
        [Route("deleteDriver/{driverId}")]
        public async Task<IActionResult> DeleteDriver([FromRoute] int driverId)
        {
            await _driverService.DeleteDriver(driverId);
            return Ok();
        }

    }
}
