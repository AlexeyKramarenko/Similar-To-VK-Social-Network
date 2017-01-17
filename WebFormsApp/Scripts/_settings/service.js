function SettingsService() {
    this.init();
}

SettingsService.prototype = function () {

    return { 
        getProfileID: getProfileID,
        getPrivacyVisibilityLevels: getPrivacyVisibilityLevels,
        init: init,
        updatePrivacyFlagForChoosenSection: updatePrivacyFlagForChoosenSection
    }

    var baseUri, ajax;

    function init() {
        ajax = new Ajax().ajaxCall;
        baseUri = "webapi/Settings/";
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
            url: baseUri + "GetPrivacyVisibilityLevels/" + id,
            datatype: 'json'
        };
        return ajax(config);
    }

    function updatePrivacyFlagForChoosenSection(privacyFlag) {

        var config = {
            type: "PUT",
            url: baseUri + "UpdatePrivacyFlagForChoosenSection",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(privacyFlag),
            async: true
        };
        return ajax(config);
    }

    //SERVICE.getProfileID = getProfileID;
    //SERVICE.getPrivacyVisibilityLevels = getPrivacyVisibilityLevels;
    //SERVICE.updatePrivacyFlagForChoosenSection = updatePrivacyFlagForChoosenSection;

}()
