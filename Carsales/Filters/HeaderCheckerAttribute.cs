using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace Carsales.Filters
{
    public class HeaderCheckerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            List<string> _vehicleTypes = new List<string> { "car" };

            if (!context.HttpContext.Request.Headers.ContainsKey("vehicleType"))
            {
                context.Result = new BadRequestObjectResult("Please add vehicleType in header.");
            }

            if (!_vehicleTypes.Contains(context.HttpContext.Request.Headers["vehicleType"]))
            {
                context.Result = new BadRequestObjectResult("Vehicle Type should be one of: " + String.Join(",", _vehicleTypes));
            }
        }
    }
}
