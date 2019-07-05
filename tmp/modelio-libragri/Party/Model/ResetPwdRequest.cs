using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.PartyDomain.Model
{
	public class ResetPwdRequest:BaseEntity<Guid>
	{
		
		public virtual string KeyCode { get; set; }
		public virtual string FromDate { get; set; }
		public virtual UserData User { get; set; }
    }
}
