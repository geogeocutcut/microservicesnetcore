using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace PartyDomain.Model
{
	public class Party:BaseEntity<Guid>
	{
		
		public virtual string Name { get; set; }
		public virtual ISet<Address> Addresses { get; set; }
    }
}
