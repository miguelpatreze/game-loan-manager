using IdentityModel;
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
                AllowedScopes = new List<string>{ "openid", "games.loan.manager.web", "write"},
                Enabled = true,
                RedirectUris = new List<string>{"http://localhost:4200"},
                PostLogoutRedirectUris = new List<string>{"http://localhost:4200"},
                AllowedCorsOrigins = new List<string>{"http://localhost:4200"},
                RequirePkce =false,
                RequireConsent = false,
                AlwaysIncludeUserClaimsInIdToken = true
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource(
                name: "games.loan.manager.web",
                userClaims: new List<string>{JwtClaimTypes.Name, JwtClaimTypes.Role } )
        };
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] {
        new ApiScope(){
            Name = "write",
            UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Role }
        } };
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] {

        new ApiResource("games.loan.manager.api")
        {
            Scopes = new List<string>{ "write" }
        }
        };
    }
}
