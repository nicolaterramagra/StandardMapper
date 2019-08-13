namespace StandardMapper
{
    public partial class Mapper : IMapper
    {
        public virtual void Map<TIn, TOut>(TIn objectIn, TOut objectOut, string[] ignoredProperties = null)
            where TIn : class
            where TOut : class, new()
        {
            if (objectOut == null)
                objectOut = new TOut();

            // Mapping between two enumerables
            if (IsEnumerableButNotString(typeof(TIn)) && IsEnumerableButNotString(typeof(TOut)))
                objectOut = (TOut)MapEnumerable(MapType.Simple, objectIn, typeof(TOut));
            else
                MapCore(MapType.Simple, objectIn, objectOut, ignoredProperties);
        }

        public virtual void OutMap<TIn, TOut>(TIn objectIn, out TOut objectOut, string[] ignoredProperties = null)
            where TIn : class
            where TOut : class, new()
        {
            objectOut = new TOut();

            // Mapping between two enumerables
            if (IsEnumerableButNotString(typeof(TIn)) && IsEnumerableButNotString(typeof(TOut)))
                objectOut = (TOut)MapEnumerable(MapType.Out, objectIn, typeof(TOut));
            else
                MapCore(MapType.Out, objectIn, objectOut, ignoredProperties);
        }
    }
}