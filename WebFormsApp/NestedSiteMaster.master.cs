using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Ninject; 
using Core.BLL.Interfaces;

namespace WebFormsApp
{
    public partial class NestedSiteMaster : MasterPageBase
    {
        [Inject]
        public IMessagesService MessagesService { get; set; }
        [Inject]
        public IRelationshipsService RelationshipsService { get; set; }

        public string targetPageOnSearchClick = "/People.aspx";


        //Definition of current visitors rights for current page (0-owner, 1-friend, 2-subscriber, 3-unknown user)
        public int RelationshipDefinitionID { get; set; }

        public string CurrentUserId { get; set; }



        protected void Page_Init(object sender, EventArgs e)
        {
            CurrentUserId = Page.User.Identity.GetUserId();

            hdnUserID.Value = CurrentUserId;//for  SignalR scripts

            hdnPageOfUserID.Value = (Request.QueryString["UserID"] != null) ? Request.QueryString["UserID"] as string : CurrentUserId;

            this.Page.InitComplete += Page_InitComplete;

        }

        //Time for Ninject initialization
        protected void Page_InitComplete(object sender, EventArgs e)
        {
            RelationshipDefinitionID = GetRelationshipDefinitionIDOfCurrentVisitor();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition(
                  "signalr",
                  new ScriptResourceDefinition
                  {
                      Path = "~/bundles/SignalR",
                  });


            lnkPeople.NavigateUrl = "~/people.aspx?UserID=" + CurrentUserId;

            var messages = MessagesService.GetUnviewedMessagesByReceiversUserID(CurrentUserId);

            var messagesCount = messages.ToList().Count;

            if (messagesCount > 0)
            {
                litMessagesCount.Text = string.Format("+{0}", messagesCount);
                litMessagesCount.CssClass = "message";
            }
            else
                litMessagesCount.Text = "";

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GoToPeoplePage();
        }

        public void GoToPeoplePage()
        {
            string currentPage = Request.FilePath;

            if (currentPage != targetPageOnSearchClick)
                this.Redirect("People.aspx");
        }

        public int GetRelationshipDefinitionIDOfCurrentVisitor()
        {
            string pageOfUserID = Request.QueryString["UserID"] as string;

            if (pageOfUserID != null && pageOfUserID != CurrentUserId)
                return RelationshipsService.GetRelationshipDefinitionIdOfPageVisitor(CurrentUserId, pageOfUserID);

            return 0;
        }
    }
}