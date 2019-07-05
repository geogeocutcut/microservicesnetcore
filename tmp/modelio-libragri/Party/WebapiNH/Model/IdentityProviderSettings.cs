namespace Libragri.PartyDomain.Webapi.Model
{
    public class IdentityProviderSettings
    {
        public string ClientId{get;set;}
        public string Iss { get; set; }
        public string Aud { get; set; }

        public int ExpireMinutes { get; set; }
    }
}