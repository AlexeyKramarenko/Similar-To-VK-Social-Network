using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using  Core.DAL;
using  Core.DAL.Interfaces;
using  Core.POCO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace  Core.DAL
{
    public class ApplicationUserManager : UserManager<ApplicationUser>, IUserRepository
    {
        DBContext db;
       
        public ApplicationUserManager(IUserStore<ApplicationUser> store, DBContext db)
                : base(store)
        {
            this.db = db;
        }
         

        public ApplicationUser GetAccount(string userId)
        {
            ApplicationUser account = db.Users.FirstOrDefault(d => d.Id == userId);
            
            return account;
        }

        public List<string> GetAllUserIDs()
        {
            return db.Users.Select(a => a.Id).ToList();
        }

        public string GetPhoneNumber(string userId)
        {
            var user = db.Users.FirstOrDefault(a => a.Id == userId);
            if (user != null)
                return user.PhoneNumber;

            return null;
        }
        public string GetUserNameByUserID(string userID)
        {
            if (userID != null)
            {
                string userName = db.Users.Where(a => a.Id == userID).Select(a => a.UserName).FirstOrDefault();
                return userName;
            }
            return null;
        }
        public ApplicationUser GetUserByName(string Username)
        {
            return db.Users.FirstOrDefault(a => a.UserName == Username);
        }

        public ApplicationUser GetUserByEmail(string Email)
        {
            return db.Users.FirstOrDefault(a => a.Email == Email);
        }

        public string GetUsernameByID(string UserID)
        {
            var user = db.Users.FirstOrDefault(a => a.Id == UserID);

            if (user != null)
                return user.UserName;

            return null;
        }
        public ApplicationUser GetUserByPhoneNumber(string input)
        {
            return db.Users.FirstOrDefault(a => a.PhoneNumber == input);
        }
        public void UpdateUser(ApplicationUser user)
        {
            user.CreateDate = DateTime.Now;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
        }

        
        public string GetPasswordByUserId(string userId)
        {
            var user = db.Users.FirstOrDefault(a => a.Id == userId);

            if (user != null)
                return user.Password;

            else return null;
        }

        public void UpdatePhoneNumber(string userId, string phoneNumber)
        {
            var user = db.Users.FirstOrDefault(a => a.Id == userId);

            if (user != null)           
                user.PhoneNumber = phoneNumber;
        }

      
    }
}
