using System;
using System.Reflection;
using System.Collections;

namespace StandardMapper
{
    public partial class Mapper
    {
        /// <summary>
        ///     Evaluate input and output properties and check if the value of the first one can be
        ///     transferred to the second one
        /// </summary>
        /// <param name="mapType"></param>
        /// <param name="inObject"></param>
        /// <param name="inProperty"></param>
        /// <param name="outProperty"></param>
        /// <param name="outObject"></param>
        protected internal void EvaluateProperty(MapType mapType, dynamic inObject, PropertyInfo inProperty, PropertyInfo outProperty, object outObject)
        {
            Type outPropertyType = outProperty.PropertyType;

            // The output property is an enumerable (exclude strings)
            if (IsEnumerableButNotString(outPropertyType))
            {
                // Map enumerable
                dynamic mappedEnumerable = MapEnumerable(mapType, inProperty.GetValue(inObject, null), outPropertyType);
                MapProperty(mappedEnumerable, outProperty, outPropertyType, outObject);
            }
            // Output property is an object
            else if (IsClass(outPropertyType))
            {
                // String is considered as a primitive type
                if (typeof(string).IsAssignableFrom(outPropertyType))
                    MapProperty(inProperty.GetValue(inObject), outProperty, outPropertyType, outObject);
                else
                {
                    // Mapper result
                    object mappedObject = outProperty.GetValue(outObject) ?? Activator.CreateInstance(outPropertyType);

                    // Recall a Map method with Invoke api
                    object[] paramArgs = new object[] { inProperty.GetValue(inObject), mappedObject, null };
                    typeof(Mapper).GetMethod((mapType == MapType.Simple) ? "Map" : "OutMap")
                        .MakeGenericMethod(new Type[] { inProperty.PropertyType, outPropertyType })
                        .Invoke(this, paramArgs);

                    mappedObject = paramArgs[1];
                    MapProperty(mappedObject, outProperty, outPropertyType, outObject);
                }
            }
            // Output property is a primitve type or enum
            else
            {
                MapProperty(inProperty.GetValue(inObject), outProperty, outPropertyType,
                                    outObject, Nullable.GetUnderlyingType(outPropertyType));
            }
        }

        /// <summary>
        ///     Check if type is an enumerable (but not a string)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected internal bool IsEnumerableButNotString(Type type)
            => typeof(IEnumerable).IsAssignableFrom(type) && !typeof(string).IsAssignableFrom(type);

        /// <summary>
        ///     Check if type is a class
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected internal bool IsClass(Type type) => type.IsClass;
    }
}