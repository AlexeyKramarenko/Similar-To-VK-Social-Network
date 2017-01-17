using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcApp
{
    public interface IChatHub
    {
        void CheckTheReceiverOpenedDialogToRead(string userID, int dialogId);        
        List<UserInfo> ConnectToGetFriends(string userId);
        void ConnectToGetOnlineInterlocutors(string userId, string[] allInterlocutersIDs);
        List<UserInfo> GetOnlineFriends(string userId);
        Task OnDisconnected(bool stopCalled);
        void SendMessage(string FromUserId, string ToUserId, int dialogId, string message);
    }
}