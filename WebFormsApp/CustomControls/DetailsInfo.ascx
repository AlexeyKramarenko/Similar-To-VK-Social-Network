<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailsInfo.ascx.cs" Inherits="WebFormsApp.CustomControls.DetailsInfo" %>

<asp:FormView
    ID="detailsInfoFormView"
    DefaultMode="ReadOnly"
    ItemType="WebFormsApp.ViewModel.ProfileViewModel"
    runat="server"
    DataKeyNames="ProfileID"
    RenderOuterTable="false">
    <ItemTemplate>
        <a id="showDetails" onclick="vm.showHideDetails()">Show detail info</a>

        <div id="contactsInfo" class="hideInfo">
            <div class="line" style="margin-left: 80px"></div>
            <h4>Contacts</h4>


            <% if (CurrentUserIsOwnerOfCurrentPage)
                    Response.Write("<a class='edit' href='profile.aspx?#contacts_'>Edit</a>"); %>


            <div class="profile_info">

                <div class="row">
                    <div class="label">Phone number:</div>
                    <div class="value"><%# Item.ApplicationUser.PhoneNumber %></div>
                </div>
                <div class="row">
                    <div class="label">Skype:</div>
                    <div class="value"><%# Item.Skype %></div>
                </div>
                <div class="row">
                    <div class="label">WebSite:</div>
                    <div class="value"><%# Item.WebSite %></div>
                </div>
            </div>
            <br />
        </div>

        <div id="interestsInfo" class="hideInfo">
            <div class="line" style="margin-left: 75px"></div>
            <h4>Interests</h4>

            <% if (CurrentUserIsOwnerOfCurrentPage)
                    Response.Write("<a class='edit' href='profile.aspx?#hobby_'>Edit</a>"); %>


            <div class="profile_info">


                <div class="row">
                    <div class="label">Activity:</div>
                    <div class="value"><%# Item.Activity %></div>
                </div>

                <div class="row">
                    <div class="label">Interests:</div>
                    <div class="value"><%# Item.Interests %></div>
                </div>

                <div class="row">
                    <div class="label">Music:</div>
                    <div class="value"><%# Item.Music %></div>
                </div>

                <div class="row">
                    <div class="label">Favourite films:</div>
                    <div class="value"><%# Item.Films %></div>
                </div>

                <div class="row">
                    <div class="label">Books:</div>
                    <div class="value"><%# Item.Books %></div>
                </div>

                <div class="row">
                    <div class="label">Games:</div>
                    <div class="value"><%# Item.Games %></div>
                </div>

                <div class="row">
                    <div class="label">Quotes:</div>
                    <div class="value"><%# Item.Quotes %></div>
                </div>

                <div class="row">
                    <div class="label">About me:</div>
                    <div class="value"><%# Item.AboutMe %></div>
                </div>

            </div>
            <br />
        </div>
        <div id="educationInfo" class="hideInfo">
            <div class="line" style="margin-left: 95px"></div>
            <h4>Education</h4>

            <% if (CurrentUserIsOwnerOfCurrentPage)
                    Response.Write("<a class='edit' href='profile.aspx?#education_'>Edit</a>"); %>


            <div class="profile_info">
                <div class="row">
                    <div class="label">School city:</div>
                    <div class="value"><%# Item.SchoolCountry %></div>
                </div>
                <div class="row">
                    <div class="label">School town:</div>
                    <div class="value"><%# Item.SchoolTown %></div>
                </div>
                <div class="row">
                    <div class="label">School:</div>
                    <div class="value"><%# Item.School %></div>
                </div>
                <div class="row">
                    <div class="label">Year of the beginning:</div>
                    <div class="value"><%# Item.StartSchoolYear %></div>
                </div>
                <div class="row">
                    <div class="label">Year of graduation:</div>
                    <div class="value"><%# Item.FinishSchoolYear %></div>
                </div>
            </div>
            <br />
        </div>
    </ItemTemplate>
</asp:FormView>
