/// <reference path="Scripts/jasmine-2.5.2/jasmine.js"/>
/// <reference path="../spyView.js" />

describe("ProfileController Test", function () {

    var spyView, spyService, controller;

    var sender = {};
    var data = {};
    var albumId = 5;


    //Arrange
    beforeEach(function () {
        InitSpyView();
        InitSpyService();
        InitController();
    });

    it("getProfileSchoolTown", function () {

        //Act
        controller.getProfileSchoolTown();

        //Assert 
        expect(spyService.getProfileSchoolTown).toHaveBeenCalled();
        var country = expect(spyView.getSelectedSchoolCountry).toHaveBeenCalled();
        expect(spyService.getProfileTowns).toHaveBeenCalledWith(country);
        expect(spyView.addTownsToDropdownlist).toHaveBeenCalled();
    });

    it("updateUserBirthDay", function () {
        //Act
        controller.updateUserBirthDay();

        //Assert 
        var birthday = expect(spyView.getChangedBirthdayProfile).toHaveBeenCalled();
        expect(spyService.updateUserBirthDay).toHaveBeenCalledWith(birthday);
    });

    it("updateUserBirthMonth", function () {
        //Act
        controller.updateUserBirthMonth();

        //Assert 
        var birthmonth = expect(spyView.getChangedBirthMonthProfile).toHaveBeenCalled();
        expect(spyService.updateUserBirthMonth).toHaveBeenCalledWith(birthmonth);
    });

    it("updateUserBirthYear", function () {
        //Act
        controller.updateUserBirthYear();

        //Assert 
        var birthyear = expect(spyView.getChangedBirthYearProfile).toHaveBeenCalled();
        expect(spyService.updateUserBirthYear).toHaveBeenCalledWith(birthyear);
    });

    it("getProfileBirthdays", function () {
        //Act
        controller.getProfileBirthdays();

        //Assert 
        expect(spyService.getProfileBirthDays).toHaveBeenCalled();
        expect(spyView.isBirthDaysIdHasData).toHaveBeenCalled();
        expect(spyView.addDataToDropdownlist).toHaveBeenCalled();
        expect(spyView.setBirthDaysIdHasData).toHaveBeenCalled();
    });

    it("getProfileSchoolStartYears", function () {
        //Act
        controller.getProfileSchoolStartYears();

        //Assert 
        expect(spyView.isSchoolStartYearsIdHasData).toHaveBeenCalled();
        expect(spyService.getProfileSchoolStartYears).toHaveBeenCalled();
        expect(spyView.addDataToDropdownlist).toHaveBeenCalled();
        expect(spyView.setSchoolStartYearsIdHasData).toHaveBeenCalled();
    });

    function InitSpyView() {

        spyView = new ProfileView();
        spyOn(window, 'ProfileView').and.callFake(function () {
            spyOn(spyView, 'getSelectedSchoolCountry');
            spyOn(spyView, 'addTownsToDropdownlist');
            spyOn(spyView, 'getChangedBirthdayProfile');
            spyOn(spyView, 'getChangedBirthMonthProfile');
            spyOn(spyView, 'getChangedBirthYearProfile');
            spyOn(spyView, 'getChangedEducationData');
            spyOn(spyView, 'updateFinishYears');
            spyOn(spyView, 'goToSelectedTab');
            spyOn(spyView, 'addDataToDropdownlist');
            spyOn(spyView, 'getSchoolStartYear');
            spyOn(spyView, 'getSchoolFinishYear');

            spyOn(spyView, 'isSchoolStartYearsIdHasData').and.returnValue(false);
            spyOn(spyView, 'setSchoolStartYearsIdHasData');

            spyOn(spyView, 'isSchoolFinishYearsIdHasData').and.returnValue(false);
            spyOn(spyView, 'setSchoolFinishYearsIdHasData');

            spyOn(spyView, 'isBirthDaysIdHasData').and.returnValue(false);
            spyOn(spyView, 'setBirthDaysIdHasData');

            spyOn(spyView, 'isBirthMonthsIdHasData').and.returnValue(false);
            spyOn(spyView, 'setBirthMonthsIdHasData');

            spyOn(spyView, 'isBirthYearsIdHasData').and.returnValue(false);
            spyOn(spyView, 'setBirthYearsIdHasData');

            return spyView;
        });
    }
    function InitSpyService() {

        spyService = new ProfileService(),
        successfulAjaxRequest = $.Deferred().resolve({}).promise();

        spyOn(window, 'ProfileService').and.callFake(function () {
            spyOn(spyService, 'getProfileSchoolTown').and.callFake(function () { return successfulAjaxRequest; });
            spyOn(spyService, 'getProfileTowns').and.callFake(function (id) { return successfulAjaxRequest; });
            spyOn(spyService, 'updateUserBirthDay').and.callFake(function (profile) { return successfulAjaxRequest; });
            spyOn(spyService, 'updateUserBirthMonth').and.callFake(function (profile) { return successfulAjaxRequest; });
            spyOn(spyService, 'updateUserBirthYear').and.callFake(function (profile) { return successfulAjaxRequest; });
            spyOn(spyService, 'getProfileBirthDays').and.callFake(function () { return successfulAjaxRequest; });
            spyOn(spyService, 'getProfileBirthMonths').and.callFake(function () { return successfulAjaxRequest; });
            spyOn(spyService, 'getProfileSchoolStartYears').and.callFake(function () { return successfulAjaxRequest; });
            spyOn(spyService, 'getProfileSchoolFinishYears').and.callFake(function () { return successfulAjaxRequest; });
            spyOn(spyService, 'getProfileBirthYears').and.callFake(function () { return successfulAjaxRequest; });
            spyOn(spyService, 'updateProfileEducationData').and.callFake(function (profile) { return successfulAjaxRequest; });
            spyOn(spyService, 'getFinishYears').and.callFake(function (selectedStartYear) { return successfulAjaxRequest; });
            return spyService;
        });
    }
    function InitController() {
        controller = new ProfileController(new ProfileView(), new ProfileService());
    }
})