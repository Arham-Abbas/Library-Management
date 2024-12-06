using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;
using System;

namespace Library_Management
{
    public partial class AdminHome : Window
    {
        public AdminHome()
        {
            this.InitializeComponent();
        }

        private Task<ContentDialogResult> ShowMessageAsync(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "Information",
                Content = message,
                CloseButtonText = "Ok",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.Content.XamlRoot // Ensure dialog is associated with the window
            };

            // Return the async operation as a Task
            return dialog.ShowAsync().AsTask();
        }

        private async void MaintenanceButton_Click(object sender, RoutedEventArgs e)
        {
            await ShowMessageAsync("Maintenance clicked.");
        }

        private async void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            await ShowMessageAsync("Reports clicked.");
        }

        private async void TransactionsButton_Click(object sender, RoutedEventArgs e)
        {
            await ShowMessageAsync("Transactions clicked.");
        }

        private async void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            await ShowMessageAsync("Log Out clicked.");
        }
    }
}
