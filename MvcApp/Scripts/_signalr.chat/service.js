function ChatService() {

    this.getDialogByID = getDialogByID;

    var ajax = null;

    init();

    function init() {
        ajax = new Ajax().ajaxCall;
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
 