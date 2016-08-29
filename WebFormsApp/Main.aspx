<%@ Page Title="" Language="C#" EnableViewState="false" MasterPageFile="~/NestedSiteMaster.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Main.aspx.cs" Inherits="WebFormsApp.Main" %>

<%@ Import Namespace="Core" %>


<asp:Content ContentPlaceHolderID="Styles" runat="server">
    <link href="Content/main.css" rel="stylesheet" />
    <link href="Content/sweetalert.css" rel="stylesheet" />
    <link href="Content/photos.css" rel="stylesheet" />
    <link href="Content/modalPopLite.css" rel="stylesheet" />
    <link href="Content/main_popup_window.css" rel="stylesheet" />
</asp:Content>




<asp:Content ContentPlaceHolderID="NestedPlaceHolder" runat="server">

    <div id="mainContent">

        <div id="popup-wrapper">
            <a href="#" id="close-btn">Close</a>
            <img class="imgLargeAvatar" src="" width="650" />
            <br />
        </div>

        <div id="leftColumn">
            <ul>
                <li>
                    <img id="imgAvatar" src="" onclick="GetLargeAvatarImg();return false;" />
                </li>
                <li>
                    <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                </li>
                <li>
                    <span id="extender"></span>
                    <div class="header_top">
                        <b>Friends</b>
                    </div>

                    <asp:ListView
                        ID="listView"
                        ClientIDMode="Static"
                        SelectMethod="GetFriendsRefs"
                        ItemType="WebFormsApp.ViewModel.FriendsViewModel"
                        runat="server">
                        <LayoutTemplate>
                            <div class="header_bottom">
                                <asp:Literal ID="litFriendsCount" runat="server" Text=""></asp:Literal>
                                friend(s)
                            </div>
                            <div class="Friends">
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <a href='<%# Item.FriendsPageUrl %>'>
                                <img src='<%# Item.ImageUrl %>' width="100" />
                            </a>
                        </ItemTemplate>
                    </asp:ListView>
                </li>

                <li>
                    <div class="header_top">
                        <b><a id="onlineFriendsLink" runat="server">Online friends</a></b>
                    </div>

                    <div class="Friends" id="onlineFriends">
                    </div>
                </li>
                <li>
                    <div class="header_top">
                        <b>Photoalbums
                        </b>
                    </div>

                    <div id="albums">

                        <asp:ListView
                            DataKeyNames="AlbumID"
                            ClientIDMode="Static"
                            SelectMethod="GetAlbums"
                            ItemType="WebFormsApp.ViewModel.AlbumViewModel"
                            runat="server">
                            <ItemTemplate>

                                <div class="album">
                                    <img src='<%# Item.ThumbnailPhotoUrl %>' onclick='<%# "getPhotosByAlbumID("+ Item.AlbumID +");return false;" %>' data-albumid='<%# Item.AlbumID %>' />
                                </div>
                                <div class="albumTitle"><%# Item.AlbumTitle %></div>

                            </ItemTemplate>
                            <EmptyItemTemplate>
                                45646
                            </EmptyItemTemplate>
                        </asp:ListView>

                    </div>
                </li>
            </ul>
        </div>
        <div id="rightColumn">



            <asp:FormView
                DefaultMode="ReadOnly"
                ItemType="WebFormsApp.ViewModel.ProfileViewModel"
                SelectMethod="GetProfile"
                runat="server"
                DataKeyNames="ProfileID"
                RenderOuterTable="false">

                <ItemTemplate>
                    <div id="profile_info">
                        <br />
                        <b><%# Item.ApplicationUser.UserName %></b>
                        <asp:HiddenField ID="hdnUserID" runat="server" ClientIDMode="Static" Value='<%# Item.ApplicationUser.Id %>' />


                        <br />
                        <div style="border-top: 1px solid #C3B6B6; margin-top: 15px; margin-right: 10px;"></div>

                        <br />
                        <div id="info">
                            <div id="mainInfo">
                                <div class="profile_info">
                                    <div class="row">
                                        <div class="label">Birthday:</div>
                                        <div class="value">
                                            <%# Item.BirthDay+"." %>

                                            <%# Item.BirthMonth+"." %>

                                            <%# Item.BirthYear %>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="label">Hometown:</div>
                                        <div class="value"><%# Item.HomeTown %></div>
                                    </div>
                                    <div class="row">
                                        <div class="label">Relationship status:</div>
                                        <div class="value"><%# Item.MaritalStatus %></div>
                                    </div>
                                    <div class="row">
                                        <div class="label">Language:</div>
                                        <div class="value"><%# Item.Language %></div>
                                    </div>
                                    <div class="row">
                                        <div class="label">Country:</div>
                                        <div class="value"><%# Item.Country %></div>
                                    </div>
                                    <div class="row">
                                        <div class="label">Town:</div>
                                        <div class="value"><%# Item.Town %></div>
                                    </div>

                                </div>
                            </div>

                            <a id="showDetails" onclick="showHideDetails()">Show detail info</a>

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
                                        <div class="value"><%# Item.StartScoolYear %></div>
                                    </div>
                                    <div class="row">
                                        <div class="label">Year of graduation:</div>
                                        <div class="value"><%# Item.FinishScoolYear %></div>
                                    </div>
                                </div>
                                <br />
                            </div>

                            <hr />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:FormView>


            <div id="allPhotos">
                <span id="photosHeader" style="background-color: #DEE5EB; border: 1px solid grey; border-right: none; display: block; padding-left: 10px; font-weight: bold; color: #45688E;">All photos</span>

                <div class="container">
                </div>

                <div class="container_album">
                </div>

                <div>
                </div>
            </div>


            <asp:FormView
                runat="server"
                RenderOuterTable="false"
                ClientIDMode="Static"
                SelectMethod="GetAccount"
                DefaultMode="ReadOnly"
                DataKeyNames="Id"
                ItemType="Core.POCO.ApplicationUser">

                <ItemTemplate>
                    <div id="createMessages">
                        <asp:Label
                            ID="Label1"
                            CssClass="messageForm"
                            ClientIDMode="Static"
                            ForeColor="#45688E"
                            Font-Bold="true"
                            runat="server"><span id="statusesCount"><%# Item.StatusesCount %> </span> messages:</asp:Label>
                        <br />
                        <textarea
                            rows="2"
                            cols="20"
                            id="txtMessage"
                            class="messageForm"
                            onclick="createPostForm(this)"
                            style="width: 370px; resize: none">
								</textarea>
                    </div>
                </ItemTemplate>
            </asp:FormView>


            <div style="margin-right: -1px; border-right: 1px solid gray; padding-right: 10px; padding-left: 10px;">

                <ul id="postsList">


                    <asp:ListView
                        ID="ListView1"
                        ClientIDMode="Static"
                        DataKeyNames="ID"
                        SelectMethod="GetStatuses"
                        ItemType="Core.POCO.Status"
                        runat="server">

                        <ItemTemplate>
                            <li>
                                <div class="table3">
                                    <div class="table3row">
                                        <div class="table3leftCell">

                                            <asp:Image
                                                ImageUrl='<%# Item.AvatarUrl %>'
                                                ID="Image3"
                                                CssClass="post_image"
                                                runat="server"
                                                Width="30px" />

                                            <br />
                                            Online
                                        </div>
                                        <div class="table3rightCell">
                                            <b><%# Item.UserName %></b>

                                            <% if (CurrentUserIsOwnerOfCurrentPage)
                                                    Response.Write("<a class='delete' onclick='deletePost(this)'>delete</a>");
                                            %>
                                            <br />
                                            <%# Item.Post %>
                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField data-statusid="hdnStatusId" runat="server" Value="<%# Item.ID %>" />
                                <%# Item.CreateDate %> | <a onclick='createCommentsForm(this)'>Comment</a>
                                <hr />

                                <a class="showDetails" onclick="scrollToCommentsForm(this);showAllComments(this);">Show all <span class="commentsCount"><%# Item.CommentsCount %></span> comments</a>

                                <div class="COMMENT" style="width: 300px;">

                                    <asp:Repeater
                                        runat="server"
                                        ClientIDMode="Static"
                                        ID="rptData"
                                        SelectMethod="GetCommentsByStatusID"
                                        ItemType="Core.POCO.Comment">
                                        <ItemTemplate>

                                            <div class="hideInfo" style="position: relative">
                                                <div class="avatar">

                                                    <b><%# Item.UserName %></b>

                                                    <% if (CurrentUserIsOwnerOfCurrentPage)
                                                            Response.Write("<a class='delete' onclick='deleteComment(this)'>delete</a>");
                                                    %>
                                                    <asp:HiddenField ID="cmnt" runat="server" Value="<%# Item.ID %>" />
                                                    <br />
                                                </div>
                                                <div class="commentCell">
                                                    <%# Item.CommentText %>
                                                </div>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>
                                <div class="commentContainer"></div>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
        </div>
    </div>

    <input class="modal-state" id="modal-1" type="checkbox" />
    <div class="modal">
        <label class="modal__bg" for="modal-1"></label>
        <div class="modal__inner">
            <label class="modal__close" for="modal-1"></label>
            <div>
                <span>Message</span>
                <textarea id="messageBody"></textarea>
                <input type="button" id="sendBtn" value="Send" />
            </div>
        </div>
    </div>

</asp:Content>



<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="Scripts/sweetalert.min.js"></script>
    <script src="Scripts/modalPopLite.min.js"></script>
    <script src="Scripts/main.js"></script>
    <script src="Scripts/signalRmainPage.js"></script>
</asp:Content>

