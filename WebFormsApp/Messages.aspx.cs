using Core.BLL.DTO;
using Microsoft.AspNet.Identity;
using Ninject; 
using WebFormsApp.ViewModel;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using Core.BLL.Interfaces;

namespace WebFormsApp
{
    public partial class Messages : BasePage
    {
        [Inject]
        public IMessagesService MessagesService { get; set; }

        [Inject]
        public IMappingService MappingService { get; set; }



        public List<MessagesViewModel> GetDialogsList()
        {
            string currentUserID = User.Identity.GetUserId();
            string userName = User.Identity.Name;

            List<MessageDTO> dialogsDto = MessagesService.GetDialogsList(currentUserID, userName);
            List<MessagesViewModel> dialogsVM = dialogsDto.Select(a => MappingService.Map<MessageDTO, MessagesViewModel>(a)).ToList();
            return dialogsVM;
        }


    }
}