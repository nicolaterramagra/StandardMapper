using System.Collections.Generic;

namespace StandardMapper.Tests.EnumerableMockData
{
    public static class EnumerableExpectedResults
    {
        public static List<DerivedListObject> ExpectedObjectListIntoAnother
                = new List<DerivedListObject>
                    {
                        new DerivedListObject { Name = "John", Surname = "Doe", Age = 35 },
                        new DerivedListObject { Name = "Michael", Surname = "Jordan", Age =  56}
                    }; 
    }

    public class BaseListObject
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
    }

    public class DerivedListObject : BaseListObject { }

    public class BaseObject
    {
        public List<BaseListObject> List { get; set; }
    }
    public class DerivedObject
    {
        public List<DerivedListObject> List { get; set; }
    }
}