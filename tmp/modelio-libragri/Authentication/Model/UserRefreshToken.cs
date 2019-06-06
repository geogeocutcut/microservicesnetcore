using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.AuthenticationDomain.Model
{
	public class UserRefreshToken:BaseEntity<Guid>
	{
		
		public virtual string RefreshToken { get; set; }
		public virtual DateTime FromDate { get; set; }
		public virtual User user { get; set; }
    }
}
