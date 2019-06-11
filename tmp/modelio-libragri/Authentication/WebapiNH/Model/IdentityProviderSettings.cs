namespace Libragri.AuthenticationDomain.Webapi.Model
{
    public class IdentityProviderSettings
    {
        public string Iss { get; set; }
        public string Aud { get; set; }

        public int ExpireMinutes { get; set; }
    }
}