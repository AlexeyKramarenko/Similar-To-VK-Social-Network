using System.Collections.Generic;
using System.Linq;
using Core.POCO;
using Core.BLL.DTO;

namespace Core.BLL.Interfaces
{
    public interface IMessagesService
    {
        void CheckTheReceiverOpenedDialogToRead(string UserID, int dialogId);
        string CreateNewDialog(Message message, string userId);
        List<string> GetDialogByID(int dialogID, string currentUserId);
        int GetDialogIdOfUsers(string fromUserId, string toUserId);
        int GetLastDialogID();
        List<DialogDTO> GetLastDialog(int dialogID, string currentUserId);
        void InsertMessage(Message message);
        IQueryable<Message> GetUnviewedMessagesByReceiversUserID(string UserID);
        List<Message> GetLastMessages(string userID);
        //string GetLastParagraph(string body);
        List<MessageDTO> GetDialogsList(string currentUserID, string userName);
        List<Message> GetMessagesByDialogId(int dialogId, string currentUserId);
    }
}