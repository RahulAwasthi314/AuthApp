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
                facebookOptions.AccessDeniedPath = "/Home/Error";
            })
            .AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = configuration["Authentication:Microsoft:ClientId"];
                microsoftOptions.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
            })
            .AddTwitter(twitterOptions =>
            {
                twitterOptions.ClientId = configuration["Authentication:Twitter:ClientId"];
                twitterOptions.ClientSecret = configuration["Authentication:Twitter:ClientSecret"];
            })
            .AddLinkedIn(linkedInOptions =>
            {
                linkedInOptions.ClientId = configuration["Authentication:LinkedIn:ClientId"];
                linkedInOptions.ClientSecret = configuration["Authentication:LinkedIn:ClientSecret"];
            });
        }
    }
}
