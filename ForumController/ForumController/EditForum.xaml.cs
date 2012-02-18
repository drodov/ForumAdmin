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
    /// Interaction logic for EditForum.xaml
    /// </summary>
    public partial class EditForum : Window
    {
        Forum TempForum;
        public EditForum(Forum ForumToEdit)
        {
            TempForum = ForumToEdit;
            InitializeComponent();
            ForumTextBox.Focus();
            ForumTextBox.Text = ForumToEdit.ForumName;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            TempForum.ForumName = ForumTextBox.Text;
            this.Close();
        }
    }
}
