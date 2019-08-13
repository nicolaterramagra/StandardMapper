using System;

namespace StandardMapper.Tests.SimpleMockData
{
    public enum MockEnum { Up, Down, Right, Left }

    public class MockEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class MockEntitySetMethodNull
    {
        public long Id { get; set; }
        public string Name { get; }
        public string Surname { get; set; }
    }

    public class MockDto : MockEntity { }

    public class MockEntityNullable : MockEntity
    {
        public int? Number { get; set; }
    }

    public class MockDtoNullable
    {
        public int Number { get; set; }
    }

    public class MockEntityImplicityConversion
    {
        public int Number { get; set; }
    }
    
    public class MockDtoImplicityConversion
    {
        public long Number { get; set; }
    }

    public class MockEntityStringParse
    {
        public string Number { get; set; }
    }

    public class MockDtoStringParse
    {
        public int Number { get; set; }
    }

    public class MockEntityEnum
    {
        public MockEnum MockEnumValue { get; set; }
    }

    public class MockDtoEnum : MockEntityEnum { }

    public class MockDtoEnumNullable
    {
        public MockEnum? MockEnumValue { get; set; }
    }

    public class MockEntityIntToEnum
    {
        public int Value { get; set; }
    }
    public class MockDtoIntToEnum
    {
        public MockEnum? Value { get; set; }
    }

    public class MockEntityStringToEnum
    {
        public string Value { get; set; }
    }
    public class MockDtoStringToEnum : MockDtoIntToEnum { }

    public class MockDtoGuid
    {
        public Guid Id { get; set; } 
    }
    public class MockEntityGuid : MockDtoGuid { }

    public class MockEntityStringGuid
    {
        public string Id { get; set; }
    }
}