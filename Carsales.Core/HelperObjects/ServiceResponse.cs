using System.Collections.Generic;

namespace Carsales.Core.HelperObjects
{
    public class ServiceResponse
    {
        public List<string> errors { get; set; }

        public bool hasSuccess { get; set; }
    }
}
