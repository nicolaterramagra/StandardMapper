using System.Linq;
using System.Reflection;

namespace StandardMapper
{
    public partial class Mapper
    {
        protected internal void MapCore(MapType mapType, object objectIn, object objectOut, string [] ignoredProperties = null)
        {
            // input object properties list
            PropertyInfo[] inProperties = objectIn.GetType().GetProperties();

            foreach (PropertyInfo inProperty in inProperties)
            {
                // If property is in "ignore list", ignore and continue with the next property
                if (ignoredProperties != null && ignoredProperties.Any())
                {
                    if (ignoredProperties.Contains(inProperty.Name))
                    {
                        continue;
                    }
                }

                // Search in the output class a property with the same name
                PropertyInfo outProperty = objectOut.GetType().GetProperty(inProperty.Name);

                // Elaborate only if the outProperty isn't readonly
                if (outProperty != null && outProperty.SetMethod != null)
                {
                    EvaluateProperty(mapType, objectIn, inProperty, outProperty, objectOut);
                }
            }
        }
    }
}