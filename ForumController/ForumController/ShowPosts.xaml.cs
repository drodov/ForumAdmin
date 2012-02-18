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
using System.Data.Objects;
using System.Collections.ObjectModel;

namespace ForumController
{
    /// <summary>
    /// Interaction logic for ShowPosts.xaml
    /// </summary>
    public partial class ShowPosts : Window
    {
        Users curUser;
        ForumV2Entities forumEntities;
        public ShowPosts(Users user, ForumV2Entities f)
        {
            forumEntities = f; 
            curUser = user;
            InitializeComponent();
            this.Title = "Messages: " + user.UserName;
            int privForumID = (from q in forumEntities.Forum
                               where q.ForumName == "usersprivateforum"
                               select q.ForumID).First();
            var posts = from q in forumEntities.Post
                        where q.UserId == curUser.UserId && q.ForumID != privForumID
                        select q;
            ObservableCollection<Post> col = new ObservableCollection<Post>(posts);
            dataGrid1.ItemsSource = col;
        }
    }
}
