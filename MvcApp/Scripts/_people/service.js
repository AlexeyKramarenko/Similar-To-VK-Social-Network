function PeopleService() {

    this.createNewDialog = createNewDialog;
    this.getCountries = getCountries;
    this.getUsersList = getUsersList;
    this.getTownsByCountry = getTownsByCountry;
    this.removeFromFriends = removeFromFriends;

    var ajax = null;

    init();

    function init() {
        ajax = new Ajax().ajaxCall;
    }
    function getCountries() {

        var config = {
            url: "/webapi/people/GetCountries",
            type: 'GET',
            dataType: 'json'
        };
        return ajax(config);
    }
    function getTownsByCountry(id) {

        var config = {
            url: '/webapi/people/GetTownsByCountry/' + id,
            type: 'GET',
            dataType: 'json'
        };
        return ajax(config);
    }
    function removeFromFriends(id) {

        var config = {
            url: '/webapi/people/RemoveFromFriends/' + id,
            type: 'DELETE'
        };
        return ajax(config);
    }
    function createNewDialog(message) {

        var config = {
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: '/WebServices/DialogService.asmx/CreateNewDialog',
            data: JSON.stringify({ message: message }),
            dataType: "json"
        };
        return ajax(config);
    }
    function getUsersList(url) {

        var config = {
            url: url,
            type: 'GET',
            dataType: 'json'
        };
        return ajax(config);
    }
}
 