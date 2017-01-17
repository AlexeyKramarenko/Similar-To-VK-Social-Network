
function MessagesController(view, service) {
    this.init(view, service);
}

MessagesController.prototype = function () {

    return { 
        checkRelationshipType: checkRelationshipType,
        getUsersInfoForDialogForm: getUsersInfoForDialogForm,
        goToDialog: goToDialog,
        init: init,
    }

    var VIEW,
        SERVICE;

    function init(view, service) {
        VIEW = view;
        SERVICE = service;
    }
    function getUsersInfoForDialogForm(sender) {
        var info = VIEW.getReceiversInfo(sender);
        SERVICE.getUsersInfoForDialogForm(info)
               .then(VIEW.createDialogForm);
    }
    function checkRelationshipType(sender) {
        var definition = VIEW.getRelationshipDefinition(sender);
        SERVICE.addRelationshipDefinition(definition);
        return false;
    }
    function goToDialog(sender) {
        var id = VIEW.getIdOfDialog(sender);
        SERVICE.getDialogByID(id)
               .then(VIEW.showDialog);
    }
}()
