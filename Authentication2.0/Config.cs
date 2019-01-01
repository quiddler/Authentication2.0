using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Sts
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        // can only get these through open id connect
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.Profile(),
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResource("location", "The person's GPS location.", new List<string>{ "location" })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // OAuth2.0 api client credentials flow console client
                new Client
                {
                    ClientId = "ConsoleClient",

                    // no interactive user, use the client id / secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // what the client is allowed to access
                    AllowedScopes = { "api1", JwtClaimTypes.Profile }
                },

                // OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "MvcClient",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "location"
                    },

                    // do not show the app permission page
                    //RequireConsent = false
                }
            };
        }
    }
}
