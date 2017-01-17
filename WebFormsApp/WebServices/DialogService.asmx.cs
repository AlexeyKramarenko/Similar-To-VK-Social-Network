
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Ninject;
using WebFormsApp.ViewModel;
using System.Web.Script.Services;
using System.Web.Services;
using Core.BLL.Interfaces;
using Core.POCO;
using System;
using System.Web;
using WebFormsApp.Services;
using System.Collections.Generic;

namespace WebFormsApp.WebServices
{
    /// <summary>
    /// Summary description for DialogService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]

    public class DialogService : Ninject.Web.WebServiceBase
    {
        [Inject]
        public IRelationshipsService relationshipsService { get; set; }
        [Inject]
        public IMessagesService messagesService { get; set; }
        [Inject]
        public IPhotoService photoService { get; set; }
        [Inject]
        public IUserService userService { get; set; }
        [Inject]
        public ISessionService sessionService { get; set; }
        [Inject]
        public IProfileService profileService { get; set; }


        string CurrentUserId
        {
            get
            {
                return sessionService.CurrentUserId;
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public MessagesFormViewModel GetUsersInfoForDialogForm(UsersInfo info)
        {
            var vm = new MessagesFormViewModel();

            vm.CurrentUserAvatar = photoService.GetThumbAvatarImg(CurrentUserId);
            vm.InterlocutorUserAvatar = photoService.GetThumbAvatarImg(info.receiversId);

            vm.CurrentUsername = userService.GetUserNameByUserID(CurrentUserId);
            vm.InterlocutorUsername = userService.GetUserNameByUserID(info.receiversId);

            return vm;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public List<MessageViewModel> GetDialogByID(int id)
        {
            List<Message> dialog = messagesService.GetMessagesByDialogId(id,CurrentUserId);

            var list = new List<MessageViewModel>();

            foreach (var item in dialog)
            {
                list.Add(new MessageViewModel
                {
                    InterlocutorAvatar = photoService.GetLargeAvatarImg(item.SendersUserID),
                    InterlocutorUserName = userService.GetUserNameByUserID(item.SendersUserID),
                    MessageText = item.Body,
                    CreateDate = item.RequestDate.ToString()
                });
            }

            return list;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CreateNewDialog(Message message)
        {
            string response = messagesService.CreateNewDialog(message, CurrentUserId);
            string json = JsonConvert.SerializeObject(response);
            return json;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AddRelationshipDefinition(string senderUserId, int relationshipId)
        {
            relationshipsService.SaveRelationshipDefinition(senderUserId, CurrentUserId, relationshipId);

        }

    }
    public class UsersInfo
    {
        public string receiversId;
    }
}
