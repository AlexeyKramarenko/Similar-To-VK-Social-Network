

function ChatSignalrController(hub, client, server, view, service) {

    MessagesSignalrController.apply(this, arguments);

    client.onConnected = function (onlineUsers) {
        view.checkOnlineUsers(onlineUsers);
    };

    this.VIEW = view;
    this.SERVICE = service;
}

ChatSignalrController.prototype = Object.create(MessagesSignalrController.prototype);

ChatSignalrController.constructor = ChatSignalrController;

ChatSignalrController.prototype.closeChatWindow = function () {
    this.VIEW.closeChatWindow();
}
ChatSignalrController.prototype.goToDialog = function (sender) {
    MessagesSignalrController.prototype.goToDialog.apply(this, arguments);
    var dialogId = this.VIEW.getDialogID(sender);
    this.SERVICE.getDialogByID(dialogId)
                .then(this.VIEW.showDialog);
}
 

//ChatSignalrController.prototype = function () {

//    return { 
//        activate: activate,
//        closeChatWindow: closeChatWindow,//+1
//        getClient: getClient,
//        getServer: getServer,
//        init: init,
//        setWhoIsMyCurrentInterlocutor: setWhoIsMyCurrentInterlocutor
//    }

//    var HUB,
//        SERVER,
//        SERVICE,//+1
//        CLIENT,
//        VIEW;

//    function getClient() {
//        return CLIENT;
//    }
//    function getServer() {
//        return SERVER;
//    }
//    function init(hub, client, server, view, service) {
//        HUB = hub;
//        CLIENT = client;
//        SERVER = server;
//        SERVICE = service;//+1
//        VIEW = view;
//        CLIENT.onConnected = onConnected;//+1
//        CLIENT.onNewUserConnected = onNewUserConnected;
//        CLIENT.onUserDisconnected = onUserDisconnected;
//        CLIENT.getMessage = getMessage;
//    }
//    function activate() {
//        HUB.start().done(function () {
//            ChatSignalrController.prototype.goToDialog = goToDialog;
//            ChatSignalrController.prototype.connectToGetOnlineInterlocutors = connectToGetOnlineInterlocutors;
//            ChatSignalrController.prototype.sendMessageToInterlocutor = sendMessageToInterlocutor;
//            connectToGetOnlineInterlocutors();
//        });
//    }
//    function onConnected(onlineUsers) {//+1
//        VIEW.checkOnlineUsers(onlineUsers);
//    }
//    function onNewUserConnected(ConnectedUser) {
//        VIEW.checkOnline(ConnectedUser.UserID, ConnectedUser.ConnectionId);
//    }
//    function onUserDisconnected(ConnectionId) {
//        VIEW.removeIcon(ConnectionId);
//    }
//    function getMessage(message) {
//        VIEW.addMessageToConversation(message);
//    }
//    function goToDialog(sender) {

//        var dialogId = VIEW.getDialogID(sender);
//        SERVICE.getDialogByID(dialogId)
//               .then(VIEW.showDialog);

//        var userId = VIEW.getCurrentUserId();//+1
//        SERVER.checkTheReceiverOpenedDialogToRead(userId, dialogId);//+1
//    }
//    function connectToGetOnlineInterlocutors() {
//        var userId = VIEW.getCurrentUserId();
//        var ids = VIEW.getAllInterlocutorIDs();
//        SERVER.connectToGetOnlineInterlocutors(userId, ids);
//    }
//    function sendMessageToInterlocutor() {
//        var currentUserId = VIEW.getCurrentUserId();
//        var interlocutorUserId = VIEW.getInterlocutorUserId();
//        var dialogId = VIEW.getActiveDialogID();
//        var message = VIEW.getMessage();
//        SERVER.sendMessage(currentUserId, interlocutorUserId, dialogId, message);
//    }
//    function setWhoIsMyCurrentInterlocutor(sender) {
//        VIEW.setWhoIsMyCurrentInterlocutor(sender);
//    }
//    function closeChatWindow() {//+1
//        VIEW.closeChatWindow();
//    }
//}()