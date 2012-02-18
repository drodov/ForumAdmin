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
    /// Interaction logic for EditPost.xaml
    /// </summary>
    public partial class EditPost : Window
    {
        Post TempPost;
        public EditPost(Post PostToEdit)
        {
            TempPost = PostToEdit;
            InitializeComponent();
            PostTextBox.Focus();
            PostTextBox.Text = PostToEdit.Text;
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            TempPost.Text = PostTextBox.Text;
            this.Close();
        }
    }
}
