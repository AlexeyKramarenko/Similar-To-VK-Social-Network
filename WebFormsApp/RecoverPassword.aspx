<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeBehind="RecoverPassword.aspx.cs" Inherits="WebFormsApp._RecoverPassword" %>


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
