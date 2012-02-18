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
using System.Text.RegularExpressions;

namespace ForumController
{
    /// <summary>
    /// Interaction logic for BanParameters.xaml
    /// </summary>
    public partial class BanParameters : Window
    {
        Users UserForBan;
        public BanParameters(Users userforban)
        {
            UserForBan = userforban;
            InitializeComponent();
        }

        private void BanButton_Click(object sender, RoutedEventArgs e)
        {
            if (DaysTextBox.Text == String.Empty)
                DaysTextBox.Text = "0";
            if (MonthsTextBox.Text == String.Empty)
                MonthsTextBox.Text = "0";
            if (YearsTextBox.Text == String.Empty)
                YearsTextBox.Text = "0";
            int forcheck = 0;
            bool validate = true;
            string MsgError = "Wrong parameters:";
            if (int.TryParse(DaysTextBox.Text.ToString(), out forcheck) == false)
            {
                MsgError += "\n- Days";
                validate = false;
            }
            if (int.TryParse(MonthsTextBox.Text.ToString(), out forcheck) == false)
            {
                MsgError += "\n- Months";
                validate = false;
            }
            if (int.TryParse(YearsTextBox.Text.ToString(), out forcheck) == false)
            {
                MsgError += "\n- Years";
                validate = false;
            }
            if (validate == false)
            {
                MessageBox.Show(MsgError);
                return;
            }
            DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            time = time.AddDays(double.Parse(DaysTextBox.Text));
            time = time.AddMonths(int.Parse(MonthsTextBox.Text));
            time = time.AddYears(int.Parse(YearsTextBox.Text));
            UserForBan.LockedReason = ReasonTextBox.Text;
            UserForBan.LockedDateOut = time;
            this.Close();
        }
    }
}