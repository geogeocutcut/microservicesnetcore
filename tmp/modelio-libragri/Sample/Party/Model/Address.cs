using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace PartyDomain.Model
{
	public class Address:BaseEntity<Guid>
	{
		
		public virtual string Ligne1 { get; set; }
		public virtual Country country { get; set; }
		public virtual ISet<Purpose> purposes { get; set; }
		
  }
}
