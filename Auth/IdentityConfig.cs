using Duende.IdentityServer.Models;

namespace Auth
{
    public static class IdentityConfig
    {
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
            new Client
            {
                ClientId = "blazor-client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "blog_api", "openid", "profile" },
                AllowOfflineAccess = true
            }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
            new ApiScope("blog_api", "Blog API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
            };
    }
}
