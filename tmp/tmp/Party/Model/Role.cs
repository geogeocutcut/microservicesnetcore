using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.partyDomain.Model
{
	public class Role:Entity<Guid>
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
		public virtual string Code { get; set; }
		public virtual string Label { get; set; }
		public virtual RoleEnum role_enum { get; set; }
    }
}
