using System.Windows;
using System.Windows.Controls;

namespace WpfApp3.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
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
}
