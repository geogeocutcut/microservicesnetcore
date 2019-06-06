using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Smag.PartyDomain.Model
{
	public class Country:BaseEntity<Guid>
	{
		
		public virtual string Name { get; set; }
    }
}
