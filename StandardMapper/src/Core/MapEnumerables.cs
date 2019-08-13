using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace StandardMapper
{
    public partial class Mapper
    {
        /// <summary>
        ///     Enumerable mapping
        /// </summary>
        /// <param name="mapType"></param>
        /// <param name="inEnumerable"></param>
        /// <param name="outPropertyType"></param>
        /// <param name="outObject"></param>
        /// <returns></returns>
        protected internal object MapEnumerable(MapType mapType, dynamic inEnumerable, Type outPropertyType)
        {
            // Get out property's assembly
            Assembly assembly = Assembly.Load(outPropertyType.GenericTypeArguments[0].Assembly.FullName);

            // Get the type of enumerable's elements
            Type outEnumerableItemsType = (outPropertyType.IsArray) ?
                                           outPropertyType.GetElementType() :
                                           assembly.GetType(outPropertyType.GenericTypeArguments[0].FullName);

            // Result app list
            Type genericListType = typeof(List<>).MakeGenericType(outEnumerableItemsType);
            IList appList = (IList)Activator.CreateInstance(genericListType);

            // Iterate input enumerable's elements
            foreach (object item in inEnumerable)
            {
                // For now, StandardMapper accept only an Enumerable of Object or an Enumerable of primitive types
                // It doesn't accept an Enumerable of Enumerables
                if (outEnumerableItemsType.IsClass)
                {
                    if (typeof(string).IsAssignableFrom(outEnumerableItemsType))
                        appList.Add(item);
                    else
                    {
                        object mappedObject = Activator.CreateInstance(outEnumerableItemsType);
                        object[] paramArgs = new object[] { item, mappedObject, null };

                        typeof(Mapper).GetMethod((mapType == MapType.Simple) ? "Map" : "OutMap")
                            .MakeGenericMethod(new Type[] { item.GetType(), outEnumerableItemsType })
                            .Invoke(this, paramArgs);

                        mappedObject = paramArgs[1];
                        appList.Add(mappedObject);
                    }
                }
                else
                {
                    appList.Add(item);
                }
            }

            return appList;
        }
    }
}