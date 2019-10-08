using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace EventFinder.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value){
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptionAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();
            return descriptionAttribute == null ? value.ToString() : descriptionAttribute.Name;
        }

        public static IList<T> GetValues<T>()
        {
            var values = Enum.GetValues(typeof(T));
            return values.Cast<T>().ToArray();
        }

    }
}