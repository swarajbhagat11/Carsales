using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carsales.Core.HelperObjects
{
    public class HandlerResponse
    {
        public ValidationResult validation { get; set; }
        public object dtoObj { get; set; }
    }
}
