using System;

namespace Core.Repository
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}