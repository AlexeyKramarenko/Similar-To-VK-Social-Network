$(function () {

    //setScreen(false);

    // Declare a proxy to reference the hub. 
    var chatHub = $.connection.chatHub;

    registerClientMethods(chatHub);

    // Start Hub
    $.connection.hub.start().done(function () {

        //серверные методы SignalR
        registerEvents(chatHub)

    });

});

function addUser(pageUrl, ThumbnailUrl, ConnectionId) {

    var link = "<a href='" + pageUrl + "' id='" + ConnectionId + "' ><img src='" + ThumbnailUrl + "' /></a>";

    $('#onlineFriends').append(link);

}



//КЛИЕНТ
function registerClientMethods(chatHub) {

    // вызов для вошедшего только что
    chatHub.client.onConnected = function (allUsers) {//, messages) {

        for (i = 0; i < allUsers.length; i++) {

            if (allUsers[i].UserID != $('#hdnUserID').val())

                addUser(allUsers[i].PageUrl, allUsers[i].ThumbnailUrl, allUsers[i].ConnectionId);
        }
    }

    // оповещение всех онлайн друзей
    chatHub.client.onNewUserConnected = function (ConnectedUser) {

        //добавить иконку вошедшего друга
        addUser(ConnectedUser.PageUrl, ConnectedUser.ThumbnailUrl, ConnectedUser.ConnectionId);
    }


    // оповещение всех юзеров, что один ушел
    chatHub.client.onUserDisconnected = function (ConnectionId) {

        //удалить иконку
        $("#" + ConnectionId).remove();
    }
}

//СЕРВЕР
function registerEvents(chatHub) {

    var userId = $('#hdnUserID').val();
    chatHub.server.connectToGetFriends(userId);
}