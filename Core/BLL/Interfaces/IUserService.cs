
using  Core.POCO;
using  Core.BLL.DTO;
using System;

namespace  Core.BLL.Interfaces
{
    public interface IUserService
    {
        void LogoutUser();
        OperationResult LoginUser(LoginDTO loginObj, Func<string> host);
        OperationResult CreateUser(Profile profile, ApplicationUser user, Func<string> host);
        System.Threading.Tasks.Task UpdatePassword(string password, string currentUserId);
        bool EmailAlreadyExists(string Email);
        bool UsernameAlreadyExists(string Username);
        ApplicationUser GetUserByName(string username);
        void UpdateUser(ApplicationUser user);
        void UpdatePhoneNumber(string currentUserId, string phoneNumber);
        string GetPhoneNumber(string currentUserId);
        ApplicationUser GetAccount(string currentUserId);
        ApplicationUser GetUserByEmail(string email);
        string GetPasswordByUserId(string currentUserId);
        string GetUserNameByUserID(string currentUserId);
    }
}