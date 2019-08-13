using Xunit;
using System;
using StandardMapper.Tests.Fixtures;
using StandardMapper.Tests.SimpleMockData;

namespace Standardmapper.Tests.Tests
{
    [Collection("MapperCollection")]
    public class SimpleObjectsOutMapTests
    {
        private readonly MapperFixture mapperFix;

        public SimpleObjectsOutMapTests(MapperFixture mapperFix)
            => this.mapperFix = mapperFix;

        /// <summary>
        ///     Simple OutMap, easy
        /// </summary>
        [Fact]
        public void OutMap_BetweenTwoClasses_Test()
        {
            var entity = new MockEntity { Id = 1, Name = "John", Surname = "Doe" };
            mapperFix.mapper.OutMap(entity, out MockDto dto);

            Assert.NotNull(dto);
            Assert.Equal(1, dto.Id);
            Assert.Equal("John", dto.Name);
            Assert.Equal("Doe", dto.Surname);
        }

        /// <summary>
        ///     Simple OutMap, with method without "out" parameter
        /// </summary>
        [Fact]
        public void OutMap_BetweenTwoClasses_Old_Test()
        {
            var entity = new MockEntity { Id = 1, Name = "John", Surname = "Doe" };
            MockDto dto = new MockDto();
            mapperFix.mapper.Map(entity, dto);

            Assert.NotNull(dto);
            Assert.Equal(1, dto.Id);
            Assert.Equal("John", dto.Name);
            Assert.Equal("Doe", dto.Surname);
        }

        /// <summary>
        ///     Simple OutMap with excluded properties. Property Id should't be OutMapped
        ///     into dto object
        /// </summary>
        [Fact]
        public void OutMap_BetweenTwoClasses_ExcludeProperties_Test()
        {
            var entity = new MockEntity { Id = 1, Name = "John", Surname = "Doe" };
            mapperFix.mapper.OutMap(entity, out MockDto dto, new string[] { "Id" });

            Assert.NotNull(dto);
            Assert.Equal(0, dto.Id);
            Assert.Equal("John", dto.Name);
            Assert.Equal("Doe", dto.Surname);
        }

        /// <summary>
        ///     Try to OutMap null into non nullable property.
        ///     Out property should OutMap with default value
        /// </summary>
        [Fact]
        public void OutMap_NullableProperties_Test()
        {
            var entity = new MockEntityNullable();
            mapperFix.mapper.OutMap(entity, out MockDtoNullable dto);

            Assert.Equal(0, dto.Number);
        }

        /// <summary>
        ///     If nullable property has a value, it should be OutMapped
        ///     into output property with the same name (and same main type)
        /// </summary>
        [Fact]
        public void OutMap_NullableProperties_WithValue_Test()
        {
            var entity = new MockEntityNullable { Number = 2 };
            mapperFix.mapper.OutMap(entity, out MockDtoNullable dto);

            Assert.Equal(2, dto.Number);
        }

        /// <summary>
        ///     Try to OutMap int property into long property; Standardmapper
        ///     should be work with implicit conversion
        /// </summary>
        [Fact]
        public void OutMap_ImplicitConversion_Test()
        {
            var entity = new MockEntityImplicityConversion { Number = 130 };
            mapperFix.mapper.OutMap(entity, out MockDtoImplicityConversion dto);

            Assert.Equal(130, dto.Number);
        }

        /// <summary>
        ///     Try to OutMap long property into int property; Standarmapper
        ///     should execute OutMapping correctly because the value of long 
        ///     property is included in int32 range
        /// </summary>
        [Fact]
        public void OutMap_ImplicitConversion_LongIntoInt_Test()
        {
            var dto = new MockDtoImplicityConversion { Number = 140 };
            mapperFix.mapper.OutMap(dto, out MockEntityImplicityConversion entity);

            Assert.Equal(140, entity.Number);
        }

        /// <summary>
        ///     Try to OutMap long propertu into int property; in this case
        ///     Standardmapper should throws an OverflowException because the value
        ///     of long property is not included in int32 range
        /// </summary>
        [Fact]
        public void OutMap_ImplicitConversion_LongIntoInt_Exception_Test()
        {
            var dto = new MockDtoImplicityConversion { Number = long.MaxValue };
            Assert.Throws<OverflowException>(()
                => mapperFix.mapper.OutMap(dto, out MockEntityImplicityConversion entity));
        }

        /// <summary>
        ///     Try to OutMap string with numeric value into int; Standardmapper
        ///     should execute OutMapping correctly
        /// </summary>
        [Fact]
        public void OutMap_StringIntoInt_Test()
        {
            var entity = new MockEntityStringParse { Number = "145" };
            mapperFix.mapper.OutMap(entity, out MockDtoStringParse dto);

            Assert.Equal(145, dto.Number);
        }

        /// <summary>
        ///     Try to OutMap int value into string; Standardmapper should
        ///     be parse correctly the int value into it's string representation
        /// </summary>
        [Fact]
        public void OutMap_IntIntoString_Test()
        {
            var dto = new MockDtoStringParse { Number = 145 };
            mapperFix.mapper.OutMap(dto, out MockEntityStringParse entity);

            Assert.Equal("145", entity.Number);
        }

