using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Auth
{
    public static class Config
    {
        // The IEnumerable is the return type that is expected 'AddInMemoryIdentityResources' in the Startup file
        // The IEnumerable is implemented by 'List' so we're returning a list which has access to the super object (IEnumerable)
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", new[] { JwtClaimTypes.Role })
            };

        // Defining the API Resources static method (this way the Startup class can just use the method) so the startup class can access it
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "mktfyapi",
                    DisplayName = "MKTFY API",
                    Scopes = { "mktfyapi.scope", JwtClaimTypes.Role}
                }
            };

        // Defining ApiScopes method for the statup class. 
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("mktfyapi.scope", "MKTFY API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "mobile",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets = 
                    {
                        new Secret("UzKjRFnAHffxUFati8HMjSEzwMGgGhmn".Sha256())
                    },
                    AllowedScopes = { "mktfyapi.scope", "roles", IdentityServerConstants.StandardScopes.OpenId }
                }
            };
    }
}
