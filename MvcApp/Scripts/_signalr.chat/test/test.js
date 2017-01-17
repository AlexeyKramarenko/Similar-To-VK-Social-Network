/// <reference path="Scripts/jasmine-2.5.2/jasmine.js"/>

describe("ChatSignalrController Test", function () {

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
    it("closeChatWindow", function () {

        //Act
        controller.closeChatWindow();

        //Assert
        expect(spyView.closeChatWindow).toHaveBeenCalled();
    });
    it("goToDialog", function () {

        //Act
        controller.goToDialog(sender);

        //Assert
        var id = expect(spyView.getDialogID).toHaveBeenCalledWith(sender);
    });
    function InitController() {
        controller = new ChatSignalrController(hub, client, server, new ChatSignalrView(), new ChatService());
    }
    function InitSpyView() {
        spyView = new ChatSignalrView();
        spyOn(window, 'ChatSignalrView').and.callFake(function () {
            spyOn(spyView, 'getDialogID');
            spyOn(spyView, 'closeChatWindow');
            return spyView;
        });
    }
    function InitSpyServer() {
        spyServer = server;
        spyOn(spyServer, 'checkTheReceiverOpenedDialogToRead');
        spyOn(spyServer, 'connectToGetOnlineInterlocutors');
    }

})