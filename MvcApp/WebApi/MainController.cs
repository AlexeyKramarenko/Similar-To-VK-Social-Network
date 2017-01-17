using Microsoft.AspNet.Identity;
using Core.BLL.Interfaces;
using Core.POCO;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using System.Web;
using MvcApp.Services;
using MvcApp.ViewModel;

namespace MvcApp.WebAPI
{
    public class MainController : ApiController
    {
        IWallStatusService wallStatusService;
        IRelationshipsService relationshipsService;
        ISessionService sessionService;
        IPhotoService photoService;
        IUserService userService;
        public string CurrentUserId
        {
            get
            {
                return sessionService.CurrentUserId;
            }
        }
        public MainController(IWallStatusService wallStatusService, IRelationshipsService relationshipsService, ISessionService sessionService, IPhotoService photoService, IUserService userService)
        {
            this.wallStatusService = wallStatusService;
            this.relationshipsService = relationshipsService;
            this.sessionService = sessionService;
            this.photoService = photoService;
            this.userService = userService;
        }

        [HttpPost]
        [ActionName("InsertComment")]
        public HttpResponseMessage InsertComment([FromBody]Comment com)
        {
            HttpResponseMessage response = null;

            if (com != null)
            {
                com.UserID = CurrentUserId;
                wallStatusService.InsertComment(com);

                var cvm = new CommentViewModel
                {
                    CommentID = com.ID,
                    CommentatorsUserName = userService.GetUserNameByUserID(com.UserID),
                    CommentText = com.CommentText
                };

                response = Request.CreateResponse();
                response.StatusCode = HttpStatusCode.Created;
                response.Content = new ObjectContent<CommentViewModel>(cvm, new JsonMediaTypeFormatter());
            }
            else
            {
                response = Request.CreateResponse("Empty");
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }

        [HttpDelete]
        [ActionName("DeleteComment")]
        public HttpResponseMessage DeleteComment(int id)
        {
            var comment = wallStatusService.GetComment(id, CurrentUserId);

            if (comment == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            else
            {
                wallStatusService.DeleteComment(comment);
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
        }

        [HttpPost]
        [ActionName("InsertStatus")]
        public HttpResponseMessage InsertStatus([FromBody]Status status)
        {
            HttpResponseMessage response = null;

            if (status != null)
            {
                status.PostByUserID = CurrentUserId;
                status.UserName = User.Identity.Name;

                var result = wallStatusService.InsertStatus(status);

                response = Request.CreateResponse();
                response.StatusCode = HttpStatusCode.Created;
                response.Content = new ObjectContent<object>(result, new JsonMediaTypeFormatter());
            }
            else
            {
                response = Request.CreateResponse("Empty");
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }

        [HttpDelete]
        [ActionName("DeleteStatus")]
        public HttpResponseMessage DeleteStatus(int id)
        {
            var status = wallStatusService.GetStatusById(id);

            if (status == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            else
            {
                wallStatusService.DeleteStatus(status);
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
        }

        [HttpPost]
        [ActionName("AddToFriendsMessage")]
        public HttpResponseMessage AddToFriendsMessage([FromBody]Message Message)
        {
            HttpResponseMessage response = null;

            if (Message != null)
            {
                Message.Invitation = true;
                Message.SendersUserID = CurrentUserId;

                bool invitationIsAlreadyInDb = relationshipsService.CheckIfTheSameInvitationAlreadyExistsInDB(Message);

                if (!invitationIsAlreadyInDb)
                {
                    Message.RequestDate = DateTime.Now;
                    Message.Body = Message.Body.Replace("--userNamePlaceHolder--", User.Identity.Name);
                    Message.ViewedByReceiver = false;

                    relationshipsService.AddToFriendsMessage(Message);
                }

                response = Request.CreateResponse();
                response.StatusCode = HttpStatusCode.Created;
                response.Content = new StringContent("Message was added", Encoding.UTF8, "text/plain");
            }
            else
            {
                response = Request.CreateResponse("Empty");
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }

    }
}
