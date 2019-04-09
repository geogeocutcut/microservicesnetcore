using System;
using System.Collections.Generic;
using Smag.Core.Common.Model;

namespace Smag.PartyDomain.Model
{
	public class Purpose:BaseEntity<Guid>
	{
		
		public virtual string Name { get; set; }
    }
}
