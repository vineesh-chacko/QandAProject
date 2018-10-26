using QandADomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QandAProjectRepository
{
    public class UserRepository : IUserRepository
    {
        QandADatabaseDBContext db;

        public UserRepository()
        {
            db = new QandADatabaseDBContext();
        }
        public int GetLatestUserId()
        {
            var userId = db.Users.Select(x => x.UserId).Max();
            return userId;
        }

        public User GetUserByEmails(string email)
        {
            return db.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public User GetUserByEmailsAndPassword(string email, string password)
        {
            var user = db.Users.Where(u => u.Email == email && u.PasswordHash == password).FirstOrDefault();
            return user;
        }

        public User GetUserByUserId(int userId)
        {
            return db.Users.Where(u => u.UserId == userId).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return db.Users.Where(u => u.IsAdmin == false).OrderBy(u => u.Name).ToList();
        }

        public void InserUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();

        }

        public void UpdateUserDetails(User user)
        {
            var u = db.Users.Where(x => x.UserId == user.UserId).FirstOrDefault();
            if (u != null)
            {
                u.Name = user.Name;
                u.Mobile = user.Mobile;
            }
            db.SaveChanges();
        }


        public void UpdateUserPassword(User user)
        {
            var u = db.Users.Where(x => x.UserId == user.UserId).FirstOrDefault();
            if (u != null)
            {
                u.PasswordHash = user.PasswordHash;
            }
            db.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
            if (user != null)
                db.Users.Remove(user);
            db.SaveChanges();
        }
    }

    public interface IUserRepository
    {
        void InserUser(User user);
        void UpdateUserDetails(User user);
        void UpdateUserPassword(User user);
        List<User> GetUsers();
        User GetUserByEmailsAndPassword(string email, string password);
        User GetUserByEmails(string email);
        User GetUserByUserId(int userId);
        int GetLatestUserId();
        void DeleteUser(int userId);



    }

    
}
