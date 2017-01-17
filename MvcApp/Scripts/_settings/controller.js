
function SettingsController(VIEW, SERVICE) {
     
    this.getPrivacy = getPrivacy;
    this.getPrivacyVisibilityLevels = getPrivacyVisibilityLevels;
    this.updatePrivacyVisibilityLevel = updatePrivacyVisibilityLevel;

    activate();

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

        return false;
    }
    function getPrivacy(sender) {
        VIEW.getPrivacy(sender);
    }
}
 