        /// <summary>
        ///     If the outProperty is readonly, StandardMapper
        ///     doesn't elaborate it. The value of the property
        ///     will be the default of it's type
        /// </summary>
        [Fact]
        public void OutMap_SetMethodIsNull_Test()
        {
            var entity = new MockEntity { Id = 1, Name = "Jon", Surname = "Snow" };
            mapperFix.mapper.OutMap(entity, out MockEntitySetMethodNull outModel);

            Assert.Equal(1, outModel.Id);
            Assert.Null(outModel.Name);
            Assert.Equal("Snow", outModel.Surname);
        }

        /// <summary>
        ///     The same test of "OutMap_SetMethodIsNull_Test"
        ///     but with the "Map" method
        /// </summary>
        [Fact]
        public void Map_SetMethodIsNull_Test()
        {
            var entity = new MockEntity { Id = 1, Name = "Daenerys", Surname = "Targaryen" };
            var outModel = new MockEntitySetMethodNull();

            mapperFix.mapper.Map(entity, outModel);
            Assert.Equal(1, outModel.Id);
            Assert.Null(outModel.Name);
            Assert.Equal("Targaryen", outModel.Surname);
        }

        /// <summary>
        ///     Map null input string into initialized output property.
        ///     StandardMapper should be set "null" into output property.
        /// </summary>
        [Fact]
        public void Map_NullString_Test()
        {
            var entity = new MockEntity { Id = 1, Name = null, Surname = "Doe" };
            var dto = new MockDto { Id = 0, Name = "", Surname = "" };

            mapperFix.mapper.Map(entity, dto);

            Assert.Equal(1, dto.Id);
            Assert.Null(dto.Name);
            Assert.Equal("Doe", dto.Surname);
        }

        /// <summary>
        ///     OutMap null input string into initialized output property.
        ///     StandardMapper should be set "null" into output property.
        /// </summary>
        [Fact]
        public void OutMap_NullString_Test()
        {
            var entity = new MockEntity { Id = 1, Name = null, Surname = "Doe" };
            mapperFix.mapper.OutMap(entity, out MockDto dto);

            Assert.Equal(1, dto.Id);
            Assert.Null(dto.Name);
            Assert.Equal("Doe", dto.Surname);
        }

        /// <summary>
        ///     Try to Map Guid property into another Guid property
        ///     with OutMap
        /// </summary>
        [Fact]
        public void OutMap_Guid_Test()
        {
            var guid = Guid.NewGuid();

            MockDtoGuid dto = new MockDtoGuid { Id = guid };
            mapperFix.mapper.OutMap(dto, out MockEntityGuid entity);

            Assert.Equal(guid, entity.Id);
        }

        /// <summary>
        ///     Try to Map Guid property into another Guid property
        ///     with SimpleMap
        /// </summary>
        [Fact]
        public void Map_Guid_Test()
        {
            var guid = Guid.NewGuid();

            MockDtoGuid dto = new MockDtoGuid { Id = guid };
            MockEntityGuid entity = new MockEntityGuid();

            mapperFix.mapper.Map(dto, entity);
            Assert.Equal(guid, entity.Id);
        }

        /// <summary>
        ///     Try to map Guid into String with OutMap
        /// </summary>
        [Fact]
        public void OutMap_GuidIntoString_Test()
        {
            var guid = Guid.NewGuid();

            MockDtoGuid dto = new MockDtoGuid { Id = guid };

            mapperFix.mapper.OutMap(dto, out MockEntityStringGuid entity);
            Assert.Equal(guid.ToString(), entity.Id);
        }

        /// <summary>
        ///     Try to map Guid into String with SimpleMap
        /// </summary>
        [Fact]
        public void Map_GuidIntoString_Test()
        {
            var guid = Guid.NewGuid();

            MockDtoGuid dto = new MockDtoGuid { Id = guid };
            MockEntityStringGuid entity = new MockEntityStringGuid();

            mapperFix.mapper.Map(dto, entity);
            Assert.Equal(guid.ToString(), entity.Id);
        }

        /// <summary>
        ///     Try to map String into Guid with OutMap
        /// </summary>
        [Fact]
        public void OutMap_StringIntoGuid_Test()
        {
            var guidStr = Guid.NewGuid().ToString();

            MockEntityStringGuid entity = new MockEntityStringGuid { Id = guidStr };

            mapperFix.mapper.OutMap(entity, out MockDtoGuid dto);
            Assert.Equal(new Guid(guidStr), dto.Id);
        }

        /// <summary>
        ///     Try to map String into Guid with SimpleMap
        /// </summary>
        [Fact]
        public void Map_StringIntoGuid_Test()
        {
            var guidStr = Guid.NewGuid().ToString();

            MockEntityStringGuid entity = new MockEntityStringGuid { Id = guidStr };
            MockDtoGuid dto = new MockDtoGuid();

            mapperFix.mapper.Map(entity, dto);
            Assert.Equal(new Guid(guidStr), dto.Id);
        }
    }
}