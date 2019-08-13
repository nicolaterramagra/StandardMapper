using System;
using System.Reflection;

namespace StandardMapper
{
    public partial class Mapper
    {
        /// <summary>
        ///     Move input property value into output property
        /// </summary>
        /// <param name="inObjectValue"></param>
        /// <param name="outProperty"></param>
        /// <param name="outPropertyType"></param>
        /// <param name="outObject"></param>
        /// <param name="outUndelyingType"></param>
        protected internal void MapProperty(object inObjectValue,
                                              PropertyInfo outProperty,
                                              Type outPropertyType,
                                              object outObject,
                                              Type outUndelyingType = null)
        {
            // Nullable property
            if (outUndelyingType != null)
            {
                if (inObjectValue == null)
                {
                    outProperty.SetValue(outObject, null);
                }
                else
                {
                    // Nullable Enum
                    if (outPropertyType.GenericTypeArguments[0].BaseType.FullName == "System.Enum")
                    {
                        Assembly assembly = Assembly.Load(outPropertyType.GenericTypeArguments[0].Assembly.FullName);

                        outProperty.SetValue(outObject,
                            Enum.Parse(assembly.GetType(outPropertyType.GenericTypeArguments[0].FullName), inObjectValue.ToString()));
                    }
                    // Other nullable type
                    else
                        outProperty.SetValue(outObject, Convert.ChangeType(inObjectValue, outUndelyingType));
                }
            }
            // Non-nullable property
            else
            {
                if (inObjectValue == null)
                {
                    outProperty.SetValue(outObject,
                                         (outPropertyType.IsValueType) ? Activator.CreateInstance(outPropertyType) : null);
                }
                else
                {
                    if (outPropertyType.IsEnum)
                        outProperty.SetValue(outObject, Enum.Parse(outPropertyType, inObjectValue.ToString()));
                    else
                    {
                        if (outPropertyType.IsArray)
                            outProperty.SetValue(outObject, inObjectValue.GetType()
                                                                .GetMethod("ToArray").Invoke(inObjectValue, null));
                        else
                        {
                            // Guid does not implement the IConvertible interface; if you try to Convert Guid
                            // into String, the System throws an InvalidCastException. 
                            // In this case, StandardMapper call the ToString() method 
                            if (inObjectValue.GetType().IsAssignableFrom(typeof(Guid)) && outPropertyType.FullName == "System.String")
                                outProperty.SetValue(outObject, inObjectValue.ToString());
                            // Is not possible the direct cast from String to Guid; in this case, StandardMapper
                            // create a new instance of Guid for the out property
                            else if (inObjectValue.GetType().FullName == "System.String" && outPropertyType.IsAssignableFrom(typeof(Guid)))
                                outProperty.SetValue(outObject, new Guid(inObjectValue.ToString()));
                            else
                                outProperty.SetValue(outObject, Convert.ChangeType(inObjectValue, outPropertyType));
                        }
                    }
                }
            }
        }
    }
}