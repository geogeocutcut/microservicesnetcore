using Core.Repository;

namespace Core.DataZone
{
    public interface IDServiceProvider
    {
        IService GetService<IService>(IUnitOfWork uow);
    }
}