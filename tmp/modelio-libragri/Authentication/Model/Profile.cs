using System;
using Core.Common.Model;

namespace Libragri.AuthenticationDomain.Model
{
    public class Profile:BaseEntity<Guid>
    {
        public virtual string Name{get;set;}
    }
}