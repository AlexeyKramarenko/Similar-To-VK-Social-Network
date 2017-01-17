function MessagesService() {

    this.addRelationshipDefinition = addRelationshipDefinition;
    this.getDialogByID = getDialogByID;
    this.getUsersInfoForDialogForm = getUsersInfoForDialogForm;

    var ajax = null;

    init();

    function init() {
        ajax = new Ajax().ajaxCall;
    }

    function getUsersInfoForDialogForm(d) {

        var config = {
            url: '/WebServices/DialogService.asmx/GetUsersInfoForDialogForm',
            type: 'POST',
            datatype: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(d)
        };
        return ajax(config);
    }

    function addRelationshipDefinition(definition) {

        var config = {
            url: '/WebServices/DialogService.asmx/AddRelationshipDefinition',
            type: 'POST',
            datatype: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(definition)
        };
        return ajax(config);
    }

    function getDialogByID(id) {

        var config = {
            url: '/WebServices/DialogService.asmx/GetDialogByID',
            type: 'GET',
            datatype: 'json',
            contentType: 'application/json; charset=utf-8',
            data: { id: id }
        };
        return ajax(config);
    }
}
 