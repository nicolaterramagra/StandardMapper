using Xunit;

namespace StandardMapper.Tests.Fixtures
{
    [CollectionDefinition("MapperCollection")]
    public class MapperFixtureCollection : ICollectionFixture<MapperFixture> { }
}