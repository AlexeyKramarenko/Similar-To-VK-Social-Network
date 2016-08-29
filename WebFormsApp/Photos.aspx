<%@ Page Title="" Language="C#" MasterPageFile="~/NestedSiteMaster.master" AutoEventWireup="true" CodeBehind="Photos.aspx.cs" Inherits="WebFormsApp.Photos" %>

<asp:Content ContentPlaceHolderID="Styles" runat="server">
    <link href="Content/photos.css" rel="stylesheet" />
    <link href="Content/sweetalert.css" rel="stylesheet" />
</asp:Content>


<asp:Content ContentPlaceHolderID="NestedPlaceHolder" runat="server"> 
    <input type="hidden" id="hdnProfileID" value="" />
    <div class="container" style="margin-top: 25px; border-left: 1px solid grey; border-right: 1px solid grey;"></div>
    <div id="hiddenAlbumLinks"></div>
</asp:Content>



<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script src="Scripts/sweetalert.min.js"></script>
    <script src="Scripts/photos.js"></script>
    <script src="Scripts/jquery-multidownload.js"></script>
</asp:Content>
