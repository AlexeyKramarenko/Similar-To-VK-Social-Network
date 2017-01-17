using Core.BLL.DTO;
using Microsoft.AspNet.Identity;
using Core.BLL.Interfaces;
using MvcApp.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcApp.Services;
using System;

namespace MvcApp.Controllers
{
    [Authorize]
    public class MessagesController :  Controller
    {
        IMessagesService messagesService;
        IMappingService mappingService; 
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
            this.sessionService = sessionService;
        }

        [HttpGet]
        public ActionResult Chat()
        {
            List<MessagesViewModel> dialogsVM = GetDialogs();
            if (dialogsVM.Count == 0)
                return new EmptyResult();

            return PartialView("Chat", dialogsVM);
        }
        [HttpGet]
        public ActionResult MessagesPage()
        {
            List<MessagesViewModel> dialogsVM = GetDialogs();
            return View("MessagesPage", dialogsVM);
        }

        private List<MessagesViewModel> GetDialogs()
        {
            //string userName = User.Identity.Name;
            //List<MessageDTO> dialogsDto = messagesService.GetDialogsList(CurrentUserId, userName);
            //List<MessagesViewModel> dialogsVM = dialogsDto.Select(a => mappingService.Map<MessageDTO, MessagesViewModel>(a)).ToList();
            //return dialogsVM;

            string userName = User.Identity.Name;

            List<MessageDTO> dialogsDto = messagesService.GetDialogsList(sessionService.CurrentUserId, userName);
            List<MessagesViewModel> dialogsVM = dialogsDto.Select(a => mappingService.Map<MessageDTO, MessagesViewModel>(a)).ToList();
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