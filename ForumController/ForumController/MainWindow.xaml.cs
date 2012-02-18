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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Objects;
using System.Collections.ObjectModel;

namespace ForumController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CDatabase DB;
        ObservableCollection<ForumExt> colForumExt;
        ObservableCollection<TopicExt> colTopicExt;
        ObservableCollection<PostExt> colPostExt;
        ObservableCollection<PostExt> colPostInbox;
        ObservableCollection<PostExt> colPostSent;
        ObservableCollection<Users> colUsers;
        Users UserLogined = new Users();
        ForumV2Entities forumEntities = new ForumV2Entities();
        CLoginState stateLog = new CLoginState(false);
        int curForumID = -1, curTopicID = -1, CurUserTopicMesIN, CurUserTopicMesOUT, curIdxTabControl2 = -1;
        string curForumName = "";

        public MainWindow()
        {
            DB = new CDatabase(forumEntities);
            InitializeComponent();
            this.IsEnabled = false;
            TryLogin tryLogWindow = new TryLogin(stateLog, UserLogined, DB);
            tryLogWindow.ShowDialog();
            //logged?
            if (stateLog.StateLog == false) //no
            {
                this.Close();
                return;
            }
            else
            {
                this.IsEnabled = true;                        //yes
            }
            UserLogined = DB.GetUserInfo(UserLogined.UserName);
            var mesin = from q in forumEntities.Topic
                        where q.TopicName == UserLogined.UserLoweredName + "privatetopicin"
                        select q.TopicID;
            CurUserTopicMesIN = mesin.First();
            var mesout = from q in forumEntities.Topic
                        where q.TopicName == UserLogined.UserLoweredName + "privatetopicout"
                        select q.TopicID;
            CurUserTopicMesOUT = mesout.First();
            FillShowForumTab();
        }

        void FillNews()
        {
            //new messages
                            // last visit priv mes
            DateTime lastVisitPrivMes;
            var lastvisit = from q in forumEntities.UserTopicLastActiv
                            where q.TopicId == CurUserTopicMesIN  && q.UserId == UserLogined.UserId
                            select q.UserActivLastTime;
            lastVisitPrivMes = lastvisit.First();
                            //search for new mes
            var newmsg = from q in forumEntities.Post
                         where q.TopicID == CurUserTopicMesIN && q.DateAdded >= lastVisitPrivMes
                         select q;
            countMessagesNews.Content = newmsg.Count().ToString();
            //new users
            var newusers = from q in forumEntities.Users
                           where q.CreatedDate >= UserLogined.LastActivityDate
                           select q;
            countUsersNews.Content = newusers.Count();
            if (newusers.Count() != 0)
            {
                colUsers = new ObservableCollection<Users>(newusers);
                NewUsersDataGrid.ItemsSource = colUsers;
            }
        }

        void FillShowForumTab()
        {
            //var c = from q in forumEntities.Forum
            //        where q.ForumName != "usersprivateforum"
            //        select q;
            //colForum = new ObservableCollection<Forum>(c);
            //ForumsDataGrid.ItemsSource = colForum;

            var query = from forum in forumEntities.Forum
                        join user in forumEntities.Users
                        on forum.UserId equals user.UserId
                        where forum.ForumName != "usersprivateforum"
                        select new ForumExt
                        {
                            ForumID = forum.ForumID,
                            TopicsCount = forum.TopicsCount,
                            ForumName = forum.ForumName,
                            LastPostID = forum.LastPostID,
                            CreateDate = forum.CreateDate,
                            UserId = forum.UserId,
                            Author = user.UserName
                        };

            colForumExt = new ObservableCollection<ForumExt>(query);
            ForumsDataGrid.ItemsSource = colForumExt;
        }

        void FillShowTopicTab(int ForumID)
        {
            /*var c = from q in forumEntities.Topic
                    where q.ForumID == ForumID
                    select q;
            colTopic = new ObservableCollection<Topic>(c);
            TopicsDataGrid.ItemsSource = colTopic;*/

            var query = from topic in forumEntities.Topic
                        join user in forumEntities.Users
                        on topic.UserId equals user.UserId
                        where topic.ForumID == curForumID
                        select new TopicExt
                        {
                            ForumID = topic.ForumID,
                            TopicID = topic.TopicID,
                            TopicName = topic.TopicName,
                            UserId = topic.UserId,
                            LastPostID = topic.LastPostID,
                            PostsCount = topic.PostsCount,
                            CreateDate = topic.CreateDate,
                            Author = user.UserName
                        };

            colTopicExt = new ObservableCollection<TopicExt>(query);
            TopicsDataGrid.ItemsSource = colTopicExt;
        }

        void FillShowPostTab(int TopicID)
        {
            /*var c = from q in forumEntities.Post
                    where q.TopicID == TopicID
                    select q;
            colPost = new ObservableCollection<Post>(c);
            PostsDataGrid.ItemsSource = colPost;*/

            var query = from post in forumEntities.Post
                        join user in forumEntities.Users
                        on post.UserId equals user.UserId
                        where post.TopicID == curTopicID
                        select new PostExt
                        {
                            ForumID = post.ForumID,
                            PostID = post.PostID,
                            Text = post.Text,
                            UserId = post.UserId,
                            TopicID = post.TopicID,
                            DateAdded = post.DateAdded,
                            Author = user.UserName
                        };

            colPostExt = new ObservableCollection<PostExt>(query);
            PostsDataGrid.ItemsSource = colPostExt;
        }

        void FillProfile()
        {
            LoginTextBox.Text = UserLogined.UserName;
            NameTextBox.Text = UserLogined.UserRealName;
            BirthDatePicker.SelectedDate = UserLogined.BirthdayDate;
            CountryTextBox.Text = UserLogined.Country;
            CityTextBox.Text = UserLogined.City;
            ICQTextBox.Text = UserLogined.ICQ;
            PhoneTextBox.Text = UserLogined.PhoneNumber;
            EmailTextBox.Text = UserLogined.Email;
            WebTextBox.Text = UserLogined.WebPage;
            ReggLabel.Content = UserLogined.CreatedDate.ToString();
            LastActLabel.Content = UserLogined.LastActivityDate.ToString();
            AboutTextBox.Text = UserLogined.About;
        }

        void FillNewMes()
        {
            //new messages
            // last visit priv mes
            UserTopicLastActiv lastVisitPrivMes;
            var lastvisit = from q in forumEntities.UserTopicLastActiv
                            where q.TopicId == CurUserTopicMesIN && q.UserId == UserLogined.UserId
                            select q;
            lastVisitPrivMes = lastvisit.First();
            //search for new mes
            var newmsg = from post in forumEntities.Post
                         join user in forumEntities.Users
                         on post.UserId equals user.UserId
                         where post.TopicID == CurUserTopicMesIN && post.DateAdded >= lastVisitPrivMes.UserActivLastTime
                         select new PostExt
                         {
                             PostID = post.PostID,
                             Text = post.Text,
                             UserId = post.UserId,
                             ForumID = post.ForumID,
                             TopicID = post.TopicID,
                             DateAdded = post.DateAdded,
                             Author = user.UserName
                         };
            colPostInbox = new ObservableCollection<PostExt>(newmsg);
            NewMesDataGrid.ItemsSource = colPostInbox;
            //update date of visit
            lastVisitPrivMes.UserActivLastTime = DateTime.Now;
            forumEntities.SaveChanges();
        }

        void FillInbox()
        {
            // last visit priv mes
            UserTopicLastActiv lastVisitPrivMes;
            var lastvisit = from q in forumEntities.UserTopicLastActiv
                            where q.TopicId == CurUserTopicMesIN && q.UserId == UserLogined.UserId
                            select q;
            lastVisitPrivMes = lastvisit.First();
            //update date of visit
            lastVisitPrivMes.UserActivLastTime = DateTime.Now;
            forumEntities.SaveChanges();

            var query = from post in forumEntities.Post
                        join user in forumEntities.Users
                        on post.UserId equals user.UserId
                        where post.TopicID == CurUserTopicMesIN
                        select new PostExt
                        {
                            PostID = post.PostID,
                            Text = post.Text,
                            UserId = post.UserId,
                            ForumID = post.ForumID,
                            TopicID = post.TopicID,
                            DateAdded = post.DateAdded,
                            Author = user.UserName
                        };
            colPostInbox = new ObservableCollection<PostExt>(query);
            InboxDataGrid.ItemsSource = colPostInbox;
        }

        void FillSent()
        {
            // last visit priv mes
            UserTopicLastActiv lastVisitPrivMes;
            var lastvisit = from q in forumEntities.UserTopicLastActiv
                            where q.TopicId == CurUserTopicMesOUT && q.UserId == UserLogined.UserId
                            select q;
            lastVisitPrivMes = lastvisit.First();
            //update date of visit
            lastVisitPrivMes.UserActivLastTime = DateTime.Now;
            forumEntities.SaveChanges();

            var query = from post in forumEntities.Post
                        join user in forumEntities.Users
                        on post.UserId equals user.UserId
                        where post.TopicID == CurUserTopicMesOUT
                        select new PostExt
                        {
                            PostID = post.PostID,
                            Text = post.Text,
                            UserId = post.UserId,
                            ForumID = post.ForumID,
                            TopicID = post.TopicID,
                            DateAdded = post.DateAdded,
                            Author = user.UserName
                        };
            colPostSent = new ObservableCollection<PostExt>(query);
            SentDataGrid.ItemsSource = colPostSent;
        }

        private void UsersRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UsersRadioButton.IsChecked = false;
            if (countUsersNews.Content.ToString() == "0")
                return;
            selectNewsGrid.Visibility = System.Windows.Visibility.Hidden;
            showNewsGrid.Visibility = System.Windows.Visibility.Visible;
            UserLogined.LastActivityDate = DateTime.Now;
            countUsersNews.Content = "0";
        }

        private void messagesRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            AccountTabItem.IsSelected = true;
            NewMesTabItem.IsSelected = true;
            messagesRadioButton.IsChecked = false;
        }

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            forumEntities.SaveChanges();
            if (NewsTabItem.IsSelected == true)
            {
                FillNews();
            }
        }

        private void tabControl2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InboxTabItem.IsSelected == true)
            {
                if( curIdxTabControl2 != tabControl2.SelectedIndex)
                    FillInbox();
            }
            else if (SentTabItem.IsSelected == true)
            {
                if (curIdxTabControl2 != tabControl2.SelectedIndex)
                    FillSent();
            }
            else if (NewMesTabItem.IsSelected == true)
            {
                if (curIdxTabControl2 != tabControl2.SelectedIndex)
                {
                    FillNewMes();
                    countMessagesNews.Content = "0";
                }
            }
            else if (ProfileTabItem.IsSelected == true)
            {
                if (curIdxTabControl2 != tabControl2.SelectedIndex)
                    FillProfile();
            }
            curIdxTabControl2 = tabControl2.SelectedIndex;
        }

        //del
        private void ForumsDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                ForumExt ForumExtToDel = (ForumsDataGrid.SelectedValue as ForumExt);
                Forum ForumToDel = new Forum();
                ForumToDel.CreateDate = ForumExtToDel.CreateDate;
                ForumToDel.ForumID = ForumExtToDel.ForumID;
                ForumToDel.ForumName = ForumExtToDel.ForumName;
                ForumToDel.LastPostID = ForumExtToDel.LastPostID;
                ForumToDel.TopicsCount = ForumExtToDel.TopicsCount;
                ForumToDel.UserId = ForumExtToDel.UserId;
                DB.DelTopicsBelongToForum(ForumToDel.ForumID);
                var forumdel = from q in forumEntities.Forum
                         where q.ForumID == ForumToDel.ForumID
                         select q;
                forumEntities.DeleteObject(forumdel.First());
            }
        }

        private void TopicsDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                TopicExt TopicExtToDel = (TopicsDataGrid.SelectedValue as TopicExt);
                Topic TopicToDel = new Topic();
                TopicToDel.CreateDate = TopicExtToDel.CreateDate;
                TopicToDel.TopicID = TopicExtToDel.TopicID;
                TopicToDel.TopicName = TopicExtToDel.TopicName;
                TopicToDel.LastPostID = TopicExtToDel.LastPostID;
                TopicToDel.PostsCount = TopicExtToDel.PostsCount;
                TopicToDel.UserId = TopicExtToDel.UserId;
                TopicToDel.ForumID = TopicExtToDel.ForumID;
                //del connections in Users-Topic-LastActivity
                var lastAct = from qq in forumEntities.UserTopicLastActiv
                              where qq.TopicId == TopicToDel.TopicID
                              select qq;
                if (lastAct.Count() != 0)
                {
                    foreach (UserTopicLastActiv UserTopPasAct in lastAct)
                    {
                        forumEntities.DeleteObject(UserTopPasAct);
                    }
                }
                DB.DelPostsBelongToTopic(TopicToDel.TopicID, true);
                //call del posts
                DB.DecreaseTopicCount(TopicToDel.ForumID);
                var topicdel = from q in forumEntities.Topic
                               where q.TopicID == TopicToDel.TopicID
                               select q;
                forumEntities.DeleteObject(topicdel.First());
                //last post id
                Forum f = (from q in forumEntities.Forum
                           where q.ForumID == TopicToDel.ForumID
                           select q).First();
                var p = from q in forumEntities.Post
                         where q.ForumID == TopicToDel.ForumID
                         orderby q.DateAdded descending
                         select q.PostID;
                if (p.Count() == 0)
                {
                    f.LastPostID = null;
                }
                else
                {
                    f.LastPostID = p.First();
                }
            }
        }

        private void PostsDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                PostExt PostExtToDel = (PostsDataGrid.SelectedValue as PostExt);
                Post PostToDel = new Post();
                PostToDel.DateAdded = PostExtToDel.DateAdded;
                PostToDel.PostID = PostExtToDel.PostID;
                PostToDel.Text = PostExtToDel.Text;
                PostToDel.ForumID = PostExtToDel.ForumID;
                PostToDel.TopicID = PostExtToDel.TopicID;
                PostToDel.UserId = PostExtToDel.UserId;
                DB.DecreasePostCount(PostToDel.TopicID);
                var postdel = from q in forumEntities.Post
                               where q.PostID == PostToDel.PostID
                               select q;
                forumEntities.DeleteObject(postdel.First());
                //last post id?
                Forum f = (from q in forumEntities.Forum
                           where q.ForumID == PostToDel.ForumID
                           select q).First();
                var p = from q in forumEntities.Post
                         where q.ForumID == PostToDel.ForumID
                         orderby q.DateAdded descending
                         select q.PostID;
                if (p.Count() == 0)
                {
                    f.LastPostID = null;
                }
                else
                {
                    f.LastPostID = p.First();
                }
                Topic t = (from q in forumEntities.Topic
                           where q.TopicID == PostToDel.TopicID
                           select q).First();
                p = from q in forumEntities.Post
                        where q.TopicID == PostToDel.TopicID
                        orderby q.DateAdded descending
                        select q.PostID;
                if (p.Count() == 0)
                {
                    t.LastPostID = null;
                }
                else
                {
                    t.LastPostID = p.First();
                }
            }
        }
        
        //add
        private void AddForumButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ForumsDataGrid.ItemsSource = null;
                this.IsEnabled = false;
                Forum ForumToAdd = new Forum();
                AddForum addForum = new AddForum(ForumToAdd);
                addForum.ShowDialog();
                if (ForumToAdd.ForumName == null)
                    throw new Exception("none");
                if (ForumToAdd.ForumName == "")
                    throw new Exception("Write forum's name!");
                ForumToAdd.CreateDate = DateTime.Now;
                ForumToAdd.TopicsCount = 0;
                ForumToAdd.LastPostID = null;
                ForumToAdd.UserId = UserLogined.UserId;
                //check for uniq
                var f = from q in forumEntities.Forum
                        where q.ForumName == ForumToAdd.ForumName
                        select q;
                if (f.Count() != 0)
                    throw new Exception("Such forum already exists!");
                forumEntities.Forum.AddObject(ForumToAdd);
                forumEntities.SaveChanges();
            }
            catch (Exception excpt)
            {
                if(excpt.Message != "none")
                    MessageBox.Show(excpt.Message);
            }
            finally
            {
                FillShowForumTab();
                this.IsEnabled = true;
            }
        }

        private void AddTopicButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TopicsDataGrid.ItemsSource = null;
                this.IsEnabled = false;
                Topic TopicToAdd = new Topic();
                AddTopic addTopic = new AddTopic(TopicToAdd);
                addTopic.ShowDialog();
                if (TopicToAdd.TopicName == null)
                    throw new Exception("none");
                if (TopicToAdd.TopicName == "")
                    throw new Exception("Write topic's name!");
                TopicToAdd.CreateDate = DateTime.Now;
                TopicToAdd.PostsCount = 0;
                TopicToAdd.LastPostID = null;
                TopicToAdd.ForumID = curForumID;
                TopicToAdd.UserId = UserLogined.UserId;
                //check for uniq in cur forum
                var f = from q in forumEntities.Topic
                        where (q.TopicName == TopicToAdd.TopicName && q.ForumID == TopicToAdd.ForumID)
                        select q;
                if (f.Count() != 0)
                    throw new Exception("Such Topic already exists!");
                forumEntities.Topic.AddObject(TopicToAdd);
                DB.IncreaseTopicCount(curForumID);
                forumEntities.SaveChanges();
            }
            catch (Exception excpt)
            {
                if (excpt.Message != "none")
                    MessageBox.Show(excpt.Message);
            }
            finally
            {
                FillShowTopicTab(curForumID);
                this.IsEnabled = true;
            }
        }

        private void AddPostButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PostsDataGrid.ItemsSource = null;
                this.IsEnabled = false;
                Post PostToAdd = new Post();
                AddPost addPost = new AddPost(PostToAdd);
                addPost.ShowDialog();
                if (PostToAdd.Text == null)
                    throw new Exception("none");
                if (PostToAdd.Text == "")
                    throw new Exception("You should write smth!");
                PostToAdd.DateAdded = DateTime.Now;
                PostToAdd.ForumID = curForumID;
                PostToAdd.TopicID = curTopicID;
                PostToAdd.UserId = UserLogined.UserId;
                forumEntities.Post.AddObject(PostToAdd);
                DB.IncreasePostCount(curTopicID);
                //set last postID
                Forum f = (from q in forumEntities.Forum
                           where q.ForumID == PostToAdd.ForumID
                           select q).First();
                f.LastPostID = PostToAdd.PostID;
                Topic t = (from q in forumEntities.Topic
                           where q.TopicID == PostToAdd.TopicID
                           select q).First();
                t.LastPostID = PostToAdd.PostID;
                forumEntities.SaveChanges();
            }
            catch (Exception excpt)
            {
                if (excpt.Message != "none")
                    MessageBox.Show(excpt.Message);
            }
            finally
            {
                FillShowPostTab(curTopicID);
                this.IsEnabled = true;
            }
        }

        //edit
        private void EditForumButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ForumsDataGrid.SelectedIndex == -1)
                    throw new Exception("Select any forum.");
                int selForumID = (ForumsDataGrid.SelectedValue as Forum).ForumID;
                Forum ForumToEdit = new Forum();
                ForumToEdit.ForumName = (ForumsDataGrid.SelectedValue as Forum).ForumName;
                EditForum editForum = new EditForum(ForumToEdit);
                editForum.ShowDialog();
                if (ForumToEdit.ForumName == null || ForumToEdit.ForumName == "")
                    throw new Exception("You should write smth!");
                (from q in forumEntities.Forum
                 where q.ForumID == selForumID
                 select q).First().ForumName = ForumToEdit.ForumName;
                forumEntities.SaveChanges();
                FillShowForumTab();
            }
            catch (Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }

        private void EditTopicButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TopicsDataGrid.SelectedIndex == -1)
                    throw new Exception("Select any topic.");
                int selTopicID = (TopicsDataGrid.SelectedValue as Topic).TopicID;
                Topic TopicToEdit = new Topic();
                TopicToEdit.TopicName = (TopicsDataGrid.SelectedValue as Topic).TopicName;
                EditTopic editTopic = new EditTopic(TopicToEdit);
                editTopic.ShowDialog();
                if (TopicToEdit.TopicName == null || TopicToEdit.TopicName == "")
                    throw new Exception("You should write smth!");
                (from q in forumEntities.Topic
                 where q.TopicID == selTopicID
                 select q).First().TopicName = TopicToEdit.TopicName;
                forumEntities.SaveChanges();
                FillShowTopicTab(curForumID);
            }
            catch (Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }

        private void EditPostButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PostsDataGrid.SelectedIndex == -1)
                    throw new Exception("Select any post.");
                int selPostID = (PostsDataGrid.SelectedValue as Post).PostID;
                Post PostToEdit = new Post();
                PostToEdit.Text = (PostsDataGrid.SelectedValue as Post).Text;
                EditPost editPost = new EditPost(PostToEdit);
                editPost.ShowDialog();
                if (PostToEdit.Text == null || PostToEdit.Text == "")
                    throw new Exception("You should write smth!");
                (from q in forumEntities.Post
                 where q.PostID == selPostID
                 select q).First().Text = PostToEdit.Text;
                forumEntities.SaveChanges();
                FillShowPostTab(curTopicID);
            }
            catch (Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }

        //author
        private void AuthorForumButton_Click(object sender, RoutedEventArgs e)
        {
            if (ForumsDataGrid.SelectedIndex == -1)
                return;
            Guid selUserID = (ForumsDataGrid.SelectedValue as Forum).UserId;
            Users selUser = (from q in forumEntities.Users
                             where q.UserId == selUserID
                             select q).First();
            ShowUserProfile UserProfile = new ShowUserProfile(UserLogined, selUser, forumEntities, DB);
            UserProfile.Show();
        }

        private void AuthorTopicButton_Click(object sender, RoutedEventArgs e)
        {
            if (TopicsDataGrid.SelectedIndex == -1)
                return;
            Guid selUserID = (TopicsDataGrid.SelectedValue as Topic).UserId;
            Users selUser = (from q in forumEntities.Users
                             where q.UserId == selUserID
                             select q).First();
            ShowUserProfile UserProfile = new ShowUserProfile(UserLogined, selUser, forumEntities, DB);
            UserProfile.Show();
        }

        private void AuthorPostButton_Click(object sender, RoutedEventArgs e)
        {
            if (PostsDataGrid.SelectedIndex == -1)
                return;
            Guid selUserID = (PostsDataGrid.SelectedValue as Post).UserId;
            Users selUser = (from q in forumEntities.Users
                             where q.UserId == selUserID
                             select q).First();
            ShowUserProfile UserProfile = new ShowUserProfile(UserLogined, selUser, forumEntities, DB);
            UserProfile.Show();
        }

        //go
        private void ForumsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((ForumsDataGrid.SelectedItem as Forum) != null)
            {
                curForumID = (ForumsDataGrid.SelectedItem as Forum).ForumID;
                curForumName = (ForumsDataGrid.SelectedItem as Forum).ForumName;
                CurForumlabel.Content = curForumName;
                showForumsGrid.Visibility = System.Windows.Visibility.Hidden;
                FillShowTopicTab(curForumID);
                showTopicsGrid.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void TopicsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((TopicsDataGrid.SelectedItem as Topic) != null)
            {
                curTopicID = (TopicsDataGrid.SelectedItem as Topic).TopicID;
                CurTopiclabel.Content = curForumName + " >> " + (TopicsDataGrid.SelectedItem as Topic).TopicName;
                showTopicsGrid.Visibility = System.Windows.Visibility.Hidden;
                FillShowPostTab(curTopicID);
                showPostsGrid.Visibility = System.Windows.Visibility.Visible;
            }
        }
        
        //back
        private void BackToForumsButton_Click(object sender, RoutedEventArgs e)
        {
            curForumID = -1;
            showTopicsGrid.Visibility = System.Windows.Visibility.Hidden;
            FillShowForumTab();
            showForumsGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void BackToTopicsButton_Click(object sender, RoutedEventArgs e)
        {
            curTopicID = -1;
            showPostsGrid.Visibility = System.Windows.Visibility.Hidden;
            FillShowTopicTab(curForumID);
            showTopicsGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            forumEntities.SaveChanges();
        }

        private void FindUsersButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginTextBox.Text == "")
            {
                var u = from q in forumEntities.Users
                        select q;
                colUsers = new ObservableCollection<Users>(u);
            }
            else
            {
                var u = from q in forumEntities.Users
                        where q.UserLoweredName.Contains(loginTextBox.Text.ToLower())
                        select q;
                colUsers = new ObservableCollection<Users>(u);
            }
            ObservableCollection<Users> tempColUsers;
            if (AllUserRadioButton.IsChecked == false)
            {
                tempColUsers = new ObservableCollection<Users>();
                if (AdminRadioButton.IsChecked == true)
                {
                    Guid ParametrRoleId = (from q in forumEntities.Roles
                                      where q.RoleName == "Admin"
                                      select q.RoleId).First();
                    var paramID = from q in forumEntities.UsersInRoles
                                   where q.RoleId == ParametrRoleId
                                   select q.UserId;
                    foreach (var id in paramID)
                    {
                        var u = from q in colUsers
                                where q.UserId == id
                                select q;
                        if(u.Count() != 0 )
                            tempColUsers.Add(u.First());
                    }
                }
                else if (ModerRadioButton.IsChecked == true)
                {
                    Guid ParametrRoleId = (from q in forumEntities.Roles
                                           where q.RoleName == "Moderator"
                                           select q.RoleId).First();
                    var paramID = from q in forumEntities.UsersInRoles
                                  where q.RoleId == ParametrRoleId
                                  select q.UserId;
                    foreach (var id in paramID)
                    {
                        var u = from q in colUsers
                                where q.UserId == id
                                select q;
                        if (u.Count() != 0)
                            tempColUsers.Add(u.First());
                    }
                }
                else if (BanUserRadioButton.IsChecked == true)
                {
                    Guid ParametrRoleId = (from q in forumEntities.Roles
                                           where q.RoleName == "LockedUser"
                                           select q.RoleId).First();
                    var paramID = from q in forumEntities.UsersInRoles
                                  where q.RoleId == ParametrRoleId
                                  select q.UserId;
                    foreach (var id in paramID)
                    {
                        var u = from q in colUsers
                                where q.UserId == id
                                select q;
                        if (u.Count() != 0)
                            tempColUsers.Add(u.First());
                    }
                }
                UsersDataGrid.ItemsSource = tempColUsers;
            }
            else
                UsersDataGrid.ItemsSource = colUsers;
        }

        private void UsersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (UsersDataGrid.SelectedIndex == -1)
                return;
            ShowUserProfile UserProfile = new ShowUserProfile(UserLogined, (UsersDataGrid.SelectedItem as Users), forumEntities, DB);
            UserProfile.Show();
        }

        private void NewUsersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (NewUsersDataGrid.SelectedIndex == -1)
                return;
            ShowUserProfile UserProfile = new ShowUserProfile(UserLogined, (NewUsersDataGrid.SelectedItem as Users), forumEntities, DB);
            UserProfile.Show();
        }

        private void SaveProfileButton_Click(object sender, RoutedEventArgs e)
        {
            //UserLogined.UserName = LoginTextBox.Text.ToString();
            //UserLogined.UserLoweredName = UserLogined.UserName.ToLower();
            UserLogined.UserRealName = NameTextBox.Text;
            //Role
            UserLogined.BirthdayDate = BirthDatePicker.SelectedDate;
            UserLogined.Country = CountryTextBox.Text;
            UserLogined.City = CityTextBox.Text;
            UserLogined.ICQ = ICQTextBox.Text;
            UserLogined.PhoneNumber = PhoneTextBox.Text;
            UserLogined.Email = EmailTextBox.Text;
            UserLogined.WebPage = WebTextBox.Text;
            UserLogined.About = AboutTextBox.Text;
            forumEntities.SaveChanges();
            //

            try
            {
                if (LoginTextBox.Text == "" || LoginTextBox.Text.Contains(" "))
                {
                    LoginTextBox.Text = UserLogined.UserName;
                    throw new Exception("Uncorrect login!");
                }
                if (UserLogined.UserName != LoginTextBox.Text)
                {
                    int checkNewLogin = (from q in forumEntities.Users
                                         where q.UserName == LoginTextBox.Text
                                         select q).Count();
                    if (checkNewLogin != 0)
                    {
                        LoginTextBox.Text = UserLogined.UserName;
                        throw new Exception("Login is in usage!");
                    }
                    //change private topics
                    var temp = from q in forumEntities.Topic
                               where q.TopicID == CurUserTopicMesIN
                               select q;
                    temp.First().TopicName = (LoginTextBox.Text.ToLower() + "privatetopicin");
                    temp = from q in forumEntities.Topic
                           where q.TopicID == CurUserTopicMesOUT
                           select q;
                    temp.First().TopicName = (LoginTextBox.Text.ToLower() + "privatetopicout");
                    UserLogined.UserName = LoginTextBox.Text.ToString();
                    UserLogined.UserLoweredName = UserLogined.UserName.ToLower();
                }
                forumEntities.SaveChanges();
            }
            catch (Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }

        private void BackToNewsButton_Click(object sender, RoutedEventArgs e)
        {
            showNewsGrid.Visibility = System.Windows.Visibility.Hidden;
            selectNewsGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void InboxDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (InboxDataGrid.SelectedIndex == -1)
                return;
            Guid selUserID = (InboxDataGrid.SelectedValue as Post).UserId;
            Users selUser = (from q in forumEntities.Users
                             where q.UserId == selUserID
                             select q).First();
            ShowUserProfile UserProfile = new ShowUserProfile(UserLogined, selUser, forumEntities, DB);
            UserProfile.Show();
        }

        private void SentTabItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SentDataGrid.SelectedIndex == -1)
                return;
            Guid selUserID = (SentDataGrid.SelectedValue as Post).UserId;
            Users selUser = (from q in forumEntities.Users
                             where q.UserId == selUserID
                             select q).First();
            ShowUserProfile UserProfile = new ShowUserProfile(UserLogined, selUser, forumEntities, DB);
            UserProfile.ShowDialog();
            FillSent();
        }

        private void NewMesTabItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (NewMesDataGrid.SelectedIndex == -1)
                return;
            Guid selUserID = (NewMesDataGrid.SelectedValue as Post).UserId;
            Users selUser = (from q in forumEntities.Users
                             where q.UserId == selUserID
                             select q).First();
            ShowUserProfile UserProfile = new ShowUserProfile(UserLogined, selUser, forumEntities, DB);
            UserProfile.Show();
        }

        private void InboxTabItem_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                PostExt PostExtToDel = (InboxDataGrid.SelectedValue as PostExt);
                Post PostToDel = new Post();
                PostToDel.DateAdded = PostExtToDel.DateAdded;
                PostToDel.PostID = PostExtToDel.PostID;
                PostToDel.Text = PostExtToDel.Text;
                PostToDel.ForumID = PostExtToDel.ForumID;
                PostToDel.TopicID = PostExtToDel.TopicID;
                PostToDel.UserId = PostExtToDel.UserId;
                DB.DecreasePostCount(PostToDel.TopicID);
                var postdel = from q in forumEntities.Post
                              where q.PostID == PostToDel.PostID
                              select q;
                forumEntities.DeleteObject(postdel.First());
            }
        }

        private void SentDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                PostExt PostExtToDel = (SentDataGrid.SelectedValue as PostExt);
                Post PostToDel = new Post();
                PostToDel.DateAdded = PostExtToDel.DateAdded;
                PostToDel.PostID = PostExtToDel.PostID;
                PostToDel.Text = PostExtToDel.Text;
                PostToDel.ForumID = PostExtToDel.ForumID;
                PostToDel.TopicID = PostExtToDel.TopicID;
                PostToDel.UserId = PostExtToDel.UserId;
                DB.DecreasePostCount(PostToDel.TopicID);
                var postdel = from q in forumEntities.Post
                              where q.PostID == PostToDel.PostID
                              select q;
                forumEntities.DeleteObject(postdel.First());
            }
        }

        private void ChngPassButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword ChPass = new ChangePassword(UserLogined, DB);
            ChPass.ShowDialog();
            forumEntities.SaveChanges();
        }
    }
}
