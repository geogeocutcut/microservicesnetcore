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
    
    public class UserRefreshTokenRepositoryNH:StoreNH<UserRefreshToken>,IUserRefreshTokenRepository
    {
        
    	public UserRefreshTokenRepositoryNH(ISession nhsession): base(nhsession)
        {
        }
    }
}
