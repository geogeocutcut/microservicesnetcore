using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.AuthenticationDomain.Model
{
	public class UserEvent:BaseEntity<Guid>
	{
		
		public virtual string TypeEvent { get; set; }
		public virtual DateTime FromDate { get; set; }
		public virtual User User { get; set; }
    }
}
