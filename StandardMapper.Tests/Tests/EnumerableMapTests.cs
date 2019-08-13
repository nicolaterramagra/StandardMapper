using Xunit;
using System.Collections.Generic;
using StandardMapper.Tests.Helpers;
using StandardMapper.Tests.Fixtures;
using StandardMapper.Tests.EnumerableMockData;

namespace StandardMapper.Tests.Tests
{
    [Collection("MapperCollection")]
    public class EnumerableMapTests
    {
        private readonly MapperFixture mapperFix;

        public EnumerableMapTests(MapperFixture mapperFix)
            => this.mapperFix = mapperFix;

        /// <summary>
        ///     Try to map one object list in another object list. The type of
        ///     the items is different, but StandardMapper should execute map correctly
        ///     because the two types have the properties with the same name
        /// </summary>
        [Fact]
        public void Map_ObjectListIntoAnother_Test()
        {
            var list1 = new List<BaseListObject>
            {
                new BaseListObject { Name = "John", Surname = "Doe", Age = 35 },
                new BaseListObject { Name = "Michael", Surname = "Jordan", Age =  56}
            };

            mapperFix.mapper.OutMap(list1, out List<DerivedListObject> list2);

            Assert.All(list2, x => Assert.IsType<DerivedListObject>(x));
            TestsHelpers.AssertTwoListsAreEqual(EnumerableExpectedResults.ExpectedObjectListIntoAnother, list2);
        }
    }
}
