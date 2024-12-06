using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.UI.Xaml;

namespace Library_Management
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private static readonly IConfiguration Configuration;
        private static readonly string clientId;
        private static readonly string tenantId;
        private static readonly string authority;

        public static IPublicClientApplication? PublicClientApp { get; private set; }

        static App()
        {
            var appLocation = AppContext.BaseDirectory; // Ensure the base path is set correctly

            var builder = new ConfigurationBuilder()
                .SetBasePath(appLocation)
                .AddJsonFile("secrets.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            clientId = Configuration["AzureAd:ClientId"] ?? throw new ArgumentNullException(nameof(clientId));
            tenantId = Configuration["AzureAd:TenantId"] ?? throw new ArgumentNullException(nameof(tenantId));
            authority = $"https://login.microsoftonline.com/{tenantId}";
        }

        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            PublicClientApp = PublicClientApplicationBuilder.Create(clientId)
                               .WithAuthority(authority)
                               .WithDefaultRedirectUri()
                               .Build();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window? m_window;
    }
}
