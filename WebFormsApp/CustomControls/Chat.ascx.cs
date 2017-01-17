using Core.BLL.DTO;
using Core.BLL.Interfaces;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebFormsApp.Services;
using WebFormsApp.ViewModel;

namespace WebFormsApp.CustomControls
{
    public partial class Chat : System.Web.UI.UserControl
    {
        [Inject]
        public IMessagesService MessagesService { get; set; }

        [Inject]
        public IMappingService MappingService { get; set; }

        [Inject]
        public ISessionService SessionService { get; set; }


        public List<MessageViewModel> GetDialogsList()
        {

            string userName = HttpContext.Current.User.Identity.Name;

            List<MessageDTO> dialogsDto = MessagesService.GetDialogsList(SessionService.CurrentUserId, userName);
            List<MessageViewModel> dialogsVM = dialogsDto.Select(a => MappingService.Map<MessageDTO, MessageViewModel>(a)).ToList();

            return dialogsVM;
        }
    }
}