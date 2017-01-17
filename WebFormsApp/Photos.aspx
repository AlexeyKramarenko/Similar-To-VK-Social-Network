<%@ Page EnableViewState="false" Title="" Language="C#" MasterPageFile="~/NestedMasterPage.master" AutoEventWireup="true" CodeBehind="Photos.aspx.cs" Inherits="WebFormsApp.Photos" %>

<%@ Register Src="~/CustomControls/Chat.ascx" TagPrefix="uc1" TagName="Chat" %>



<asp:Content ContentPlaceHolderID="Styles" runat="server">
    <link href="/Content/photos.css" rel="stylesheet" />
    <link href="/Content/sweetalert.css" rel="stylesheet" />
</asp:Content>


<asp:Content ContentPlaceHolderID="NestedPlaceHolder" runat="server">
    <input type="hidden" id="hdnProfileID" value="" />
    <div class="container" style="margin-top: 25px; border-left: 1px solid grey; border-right: 1px solid grey;"></div>
    <div id="hiddenAlbumLinks"></div>
</asp:Content>


<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script src="Scripts/jquery-2.1.4.min.js"></script>
    <script src="Scripts/sweetalert.min.js"></script>
    <script src="Scripts/jquery-multidownload.js"></script>
    <%: Scripts.Render("~/bundles/photos") %>
    <script>
        var vm = new PhotosController(new PhotosView(), new PhotosService());       
    </script> 
    <uc1:Chat runat="server" ID="Chat" />
</asp:Content>
