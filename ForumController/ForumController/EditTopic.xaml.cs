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
    /// Interaction logic for EditTopic.xaml
    /// </summary>
    public partial class EditTopic : Window
    {
        Topic TempTopic;
        public EditTopic(Topic TopicToEdit)
        {
            TempTopic = TopicToEdit;
            InitializeComponent();
            TopicTextBox.Focus();
            TopicTextBox.Text = TopicToEdit.TopicName;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            TempTopic.TopicName = TopicTextBox.Text;
            this.Close();
        }
    }
}
