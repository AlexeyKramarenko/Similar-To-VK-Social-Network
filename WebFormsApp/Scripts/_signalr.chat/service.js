function ChatService() {
    this.init();
}

ChatService.prototype = function () {

    return {         
        getDialogByID: getDialogByID ,
        init: init
    }

    var ajax = null;

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
}()