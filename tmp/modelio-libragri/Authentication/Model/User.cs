using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.AuthenticationDomain.Model
{
	public class User:BaseEntity<Guid>
	{
		
		public virtual string Login { get; set; }
		public virtual string PwdSHA256 { get; set; }
		public virtual string Email { get; set; }
		public virtual bool Active { get; set; }

		public virtual ISet<Profile> Profiles { get; set; }
    }
}
