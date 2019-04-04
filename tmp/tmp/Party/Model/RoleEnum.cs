using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.partyDomain.Model
{
	public class RoleEnum:Entity<Guid>
	{
		
		public virtual Guid Id { 
			get{
				return _id;
			}
			set{
				_id = value;
			}
		}
	    
		public override Guid GetId()
		{
			return Id;
		}
    }
}
