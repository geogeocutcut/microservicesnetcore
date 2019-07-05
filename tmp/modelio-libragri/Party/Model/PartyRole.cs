using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.PartyDomain.Model
{
	public class PartyRole:BaseEntity<Guid>
	{
		
		public virtual DateTime FromDate { get; set; }
		public virtual DateTime ThruDate { get; set; }
		public virtual RoleEnum RoleEnum { get; set; }
    }
}
