<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="EmailVerification.aspx.cs" Inherits="WebFormsApp.EmailVerification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="_Styles" runat="server">
    <link href="Content/email_verification.css" rel="stylesheet" />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder" runat="server">

    <asp:Label ID="lblMessage" ClientIDMode="Static" runat="server" />

</asp:Content>
