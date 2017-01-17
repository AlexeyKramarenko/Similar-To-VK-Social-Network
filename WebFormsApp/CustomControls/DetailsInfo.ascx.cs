using System;
using System.Collections;
using System.Collections.Generic;
using WebFormsApp.ViewModel;

namespace WebFormsApp.CustomControls
{
    public partial class DetailsInfo : System.Web.UI.UserControl
    {
        public bool CurrentUserIsOwnerOfCurrentPage { get; set; }
        public ProfileViewModel Model { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            detailsInfoFormView.DataSource = new List<ProfileViewModel> { Model };
            detailsInfoFormView.DataBind();
        }
    }
}