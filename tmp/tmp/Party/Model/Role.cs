using System;
using System.Collections.Generic;

namespace Libragri.partyDomain.Model
{
	public class Role
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
		public string Code { get; set; }
		public string Label { get; set; }
    }
}