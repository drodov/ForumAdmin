using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ForumController
{
    public class CDatabase
    {
        ForumV2Entities forumEntities;
        public CDatabase(ForumV2Entities ForumEntities)
        {
            forumEntities = ForumEntities;
        }
        public bool TryLogin(string login, string pass)
        {
            bool flag = false;
            Guid IDTryLogin = new Guid();
            string PassTryLogin = "";
            var UserTryLogin = from q in forumEntities.Users
                               where q.UserLoweredName == login.ToLower()
                               select q;
            if (UserTryLogin.Count() != 0)  // login found
                flag = true;
            if (flag == true)
            {
                PassTryLogin = UserTryLogin.First().Password;
                IDTryLogin = UserTryLogin.First().UserId;
                if (pass != PassTryLogin)                   // check pass
                    flag = false;                           // pass are not valid
            }
            if (flag == true)                               // check Role
            {                                               // get RoleID of Admin
                var RoleAdmin = from q in forumEntities.Roles
                                where q.RoleName == "Admin"
                                select q.RoleId;
                Guid roleAdmin = RoleAdmin.First();
                // get roles of User trying login
                var UserRoleTryLogin = from q in forumEntities.UsersInRoles
                                       where q.UserId == IDTryLogin
                                       select q.RoleId;
                foreach (var tRole in UserRoleTryLogin)
                {
                    if (tRole == roleAdmin)
                    {
                        flag = true;
                        break;
                    }
                    else
                        flag = false;
                }

            }
            return flag;
        }

        /*public Guid GetUserID(string UserName)
        {
            Guid UserID = new Guid();
            var UserTryLogin = from q in forumEntities.Users
                               where q.UserLoweredName == UserName.ToLower()
                               select q;
            foreach (var tUser in UserTryLogin)
            {
                UserID = tUser.UserId;
            }
            return UserID;
        }
         */

        public Users GetUserInfo(string UserName)
        {
            var UserTryLogin = from q in forumEntities.Users
                               where q.UserLoweredName == UserName.ToLower()
                               select q;
            return UserTryLogin.First();
        }

        public void IncreaseTopicCount(int ForumID)
        {
            var q = from f in forumEntities.Forum
                    where f.ForumID == ForumID
                    select f;
            foreach (var forum in q)     //inc TopicsCount 
            {
                forum.TopicsCount++;
            }
        }

        public void DecreaseTopicCount(int ForumID)
        {
            var q = from f in forumEntities.Forum
                    where f.ForumID == ForumID
                    select f;
            foreach (var forum in q)     //dec TopicsCount 
            {
                forum.TopicsCount--;
            }
        }

        public void IncreasePostCount(int TopicID)
        {
            var q = from f in forumEntities.Topic
                    where f.TopicID == TopicID
                    select f;
            foreach (var topic in q)     //inc PostsCount 
            {
                topic.PostsCount++;
            }
        }

        public void DecreasePostCount(int TopicID)
        {
            var q = from f in forumEntities.Topic
                    where f.TopicID == TopicID
                    select f;
            foreach (var topic in q)     //dec PostsCount 
            {
                topic.PostsCount--;
            }
        }

        public void DelTopicsBelongToForum(int ForumID)
        {
            var q = from t in forumEntities.Topic
                    where t.ForumID == ForumID
                    select t;
            foreach (var topic in q)     //del Topics
            {
                DelPostsBelongToTopic(topic.TopicID, false);
                //del connections in Users-Topic-LastActivity
                var lastAct = from qq in forumEntities.UserTopicLastActiv
                         where qq.TopicId == topic.TopicID
                         select qq;
                if (lastAct.Count() != 0)
                {
                    foreach (UserTopicLastActiv UserTopPasAct in lastAct)
                    {
                        forumEntities.DeleteObject(UserTopPasAct);
                    }
                }
                forumEntities.DeleteObject(topic as Topic);
            }
            forumEntities.SaveChanges();
        }

        public void DelPostsBelongToTopic(int TopicID, bool save)
        {
            var q = from p in forumEntities.Post
                    where p.TopicID == TopicID
                    select p;
            foreach (var post in q)     //del Posts 
            {
                forumEntities.DeleteObject(post as Post);
            }
            if(save == true)
                forumEntities.SaveChanges();
        }
    }
}
