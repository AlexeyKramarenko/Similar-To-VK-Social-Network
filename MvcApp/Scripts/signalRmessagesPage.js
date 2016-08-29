$(function () {

    //setScreen(false);

    // Declare a proxy to reference the hub. 
    var chatHub = $.connection.chatHub;

    //клиентксие методы SignalR
    registerClientMethods(chatHub);

    // Start Hub
    $.connection.hub.start().done(function () {

        //серверные методы SignalR
        registerEvents(chatHub)

    });

});

$(document).ready(function () {


    $("a.privacy").click(function () {
        $("#choosePrivacy").remove();
        $('a.privacy').show();
        var id = $(this).attr('id');
        $(this).hide();

        var currentValue = $(this).html();
        $(this).after(

            "<select size='1' id='choosePrivacy'  " +

            "onchange=\"$('a.privacy').show();" +

            //запись в ссылку
            "$('a#" + id + "').html(" +

            //название строки в DDL
            "$('#choosePrivacy').find('option:selected').text()" +

            "); " +

            "$(this).remove();" +
            "" +
            " \"   >" +

            "<option value=\"\" selected disabled>" + currentValue + "</option>" +
            "<option value=\"All users\">Все пользователи</option>" +
            "<option value=\"Option1\">Option 1</option>" +
            "<option value=\"Option2\">Option 2</option>" +
            "</select>"
        );
    });
});


function AddUser(pageUrl, ThumbnailUrl, ConnectionId) {

    var link = "<a href='" + pageUrl + "' id='" + ConnectionId + "' ><img src='" + ThumbnailUrl + "' /></a>";

    $('#onlineFriends').append(link);

}

function CheckOnline(UserID, ConnectionID) {

    var div = $("div.dialogs_user[data-interlocutoruserid='" + UserID + "']");

    var onlineSpanElem = $('#' + ConnectionID);

    if (!onlineSpanElem.length)
        div.after("<span id='" + ConnectionID + "' style='color:blue;'><br>Online<span>");
}

//КЛИЕНТ
function registerClientMethods(chatHub) {

    // вызов для вошедшего только что
    chatHub.client.onConnected = function (users) {//, messages) {

        for (i = 0; i < users.length; i++) {

            if (users[i].UserID != $('#hdnCurrentUserID').val())

                CheckOnline(users[i].UserID, users[i].ConnectionId);
        }
    }

    // оповещение всех онлайн друзей
    chatHub.client.onNewUserConnected = function (ConnectedUser) {

        //добавить иконку вошедшего друга
        //AddUser(ConnectedUser.PageUrl, ConnectedUser.ThumbnailUrl, ConnectedUser.ConnectionId);
        CheckOnline(ConnectedUser.UserID, ConnectedUser.ConnectionId);
    }

    // оповещение всех юзеров, что один ушел
    chatHub.client.onUserDisconnected = function (ConnectionId) {

        //удалить иконку
        $("span#" + ConnectionId).remove();
    }

    chatHub.client.getMessage = function (fromUserId, message, date) {

        $('#conversation').append(message);
    }
}

//СЕРВЕР
function registerEvents(chatHub) {

    var chatHub = $.connection.chatHub;
    var userId = $('#hdnCurrentUserID').val();
    var allInterlocutersIDs = [];

    $('.dialogs_info>div').each(function () {
        var id = $(this).attr('data-interlocutoruserid');
        allInterlocutersIDs.push(id);
    });

    chatHub.server.connectToGetOnlineInterlocutors(userId, allInterlocutersIDs);
}


function goToDialog(el) {

    $('#li_lookDialog').addClass('active');
    $('#li_dialogs').removeClass('active');

    var dialogid = $(el).attr('data-dialogid');
    var interlocutorid = $(el).attr('data-interlocutoruserid');

    $('#hdnActiveDialogID').attr('value', dialogid);
    $('#hdnMyCurrentInterlocutor').attr('value', interlocutorid);


    var userid = $('#hdnCurrentUserID').val();
    var chatHub = $.connection.chatHub;

    chatHub.server.checkTheReceiverOpenedDialogToRead(userid, dialogid);

}
