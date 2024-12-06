using System;
using Microsoft.Identity.Client;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;

namespace Library_Management
{
    public partial class LoginWindow : Window
    {
        private readonly string[] scopes = new string[] { "User.Read" };

        public LoginWindow()
        {
            this.InitializeComponent();
        }

        private async void SignInWithMicrosoft_Click(object sender, RoutedEventArgs e)
        {
            if (App.PublicClientApp == null)
            {
                Debug.WriteLine("Initialization error: PublicClientApp is null.");
                return;
            }

            try
            {
                var result = await App.PublicClientApp.AcquireTokenInteractive(scopes)
                    .WithPrompt(Prompt.SelectAccount)
                    .ExecuteAsync();

                if (result != null)
                {
                    SuccessMessage.Text = "Login successful!";

                    // Navigate to Admin Home screen
                    var adminHome = new AdminHome();
                    adminHome.Activate();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during interactive login: {ex.Message}");
            }
        }
    }
}
