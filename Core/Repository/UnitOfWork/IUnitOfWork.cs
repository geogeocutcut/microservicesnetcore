namespace Core.Repository
{
    public interface IUnitOfWork
    {
        ITransaction Begin();

        void Commit();

        void Rollback();

        void Dispose();

        TRepository GetRepository<TRepository>();
    }
}