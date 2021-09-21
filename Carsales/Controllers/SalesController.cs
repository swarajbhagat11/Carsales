using Autofac.Features.Indexed;
using Carsales.Core.HelperObjects;
using Carsales.Filters;
using Carsales.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Carsales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [HeaderChecker]
    public class SalesController : ControllerBase
    {
        private readonly ILogger<SalesController> _logger;
        private readonly IIndex<String, IVehicleService> _serviceObj;

        public SalesController(ILogger<SalesController> logger, IIndex<String, IVehicleService> serviceObj)
        {
            _logger = logger;
            _serviceObj = serviceObj;
        }

        [HttpPost]
        [Route("addVehicle")]
        public IActionResult AddVehicle([FromBody] JObject vehicle)
        {
            IVehicleService _service = _serviceObj[Request.Headers["vehicleType"]];
            _logger.LogInformation("AddVehicle service call started.");
            ServiceResponse res = _service.AddVehicle(vehicle);
            _logger.LogInformation("AddVehicle service call completed.", res);
            return res.hasSuccess ? Ok("Vehicle added successfully.") : BadRequest(res.errors);
        }

        [HttpPut]
        [Route("editVehicle/{id}")]
        public IActionResult EditVehicle(Guid id, [FromBody] JObject vehicle)
        {
            IVehicleService _service = _serviceObj[Request.Headers["vehicleType"]];
            _logger.LogInformation("EditVehicle service call started.");
            ServiceResponse res = _service.EditVehicle(id, vehicle);
            _logger.LogInformation("EditVehicle service call completed.", res);
            return res.hasSuccess ? Ok("Vehicle edited successfully.") : BadRequest(res.errors);
        }

        [HttpDelete]
        [Route("deleteVehicle/{id}")]
        public IActionResult DeleteVehicle(Guid id)
        {
            IVehicleService _service = _serviceObj[Request.Headers["vehicleType"]];
            _logger.LogInformation("DeleteVehicle service call started.");
            ServiceResponse res = _service.DeleteVehicle(id);
            _logger.LogInformation("DeleteVehicle service call completed.", res);
            return res.hasSuccess ? Ok("Vehicle deleted successfully.") : BadRequest(res.errors);
        }

        [HttpGet]
        [Route("searchedVehicle")]
        public IActionResult SearchedVehicle([FromQuery] string email)
        {
            IVehicleService _service = _serviceObj[Request.Headers["vehicleType"]];
            _logger.LogInformation("GetSearchedVehicle service call started.");
            List<JObject> res = _service.GetSearchedVehicle(email);
            _logger.LogInformation("GetSearchedVehicle service call completed.");
            return Ok(res);
        }
    }
}
