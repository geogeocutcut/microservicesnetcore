using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.PartyDomain.Model
{
	public class UserEvent:BaseEntity<Guid>
	{
		
		public virtual string TypeEvent { get; set; }
		public virtual DateTime FromDate { get; set; }
		public virtual UserData User { get; set; }
    }
}
