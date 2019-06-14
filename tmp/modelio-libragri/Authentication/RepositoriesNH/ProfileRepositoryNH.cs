using Libragri.AuthenticationDomain.IRepositories;
using Libragri.AuthenticationDomain.Model;
using NHibernate;

namespace Libragri.AuthenticationDomain.RepositoriesNH
{
    public class ProfileRepositoryNH : StoreNH<Profile>, IProfileRepository
    {
        public ProfileRepositoryNH(ISession session) : base(session)
        {
        }
    }
}