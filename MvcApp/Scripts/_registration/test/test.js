/// <reference path="Scripts/jasmine-2.5.2/jasmine.js"/>


describe("RegistrationController Test", function () {

    var spyView = null,
        spyService = null,
        controller = null,

   //fake arguments
        sender = {},
        data = {},
        albumId = 5;

    //Arrange
    beforeEach(function () { 
        InitSpyView();
        InitSpyService();
        InitController();
    });

    it("getTowns", function () {

        //Act
        controller.getTowns();

        //Assert  
        expect(spyView.defineSelectedCountry).toHaveBeenCalled();
    });
    it("getCountries", function () {

        //Act
        controller.__getCountries();

        //Assert  
        expect(spyService.getCountries).toHaveBeenCalled();
        expect(spyView.addCountriesToDropdownList).toHaveBeenCalled();
    });
    it("setTowns", function () {

        //Act
        controller.__setTowns();

        //Assert  
        var country = expect(spyView.getSelectedCountry).toHaveBeenCalled();
        expect(spyService.getTownsByCountryName).toHaveBeenCalledWith(country);
        expect(spyView.addTownsToDropdownList).toHaveBeenCalled();

    });
    it("setBirthdays", function () {

        //Act
        controller.__setBirthdays();

        //Assert  
        var obj = expect(spyService.getBirthdayModel).toHaveBeenCalled();
        expect(spyView.setBirthDays).toHaveBeenCalledWith(obj);
    });
    it("setTownsListToDefaultState", function () {

        //Act
        controller.__setTownsListToDefaultState();

        //Assert  
        var value = expect(spyService.getDefaultTownValue).toHaveBeenCalled();
        expect(spyView.setTownsToDefaultState).toHaveBeenCalledWith(value);
    });
    function InitSpyView() { 
        spyView = new RegistrationView();
        spyOn(window, 'RegistrationView').and.callFake(function () {
            spyOn(spyView, 'addCountriesToDropdownList');
            spyOn(spyView, 'addTownsToDropdownList');
            spyOn(spyView, 'setBirthDays');
            spyOn(spyView, 'setTownsToDefaultState');
            spyOn(spyView, 'defineSelectedCountry');
            spyOn(spyView, 'getSelectedCountry');
            return spyView;
        });
    }
    function InitSpyService() { 
        spyService = new RegistrationService();
        var successfulAjaxRequest = $.Deferred().resolve({}).promise();
        spyOn(window, 'RegistrationService').and.callFake(function () {
            spyOn(spyService, 'getCountries').and.callFake(function () { return successfulAjaxRequest; });
            spyOn(spyService, 'getTownsByCountryName').and.callFake(function (name) { return successfulAjaxRequest; });
            spyOn(spyService, 'getBirthdayModel');
            spyOn(spyService, 'getDefaultTownValue');
            return spyService;
        });
    }
    function InitController() { 
        controller = new RegistrationController(new RegistrationView(), new RegistrationService());
    }
})