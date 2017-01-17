/// <reference path="Scripts/jasmine-2.5.2/jasmine.js"/>

describe("MessagesController Test", function () {

    var controller = null,
        spyView = null,
        spyService = null,
        server = {
            sendMessage: function (a, b, c, d) {
            }
        };

    var sender = {},
        albumId = 5;
    
    //Arrange
    beforeEach(function () {
        InitSpyView();
        InitSpyService();
        InitController();        
    });
     
    it("getUsersInfoForDialogForm", function () {

        //Act
        controller.getUsersInfoForDialogForm(sender);

        //Assert
        var info = expect(spyView.getReceiversInfo).toHaveBeenCalledWith(sender);
        expect(spyService.getUsersInfoForDialogForm).toHaveBeenCalledWith(info);
        expect(spyView.createDialogForm).toHaveBeenCalled();
    });

    it("checkRelationshipType", function () {

        //Act
        controller.checkRelationshipType(sender);

        //Assert
        var definition = expect(spyView.getRelationshipDefinition).toHaveBeenCalledWith(sender);
        expect(spyService.addRelationshipDefinition).toHaveBeenCalledWith(definition);
    });
     
    it("goToDialog", function () {

        //Act
        controller.goToDialog(sender);

        //Assert
        var id = expect(spyView.getIdOfDialog).toHaveBeenCalledWith(sender);
        expect(spyService.getDialogByID).toHaveBeenCalledWith(id);
        expect(spyView.showDialog).toHaveBeenCalled();
    });
     
    function InitSpyView() {

        spyView = new MessagesView();
        spyOn(window, 'MessagesView').and.callFake(function () { 
            spyOn(spyView, 'getReceiversInfo');
            spyOn(spyView, 'createDialogForm');
            spyOn(spyView, 'getRelationshipDefinition');
            spyOn(spyView, 'checkDialogID');
            spyOn(spyView, 'showDialog');
            spyOn(spyView, 'getIdOfDialog'); 
            return spyView;
        });
    }
    function InitSpyService() {

        spyService = new MessagesService(),
        successfulAjaxRequest = $.Deferred().resolve({}).promise();
        spyOn(window, 'MessagesService').and.callFake(function () {
            spyOn(spyService, 'getUsersInfoForDialogForm').and.callFake(function (d) { return successfulAjaxRequest; });
            spyOn(spyService, 'addRelationshipDefinition').and.callFake(function (definition) { return successfulAjaxRequest; });
            spyOn(spyService, 'getDialogByID').and.callFake(function (id) { return successfulAjaxRequest; });
            return spyService;
        });
    }
    function InitController() {
        controller = new MessagesController(new MessagesView(), new MessagesService(), server);
    }

})