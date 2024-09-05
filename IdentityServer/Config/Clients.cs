using IdentityServer4.Models;

namespace IdentityServer.Config
{

    public static class Clients
    {

        public static IEnumerable<Client> Get()
        {
            return new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:5002/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                AllowedScopes = { "openid", "profile", "api1" },
                RequirePkce = true,
                AllowOfflineAccess = true
            }
        };
        }
    }

}