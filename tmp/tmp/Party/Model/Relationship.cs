using System;
using System.Collections.Generic;

namespace Libragri.partyDomain.Model
{
	public class Relationship
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
		public string FromDate { get; set; }
		public string EndDate { get; set; }
    }
}
