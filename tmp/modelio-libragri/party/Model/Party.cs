using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.PartyDomain.Model
{
	public class Party:Entity<Guid>
    {
    	
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
		public string Name { get; set; }

        public override Guid GetId()
        {
            return Id;
        }
    }
}
