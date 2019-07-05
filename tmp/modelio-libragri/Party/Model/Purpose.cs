using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.PartyDomain.Model
{
	public class Purpose:BaseEntity<Guid>
	{
		
		public virtual string Name { get; set; }
    }
}
