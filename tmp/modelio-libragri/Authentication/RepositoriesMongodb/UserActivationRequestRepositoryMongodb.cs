using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Core.Repository;
using Libragri.AuthenticationDomain.Model;
using Libragri.AuthenticationDomain.IRepositories;
using System.Linq;
using MongoDB.Driver;

namespace Libragri.AuthenticationDomain.RepositoriesMongodb
{
    
    public class UserActivationRequestRepositoryMongodb:StoreMongodb<UserActivationRequest>,IUserActivationRequestRepository
    {
    	
    	public UserActivationRequestRepositoryMongodb(IMongoDatabase db):base(db)
        {
        }
    }
}
