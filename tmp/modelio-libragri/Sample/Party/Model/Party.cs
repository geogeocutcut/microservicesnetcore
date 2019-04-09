using System;
using System.Collections.Generic;
using Smag.Core.Common.Model;

namespace Smag.PartyDomain.Model
{
	public class Party:BaseEntity<Guid>
	{
		
		public virtual string Name { get; set; }
		public virtual ISet<Address> Addresses { get; set; }
    }
}
