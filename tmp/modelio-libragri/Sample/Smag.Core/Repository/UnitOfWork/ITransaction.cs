using System;

namespace Smag.Core.Repository
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}