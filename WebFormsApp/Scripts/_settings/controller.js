
function SettingsController(view, service) {
    this.init(view, service);
    this.activate();
}

SettingsController.prototype = function () {

    return { 
        activate: activate,
        getPrivacy: getPrivacy,
        getPrivacyVisibilityLevels: getPrivacyVisibilityLevels,
        init: init,
        updatePrivacyVisibilityLevel: updatePrivacyVisibilityLevel, 
    }
    var VIEW, SERVICE;

    function init(view, service) {
        VIEW = view;
        SERVICE = service;
    }
    function activate() {
        getPrivacyVisibilityLevels();
    }
    function getPrivacyVisibilityLevels() {
        SERVICE.getProfileID()
               .then(function (data, statusText, jqXhr) {
                   var id = data;
                   VIEW.setProfileId(id);
                   SERVICE.getPrivacyVisibilityLevels(id)
                          .then(VIEW.showSavingVisibilityLevels);
               });
    }
    //Cрабатывает, когда пользователь кликает по тегу <select> и выбирает значения видимости раздела.
    // privacyFlagTypeID - id заголовка(названия раздела, например "Кто видит комментарии к записям")
    // visibilityLevelId - уровень видимости(доступности) данного заголовка
    function updatePrivacyVisibilityLevel(privacyFlagTypeID, visibilityLevelId) {

        var privacyFlag = VIEW.getPrivacyFlag(privacyFlagTypeID, visibilityLevelId);
        SERVICE.updatePrivacyFlagForChoosenSection(privacyFlag);
    }
    function getPrivacy(sender) {
        VIEW.getPrivacy(sender);
    }
}()


