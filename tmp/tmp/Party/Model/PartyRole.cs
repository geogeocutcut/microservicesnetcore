using System;
using System.Collections.Generic;

namespace Libragri.partyDomain.Model
{
	public class PartyRole
    {
    	private Guid _id;
    	
        public Guid Id { 
            get{
                if(_id==null)
                {
                    _id=Guid.NewGuid();
                }
                return _id;
            }
            set{
                _id = value;
            }
        }
		public DateTime FromDate { get; set; }
		public DateTime EndDate { get; set; }
    }
}
