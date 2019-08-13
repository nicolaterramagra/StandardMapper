namespace StandardMapper
{
    public interface IMapper
    {
        void Map<TIn, TOut>(TIn objectIn, TOut objectOut, string[] ignoredProperties = null)
                 where TIn : class
                 where TOut : class, new();

        void OutMap<TIn, TOut>(TIn objectIn, out TOut objectOut, string[] ignoredProperties = null)
                 where TIn : class
                 where TOut : class, new();
    }
}