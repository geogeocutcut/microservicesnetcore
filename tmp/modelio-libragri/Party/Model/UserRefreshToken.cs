using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.PartyDomain.Model
{
	public class UserRefreshToken:BaseEntity<Guid>
	{
		
		public virtual string RefreshToken { get; set; }
		public virtual DateTime FromDate { get; set; }
		public virtual UserData User { get; set; }
    }
}
