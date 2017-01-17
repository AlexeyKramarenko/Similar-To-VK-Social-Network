function ChatSignalrView() {
}
ChatSignalrView.prototype = function () {

    return {
        addMessageToConversation: addMessageToConversation, 
        checkOnline: checkOnline,
        checkOnlineUsers: checkOnlineUsers,
        closeChatWindow: closeChatWindow,
        getAllInterlocutorIDs: getAllInterlocutorIDs,
        getDialogID: getDialogID,
        getCurrentUserId: getCurrentUserId,
        getInterlocutorUserId: getInterlocutorUserId,
        getActiveDialogID: getActiveDialogID,
        getMessage: getMessage,
        removeIcon: removeIcon,
        setWhoIsMyCurrentInterlocutor: setWhoIsMyCurrentInterlocutor,
        showDialog: showDialog
    }
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
        var message = $('#messageForm input').val();
        $('#messageForm input').empty();
        return message;
    }    
    function checkOnline(UserID, ConnectionID) { 
        var el = $(".linkToDialog[data-interlocutoruserid='" + UserID + "']");
        el.attr('data-connectionid', ConnectionID);
        el.find('.onlineFlag')
          .addClass('circle');
    } 
    function checkOnlineUsers(users) {
        for (i = 0; i < users.length; i++) {
            var user = users[i];
            if (user.UserID != $('#hdnPageOfUserID').val())
                checkOnline(user.UserID,user.ConnectionId);
        }
    }
    function closeChatWindow() {
        $('#chatContent').addClass('chatInvisible');
    }
    function removeIcon(ConnectionId) {
        $(".linkToDialog[data-connectionid='" + ConnectionId + "']")
            .find('.onlineFlag')
            .removeClass('circle');
    }
    function addMessageToConversation(message) {
        var msg = "<div class='msg'>" +
                      "<div class='user'>" +
                          "<img width='30' height='30' src='../../" + message.InterlocutorAvatar + "' />" +
                          "<span class='date'><b>" + message.InterlocutorUserName + "</b>" + message.CreateDate + "</span>" +
                      "</div><br/><br/>" +
                      "<div class='text currentUsersMsg'>" + message.MessageText + "</div>" +
                  "</div>";
        $('#messages').append(msg);
        $("#messages").scrollTop(1000000);
    }
    function getAllInterlocutorIDs() {
        var allInterlocutersIDs = [];
        $('.linkToDialog').each(function () {
            var id = $(this).attr('data-interlocutoruserid');
            allInterlocutersIDs.push(id);
        });
        return allInterlocutersIDs;
    }
    function getDialogID(sender) {
        var dialogid = $(sender).attr('data-dialogid');
        $('#hdnActiveDialogID').attr('value', dialogid);
        return dialogid;
    }
    function setWhoIsMyCurrentInterlocutor(sender) {
        var interlocutorid = $(sender).attr('data-interlocutoruserid');
        $('#hdnMyCurrentInterlocutor').attr('value', interlocutorid);
    }
    function showDialog(messages) {
        $('#messages').empty();
        $('#chatContent').removeClass('chatInvisible');
        var html = "";
        for (var i = 0; i < messages.d.length; i++) {
            var msg = messages.d[i];
            html +=
                  "<div class='msg'>" +
                      "<div class='user'>" +
                          "<img width='30' height='30' src='../../" + msg.InterlocutorAvatar + "' />" +
                          "<span class='date'>&nbsp;<b>" + msg.InterlocutorUserName + "</b>&nbsp;" + msg.CreateDate + "</span>" +
                      "</div><br/><br/>" +
                      "<div class='text currentUsersMsg'>" + msg.MessageText + "</div>" +
                  "</div>";
        }
        $('#messages').html(html);
        $("#messages").scrollTop(1000000);
        
    }
}()