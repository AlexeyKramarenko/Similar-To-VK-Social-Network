using Core.BLL.DTO;
using Core.BLL.Interfaces;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WebFormsApp.Services;
using WebFormsApp.ViewModel;

namespace WebFormsApp
{
    public partial class Messages : System.Web.UI.Page
    {
        [Inject]
        public IMessagesService MessagesService { get; set; }

        [Inject]
        public IMappingService MappingService { get; set; }

        [Inject]
        public ISessionService SessionService { get; set; }


        public List<MessageViewModel> GetDialogsList()
        {

            string userName = User.Identity.Name;

            List<MessageDTO> dialogsDto = MessagesService.GetDialogsList(SessionService.CurrentUserId, userName);
            List<MessageViewModel> dialogsVM = dialogsDto.Select(a => MappingService.Map<MessageDTO, MessageViewModel>(a)).ToList();
            var lastMessage = dialogsVM.LastOrDefault();
            if (lastMessage != null)
            {
                if (lastMessage.MessageText.Length > 33)
                {
                    if (lastMessage.MessageText.Length > 33)
                        lastMessage.MessageText = lastMessage.MessageText.Substring(0, 33) + "...";
                }
            }
            return dialogsVM;
        }
        
    }
}