using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.PartyDomain.Model
{
	public class Address:BaseEntity<Guid>
	{
		
		public virtual string Ligne1 { get; set; }
		public virtual string Ligne2 { get; set; }
		public virtual Country Country { get; set; }
		public virtual ISet<PurposeEnum> Purposes { get; set; }
    }
}
