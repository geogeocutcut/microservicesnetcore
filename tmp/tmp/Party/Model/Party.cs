using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.partyDomain.Model
{
	public class Party:Entity<Guid>
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
        
        public override Guid GetId()
        {
            return Id;
        }
		public string Email { get; set; }
		public bool IsActive { get; set; }
		public string FirstName { get; set; }
		public string Name { get; set; }
    }
}
