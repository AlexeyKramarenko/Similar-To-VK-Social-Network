function SettingsService() {

    this.getProfileID = getProfileID;
    this.getPrivacyVisibilityLevels = getPrivacyVisibilityLevels;
    this.updatePrivacyFlagForChoosenSection = updatePrivacyFlagForChoosenSection;

    var baseUri, ajax;

    init();

    function init() {
        ajax = new Ajax().ajaxCall;
        baseUri = "/webapi/Settings/";
    }
    function getProfileID() {

        var config = {
            type: 'GET',
            url: baseUri + "GetProfileID",
            datatype: 'json',
            async: true
        };
        return ajax(config);
    }

    function getPrivacyVisibilityLevels(id) {

        var config = {
            type: 'GET',
            url: "/webapi/Settings/GetPrivacyVisibilityLevels/" + id,
            datatype: 'json'
        };
        return ajax(config);
    }

    function updatePrivacyFlagForChoosenSection(pf) {

        var pftID = pf.PrivacyFlagTypeID;
        var pID = pf.ProfileID;
        var vlID = pf.VisibilityLevelID;

        //put & post call undefined problem
        var config = {
            type: 'GET',
            url: '/Settings/UpdatePrivacyFlag?PrivacyFlagTypeID=' + pftID + '&ProfileID=' + pID + '&VisibilityLevelID=' + vlID
        };

        return ajax(config); 
    }
}
 
