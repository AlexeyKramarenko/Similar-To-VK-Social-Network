
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Ninject;
using WebFormsApp.ViewModel;
using System.Web.Script.Services;
using System.Web.Services;
using Core.BLL.Interfaces;
using Core.POCO;

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


        string currentUserId;
        public DialogService()
        {
            currentUserId = User.Identity.GetUserId();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public MessagesFormViewModel GetUsersInfoForDialogForm(UsersInfo info)
        {
            var vm = new MessagesFormViewModel();

            vm.CurrentUserAvatar = photoService.GetThumbAvatarImg(currentUserId);
            vm.InterlocutorUserAvatar = photoService.GetThumbAvatarImg(info.receiversId);

            vm.CurrentUsername = userService.GetUserNameByUserID(currentUserId);
            vm.InterlocutorUsername = userService.GetUserNameByUserID(info.receiversId);

            return vm;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string GetDialogByID(int id)
        {

            string dialog = messagesService.GetDialogByID(id, currentUserId);

            string json = JsonConvert.SerializeObject(dialog);
            return json;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CreateNewDialog(Message message)
        {
            string response = messagesService.CreateNewDialog(message, currentUserId);

            string json = JsonConvert.SerializeObject(response);
            return json;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AddRelationshipDefinition(string senderUserId, int relationshipId)
        {
            string receiverUserId = User.Identity.GetUserId();

            relationshipsService.SaveRelationshipDefinition(senderUserId, receiverUserId, relationshipId);

        }

    }
    public class UsersInfo
    {
        public string receiversId;
    }
}
