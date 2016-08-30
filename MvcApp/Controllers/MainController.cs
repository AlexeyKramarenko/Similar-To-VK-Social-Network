using Core.BLL.DTO;
using Core.BLL.Interfaces;
using Core.DAL;
using Core.POCO;
using Microsoft.AspNet.Identity;
using MvcApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MvcApp.Services;

namespace MvcApp.Controllers
{
    [Authorize]
    public class MainController : Controller
    {
        IUserService userService;
        IRelationshipsService relationshipsService;
        IMappingService mappingService;
        IProfileService profileService;
        IPhotoService photoService;
        ISettingsService settingsService;
        ISessionService sessionService;

        string BaseUrl
        {
            get
            {
                return Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
            }
        }

        public string CurrentUserId
        {
            get
            {
                return sessionService.CurrentUserId;
            }
        }

        public MainController(IUserService userService, IRelationshipsService relationshipsService, IMappingService mappingService, IProfileService profileService, IPhotoService photoService, ISettingsService settingsService, ISessionService sessionService)
        {
            this.userService = userService;
            this.relationshipsService = relationshipsService;
            this.mappingService = mappingService;
            this.profileService = profileService;
            this.photoService = photoService;
            this.settingsService = settingsService;
            this.sessionService = sessionService;
        }

        [HttpGet]
        public ActionResult MainPage(string UserID)
        {
            if (UserID == null)
                UserID = CurrentUserId;

            ViewBag.DisplayPosts = Privacy.DisplayPosts;
            ViewBag.DisplayMessageForm = Privacy.DisplayMessageForm;

            ViewBag.OnlineFriendsLink = "/people/peoplepage/" + UserID + "/true";

            return View();
        }
        [HttpGet]
        public ActionResult FriendsLinks(string UserID)
        {
            if (UserID == null)
                UserID = CurrentUserId;

            int amountFriendsToShow = 6;
            int friendsCount;
            List<FriendsDTO> listDto = relationshipsService.CreateLinks(amountFriendsToShow, UserID, out friendsCount);
            List<FriendsViewModel> listVM = listDto.Select(a => mappingService.Map<FriendsDTO, FriendsViewModel>(a)).ToList();

            return PartialView(listVM);
        }
        [HttpGet]
        public ActionResult Albums(string UserID)
        {
            if (UserID == null)
                UserID = CurrentUserId;

            AlbumDTO[] albumsDto = profileService.GetAlbums(UserID);

            for (int i = 0; i < albumsDto.Length; i++)
            {
                albumsDto[i].ThumbnailPhotoUrl = BaseUrl + albumsDto[i].ThumbnailPhotoUrl;
            }

            AlbumViewModel[] albumsVM = albumsDto.Select(a => mappingService.Map<AlbumDTO, AlbumViewModel>(a)).ToArray();

            return PartialView(albumsVM);
        }
        [HttpGet]
        public ActionResult ProfileInfo(string UserID)
        {
            if (UserID == null)
                UserID = CurrentUserId;

            ViewBag.CurrentVisitorIsOwnerOfCurrentPage = CurrentVisitorIsOwnerOfCurrentPage;
            ViewBag.DisplayDetailsInfo = Privacy.DisplayDetailsInfo;

            Profile profile = profileService.GetProfile(UserID);
            ProfileViewModel profileVM = null;

            if (profile != null)
            {
                profileVM = mappingService.Map<Core.POCO.Profile, ProfileViewModel>(profile);
                profileVM.PhoneNumber = userService.GetPhoneNumber(UserID);
            }

            return PartialView(profileVM);
        }
        [HttpGet]
        public ActionResult Statuses(string UserID)
        {
            if (UserID == null)
                UserID = CurrentUserId;

            List<Status> statuses = profileService.GetStatuses(UserID);

            foreach (var s in statuses)
            {
                s.AvatarUrl = BaseUrl + photoService.GetAvatar(s.PostByUserID);
                s.Comments = profileService.GetCommentsByStatusID(s.ID);
            }

            ViewBag.DisplayComments = Privacy.DisplayComments;
            ViewBag.DisplayCommentLink = Privacy.DisplayCommentLink;
            ViewBag.CurrentVisitorIsOwnerOfCurrentPage = CurrentVisitorIsOwnerOfCurrentPage;

            return PartialView(statuses);
        }
        [HttpGet]
        public ActionResult MessageControl()
        {
            ViewBag.RelationshipDefinitionIDOfCurrentVisitor = RelationshipDefinitionIDOfCurrentVisitor;

            return PartialView();
        }




        private PrivacyRestrictions Privacy
        {
            get
            {
                string pageOfUserID = HttpContext.Request.RequestContext.RouteData.Values["UserID"] as string;

                //if current user came to page of some another user
                if (pageOfUserID != null)
                    return relationshipsService.GetPrivacyRestrictionsOfCurrentPage(pageOfUserID, CurrentUserId, RelationshipDefinitionIDOfCurrentVisitor);

                return
                    new PrivacyRestrictions
                    {
                        DisplayCommentLink = true,
                        DisplayComments = true,
                        DisplayDetailsInfo = true,
                        DisplayMessageForm = true,
                        DisplayPosts = true,
                        SendMessagePossibility = true,
                        ShowInSearch = true
                    };
            }
        }
        private bool CurrentVisitorIsOwnerOfCurrentPage
        {
            get
            {
                if (RelationshipDefinitionIDOfCurrentVisitor == 1)
                    return true;

                return false;
            }

        }
        private int RelationshipDefinitionIDOfCurrentVisitor
        {
            get
            {
                string pageOfUserID = HttpContext.Request.RequestContext.RouteData.Values["UserID"] as string;

                if (pageOfUserID != null && pageOfUserID != CurrentUserId)
                    //get data from cache
                    return relationshipsService.GetRelationshipDefinitionIdOfPageVisitor(CurrentUserId, pageOfUserID);

                return 1;
            }
        }
    }
}