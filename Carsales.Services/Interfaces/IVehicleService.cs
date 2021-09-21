using Carsales.Core.HelperObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Carsales.Services.Interfaces
{
    public interface IVehicleService
    {
        ServiceResponse AddVehicle(JObject vehicle);

        ServiceResponse EditVehicle(Guid id, JObject vehicle);

        ServiceResponse DeleteVehicle(Guid id);

        List<JObject> GetSearchedVehicle(string email);
    }
}
