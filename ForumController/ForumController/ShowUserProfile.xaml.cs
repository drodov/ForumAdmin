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
    /// Interaction logic for ShowUserProfile.xaml
    /// </summary>
    public partial class ShowUserProfile : Window
    {
        CDatabase DB;
        Users curUser, LoginedUser;
        ForumV2Entities forumEntities;
        Guid RoleAdminID = new Guid(), RoleModerID = new Guid(), RoleBannedID = new Guid();
        public ShowUserProfile(Users LogUser,Users selectUser, ForumV2Entities forum, CDatabase db)
        {
            LoginedUser = LogUser;
            curUser = selectUser;
            forumEntities = forum;
            DB = db;
            InitializeComponent();
            foreach (Roles r in forumEntities.Roles)
            {
                switch (r.RoleName)
                {
                    case "Admin": RoleAdminID = r.RoleId; break;
                    case "Moderator": RoleModerID = r.RoleId; break;
                    case "LockedUser": RoleBannedID = r.RoleId; break;
                }
            }
            FillProfile();
        }
        void FillProfile()
        {
            this.Title = "Profile: " + curUser.UserName;
            LoginTextBox.Text = curUser.UserName;
            NameTextBox.Content = curUser.UserRealName;
            BirthTextBox.Content = curUser.BirthdayDate.ToString();
            CountryTextBox.Content = curUser.Country;
            CityTextBox.Content = curUser.City;
            ICQTextBox.Content = curUser.ICQ;
            PhoneTextBox.Content = curUser.PhoneNumber;
            EmailTextBox.Content = curUser.Email;
            WebTextBox.Content = curUser.WebPage;
            RegTextBox.Content = curUser.CreatedDate.ToString();
            LastActTextBox.Content = curUser.LastActivityDate.ToString();
            AboutTextBox.Text = curUser.About;
            //Roles
            var rolesUser = from q in forumEntities.UsersInRoles
                        where q.UserId == curUser.UserId
                        select q.RoleId;
            foreach (Guid userRoleID in rolesUser)
            {
                if (userRoleID == RoleAdminID)
                    RoleAdminCheckBox.IsChecked = true;
                else if(userRoleID == RoleModerID)
                    RoleModerCheckBox.IsChecked = true;
                else if (userRoleID == RoleBannedID)
                {
                    RoleBannedCheckBox.IsChecked = true;
                    AboutTextBox.Text += "\n=====================================================\nBanned:\nDate:" + curUser.LockedDateOut.ToShortDateString() + "\nReason: " + curUser.LockedReason.ToString();
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (curUser.UserName != LoginTextBox.Text)
                {
                    if (LoginTextBox.Text == "" || LoginTextBox.Text.Contains(" "))
                    {
                        LoginTextBox.Text = curUser.UserName;
                        throw new Exception("Uncorrect login!");
                    }
                    int checkNewLogin = (from q in forumEntities.Users
                                         where q.UserName == LoginTextBox.Text
                                         select q).Count();
                    if (checkNewLogin != 0)
                    {
                        LoginTextBox.Text = curUser.UserName;
                        throw new Exception("Login is in usage!");
                    }
                    //change private topics
                    var temp = from q in forumEntities.Topic
                               where q.TopicName == (curUser.UserName.ToLower() + "privatetopicin")
                               select q;
                    temp.First().TopicName = (LoginTextBox.Text.ToLower() + "privatetopicin");
                    temp = from q in forumEntities.Topic
                               where q.TopicName == (curUser.UserName.ToLower() + "privatetopicout")
                               select q;
                    temp.First().TopicName = (LoginTextBox.Text.ToLower() + "privatetopicout");
                    curUser.UserName = LoginTextBox.Text.ToString();
                    curUser.UserLoweredName = curUser.UserName.ToLower();
                }
                forumEntities.SaveChanges();
            }
            catch (Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }

        private void SendMesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Post PostToAdd1 = new Post();
                Post PostToAdd2 = new Post();
                AddPost Mes = new AddPost(PostToAdd1, curUser.UserName);
                Mes.ShowDialog();
                if (PostToAdd1.Text == null)
                    throw new Exception("none");
                if (PostToAdd1.Text == "")
                    throw new Exception("You should write smth!");
                //message in OUT
                var privTopicOUT = from q in forumEntities.Topic
                                   where q.TopicName == LoginedUser.UserLoweredName + "privatetopicout"
                                   select q;
                PostToAdd1.DateAdded = DateTime.Now;
                PostToAdd1.ForumID = privTopicOUT.First().ForumID;
                PostToAdd1.TopicID = privTopicOUT.First().TopicID;
                PostToAdd1.UserId = curUser.UserId;
                DB.IncreasePostCount(PostToAdd1.TopicID);
                forumEntities.Post.AddObject(PostToAdd1);
                forumEntities.SaveChanges();
                //message in IN
                var privTopicIN = from q in forumEntities.Topic
                                   where q.TopicName == curUser.UserLoweredName + "privatetopicin"
                                   select q;
                PostToAdd2.Text = PostToAdd1.Text;
                PostToAdd2.ForumID = PostToAdd1.ForumID;
                PostToAdd2.DateAdded = PostToAdd1.DateAdded;
                PostToAdd2.UserId = LoginedUser.UserId;
                PostToAdd2.TopicID = privTopicIN.First().TopicID;
                DB.IncreasePostCount(PostToAdd2.TopicID);
                forumEntities.Post.AddObject(PostToAdd2);
                forumEntities.SaveChanges();
            }
            catch (Exception excpt)
            {
                if (excpt.Message != "none")
                    MessageBox.Show(excpt.Message);
            }
        }

        private void RoleAdminCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (RoleBannedCheckBox.IsChecked == true)
            {
                RoleAdminCheckBox.IsChecked = false;
                MessageBox.Show("Banned Users can't become admin!");
                return;
            }
            if (RoleAdminCheckBox.IsChecked == false)
            {
                var userinrole = from q in forumEntities.UsersInRoles
                                 where q.UserId == curUser.UserId && q.RoleId == RoleAdminID
                                 select q;
                UsersInRoles UserInRoleDel = userinrole.First();
                forumEntities.DeleteObject(UserInRoleDel);
            }
            else
            {
                //moder
                if (RoleModerCheckBox.IsChecked == false)
                {
                    RoleModerCheckBox.IsChecked = true;
                    UsersInRoles UserInRoleAddModer = new UsersInRoles();
                    UserInRoleAddModer = new UsersInRoles();
                    UserInRoleAddModer.RoleId = RoleModerID;
                    UserInRoleAddModer.UserId = curUser.UserId;
                    forumEntities.UsersInRoles.AddObject(UserInRoleAddModer);
                }
                //admin
                UsersInRoles UserInRoleAdd = new UsersInRoles();
                UserInRoleAdd = new UsersInRoles();
                UserInRoleAdd.RoleId = RoleAdminID;
                UserInRoleAdd.UserId = curUser.UserId;
                forumEntities.UsersInRoles.AddObject(UserInRoleAdd);
            }
            forumEntities.SaveChanges();
        }

        private void RoleModerCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (RoleBannedCheckBox.IsChecked == true)
            {
                RoleModerCheckBox.IsChecked = false;
                MessageBox.Show("Banned user can't become moderator!");
                return;
            }
            if (RoleModerCheckBox.IsChecked == false)
            {
                if (RoleAdminCheckBox.IsChecked == true)
                {
                    RoleModerCheckBox.IsChecked = true;
                    MessageBox.Show("Admin is always moderator!");
                    return;
                }

                var userinrole = from q in forumEntities.UsersInRoles
                                 where q.UserId == curUser.UserId && q.RoleId == RoleModerID
                                 select q;
                UsersInRoles UserInRoleDel = userinrole.First();
                forumEntities.DeleteObject(UserInRoleDel);
            }
            else
            {
                UsersInRoles UserInRoleAdd = new UsersInRoles();
                UserInRoleAdd = new UsersInRoles();
                UserInRoleAdd.RoleId = RoleModerID;
                UserInRoleAdd.UserId = curUser.UserId;
                forumEntities.UsersInRoles.AddObject(UserInRoleAdd);
            }
            forumEntities.SaveChanges();
        }

        private void RoleBanCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (RoleAdminCheckBox.IsChecked == true)
            {
                RoleBannedCheckBox.IsChecked = false;
                MessageBox.Show("Admin can't be banned!");
                return;
            }
            else if (RoleModerCheckBox.IsChecked == true)
            {
                RoleBannedCheckBox.IsChecked = false;
                MessageBox.Show("Moderator can't be banned!");
                return;
            }
            if (RoleBannedCheckBox.IsChecked == false)
            {
                var userinrole = from q in forumEntities.UsersInRoles
                                 where q.UserId == curUser.UserId && q.RoleId == RoleBannedID
                                 select q;
                UsersInRoles UserInRoleDel = userinrole.First();
                forumEntities.DeleteObject(UserInRoleDel);
                curUser.IsLocked = false;
                curUser.LockedDateOut = DateTime.Now;
            }
            else
            {
                //BAN info
                BanParameters BanPar = new BanParameters(curUser);
                BanPar.ShowDialog();
                if (curUser.LockedDateOut.Date <= DateTime.Now.Date)
                {
                    RoleBannedCheckBox.IsChecked = false;
                    return;
                }
                //ban
                {
                    UsersInRoles UserInRoleAdd = new UsersInRoles();
                    UserInRoleAdd = new UsersInRoles();
                    UserInRoleAdd.RoleId = RoleBannedID;
                    UserInRoleAdd.UserId = curUser.UserId;
                    forumEntities.UsersInRoles.AddObject(UserInRoleAdd);
                    curUser.IsLocked = true;
                    //
                    Post postBan = new Post();
                    postBan.DateAdded = DateTime.Now;
                    postBan.ForumID = (from q in forumEntities.Forum
                                           where q.ForumName == "usersprivateforum"
                                           select q).First().ForumID;
                    postBan.TopicID = (from q in forumEntities.Topic
                                       where q.TopicName == curUser.UserLoweredName + "privatetopicin"
                                       select q).First().TopicID;
                    postBan.Text = "You are banned!\nDate: " + curUser.LockedDateOut.ToString() + "\nReason: " + curUser.LockedReason;
                    postBan.UserId = LoginedUser.UserId;
                    forumEntities.AddToPost(postBan);
                }
            }
            forumEntities.SaveChanges();
            FillProfile();
        }

        private void ShowUsersPostButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPosts shPost = new ShowPosts(curUser, forumEntities);
            shPost.ShowDialog();
        }
    }
}
