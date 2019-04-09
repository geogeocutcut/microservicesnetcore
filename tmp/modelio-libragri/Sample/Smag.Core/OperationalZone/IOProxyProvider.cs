namespace Smag.Core.OperationalZone
{
    public interface IOProxyProvider
    {
        TProxy GetProxy<TProxy>(string name = "") where TProxy : class;
    }
}