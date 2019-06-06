namespace Core.OperationalZone
{
    public interface IOServiceProvider
    {
        TService GetService<TService>(IOProxyProvider provider);
    }
}