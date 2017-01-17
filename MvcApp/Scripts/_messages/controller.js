
function MessagesController(VIEW, SERVICE) {

    this.checkRelationshipType = checkRelationshipType;
    this.getUsersInfoForDialogForm = getUsersInfoForDialogForm;
    this.goToDialog = goToDialog;

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
}
