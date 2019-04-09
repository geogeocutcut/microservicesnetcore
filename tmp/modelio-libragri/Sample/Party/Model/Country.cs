using System;
using System.Collections.Generic;
using Smag.Core.Common.Model;

namespace Smag.PartyDomain.Model
{
	public class Country:BaseEntity<Guid>
	{
		public virtual string Name { get; set; }
    }
}
