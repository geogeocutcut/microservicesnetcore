using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.partyDomain.Model
{
	public class User:Entity<Guid>
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
		public virtual string Login { get; set; }
		public virtual string PasswordSha1 { get; set; }
		public virtual bool IsActive { get; set; }
		public virtual Party party { get; set; }
    }
}
