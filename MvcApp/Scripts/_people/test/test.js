/// <reference path="Scripts/jasmine-2.5.2/jasmine.js"/>

describe("PeopleController Test", function () {

    var spyView = null,
        spyService = null,
        controller = null;

    var sender = {};
    var data = {};
    var albumId = 5;


    //Arrange
    beforeEach(function () {
        InitSpyView();
        InitSpyService();
        InitController(); 
    });

    it("activate", function () {

        //Act
        controller.activate();

        //Assert
        expect(spyView.getAgeValues).toHaveBeenCalled();
        expect(spyService.getCountries).toHaveBeenCalled();
        expect(spyView.setCountries).toHaveBeenCalled();
        expect(spyView.setFriendsSearchMode).toHaveBeenCalled();
        expect(spyView.constructUsersListUrl).toHaveBeenCalled();
        expect(spyService.getUsersList).toHaveBeenCalled();
        expect(spyView.showUserList).toHaveBeenCalled();
    });
    it("getTownsBySelectedCountry", function () {

        //Act
        controller.getTownsBySelectedCountry();

        //Assert
        var id = expect(spyView.getSelectedCountryId).toHaveBeenCalled();
        expect(spyService.getTownsByCountry).toHaveBeenCalledWith(id);
        expect(spyView.addTownsToDropdownlist).toHaveBeenCalled();
    });
    it("updateAgeValue", function () {

        //Act
        controller.updateAgeValue();

        expect(spyView.updateAgeValue).toHaveBeenCalled();

    });
    it("setTownsToDefault", function () {

        //Act
        controller.setTownsToDefault();

        expect(spyView.setTownsToDefault).toHaveBeenCalled();

    });
    it("getUsers", function () {

        //Act
        controller.getUsers();

        //Assert
        var url = expect(spyView.constructUsersListUrl).toHaveBeenCalled();
        expect(spyService.getUsersList).toHaveBeenCalledWith(url);
        expect(spyView.showUserList).toHaveBeenCalled();

    });
    it("createNewDialog", function () {

        //Act
        controller.createNewDialog();

        //Assert
        var message = expect(spyView.getCurrentMessage).toHaveBeenCalled();
        expect(spyService.createNewDialog).toHaveBeenCalledWith(message);
        expect(spyView.onSuccessCreateDialog).toHaveBeenCalled();
    });
    it("removeFromFriends", function () {

        var id = 5;

        //Act
        controller.removeFromFriends(id);

        //Assert 
        expect(spyService.removeFromFriends).toHaveBeenCalledWith(id);
        expect(spyView.removeFromFriendsInMarkup).toHaveBeenCalledWith(id);
    });
    function InitSpyView() {

        spyView = new PeopleView();
        spyOn(window, 'PeopleView').and.callFake(function () {
            spyOn(spyView, 'setCountries');
            spyOn(spyView, 'getSelectedCountryId');
            spyOn(spyView, 'addTownsToDropdownlist');
            spyOn(spyView, 'removeFromFriendsInMarkup');
            spyOn(spyView, 'setRecipientUserID');
            spyOn(spyView, 'getCurrentMessage');
            spyOn(spyView, 'onSuccessCreateDialog');
            spyOn(spyView, 'setPeopleSearchMode');
            spyOn(spyView, 'setFriendsSearchMode');
            spyOn(spyView, 'showUserList');
            spyOn(spyView, 'getAgeValues');
            spyOn(spyView, 'getValueByParameterName');
            spyOn(spyView, 'updateAgeValue');
            spyOn(spyView, 'setTownsToDefault');
            spyOn(spyView, 'constructUsersListUrl');
            return spyView;
        });
    }
    function InitSpyService() {

        spyService = new PeopleService(),
        successfulAjaxRequest = $.Deferred().resolve({}).promise();
        spyOn(window, 'PeopleService').and.callFake(function () {

            spyOn(spyService, 'getCountries').and.callFake(function () { return successfulAjaxRequest; });
            spyOn(spyService, 'getTownsByCountry').and.callFake(function (id) { return successfulAjaxRequest; });
            spyOn(spyService, 'removeFromFriends').and.callFake(function (id) { return successfulAjaxRequest; });
            spyOn(spyService, 'createNewDialog').and.callFake(function (message) { return successfulAjaxRequest; });
            spyOn(spyService, 'getUsersList').and.callFake(function (url) { return successfulAjaxRequest; });
            return spyService;
        });
    }
    function InitController() {
        controller = new PeopleController(new PeopleView(), new PeopleService());
    } 
})