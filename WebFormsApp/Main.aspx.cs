
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.ModelBinding;
using System.Web.UI;
using WebFormsApp.ViewModel;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Ninject;
using Core.BLL.DTO;
using Core.POCO;
using Core.BLL.Interfaces;

namespace WebFormsApp
{
    public partial class Main : BasePage
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
        
        public string currentUserId;
        public int amountFriendsToShow = 6;
        public string FriendsCount
        {
            get { return ((Literal)listView.FindControl("litFriendsCount")).Text; }
            set { ((Literal)listView.FindControl("litFriendsCount")).Text = value; }
        }

        void Page_PreInit(object s, EventArgs e)
        {
            currentUserId = User.Identity.GetUserId();
        }


        public bool CurrentUserIsOwnerOfCurrentPage
        {
            get
            {
                if (((NestedSiteMaster)this.Master).RelationshipDefinitionID == 0)
                    return true;

                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            string pageOfUserID = Request.QueryString["UserID"] as string;

            if (pageOfUserID == null)
                pageOfUserID = currentUserId;

            Panel1.Controls.Add(new MessageControl { RelationshipDefinitionID = ((NestedSiteMaster)this.Master).RelationshipDefinitionID });

            onlineFriendsLink.HRef = "People.aspx?UserID=" + pageOfUserID + "&online=true";
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            FriendsCount = friendsCount.ToString();
        }
      

        private int friendsCount;
        public List<FriendsViewModel> GetFriendsRefs([QueryString] string UserID)
        {
            if (UserID == null)
                UserID = currentUserId;

            List<FriendsDTO> listDto = RelationshipsService.CreateLinks(amountFriendsToShow, UserID, out friendsCount);
            List<FriendsViewModel> listVM = listDto.Select(a => MappingService.Map<FriendsDTO, FriendsViewModel>(a)).ToList();

            return listVM;
        }


        public AlbumViewModel[] GetAlbums([QueryString]string UserID)
        {
            if (UserID == null)
                UserID = currentUserId;

            AlbumDTO[] albumsDto = ProfileService.GetAlbums(UserID);
            AlbumViewModel[] albumsVM = albumsDto.Select(a => MappingService.Map<AlbumDTO, AlbumViewModel>(a)).ToArray();
            return albumsVM;
        }

        public ProfileViewModel GetProfile([QueryString]string UserID)
        {
            if (UserID == null)
                UserID = currentUserId;

            var profile = ProfileService.GetProfile(UserID);

            if (profile != null)
            {
                var profilevm = MappingService.Map< Core.POCO.Profile, ProfileViewModel>(profile);
                profilevm.PhoneNumber = UserService.GetPhoneNumber(UserID);
                return profilevm;
            }
            return null;
        }

        public List<Status> GetStatuses([QueryString] string UserID)
        {
            if (UserID == null)
                UserID = currentUserId;

            List<Status> statuses = ProfileService.GetStatuses(UserID);

            foreach (var s in statuses)
                s.AvatarUrl = PhotoService.GetAvatar(s.PostByUserID);

            return statuses;
        }

        public List<Comment> GetCommentsByStatusID([Control("hdnStatusId")] int ID)
        {
            return ProfileService.GetCommentsByStatusID(ID);
        }

        public ApplicationUser GetAccount()
        {
            var user = UserService.GetAccount(currentUserId);
            return user;
        }

    }
}