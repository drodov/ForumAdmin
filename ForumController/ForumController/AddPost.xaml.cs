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
    /// Interaction logic for AddPost.xaml
    /// </summary>
    public partial class AddPost : Window
    {
        Post TempPost;
        public AddPost(Post PostToAdd)
        {
            TempPost = PostToAdd;
            InitializeComponent();
            PostTextBox.Focus();
        }
        public AddPost(Post PostToAdd, string name)
        {
            TempPost = PostToAdd;
            InitializeComponent();
            this.Title = "Send Message To " + name;
            PostTextBox.Focus();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            TempPost.Text = PostTextBox.Text;
            this.Close();
        }
    }
}
