using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.partyDomain.Model
{
	public class PartyRole:Entity<Guid>
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
		public virtual DateTime FromDate { get; set; }
		public virtual DateTime EndDate { get; set; }
		public virtual Role role { get; set; }
    }
}
