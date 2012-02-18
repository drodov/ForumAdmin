using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ForumController
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        Users User;
        CDatabase DB;
        public ChangePassword(Users UserTryChPass, CDatabase db)
        {
            User = UserTryChPass;
            db = DB;
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            //matching old pass
            if (User.Password != OldPasswordBox.Password)
            {
                clearFields();
                ErrorLabel.Foreground = Brushes.Red;
                ErrorLabel.Content = "Old password is wrong!";
            }
            else if (New1PasswordBox.Password != New2PasswordBox.Password)
            {
                ErrorLabel.Foreground = Brushes.Red;
                ErrorLabel.Content = "New passwords are different!";
                New1PasswordBox.Password = String.Empty;
                New2PasswordBox.Password = String.Empty;
            }
            else if (New1PasswordBox.Password == "")
            {
                ErrorLabel.Foreground = Brushes.Red;
                ErrorLabel.Content = "Password can't be empty!";
            }
            else
            {
                User.Password = New1PasswordBox.Password;
                clearFields();
                ErrorLabel.Foreground = Brushes.Green;
                ErrorLabel.Content = "Password's change is successful!";
            }
        }

        void clearFields()
        {
            New1PasswordBox.Password = String.Empty;
            New2PasswordBox.Password = String.Empty;
            OldPasswordBox.Password = String.Empty;
        }
    }
}
