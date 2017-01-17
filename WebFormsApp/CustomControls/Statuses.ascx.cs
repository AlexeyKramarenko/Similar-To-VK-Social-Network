using Core.BLL.Interfaces;
using Core.POCO;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsApp.Services;

namespace WebFormsApp.CustomControls
{
    public partial class Statuses : System.Web.UI.UserControl
    {
        public IPhotoService PhotoService { get; set; }
        public IProfileService ProfileService { get; set; }
        public IRelationshipsService RelationshipsService { get; set; } 
        public bool DisplayCommentLink { get; set; }
        public bool DisplayComments { get; set; }
        public bool CurrentUserIsOwnerOfCurrentPage { get; set; }
        public string CurrentUserId { get; set; }




        public List<Status> GetStatuses([QueryString] string UserID)
        {
            if (UserID == null)
                UserID = CurrentUserId;

            List<Status> statuses = ProfileService.GetStatuses(UserID);

            foreach (var s in statuses)
                s.AvatarUrl ="~/"+ PhotoService.GetAvatar(s.PostByUserID);

            return statuses;
        }



        protected void ListView1_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (DisplayComments == true)
            {
                var CommentsPlaceHolder = (PlaceHolder)e.Item.FindControl("CommentsPlaceHolder");

                var Comments = (CustomControls.Comments)LoadControl("~/CustomControls/Comments.ascx");
                Comments.CurrentUserIsOwnerOfCurrentPage = CurrentUserIsOwnerOfCurrentPage;
                Comments.StatusID = ((Status)e.Item.DataItem).ID;
                Comments.ProfileService = ProfileService;

                CommentsPlaceHolder.Controls.Add(Comments);
            }
        }



    }
}