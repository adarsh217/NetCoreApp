using System.Windows;
using WpfApp3.Models;
using WpfApp3.Services;
using WpfApp3.Views;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
{
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (username == "jjusino" && password == "COMP205")
            {
                // Navigate to the IntermediateWindow
                IntermediateWindow intermediateWindow = new IntermediateWindow();
                intermediateWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
