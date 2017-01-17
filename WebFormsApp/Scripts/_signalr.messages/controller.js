

function MessagesSignalrController(hub, client, server, view) {

    this.init(hub, client, server, view);
    this.activate();
}

MessagesSignalrController.prototype = function () {
     
    return { 
        activate: activate,
        getClient: getClient,
        getServer: getServer,
        init: init,
        setWhoIsMyCurrentInterlocutor: setWhoIsMyCurrentInterlocutor
    }

    var HUB,
        SERVER,
        CLIENT,
        VIEW;

    function getClient() {
        return CLIENT;
    }
    function getServer() {
        return SERVER;
    }
    function init(hub, client, server, view) {
        HUB = hub;
        CLIENT = client;
        SERVER = server;
        VIEW = view;
        CLIENT.onNewUserConnected = onNewUserConnected;
        CLIENT.onUserDisconnected = onUserDisconnected;
        CLIENT.getMessage = getMessage;
    }
    function activate() {
        HUB.start().done(function () {
            MessagesSignalrController.prototype.goToDialog = goToDialog;
            MessagesSignalrController.prototype.connectToGetOnlineInterlocutors = connectToGetOnlineInterlocutors;
            MessagesSignalrController.prototype.sendMessageToInterlocutor = sendMessageToInterlocutor;
            connectToGetOnlineInterlocutors(); 
        });
    }

    function onNewUserConnected(ConnectedUser) {
        VIEW.checkOnline(ConnectedUser.UserID, ConnectedUser.ConnectionId);
    }
    function onUserDisconnected(ConnectionId) {
        VIEW.removeIcon(ConnectionId);
    }
    function getMessage(message) {
        VIEW.addMessageToConversation(message);
    }
    function goToDialog(sender) {
        var dialogId = VIEW.getDialogID(sender);
        var userId = VIEW.getCurrentUserId();
        SERVER.checkTheReceiverOpenedDialogToRead(userId, dialogId);
    }
    function connectToGetOnlineInterlocutors() {
        var userId = VIEW.getCurrentUserId();
        var ids = VIEW.getAllInterlocutorIDs();
        SERVER.connectToGetOnlineInterlocutors(userId, ids);
    }     
    function sendMessageToInterlocutor() {
        var currentUserId = VIEW.getCurrentUserId();
        var interlocutorUserId = VIEW.getInterlocutorUserId();
        var dialogId = VIEW.getActiveDialogID();
        var message = VIEW.getMessage();
        SERVER.sendMessage(currentUserId, interlocutorUserId, dialogId, message);
    }
    function setWhoIsMyCurrentInterlocutor(sender) {
        VIEW.setWhoIsMyCurrentInterlocutor(sender);
    }
}()