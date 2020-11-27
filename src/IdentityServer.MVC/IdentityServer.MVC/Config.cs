using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer.MVC
{
    public static class Config
    {

        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client
            {
                ClientId = "client_miguel",
                ClientName = "Client Miguel",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = new List<string>{ "openid" },
                Enabled = true,
                RedirectUris = new List<string>{"http://localhost:4200"},
                PostLogoutRedirectUris = new List<string>{"http://localhost:4200"},
                AllowedCorsOrigins = new List<string>{"http://localhost:4200"},
                RequirePkce =false,
                RequireConsent = false
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] { };
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] { };
    }
}
