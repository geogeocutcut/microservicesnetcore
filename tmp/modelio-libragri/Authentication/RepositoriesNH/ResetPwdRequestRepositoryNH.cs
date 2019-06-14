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
    
    public class ResetPwdRequestRepositoryNH:StoreNH<ResetPwdRequest>,IResetPwdRequestRepository
    {
    	
    	public ResetPwdRequestRepositoryNH(ISession nhsession): base(nhsession)
        {
        }
    	
    }
}
