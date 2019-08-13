using Xunit;
using StandardMapper.Tests.Fixtures;
using StandardMapper.Tests.SimpleMockData;

namespace StandardMapper.Tests.Tests
{
    [Collection("MapperCollection")]
    public class EnumMapTests
    {
        private readonly MapperFixture mapperFix;

        public EnumMapTests(MapperFixture mapperFix)
            => this.mapperFix = mapperFix;

        /// <summary>
        ///     Try to map Enum value in another; StandarMapper should
        ///     execute map correctly because the type of two properties
        ///     is the same
        /// </summary>
        [Fact]
        public void Map_SimpleEnum_Test()
        {
            var entity = new MockEntityEnum { MockEnumValue = MockEnum.Up };
            mapperFix.mapper.OutMap(entity, out MockDtoEnum dto);

            Assert.Equal(MockEnum.Up, dto.MockEnumValue);
        }

        /// <summary>
        ///     Try to map Enum value in nullable enum property;
        ///     in this situation, StandardMapper shoud execute map correctly
        ///     because the main enum type of out property is the same of
        ///     the input property
        /// </summary>
        [Fact]
        public void Map_EnumIntoNullableEnum_Test()
        {
            var entity = new MockEntityEnum { MockEnumValue = MockEnum.Up };
            mapperFix.mapper.OutMap(entity, out MockDtoEnumNullable dto);

            Assert.Equal(MockEnum.Up, dto.MockEnumValue);
        }

        /// <summary>
        ///     Try to map int value into enum; StandardMapper should
        ///     be execute map correctly and parse int value into enum value
        /// </summary>
        [Fact]
        public void Map_IntIntoEnum_Test()
        {
            var entity = new MockEntityIntToEnum { Value = 1 };
            mapperFix.mapper.OutMap(entity, out MockDtoIntToEnum dto);

            Assert.Equal(MockEnum.Down, dto.Value);
        }

        /// <summary>
        ///     Try to map string value into enum; StandardMapper should
        ///     be execute map correctly and assign the enum representation
        ///     of string value
        /// </summary>
        [Fact]
        public void Map_StringIntoEnum_Test()
        {
            var entity = new MockEntityStringToEnum { Value = "Right" };
            mapperFix.mapper.OutMap(entity, out MockDtoStringToEnum dto);

            Assert.Equal(MockEnum.Right, dto.Value);
        }

        /// <summary>
        ///     Try to map enum value into string; StandardMapper should 
        ///     be execute map correctly and assign the string representation
        ///     of enum value
        /// </summary>
        [Fact]
        public void Map_EnumIntoString_Test()
        {
            var dto = new MockDtoStringToEnum { Value = MockEnum.Up };
            mapperFix.mapper.OutMap(dto, out MockEntityStringToEnum entity);

            Assert.Equal("Up", entity.Value);
        }
    }
}