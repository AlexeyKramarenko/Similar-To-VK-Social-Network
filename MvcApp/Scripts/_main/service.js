
function MainService() {

    this.addToFriendsMessage = addToFriendsMessage;
    this.createNewDialog = createNewDialog;
    this.getAllPhotosByUserID = getAllPhotosByUserID;
    this.getThumbAvatarImg = getThumbAvatarImg;
    this.getLargeAvatarImg = getLargeAvatarImg;
    this.getPhotosByAlbumID = getPhotosByAlbumID;
    this.deleteStatus = deleteStatus;
    this.deleteComment = deleteComment;
    this.insertComment = insertComment;
    this.insertStatus = insertStatus;
    this.postFile = postFile;

    var ajax = null;
    var baseUri = null;

    init();

    function init() {
        ajax = new Ajax().ajaxCall;
        baseUri = '/webapi/main/';
    }
    function insertComment(comment) {

        var config = {
            url: baseUri + 'InsertComment',
            type: 'POST',
            data: JSON.stringify(comment),
            contentType: "application/json;charset=utf-8"
        };
        return ajax(config);
    }

    function insertStatus(status) {

        var config = {
            url: baseUri + 'InsertStatus',
            type: 'POST',
            data: JSON.stringify(status),
            contentType: 'application/json;charset=utf-8'
        };
        return ajax(config);
    }

    function deleteStatus(id) {

        var config = {
            url: baseUri + 'DeleteStatus/' + parseInt(id),
            type: 'DELETE',
            contentType: "application/json;charset=utf-8"
        };
        return ajax(config);
    }

    function addToFriendsMessage(message) {

        var config = {
            url: baseUri + 'AddToFriendsMessage/',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: JSON.stringify(message)
        };
        return ajax(config);

    }
    function getPhotosByAlbumID(albumId) {

        var config = {
            url: '/webapi/photos/GetPhotosByAlbumID/',
            type: 'GET',
            dataType: 'json',
            data: { albumId: albumId }
        };
        return ajax(config);
    }
    function deleteComment(id) {
        var config = {
            url: baseUri + 'DeleteComment/' + id,
            type: 'DELETE',
            contentType: "application/json;charset=utf-8"
        };
        return ajax(config);
    }

    function getAllPhotosByUserID(userId) {
        var config = {
            url: '/webapi/photos/GetAllPhotosByUserID/',
            type: 'GET',
            dataType: 'json',
            data: { userId: userId }
        };
        return ajax(config);
    }
    function getThumbAvatarImg(userId) {

        var config = {
            type: 'GET',
            url: '/webapi/photos/GetThumbAvatarImg',
            dataType: 'json',
            data: { userId: userId }
        };
        return ajax(config);
    }

    function getLargeAvatarImg(userId) {

        var config = {
            type: 'GET',
            url: '/webapi/photos/GetLargeAvatarImg',
            dataType: 'json',
            data: { userId: userId }
        };
        return ajax(config);
    }

    function postFile(data) {

        var config = {
            type: "POST",
            url: '/webapi/photos/PostFile',
            contentType: false,
            processData: false,
            data: data
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
}
 


