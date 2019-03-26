using System;
using System.Collections.Generic;

namespace Libragri.partyDomain.Model
{
	public class User
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
		public string Login { get; set; }
		public string PasswordSha1 { get; set; }
		public bool IsActive { get; set; }
    }
}
