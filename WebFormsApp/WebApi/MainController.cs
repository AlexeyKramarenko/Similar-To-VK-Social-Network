using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using Core.BLL.Interfaces;
using Core.POCO;

namespace WebFormsApp.WebApi
{ 
    public class MainController : ApiController
    {
        string currentUserId;
        string userName;

        IWallStatusService wallStatusService;
        IRelationshipsService relationshipsService;

        public MainController(IWallStatusService _wallStatusService, IRelationshipsService _relationshipsService)
        {
            wallStatusService = _wallStatusService;
            relationshipsService = _relationshipsService;

            currentUserId = User.Identity.GetUserId();
            userName = User.Identity.Name;
        }

        [HttpPost]
        [ActionName("InsertComment")]
        public HttpResponseMessage InsertComment([FromBody]Comment com)
        {
            HttpResponseMessage response = null;

            if (com != null)
            {
                com.UserID = currentUserId;
                com.UserName = userName;
                var result = wallStatusService.InsertComment(com);

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
        [ActionName("DeleteComment")]
        public HttpResponseMessage DeleteComment(int id)
        {
            var comment = wallStatusService.GetComment(id, currentUserId);

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
                status.PostByUserID = currentUserId;
                status.UserName = userName;

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
                Message.SendersUserID = currentUserId;

                bool invitationIsAlreadyInDb = relationshipsService.CheckIfTheSameInvitationAlreadyExistsInDB(Message);

                if (!invitationIsAlreadyInDb)
                {
                    Message.RequestDate = string.Format("{0}.{1}.{2}  {3}:{4}", DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Year, DateTime.Today.Hour, DateTime.Today.Minute);
                    Message.Body = "<p>Hello! I wanna be your friend!<br/>" +
                                      "FROM: " + userName +
                                        "<br/>" +
                                        "Choose status for this human:" +
                                        "<select class='relationshipType'>" +

                                            "<option value='1'>Add to friends</option>" +
                                            "<option value='2'>Add to subscribers</option>" +
                                            "<option value='3'>Ignore this human</option>" +

                                        "</select>" +
                                        //"<br/>" +
                                        "<input type = 'submit' value = 'Отправить' data-senderid='" + currentUserId + "' onclick = 'CheckRelationshipType(this);return false;' /> " +
                                       "</p>";

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
