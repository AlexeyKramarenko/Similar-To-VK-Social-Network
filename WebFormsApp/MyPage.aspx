<%@ Page Title="" EnableViewState="false" Language="C#" MasterPageFile="~/NestedMasterPage.master" AutoEventWireup="true" CodeBehind="MyPage.aspx.cs" Inherits="WebFormsApp.MyPage" %>

<%@ Import Namespace="Core" %>
<%@ Import Namespace="System.Web.Optimization" %>

<%@ Register Assembly="WebFormsApp"
    Namespace="WebFormsApp"
    TagPrefix="control" %>
<%@ Register Src="~/CustomControls/Chat.ascx" TagPrefix="uc1" TagName="Chat" %>


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
            <img class="imgLargeAvatar" width="650" />
            <br />
        </div>

        <div id="leftColumn">
            <ul>
                <li>
                    <img
                        id="imgAvatar"
                        onclick="vm.getLargeAvatarImg()" />
                </li>
                <li>
                    <asp:PlaceHolder ID="MessageComponentPlaceHolder" runat="server"></asp:PlaceHolder>
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
                                    <img src='<%# Item.ThumbnailPhotoUrl %>'
                                        onclick='<%# "return vm.getPhotosByAlbumID("+ Item.AlbumID +")" %>'
                                        data-albumid='<%# Item.AlbumID %>' />
                                </div>
                                <div class="albumTitle"><%# Item.AlbumTitle %></div>

                            </ItemTemplate>
                        </asp:ListView>

                    </div>
                </li>
            </ul>
        </div>
        <div id="rightColumn">



            <asp:FormView
                ID="UsersInfoFormView"
                DefaultMode="ReadOnly"
                ItemType="WebFormsApp.ViewModel.ProfileViewModel"
                SelectMethod="GetProfile"
                OnItemCreated="UsersInfoFormView_ItemCreated"
                runat="server"
                DataKeyNames="ProfileID"
                RenderOuterTable="false">

                <ItemTemplate>
                    <div id="profile_info">
                        <br />
                        <b><span id="userName"><%# Item.ApplicationUser.UserName %></b>
                        <asp:HiddenField
                            ID="hdnCurrentUserID"
                            runat="server"
                            ClientIDMode="Static"
                            Value='<%# Item.ApplicationUser.Id %>' />


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

                            <asp:PlaceHolder ID="DetailsInfoPlaceHolder" runat="server"></asp:PlaceHolder>

                            <hr />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:FormView>

            <style>
                #photosHeader {
                    background-color: #DEE5EB;
                    border: 1px solid grey;
                    border-right: none;
                    display: block;
                    padding-left: 10px;
                    font-weight: bold;
                    color: #45688E;
                }
            </style>
            <div id="allPhotos">
                <span id="photosHeader">All photos</span>

                <div class="container">
                </div>

                <div class="container_album">
                </div>

                <div>
                </div>
            </div>

            <asp:PlaceHolder ID="MessageFormPlaceHolder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="PostsPlaceHolder" runat="server"></asp:PlaceHolder>

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
                <input
                    type="button"
                    id="sendBtn"
                    value="Send"
                    onclick="vm.setRecipientUserID($('#hdnPageOfUserID').val());
                             vm.createNewDialog();" />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">

    <script src="Scripts/sweetalert.min.js"></script>
    <%: Scripts.Render("~/bundles/modalPopLite") %>
    <%: Scripts.Render("~/bundles/main") %>
    <script>
        var vm = new MainController(new MainView(), new MainService());
    </script>
    <uc1:Chat runat="server" ID="Chat" />

</asp:Content>
