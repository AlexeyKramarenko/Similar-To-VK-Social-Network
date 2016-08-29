using  Core.POCO;
using System.Collections.Generic;

namespace  Core.DAL.Interfaces
{
    public interface IUserRepository
    {
        ApplicationUser GetAccount(string userId);
        List<string> GetAllUserIDs();
        string GetPasswordByUserId(string userId);
        string GetPhoneNumber(string userId);
        ApplicationUser GetUserByEmail(string Email);
        ApplicationUser GetUserByName(string Username);
        ApplicationUser GetUserByPhoneNumber(string input);
        string GetUsernameByID(string UserID);
        string GetUserNameByUserID(string userID);
        void UpdateUser(ApplicationUser user);
        void UpdatePhoneNumber(string userId, string phoneNumber);
    }
}