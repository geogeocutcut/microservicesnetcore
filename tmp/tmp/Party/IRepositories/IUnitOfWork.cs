using System.Threading.Tasks;
using Core.Repository;

namespace Libragri.partyDomain.IRepositories
{
    public interface IUnitOfWork
    {
         Task<TRepository> GetRepository<TRepository>() where TRepository:IRepository;
    }
}
