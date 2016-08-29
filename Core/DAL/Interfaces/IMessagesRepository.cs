using System.Linq;
using System.Collections.Generic;
using  Core.POCO;

namespace  Core.DAL.Interfaces
{
    public interface IMessagesRepository
    {
        List<Message> ExcludeInvitationMessagesSentedByCurrentUser(List<Message> messages, string currentUserId);
        void CheckTheReceiverOpenedDialogToRead(string UserID, int dialogId);
        string CreateNewDialog(Message message, string userId);
        string GetDialogByID(int dialogID, string currentUserId);
        int GetLastDialogID();
        string GetLastParagraph(string text);
        IQueryable<Message> GetUnviewedMessagesByReceiversUserID(string UserID);
        void InsertMessage(Message message);
         
        int GetDialogIdOfUsers(string fromUserId, string toUserId);
        List<Message> GetLastMessages(string userID);
    }
}