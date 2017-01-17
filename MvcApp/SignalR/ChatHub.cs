using Microsoft.AspNet.SignalR;
using Core.BLL.Interfaces;
using Core.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcApp.ViewModel;

namespace MvcApp
{
    public class MessageDetail
    {
        public string UserName { get; set; }
        public string Message { get; set; }
    }
    public class UserInfo
    { 
        public string UserID { get; set; }
        public string FriendsID { get; set; }
        public string ConnectionId { get; set; }
        public string ThumbnailUrl { get; set; }
        public string PageUrl { get; set; }
    }
    public static class OnlineVariables
    {
        public static List<UserInfo> OnlineUsers = new List<UserInfo>();
        public static List<MessageDetail> CurrentMessages = new List<MessageDetail>();
        public static ApplicationUser LoggedInUser;
    }
    public class ChatHub : Hub, IChatHub
    {
        IMessagesService messagesService;
        IPhotoService photoService;
        IRelationshipsService relationshipsService;
        IUserService userService;

        public ChatHub(IMessagesService messagesService, IPhotoService photoService, IRelationshipsService relationshipsService, IUserService userService)
        {
            this.messagesService = messagesService;
            this.photoService = photoService;
            this.relationshipsService = relationshipsService;
            this.userService = userService;
        }


        public void CheckTheReceiverOpenedDialogToRead(string userID, int dialogId)
        {
            messagesService.CheckTheReceiverOpenedDialogToRead(userID, dialogId);
        }

        public void SendMessage(string FromUserId, string ToUserId, int dialogId, string message)
        {
            if (dialogId == 0)
                dialogId = messagesService.GetDialogIdOfUsers(FromUserId, ToUserId);


            string imgPath = photoService.GetThumbAvatarImg(FromUserId);
            string userName = userService.GetUserNameByUserID(FromUserId);

            messagesService.InsertMessage(new Message
            {
                Body = message,
                DialogID = dialogId,
                SendersUserID = FromUserId,
                ReceiversUserID = ToUserId,
                RequestDate = DateTime.Now
            });

            string date = string.Format("{0}.{1}.{2}  {3}:{4}", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year, DateTime.Now.Hour, DateTime.Now.Minute);

            var _message = new MessagesViewModel
            {
                CreateDate = date,
                InterlocutorAvatar = imgPath,
                InterlocutorUserName = userName,
                MessageText = message
            };

            Clients.Caller.getMessage(_message);

            var toUser = OnlineVariables.OnlineUsers.FirstOrDefault(a => a.UserID == ToUserId.Trim());

            if (toUser != null)
                Clients.Client(toUser.ConnectionId).getMessage(_message);
        }

        public List<UserInfo> ConnectToGetFriends(string userId)
        {
            string thumbnailUrl =  photoService.GetThumbAvatarImg(userId);
            var ConnectedUser = new UserInfo
            {
                UserID = userId,
                FriendsID = null,
                ConnectionId = Context.ConnectionId,
                ThumbnailUrl = thumbnailUrl,
                PageUrl = "/main/mainpage/" + userId
            };
            
            var user = OnlineVariables.OnlineUsers.FirstOrDefault(a => a.UserID == userId);

            if (user == null)
            {
                OnlineVariables.OnlineUsers.Add(ConnectedUser);
            }
         
            var friendsOnline = new List<UserInfo>();

            var allFriendsIDs = relationshipsService.GetFriendsIDsOfUser(userId);

            for (int i = 0; i < allFriendsIDs.Count; i++)
            {
                var friend = OnlineVariables.OnlineUsers.FirstOrDefault(a => a.UserID == allFriendsIDs[i]);

                if (friend != null)
                    friendsOnline.Add(friend);
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

        public void ConnectToGetOnlineInterlocutors(string userId, string[] allInterlocutersIDs)
        {

            var interlocutorsOnline = new List<UserInfo>();

            for (int i = 0; i < allInterlocutersIDs.Length; i++)
            {
                if (allInterlocutersIDs[i] != null)
                {
                    var _userId = allInterlocutersIDs[i].Trim();
                    var interlocutor = OnlineVariables.OnlineUsers.FirstOrDefault(a => a.UserID == _userId);

                    if (interlocutor != null)
                        interlocutorsOnline.Add(interlocutor);
                }
            }
            string thumbnailUrl = photoService.GetThumbAvatarImg(userId);

            var ConnectedUser = new UserInfo
            {
                UserID = userId,
                FriendsID = null,
                ConnectionId = Context.ConnectionId,
                ThumbnailUrl = thumbnailUrl,
                PageUrl = "/main/mainpage/" + userId
            };

            if (OnlineVariables.OnlineUsers.FirstOrDefault(a => a.UserID == userId) == null)
                OnlineVariables.OnlineUsers.Add(ConnectedUser);


            //send list of friends to user that called ConnectToGetOnlineInterlocutors            
            Clients.Caller.onConnected(interlocutorsOnline);

            //every friend of user that called ConnectToGetOnlineInterlocutors  will be notified that he is online now            
            foreach (var interlocutor in interlocutorsOnline)
            {
                Clients.Client(interlocutor.ConnectionId).onNewUserConnected(ConnectedUser);
            }           
        }

        public List<UserInfo> GetOnlineFriends(string userId)
        {
            var friendsOnline = new List<UserInfo>();

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

                Clients.All.onUserDisconnected(item.ConnectionId);

            }

            return base.OnDisconnected(stopCalled);
            
        }
    }

}
