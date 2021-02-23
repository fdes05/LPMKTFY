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
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", new[] { JwtClaimTypes.Role })
            };

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
                    AllowedScopes = { "mktfyapi.scope", IdentityServerConstants.StandardScopes.OpenId }
                }
            };
    }
}
