using IdentityServer4.Models;

namespace IdentityServer.Config
{

    public static class ApiResources
    {

        public static IEnumerable<ApiResource> Get()
        {
            return new List<ApiResource>
        {
            new ApiResource("api1", "My API")
        };
        }

    }

}