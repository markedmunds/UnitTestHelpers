using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace BigSnow.UnitTestHelpers
{
    public class ModelAssert
    {
        public static void AreEqual<T>(T expected, T actual) 
        {
            AreEqual<T>(expected, actual, false, null);
        }

        public static void AreEqual<T>(T expected, T actual, bool ignoreIds) 
        {
            AreEqual<T>(expected, actual, ignoreIds, null);
        }

        public static void AreEqual<T>(T expected, T actual, string[] excludePropertyFilter) 
        {
            AreEqual<T>(expected, actual, false, excludePropertyFilter);
        }

        public static void AreEqual<T>(T expected, T actual, bool ignoreIds, string[] excludePropertyFilter) 
        {
            PropertyInfo[] properties = expected.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                //only validate system and primative types only
                if (!prop.PropertyType.Namespace.StartsWith("System"))
                    continue;

                //Ignore Ids
                if (ignoreIds && prop.Name.EndsWith("id", StringComparison.CurrentCultureIgnoreCase))
                    continue;

                //Ignore properties not in the propertyFilter
                if (excludePropertyFilter != null && excludePropertyFilter.Contains(prop.Name))
                    continue;

                //TODO: we don't want to recurse into arrays or enumerables, only check the counts
                object propValue = prop.GetValue(expected, null);
                if (propValue is IEnumerable || prop.PropertyType.IsArray || (prop.PropertyType.IsGenericType
                    && prop.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))))
                {
                    continue;
                }

                Assert.AreEqual(propValue, prop.GetValue(actual, null), expected.GetType().Name + "." + prop.Name);
            }
        }
    }
}
