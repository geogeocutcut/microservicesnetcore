using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.partyDomain.Model
{
	public class Role:Entity<Guid>
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
		public string Code { get; set; }
		public string Label { get; set; }
    }
}
