using System.Collections.Generic;
using System.Linq;

namespace Core.Specification
{
    public class BusinessResult
    {
        public List<BusinessViolation> Violations;
        public bool IsSuccess {
            get {
                return !Violations.Any(x=>x.Level==BusinessLevel.Error);
            }
        }


        public bool HasBlockingError
        {
            get
            {
                return Violations.Any(x => x.Level == BusinessLevel.BlockingError);
            }
        }
        public string Messages
        {
            get
            {
                string mess = "";
                foreach(BusinessViolation v in Violations)
                {
                    mess+=(string.IsNullOrEmpty(mess) ? "" : "\n")+v.Message;
                }
                return mess;
            }
        }
        public BusinessResult()
        {
            Violations = new List<BusinessViolation>();
        }
        
    }
}
