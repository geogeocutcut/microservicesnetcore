namespace Core.Repository
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _uow;

        public Transaction(IUnitOfWork uow) => _uow = uow;

        public void Commit() => _uow.Commit();

        public void Rollback() => _uow.Rollback();

        public void Dispose() => _uow.Dispose();
    }
}