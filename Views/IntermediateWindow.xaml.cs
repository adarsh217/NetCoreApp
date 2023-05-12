using System.Windows;
using WpfApp3.Models;
using WpfApp3.Services;

namespace WpfApp3.Views
{
    public partial class IntermediateWindow : Window
    {
        public IntermediateWindow()
        {
            InitializeComponent();
        }

        private void InventoryButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the InventoryManagementWindow
            InventoryManagementWindow inventoryWindow = new InventoryManagementWindow();
            inventoryWindow.Show();
            this.Close();
        }

        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the SalesManagementWindow
            SalesManagementWindow salesWindow = new SalesManagementWindow();
            salesWindow.Show();
            this.Close();
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the SalesReportsWindow
            SalesReportsWindow reportsWindow = new SalesReportsWindow();
            reportsWindow.Show();
            this.Close();
        }
    }
}
