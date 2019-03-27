using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Libragri.partyDomain.Model
{
	public class User:Entity<Guid>
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
		public string Login { get; set; }
		public string PasswordSha1 { get; set; }
		public bool IsActive { get; set; }
    }
}
