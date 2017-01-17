  
describe("SettingsController Test", function () {

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

    it("getPrivacyVisibilityLevels", function () {

        //Act
        controller.getPrivacyVisibilityLevels();

        //Assert 
        expect(spyService.getProfileID).toHaveBeenCalled();
        expect(spyView.setProfileId).toHaveBeenCalled();
        expect(spyService.getPrivacyVisibilityLevels).toHaveBeenCalled();
        expect(spyView.showSavingVisibilityLevels).toHaveBeenCalled();
    });
    it("updatePrivacyVisibilityLevel", function () {

        var privacyFlagTypeID = {},
            visibilityLevelId = {};
        //Act
        controller.updatePrivacyVisibilityLevel(privacyFlagTypeID, visibilityLevelId);

        //Assert  
        var privacyFlag = expect(spyView.getPrivacyFlag).toHaveBeenCalledWith(privacyFlagTypeID, visibilityLevelId);
        expect(spyService.updatePrivacyFlagForChoosenSection).toHaveBeenCalled();
    });
    it("getPrivacy", function () {
 
        //Act
        controller.getPrivacy(sender);

        //Assert  
        expect(spyView.getPrivacy).toHaveBeenCalledWith(sender);
        
    });
    function InitSpyView() {
        spyView = new SettingsView();
        spyOn(window, 'SettingsView').and.callFake(function () {
            spyOn(spyView, 'setProfileId').and.callFake(function (id) { });
            spyOn(spyView, 'showSavingVisibilityLevels').and.callFake(function (data) { });
            spyOn(spyView, 'getPrivacyFlag').and.callFake(function (privacyFlagTypeID, visibilityLevelId) { return {}; });
            spyOn(spyView, 'getPrivacy').and.callFake(function (sender) { });
            return spyView;
        });
    }
    function InitSpyService() {
        spyService = new SettingsService();
        var successfulAjaxRequest = $.Deferred().resolve({}).promise();
        spyOn(window, 'SettingsService').and.callFake(function () {
            spyOn(spyService, 'getProfileID').and.callFake(function () { return successfulAjaxRequest; });
            spyOn(spyService, 'getPrivacyVisibilityLevels').and.callFake(function (photos) { return successfulAjaxRequest; });
            spyOn(spyService, 'updatePrivacyFlagForChoosenSection').and.callFake(function (privacyFlag) { return successfulAjaxRequest; });
            return spyService;
        });
    }
     
    function InitController() {
        controller = new SettingsController(new SettingsView(), new SettingsService());
    }
    
})