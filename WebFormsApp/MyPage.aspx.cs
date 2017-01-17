using Core.BLL.DTO;
using Core.BLL.Interfaces;
using Core.POCO;
using Microsoft.AspNet.Identity;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsApp.Services;
using WebFormsApp.ViewModel;

namespace WebFormsApp
{
    public partial class MyPage : System.Web.UI.Page
    {
        [Inject]
        public IMappingService MappingService { get; set; }
        [Inject]
        public IRelationshipsService RelationshipsService { get; set; }
        [Inject]
        public IPhotoService PhotoService { get; set; }
        [Inject]
        public IProfileService ProfileService { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public ISessionService SessionService { get; set; }


        public string CurrentUserId
        {
            get
            {
                return SessionService.CurrentUserId;
            }
        }

        public int amountFriendsToShow = 6;

        public bool CurrentUserIsOwnerOfCurrentPage
        {
            get
            {
                if (RelationshipDefinitionIDOfCurrentVisitor == 1)
                    return true;

                return false;
            }
        }
        public int RelationshipDefinitionIDOfCurrentVisitor
        {
            get
            {
                string pageOfUserID = Request.QueryString["UserID"] as string;

                if (pageOfUserID != null && pageOfUserID != CurrentUserId)
                    return RelationshipsService.GetRelationshipDefinitionIdOfPageVisitor(CurrentUserId, pageOfUserID);

                return 1;
            }
        }
        private PrivacyRestrictions Privacy
        {
            get
            {
                string pageOfUserID = Request.QueryString["UserID"] as string;

                //if current user came to page of some another user
                if (pageOfUserID != null)
                    return RelationshipsService.GetPrivacyRestrictionsOfCurrentPage(pageOfUserID, CurrentUserId, RelationshipDefinitionIDOfCurrentVisitor);

                //current user is owner of page (admin)
                else
                    return
                        new PrivacyRestrictions
                        {
                            DisplayCommentLink = true,
                            DisplayComments = true,
                            DisplayDetailsInfo = true,
                            DisplayMessageForm = true,
                            DisplayPosts = true
                        };
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string pageOfUserID = Request.QueryString["UserID"] as string;

            if (pageOfUserID == null)
                pageOfUserID = CurrentUserId;

            onlineFriendsLink.HRef = "People.aspx?UserID=" + pageOfUserID + "&online=true";
        }



        private int friendsCount;
        public List<FriendsViewModel> GetFriendsRefs([QueryString] string UserID)
        {
            if (UserID == null)
                UserID = CurrentUserId;

            List<FriendsDTO> listDto = RelationshipsService.CreateLinks(amountFriendsToShow, UserID, out friendsCount);
            List<FriendsViewModel> listVM = listDto.Select(a => MappingService.Map<FriendsDTO, FriendsViewModel>(a)).ToList();

            return listVM;
        }


        public AlbumViewModel[] GetAlbums([QueryString]string UserID)
        {
            if (UserID == null)
                UserID = CurrentUserId;

            AlbumDTO[] albumsDto = ProfileService.GetAlbums(UserID);
            AlbumViewModel[] albumsVM = albumsDto.Select(a => MappingService.Map<AlbumDTO, AlbumViewModel>(a)).ToArray();
            return albumsVM;
        }

        public ProfileViewModel GetProfile([QueryString]string UserID)
        {
            if (UserID == null)
                UserID = CurrentUserId;

            var profile = ProfileService.GetProfile(UserID);

            if (profile != null)
            {
                var profilevm = MappingService.Map<Core.POCO.Profile, ProfileViewModel>(profile);
                profilevm.PhoneNumber = UserService.GetPhoneNumber(UserID);

                return profilevm;
            }
            return null;
        }

        public void UsersInfoFormView_ItemCreated(object sender, EventArgs e)
        {
            if (Privacy.DisplayDetailsInfo == true)
            {
                var UsersInfoFormView = (FormView)sender;

                var DetailsInfoPlaceHolder = (PlaceHolder)UsersInfoFormView.FindControl("DetailsInfoPlaceHolder");

                var DetailsInfo = (CustomControls.DetailsInfo)LoadControl("~/CustomControls/DetailsInfo.ascx");
                DetailsInfo.CurrentUserIsOwnerOfCurrentPage = CurrentUserIsOwnerOfCurrentPage;
                DetailsInfo.Model = (ProfileViewModel)UsersInfoFormView.DataItem;

                DetailsInfoPlaceHolder.Controls.Add(DetailsInfo);
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            var MessageComponent = new CustomControls.MessageComponent();// (CustomControls.MessageComponent)LoadControl("~/CustomControls/MessageComponent.cs");
            MessageComponent.RelationshipDefinitionID = RelationshipDefinitionIDOfCurrentVisitor;
            MessageComponentPlaceHolder.Controls.Add(MessageComponent);

            if (Privacy.DisplayMessageForm == true)
            {
                var MessageForm = (CustomControls.MessageForm)LoadControl("~/CustomControls/MessageForm.ascx");
                MessageFormPlaceHolder.Controls.Add(MessageForm);
            }

            if (Privacy.DisplayPosts == true)
            {
                var Statuses = (CustomControls.Statuses)LoadControl("~/CustomControls/Statuses.ascx");
                Statuses.PhotoService = PhotoService;
                Statuses.ProfileService = ProfileService;
                Statuses.RelationshipsService = RelationshipsService;
                Statuses.CurrentUserId = CurrentUserId;
                Statuses.DisplayCommentLink = Privacy.DisplayCommentLink;
                Statuses.DisplayComments = Privacy.DisplayComments;
                Statuses.CurrentUserIsOwnerOfCurrentPage = CurrentUserIsOwnerOfCurrentPage;

                PostsPlaceHolder.Controls.Add(Statuses);
            }
        }
         
    }
}