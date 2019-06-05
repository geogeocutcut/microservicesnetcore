using System.Collections.Generic;

namespace Core.Common.Authentication
{
    /// <summary>
    /// information related to user application subscriptions
    /// </summary>
    public class Identity
    {
        public string ApplicationId { get; set; }
        public string ApplicationType { get; set; }
        public string ApplicationCode { get; set; }

        //a set of legacy ids under current application
        public List<string> Logins { get; set; }
    }
}