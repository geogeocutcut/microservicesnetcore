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
    
    public class UserEventRepositoryMongodb:StoreMongodb<UserEvent>,IUserEventRepository
    {
        
    	public UserEventRepositoryMongodb(IMongoDatabase db):base(db)
        {
        }
    }
}
