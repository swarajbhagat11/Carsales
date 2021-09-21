using Carsales.Models.Attributes;
using System;
using System.Reflection;

namespace Carsales.Core
{
    public static class Utility
    {
        public static void updateProp(object obj, string propName, object propValue)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propName);
            propertyInfo.SetValue(obj, propValue, null);
        }

        public static void copyObject(object source, object destination)
        {
            foreach (PropertyInfo pi in source.GetType().GetProperties())
            {
                if (!Attribute.IsDefined(pi, typeof(NonUpdatableAttribute)))
                {
                    destination.GetType().GetProperty(pi.Name).SetValue(destination, pi.GetValue(source, null), null);
                }
            }
        }
    }
}
