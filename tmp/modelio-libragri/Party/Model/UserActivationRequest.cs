using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.PartyDomain.Model
{
	public class UserActivationRequest:BaseEntity<Guid>
	{
		
		public virtual string Key { get; set; }
		public virtual DateTime FromDate { get; set; }
		public virtual UserData User { get; set; }
    }
}
