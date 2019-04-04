namespace Core.Common
{
    public interface IFactory
    {
        TIObject Resolve<TIObject>();

        void Register<TIObject,TObject>();
        void Register<TIObject>(TIObject obj);

        bool IsRegistered<TIObject>();
    }
}