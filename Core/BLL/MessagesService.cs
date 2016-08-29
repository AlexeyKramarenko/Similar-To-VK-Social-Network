using Core.BLL.DTO;
using  Core.BLL.Interfaces;
using  Core.DAL.Interfaces;
using  Core.POCO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.BLL
{
    public class MessagesService : LogicLayer, IMessagesService
    {
        IUnitOfWork Database;
        public MessagesService(IUnitOfWork db)
        {
            Database = db;
        }

        public int GetDialogIdOfUsers(string fromUserId, string toUserId)
        {
            string key = "DialogIdOfUsers_" + fromUserId + "_" + toUserId;

            int id;

            if (Cache[key] != null)
                id = (int)Cache[key];
            else
            {
                id = Database.Messages.GetDialogIdOfUsers(fromUserId, toUserId);
                Cache[key] = id;
            }

            return id;
        }

        public int GetLastDialogID()
        {
            int id = Database.Messages.GetLastDialogID();
            return id;
        }

        public string CreateNewDialog(Message message, string userId)
        {
            string resultMessage = Database.Messages.CreateNewDialog(message, userId);
            return resultMessage;
        }

        public void CheckTheReceiverOpenedDialogToRead(string UserID, int dialogId)
        {
            Database.Messages.CheckTheReceiverOpenedDialogToRead(UserID, dialogId);
            Database.Save();
        }

        public IQueryable<Message> GetUnviewedMessagesByReceiversUserID(string UserID)
        {
            var messages = Database.Messages.GetUnviewedMessagesByReceiversUserID(UserID);
            return messages;
        }


        public string GetDialogByID(int dialogID, string currentUserId)
        {
            string dialog = Database.Messages.GetDialogByID(dialogID, currentUserId);
            return dialog;
        }

        public List<Message> GetLastMessages(string userID)
        {
           return Database.Messages.GetLastMessages(userID);
        }

        public string GetLastParagraph(string body)
        {
            return Database.Messages.GetLastParagraph(body);
        }

        public void InsertMessage(Message message)
        {
            Database.Messages.InsertMessage(message);
            Database.Save();
        }

        public List<MessageDTO> GetDialogsList(string currentUserID, string userName)
        {
            string defaultUrl = null;
            //last message is the last added message in dialog
            List<Message> lastMessages = null;
            List<int> dialogsIds = null;
            List<string> interlocutorsIds = null;
            List<string> avatars = null;
            List<MessageDTO> dialogs = null;
            string currentUserAvatar = null;

            defaultUrl = ConfigurationManager.AppSettings["defaultUrl"];
            currentUserAvatar = Database.Photos.GetThumbAvatarImg(currentUserID);
            lastMessages = Database.Messages.GetLastMessages(currentUserID);
            dialogsIds = new List<int>();
            interlocutorsIds = new List<string>();
            avatars = new List<string>();
            dialogs = new List<MessageDTO>();

            //Get all dialogs of current user
            if (lastMessages != null)
                foreach (var m in lastMessages)
                {
                    dialogsIds.Add(m.DialogID);

                    if (!interlocutorsIds.Contains(m.ReceiversUserID) && currentUserID != m.ReceiversUserID)
                        interlocutorsIds.Add(m.ReceiversUserID);

                    if (!interlocutorsIds.Contains(m.SendersUserID) && currentUserID != m.SendersUserID)
                        interlocutorsIds.Add(m.SendersUserID);
                }

            //Get avatars of current user interlocuters
            if (interlocutorsIds != null)
                foreach (var id in interlocutorsIds)
                    if (id != currentUserID)
                    {
                        string avatar = Database.Photos.GetThumbAvatarImg(id);
                        avatars.Add(avatar);
                    }


            //Translate all gathering data to viewmodel
            for (int i = 0; i < lastMessages.Count; i++)
            {
                string interlocutorUserID = null;

                if (lastMessages[i].SendersUserID != currentUserID)
                    interlocutorUserID = lastMessages[i].SendersUserID;

                else
                    interlocutorUserID = lastMessages[i].ReceiversUserID;


                dialogs.Add(new MessageDTO
                {
                    CreateDate = DateTime.Now,
                    CurrentUserAvatar = currentUserAvatar,
                    CurrentUserName = userName,
                    InterlocutorAvatar = (i < avatars.Count) ? avatars[i] : defaultUrl,
                    DialogID = dialogsIds[i],
                    InterlocutorUserName =Database.UserManager.GetUsernameByID(interlocutorUserID),
                    InterlocutorUserID = interlocutorUserID,
                    MessageText = Database.Messages.GetLastParagraph(lastMessages[i].Body)
                });
            }


            return dialogs;
        }
    }
}
