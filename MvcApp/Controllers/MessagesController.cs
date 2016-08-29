using Core.BLL.DTO;
using Microsoft.AspNet.Identity;
using Core.BLL.Interfaces;
using MvcApp.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcApp.Services;

namespace MvcApp.Controllers
{
    [Authorize]
    public class MessagesController :  Controller
    {
        IMessagesService messagesService;
        IMappingService mappingService;
        IRelationshipsService relationshipsService;
        ISessionService sessionService;
        public string CurrentUserId
        {
            get
            {
                return sessionService.CurrentUserId;
            }
        }

        public MessagesController(IMessagesService messagesService, IMappingService mappingService, IRelationshipsService relationshipsService, ISessionService sessionService)
        {
            this.messagesService = messagesService;
            this.mappingService = mappingService;
            this.relationshipsService = relationshipsService;
            this.sessionService = sessionService;
        }

        [HttpGet]
        public ActionResult MessagesPage()
        {
            string userName = User.Identity.Name;

            List<MessageDTO> dialogsDto = messagesService.GetDialogsList(CurrentUserId, userName);
            List<MessagesViewModel> dialogsVM = dialogsDto.Select(a => mappingService.Map<MessageDTO, MessagesViewModel>(a)).ToList();

            return View(dialogsVM);
        }

        
    }
}