using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Smag.PartyDomain.Model
{
	public class AdressPurpose:BaseEntity<Guid>
	{
		
		public virtual ISet<PurposeEnum> purposeEnum { get; set; }
    }
}
