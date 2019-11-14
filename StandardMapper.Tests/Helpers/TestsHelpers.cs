using System.Reflection;
using System.Collections;

namespace StandardMapper.Tests.Helpers
{
    public static class TestsHelpers
    {
        public static void AssertTwoListsAreEqual(IList expected, IList actual)
        {
            if (expected.Count != actual.Count)
            {
                Error($"Expected list contains {expected.Count} elements, but actual list contains {actual.Count}");
            }

            foreach(object expectedItem in expected)
            {
                int index = expected.IndexOf(expectedItem);
                object actualItem = actual[index];

                foreach (PropertyInfo exProperty in expectedItem.GetType().GetProperties())
                {
                    PropertyInfo acProperty = actualItem.GetType().GetProperty(exProperty.Name);
                    if (acProperty == null)
                        Error($"Expected list contains a property with name {exProperty.Name}, but actual list doesn't");

                    if (exProperty.GetType().Name != acProperty.GetType().Name)
                        Error($"The type of property {exProperty.Name} in expected list is not the same of the type of property {acProperty.Name} in actual list");

                    var exValue = exProperty.GetValue(expectedItem);
                    var acValue = acProperty.GetValue(actualItem);

                    if (!exValue.Equals(acValue))
                        Error($"The value of property {exProperty.Name} in expected list is not the same of the value of property {acProperty.Name} in actual list");
                }
            }
        }

        private static void Error(string message)
        {
            throw new Xunit.Sdk.XunitException(message);
        }
    }
}
