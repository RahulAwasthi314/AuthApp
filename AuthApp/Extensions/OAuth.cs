using AuthApp.Utilities;

namespace AuthApp.Extensions
{
    public static class OAuth
    {
        public static void OAuthenticationExtension(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddAuthentication()
            .AddFacebook(facebookOptions =>
            {
                facebookOptions.ClientId = configuration["Authentication:Facebook:ClientId"];
                facebookOptions.ClientSecret = configuration["Authentication:Facebook:ClientSecret"];
                facebookOptions.AccessDeniedPath = Constants.ErrorPath;
            })
            .AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = configuration["Authentication:Microsoft:ClientId"];
                microsoftOptions.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
                microsoftOptions.AccessDeniedPath = Constants.ErrorPath;
            })
            .AddTwitter(twitterOptions =>
            {
                twitterOptions.ClientId = configuration["Authentication:Twitter:ClientId"];
                twitterOptions.ClientSecret = configuration["Authentication:Twitter:ClientSecret"];
                twitterOptions.AccessDeniedPath = Constants.ErrorPath;
            })
            .AddLinkedIn(linkedInOptions =>
            {
                linkedInOptions.ClientId = configuration["Authentication:LinkedIn:ClientId"];
                linkedInOptions.ClientSecret = configuration["Authentication:LinkedIn:ClientSecret"];
                linkedInOptions.AccessDeniedPath = Constants.ErrorPath;
            });
        }
    }
}
