<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="WebFormsApp._404" %>

<asp:Content ContentPlaceHolderID="_Styles" runat="server">
    <style>
        span {
            margin-left: auto;
            margin-right: auto;
            display: block;
            width: 100px;
            font-size: large;
            font-weight: bold;
            margin-top: 60px;
        }   
    </style>
</asp:Content>



<asp:Content ContentPlaceHolderID="PlaceHolder" runat="server">

    <span>404 Error</span>

</asp:Content>
