
function PeopleController(VIEW, SERVICE) {
    this.activate = activate;
    this.createNewDialog = createNewDialog;
    this.getTownsBySelectedCountry = getTownsBySelectedCountry;
    this.getUsers = getUsers;
    this.removeFromFriends = removeFromFriends;
    this.setRecipientUserID = setRecipientUserID;
    this.setTownsToDefault = setTownsToDefault;
    this.updateAgeValue = updateAgeValue;

    activate();

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
               .then(VIEW.showUserList, function (data) {
               });
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
}
 

