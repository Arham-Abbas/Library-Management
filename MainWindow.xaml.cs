using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;

namespace Library_Management
{
    public sealed partial class MainWindow : Window
    {
        private readonly string[] scopes = new string[] { "User.Read" };

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void MyButton_Click(object sender, RoutedEventArgs e)
        {
            await SignInUserAsync();
        }

        private async Task SignInUserAsync()
        {
            if (App.PublicClientApp == null)
            {
                Debug.WriteLine("Initialization error: PublicClientApp is null.");
                return;
            }

            var accounts = await App.PublicClientApp.GetAccountsAsync();
            AuthenticationResult result;

            try
            {
                if (accounts.Any())
                {
                    result = await App.PublicClientApp.AcquireTokenSilent(scopes, accounts.FirstOrDefault())
                                                     .ExecuteAsync();
                }
                else
                {
                    result = await App.PublicClientApp.AcquireTokenInteractive(scopes)
                                                     .ExecuteAsync();
                }

                myButton.Content = "Authenticated";
                var accessToken = result.AccessToken;
            }
            catch (MsalUiRequiredException)
            {
                try
                {
                    result = await App.PublicClientApp.AcquireTokenInteractive(scopes)
                                                     .ExecuteAsync();
                    myButton.Content = "Authenticated";
                    var accessToken = result.AccessToken;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error during interactive login: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error acquiring token silently: {ex.Message}");
            }
        }
    }
}
