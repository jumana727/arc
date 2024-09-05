using IdentityServer4.Models;

namespace IdentityServer.Config
{

    public static class IdentityResources
    {

        public static IEnumerable<IdentityResource> Get()
        {
            return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
        }

    }

}