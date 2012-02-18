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
using System.Configuration;

namespace ForumController
{
    /// <summary>
    /// Interaction logic for TryLogin.xaml
    /// </summary>
    public partial class TryLogin : Window
    {
        Users User;
        CDatabase DB;
        CLoginState stateLog;
        public TryLogin(CLoginState state, Users UserTryLogin, CDatabase db )
        {
            DB = db;
            User = UserTryLogin;
            stateLog = state;
            InitializeComponent();
            loginTextBox.Focus();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (DB.TryLogin(loginTextBox.Text.ToString(), passTextBox.Password.ToString()) == true) //success
            {
                User.UserName = loginTextBox.Text.ToString();
                //User = DB.GetUserInfo(User.UserName);
                stateLogLabel.Foreground = Brushes.Green;
                stateLogLabel.Content = "Success!";
                stateLog.StateLog = true;
                this.Close();
            }
            else                                                                                    //fail
            {
                stateLogLabel.Foreground = Brushes.Red;
                stateLogLabel.Content = "Error! Try again.";
                passTextBox.Clear();
                stateLog.StateLog = false;
            }
        }
    }
}
