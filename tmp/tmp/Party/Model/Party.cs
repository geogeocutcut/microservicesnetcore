using System;
using System.Collections.Generic;

namespace Libragri.partyDomain.Model
{
	public class Party
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
		public string Email { get; set; }
		public bool IsActive { get; set; }
		public string FirstName { get; set; }
		public string Name { get; set; }
    }
}
