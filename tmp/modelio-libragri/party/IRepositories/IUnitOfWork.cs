using System.Threading.Tasks;
using Core.Common.Repository;

namespace Libragri.PartyDomain.IRepositories
{
    public interface IUnitOfWork
    {
         Task<TRepository> GetRepository<TRepository>() where TRepository:IRepository;
    }
}