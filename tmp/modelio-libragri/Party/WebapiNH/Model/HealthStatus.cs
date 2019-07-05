namespace Libragri.PartyDomain.Webapi.Model
{
    public enum HealthStatusEnum
    {
        Ok,
        Error
    }
    public class HealthStatus
    {
        public string Status {get;set;}
        public string Message {get;set;}

    }
}