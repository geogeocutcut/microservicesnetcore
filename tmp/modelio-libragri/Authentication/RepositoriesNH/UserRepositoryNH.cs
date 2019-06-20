using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Core.Repository;
using Libragri.AuthenticationDomain.Model;
using Libragri.AuthenticationDomain.IRepositories;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace Libragri.AuthenticationDomain.RepositoriesNH
{
    
    public class UserRepositoryNH:StoreNH<User>,IUserRepository
    {
    	
    	public UserRepositoryNH(ISession nhsession): base(nhsession)
        {
            
        }
        public override async Task<User> GetByIdAsync(Guid id)
        {
            return await _nhsession.Query<User>().Where(x =>x.Id==id)
                        .Fetch(x=>x.Profiles)
                        .FirstOrDefaultAsync();
        }
    }
}
