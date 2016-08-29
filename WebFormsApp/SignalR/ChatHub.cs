using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.POCO;
using Core.BLL.Interfaces;

namespace WebFormsApp
{
    public class MessageDetail
    {
        public string UserName { get; set; }
        public string Message { get; set; }
    }
    public class UserDetail
    {
        public string UserID { get; set; }
        public string FriendsID { get; set; }
        public string ConnectionId { get; set; }
        public string ThumbnailUrl { get; set; }
        public string PageUrl { get; set; }
    }
    public static class OnlineVariables
    {
        public static List<UserDetail> OnlineUsers = new List<UserDetail>();
        public static List<MessageDetail> CurrentMessages = new List<MessageDetail>();
        public static ApplicationUser LoggedInUser;
    }
    public class ChatHub : Hub, IChatHub
    {
        IMessagesService messagesService;
        IPhotoService photoService;
        IRelationshipsService relationshipsService;

        public ChatHub(IMessagesService _messagesService, IPhotoService _photoService, IRelationshipsService _relationshipsService)
        {
            messagesService = _messagesService;
            photoService = _photoService;
            relationshipsService = _relationshipsService;
        }


        public void CheckTheReceiverOpenedDialogToRead(string userID, int dialogId)
        {
            messagesService.CheckTheReceiverOpenedDialogToRead(userID, dialogId);
        }

        public void SendMessage(string FromUserId, string ToUserId, int dialogId, string message)
        {
            if (dialogId == 0)
                dialogId = messagesService.GetDialogIdOfUsers(FromUserId, ToUserId);

            string date = string.Format("{0}.{1}.{2}  {3}:{4}", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year, DateTime.Now.Hour, DateTime.Now.Minute);

            string imgPath = photoService.GetThumbAvatarImg(FromUserId);

            string body = "<tr>" +
                              "<td class='log_author'><img src='" + imgPath + "' /></td>" +
                              "<td class='log_body'><p>" + message + "</p></td>" +
                              "<td class='log_date'>" + date + "</td>" +
                          "</tr>";

            messagesService.InsertMessage(new Message
            {
                Body = body,
                DialogID = dialogId,
                SendersUserID = FromUserId,
                ReceiversUserID = ToUserId,
                RequestDate = date
            });

            Clients.Caller.getMessage(FromUserId, body, date);

            var toUser = OnlineVariables.OnlineUsers.FirstOrDefault(a => a.UserID == ToUserId);

            if (toUser != null)
                Clients.Client(toUser.ConnectionId).getMessage(FromUserId, body, date);
        }

        public void Connect(string userId)
        {
            if (OnlineVariables.OnlineUsers.FirstOrDefault(a => a.UserID == userId) == null)
            {
                string ThumbnailUrl = photoService.GetThumbAvatarImg(userId);

                var ConnectedUser = new UserDetail
                {
                    UserID = userId,
                    ConnectionId = Context.ConnectionId,
                    PageUrl = "Main.aspx?UserID=" + userId,
                    ThumbnailUrl = ThumbnailUrl,
                };

                OnlineVariables.OnlineUsers.Add(ConnectedUser);
            }
        }

        public List<UserDetail> ConnectToGetFriends(string userId)
        {
            var friendsOnline = new List<UserDetail>();

            var allFriendsIDs = relationshipsService.GetFriendsIDsOfUser(userId);

            for (int i = 0; i < allFriendsIDs.Count; i++)
            {
                var friend = OnlineVariables.OnlineUsers.FirstOrDefault(a => a.UserID == allFriendsIDs[i]);
                if (friend != null)
                    friendsOnline.Add(friend);
            }

            string ThumbnailUrl = photoService.GetThumbAvatarImg(userId);

            var ConnectedUser = new UserDetail
            {
                UserID = userId,
                ConnectionId = Context.ConnectionId,
                PageUrl = "Main.aspx?UserID=" + userId,
                ThumbnailUrl = ThumbnailUrl,
            };

            if (OnlineVariables.OnlineUsers.FirstOrDefault(a => a.UserID == userId) == null)
            {
                OnlineVariables.OnlineUsers.Add(ConnectedUser);
            }

            //send friends list to user that called 'ConnectToGetFriends'             
            Clients.Caller.onConnected(friendsOnline);

            //every friend of user that called 'ConnectToGetFriends' will be notified that the user is online now            
            foreach (var friend in friendsOnline)
            {
                Clients.Client(friend.ConnectionId).onNewUserConnected(ConnectedUser);
            }
            return friendsOnline;
        }

        public List<UserDetail> ConnectToGetOnlineInterlocutors(string userId, string[] allInterlocutersIDs)
        {
            var interlocutorsOnline = new List<UserDetail>();

            for (int i = 0; i < allInterlocutersIDs.Length; i++)
            {
                var interlocutor = OnlineVariables.OnlineUsers.FirstOrDefault(a => a.UserID == allInterlocutersIDs[i]);

                if (interlocutor != null)
                    interlocutorsOnline.Add(interlocutor);
            }

            var ConnectedUser = new UserDetail
            {
                UserID = userId,
                ConnectionId = Context.ConnectionId
            };

            if (OnlineVariables.OnlineUsers.FirstOrDefault(a => a.UserID == userId) == null)
            {
                OnlineVariables.OnlineUsers.Add(ConnectedUser);
            }

            //send list of friends to user that called ConnectToGetOnlineInterlocutors            
            Clients.Caller.onConnected(interlocutorsOnline);

            //every friend of user that called ConnectToGetOnlineInterlocutors  will be notified that he is online now            
            foreach (var interlocutor in interlocutorsOnline)
            {
                Clients.Client(interlocutor.ConnectionId).onNewUserConnected(ConnectedUser);
            }
            return interlocutorsOnline;
        }

        public List<UserDetail> GetOnlineFriends(string userId)
        {
            var friendsOnline = new List<UserDetail>();

            var allFriendsIDs = relationshipsService.GetFriendsIDsOfUser(userId);

            for (int i = 0; i < allFriendsIDs.Count; i++)
            {
                var friend = OnlineVariables.OnlineUsers.FirstOrDefault(a => a.UserID == allFriendsIDs[i]);
                if (friend != null)
                    friendsOnline.Add(friend);
            }
            return friendsOnline;
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            var users = OnlineVariables.OnlineUsers;
            var connectionId = Context.ConnectionId;

            var item = OnlineVariables.OnlineUsers.FirstOrDefault(x => x.ConnectionId == connectionId);

            if (item != null)
            {
                OnlineVariables.OnlineUsers.Remove(item);

                Clients.All.onUserDisconnected(connectionId);
            }

            return base.OnDisconnected(stopCalled);

        }
    }

}
