
function PeopleController(view, service) {
    this.init(view, service);
    this.activate();
}

PeopleController.prototype = function () {

    return { 
        activate: activate,
        createNewDialog: createNewDialog,  
        getTownsBySelectedCountry: getTownsBySelectedCountry,
        getUsers: getUsers,
        init: init,
        removeFromFriends: removeFromFriends,
        setRecipientUserID: setRecipientUserID,
        setTownsToDefault: setTownsToDefault,
        updateAgeValue: updateAgeValue,
    }

    var VIEW, SERVICE;

    function init(view, service) {
        VIEW = view;
        SERVICE = service;
    }
    function activate() {
        VIEW.getAgeValues();

        SERVICE.getCountries()
               .then(VIEW.setCountries)
               .then(getTownsBySelectedCountry);

        if (VIEW.getValueByParameterName('UserID') != '0')
            VIEW.setFriendsSearchMode();
        else
            VIEW.setPeopleSearchMode();
        getUsers();
    }
    function getTownsBySelectedCountry() {
        var id = VIEW.getSelectedCountryId();
        SERVICE.getTownsByCountry(id)
               .then(VIEW.addTownsToDropdownlist);
    }
    function updateAgeValue() {
        VIEW.updateAgeValue();
    }
    function setTownsToDefault() {
        VIEW.setTownsToDefault();
    }
    function getUsers() {
        var url = VIEW.constructUsersListUrl();
        SERVICE.getUsersList(url)
               .then(VIEW.showUserList);
    }
    function createNewDialog() {
        var message = VIEW.getCurrentMessage();
        SERVICE.createNewDialog(message)
               .then(VIEW.onSuccessCreateDialog);
    }
    function removeFromFriends(id) {
        SERVICE.removeFromFriends(id)
               .then(function (data, textStatus, jqXHR) { VIEW.removeFromFriendsInMarkup(id) });
        return false;
    }
    function setRecipientUserID(id) {
        VIEW.setRecipientUserID(id);
    }
}()


