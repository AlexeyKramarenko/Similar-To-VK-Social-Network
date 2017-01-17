<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Chat.ascx.cs" Inherits="WebFormsApp.CustomControls.Chat" %>

<link href="../../Content/chat.css" rel="stylesheet" />
<script src="../../Scripts/jquery-2.1.4.min.js"></script>
<script src="../../Scripts/jquery.signalR-2.2.0.min.js"></script>
<script src="../../signalr/hubs"></script>
<script src="../../Scripts/_signalr.chat/view.js"></script>
<script src="../../Scripts/_signalr.chat/service.js"></script>
<script src="../../Scripts/_signalr.messages/controller.js"></script>
<script src="../../Scripts/_signalr.chat/controller.js"></script>
<script>
    var chat = new ChatSignalrController($.connection.hub, $.connection.chatHub.client, $.connection.chatHub.server, new ChatSignalrView(), new ChatService());
    chat
</script>


<div id="chat">
    <div id="chatContent" class="chatInvisible">
        <div id="close">
            <a href="#" onclick="chat.closeChatWindow()">close</a>
        </div>
        <div id="messages">
        </div>
        <div id="messageForm">
            <input type="text" placeholder="type your message..." />
            <button type="button" onclick="chat.sendMessageToInterlocutor()">Send</button>
        </div>
    </div>

    <asp:ListView
        runat="server"
        ItemType="WebFormsApp.ViewModel.MessagesViewModel"
        SelectMethod="GetDialogsList">
        <LayoutTemplate>
            <asp:HiddenField ID="hdnActiveDialogID" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hdnMyCurrentInterlocutor" ClientIDMode="Static" runat="server" />
            <div id="chatUsers">
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="linkToDialog"
                data-dialogid='<%# Item.DialogID %>'
                data-interlocutoruserid='<%# Item.InterlocutorUserID %>'
                data-connectionid=''
                onclick="chat.setWhoIsMyCurrentInterlocutor(this);
                         chat.goToDialog(this);">
                <span class="onlineFlag"></span>
                <img src='<%# "../" + Item.InterlocutorAvatar %>' runat="server" width="30" height="30">
                <span><%# Item.InterlocutorUserName %></span>
            </div>
        </ItemTemplate>
    </asp:ListView>

</div>






