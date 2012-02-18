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
    /// Interaction logic for AddTopic.xaml
    /// </summary>
    public partial class AddTopic : Window
    {
        Topic TempTopic;
        public AddTopic( Topic TopicToAdd)
        {
            TempTopic = TopicToAdd;
            InitializeComponent();
            TopicTextBox.Focus();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            TempTopic.TopicName = TopicTextBox.Text;
            this.Close();
        }
    }
}
