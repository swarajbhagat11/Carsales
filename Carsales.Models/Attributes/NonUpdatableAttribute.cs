using System;

namespace Carsales.Models.Attributes
{

    [AttributeUsage(AttributeTargets.All)]
    public class NonUpdatableAttribute : Attribute
    {
        public NonUpdatableAttribute()
        {
        }
    }
}
