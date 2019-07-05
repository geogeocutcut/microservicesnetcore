using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.PartyDomain.Model
{
	public class UserData:BaseEntity<Guid>
	{
		
		public virtual string Login { get; set; }
		public virtual string PwdSHA256 { get; set; }
		public virtual string Email { get; set; }
		public virtual bool Active { get; set; }
		public virtual Party Party { get; set; }
    }
}
