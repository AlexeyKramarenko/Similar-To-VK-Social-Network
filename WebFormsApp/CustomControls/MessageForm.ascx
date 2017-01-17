<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageForm.ascx.cs" Inherits="WebFormsApp.CustomControls.MessageForm" %>


<div id="createMessages">
    <asp:Label
        ID="Label1"
        CssClass="messageForm"
        ClientIDMode="Static"
        ForeColor="#45688E"
        Font-Bold="true"
        runat="server"></asp:Label>
    <br />
    <textarea
        rows="2"
        cols="20"
        id="txtMessage"
        class="messageForm"
        onclick="vm.createPostForm(this)"
        style="width: 370px; resize: none; z-index: 1;">
	 </textarea>
</div>
