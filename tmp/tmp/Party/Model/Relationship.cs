using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.partyDomain.Model
{
	public class Relationship:Entity<Guid>
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
		public virtual string FromDate { get; set; }
		public virtual string EndDate { get; set; }
		public virtual PartyRole party_from { get; set; }
		public virtual PartyRole party_to { get; set; }
    }
}
