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

namespace WebFormsApp.CustomControls
{
    public partial class Comments : System.Web.UI.UserControl
    {

        public IProfileService ProfileService { get; set; }

        public bool CurrentUserIsOwnerOfCurrentPage { get; set; }
        public int StatusID { get; set; }


        public List<Comment> GetCommentsByStatusID()
        {
            List<Comment> comments = ProfileService.GetCommentsByStatusID(StatusID);
            return comments;
        }

       
    }
}