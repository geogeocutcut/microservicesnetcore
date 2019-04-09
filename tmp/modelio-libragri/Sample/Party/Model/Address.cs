using System;
using System.Collections.Generic;
using Smag.Core.Common.Model;

namespace Smag.PartyDomain.Model
{
	public class Address:BaseEntity<Guid>
	{
		
		public virtual string Ligne1 { get; set; }
		public virtual Country country { get; set; }
		public virtual ISet<Purpose> purposes { get; set; }
		
  }
}
