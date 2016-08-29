using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcApp
{
    public interface IChatHub
    {
        void CheckTheReceiverOpenedDialogToRead(string userID, int dialogId);
        void Connect(string userId);
        List<UserDetail> ConnectToGetFriends(string userId);
        List<UserDetail> ConnectToGetOnlineInterlocutors(string userId, string[] allInterlocutersIDs);
        List<UserDetail> GetOnlineFriends(string userId);
        Task OnDisconnected(bool stopCalled);
        void SendMessage(string FromUserId, string ToUserId, int dialogId, string message);
    }
}