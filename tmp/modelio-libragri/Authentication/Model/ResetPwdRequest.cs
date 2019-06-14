using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.AuthenticationDomain.Model
{
	public class ResetPwdRequest:BaseEntity<Guid>
	{
		
		public virtual string Key { get; set; }
		public virtual string FromDate { get; set; }
		public virtual User User { get; set; }
    }
}
