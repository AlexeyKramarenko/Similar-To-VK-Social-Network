

function ChatSignalrController(hub, client, server, view, service) {

    MessagesSignalrController.apply(this, arguments);

    activate2();

    client.onConnected = function (onlineUsers) {
        view.checkOnlineUsers(onlineUsers);
    };

    function activate2() {
        
        hub.start().done(function () {
            
        });
        
    } 

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
 
 