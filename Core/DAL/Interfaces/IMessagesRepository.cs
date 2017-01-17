using System.Linq;
using System.Collections.Generic;
using Core.POCO;

namespace Core.DAL.Interfaces
{
    public interface IMessagesRepository
    { 
        void CheckTheReceiverOpenedDialogToRead(string UserID, int dialogId);
        string CreateNewDialog(Message message, string userId);
        List<string> GetDialogByID(int dialogID, string currentUserId);
        int GetLastDialogID();        
        IQueryable<Message> GetUnviewedMessagesByReceiversUserID(string UserID);
        void InsertMessage(Message message);
        int GetDialogIdOfUsers(string fromUserId, string toUserId);
        List<Message> GetLastMessages(string userID);
        List<Message> GetMessagesByDialogId(int dialogId, string currentUserId);
    }
}