function MessagesSignalrView() {

    this.addMessageToConversation = addMessageToConversation;
    this.checkOnline = checkOnline;
    this.getAllInterlocutorIDs = getAllInterlocutorIDs;
    this.getDialogID = getDialogID;
    this.getCurrentUserId = getCurrentUserId;
    this.getInterlocutorUserId = getInterlocutorUserId;
    this.getActiveDialogID = getActiveDialogID;
    this.getMessage = getMessage;
    this.removeIcon = removeIcon;
    this.setWhoIsMyCurrentInterlocutor = setWhoIsMyCurrentInterlocutor;

    function getCurrentUserId() {
        var currentUserId = $('#hdnCurrentUserID').val();
        return currentUserId;
    }
    function getInterlocutorUserId() {
        var interlocutorUserId = $('#hdnMyCurrentInterlocutor').val();
        return interlocutorUserId;
    }
    function getActiveDialogID() {
        var dialogID = $('#hdnActiveDialogID').val();
        return dialogID;
    }
    function getMessage() {
        var message = $('textarea').val();
        $('textarea').empty();
        return message;
    }
    function checkOnline(UserID, ConnectionID) {

        var div = $("div.dialogs_user").attr("data-interlocutoruserid", UserID);

        //var div = $("div.dialogs_user[data-interlocutoruserid='" + UserID + "']");

        var onlineSpanElem = $('#' + ConnectionID);
        if (!onlineSpanElem.length)
            div.after("<span id='" + ConnectionID + "' style='color:blue;'><br>Online<span>");
    }
    function removeIcon(ConnectionId) {
        $("span#" + ConnectionId).remove();
    }
    function addMessageToConversation(message) {
        var messageContent = "<tr>" +
                                    "<td>" +
                                       "<span class='UserName'>" + message.InterlocutorUserName + "<br/></span>" +
                                    "</td>" +
                                    "<td></td>" +
                                    "<td></td>" +
                              "</tr>" +
                              "<tr>" +
                                     "<td class='log_author'>" +
                                         "<img src='" + ROOT + message.InterlocutorAvatar + "' />" +
                                     "</td>" +
                                     "<td class='log_body'><p>" + message.MessageText + "</p></td>" +
                                     "<td class='log_date'>" + message.CreateDate + "</td>" +
                               "</tr>";

        $("#conversation").append(messageContent);
        $(".myBox").scrollTop(1000000);
    }
    function getAllInterlocutorIDs() {
        var allInterlocutersIDs = [];
        $('.dialogs_info>div').each(function () {
            var id = $(this).attr('data-interlocutoruserid');
            allInterlocutersIDs.push(id);
        });
        return allInterlocutersIDs;
    }
    function getDialogID(sender) {
        $('#li_lookDialog').addClass('active');
        $('#li_dialogs').removeClass('active');
        var dialogid = $(sender).attr('data-dialogid');
        $('#hdnActiveDialogID').attr('value', dialogid);
        return dialogid;
    }
    function setWhoIsMyCurrentInterlocutor(sender) {
        var interlocutorid = $(sender).attr('data-interlocutoruserid');
        $('#hdnMyCurrentInterlocutor').attr('value', interlocutorid);
    }
} 

