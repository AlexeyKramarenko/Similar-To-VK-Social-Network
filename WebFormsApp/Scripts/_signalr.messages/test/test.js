/// <reference path="Scripts/jasmine-2.5.2/jasmine.js"/>

describe("MessagesController Test", function () {

    var spyView = null,
        spyServer = null,
        controller = null,

        sender = {};

    //stubs
    var server = {
        checkTheReceiverOpenedDialogToRead: function (userId, dialogId) { },
        connectToGetOnlineInterlocutors: function (userId, ids) { }
    },
    hub = {
        start: function () { return $.Deferred().resolve({}).promise(); }
    },
    client = {};

    //Arrange
    beforeEach(function () {
        InitSpyServer();
        InitSpyView();
        InitController();
    });    
    it("onNewUserConnected", function () {

        //Arrange
        var ConnectedUser = {
            UserID: 1,
            ConnectionId: 1
        }
        controller.getClient().onNewUserConnected(ConnectedUser);

        //Assert
        expect(spyView.checkOnline).toHaveBeenCalledWith(ConnectedUser.UserID, ConnectedUser.ConnectionId);

    });
    it("onUserDisconnected", function () {

        //Arrange   
        var connectionId = 1;

        //Act
        controller.getClient().onUserDisconnected(connectionId);

        //Assert
        expect(spyView.removeIcon).toHaveBeenCalledWith(connectionId);

    });
    it("getMessage", function () {

        //Arrange   
        var message = 'awesome message';

        //Act
        controller.getClient().getMessage(message);

        //Assert
        //expect(spyClient.onNewUserConnected).toHaveBeenCalled();
        expect(spyView.addMessageToConversation).toHaveBeenCalledWith(message);
    });
    it("goToDialog", function () {

        //Act
        controller.goToDialog(sender);

        //Assert
        var dialogId = expect(spyView.getDialogID).toHaveBeenCalledWith(sender);
        var userId = expect(spyView.getCurrentUserId).toHaveBeenCalled();
        expect(controller.getServer().checkTheReceiverOpenedDialogToRead).toHaveBeenCalledWith(userId, dialogId);


    });
    it("connectToGetOnlineInterlocutors", function () {

        //Act
        controller.connectToGetOnlineInterlocutors();

        //Assert
        var ids = expect(spyView.getAllInterlocutorIDs).toHaveBeenCalled();
        var userId = expect(spyView.getCurrentUserId).toHaveBeenCalled();
        expect(spyServer.connectToGetOnlineInterlocutors).toHaveBeenCalledWith(userId, ids);
    });
    function InitController() {
        controller = new MessagesSignalrController(hub, client, server, new MessagesSignalrView());
    }
    function InitSpyView() {
        spyView = new MessagesSignalrView();
        spyOn(window, 'MessagesSignalrView').and.callFake(function () {
            spyOn(spyView, 'getCurrentUserId');
            spyOn(spyView, 'checkOnline');
            spyOn(spyView, 'removeIcon');
            spyOn(spyView, 'addMessageToConversation');
            spyOn(spyView, 'getAllInterlocutorIDs');
            spyOn(spyView, 'getDialogID');
            spyOn(spyView, 'setWhoIsMyCurrentInterlocutor');
            return spyView;
        });
    }
    function InitSpyServer() {
        spyServer = server;
        spyOn(spyServer, 'checkTheReceiverOpenedDialogToRead');
        spyOn(spyServer, 'connectToGetOnlineInterlocutors');
    }

})