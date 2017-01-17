<%@ Page EnableViewState="false"  Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecoverPasswordPage.aspx.cs" Inherits="WebFormsApp.RecoverPasswordPage" %>


<asp:Content ContentPlaceHolderID="_Styles" runat="server">
    <link href="Content/recoverPassword.css" rel="stylesheet" />
</asp:Content>



<asp:Content ContentPlaceHolderID="PlaceHolder" runat="server">

    <div id="recoverPasswordForm">
        Email:     
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>

        <asp:Button
            ClientIDMode="Static"
            ID="btnRecoverPassword"
            Text="Recover Password"
            runat="server" />

        <br />

        <asp:Label
            ID="lblErrors"
            ClientIDMode="Static"
            runat="server" />
    </div>
</asp:Content>
