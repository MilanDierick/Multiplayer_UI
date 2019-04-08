using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace MULTIPLAYER.UI.Extensions
{
    [PublicAPI]
    public static class ClassExtensions
    {
        /// <summary>
        /// Trim all String properties of the given object
        /// </summary>
        /// <typeparam name="TSelf">Class type</typeparam>
        /// <param name="input">Instance</param>
        /// <returns></returns>
        public static TSelf TrimStringProperties<TSelf>(this TSelf input)
        {
            IEnumerable<PropertyInfo> stringProperties = input.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string));

            foreach (PropertyInfo stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(input, null);
                if (currentValue != null)
                    stringProperty.SetValue(input, currentValue.Trim(), null);
            }
            return input;
        }

        public static object GetPropertyValue(this object source, string propertyName)
        {
            return source.GetType().GetProperties()
                .Single(pi => pi.Name == propertyName)
                .GetValue(source, null);
        }
    }
}