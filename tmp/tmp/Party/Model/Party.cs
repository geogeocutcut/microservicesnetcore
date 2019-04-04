using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.partyDomain.Model
{
	public class Party:Entity<Guid>
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
		public virtual string Email { get; set; }
		public virtual bool IsActive { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string Name { get; set; }
		public virtual IList<PartyRole> party_role { get; set; }
    }
}
