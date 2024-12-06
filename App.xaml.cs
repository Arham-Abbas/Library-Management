using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.UI.Xaml;

namespace Library_Management
{
    public partial class App : Application
    {
        private static readonly IConfiguration Configuration;
        private static readonly string clientId;
        private static readonly string tenantId;
        private static readonly string authority;

        public static IPublicClientApplication? PublicClientApp { get; private set; }

        static App()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("secrets.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            clientId = Configuration["AzureAd:ClientId"] ?? throw new ArgumentNullException(nameof(clientId));
            tenantId = Configuration["AzureAd:TenantId"] ?? throw new ArgumentNullException(nameof(tenantId));
            authority = $"https://login.microsoftonline.com/{tenantId}";
        }

        public App()
        {
            this.InitializeComponent();
            PublicClientApp = PublicClientApplicationBuilder.Create(clientId)
                               .WithAuthority(authority)
                               .WithDefaultRedirectUri()
                               .Build();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Activate();
        }
    }
}
