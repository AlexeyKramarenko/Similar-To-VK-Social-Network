
function MainController(view, service) {
    this.init(view, service);
    this.activate();
}

MainController.prototype = function () {

    return { 
        activate: activate,
        addToFriends: addToFriends,
        changeColor: changeColor,
        createPostForm: createPostForm,
        createCommentsForm: createCommentsForm,
        createNewDialog: createNewDialog,
        deletePost: deletePost,
        deleteComment: deleteComment,
        getAllPhotosByUserID: getAllPhotosByUserID,
        getPhotosByAlbumID: getPhotosByAlbumID,
        getLargeAvatarImg: getLargeAvatarImg,
        getThumbAvatarImg: getThumbAvatarImg,
        init: init,
        insertComment: insertComment,
        insertStatus: insertStatus,
        sendAvatar: sendAvatar,
        setRecipientUserID: setRecipientUserID,
        showHideDetails: showHideDetails,
        scrollToCommentsForm: scrollToCommentsForm,
        showAllComments: showAllComments,
        upload: upload
    }
    var VIEW, SERVICE;

    function init(view, service) {
        VIEW = view;
        SERVICE = service;
    }
    function activate() {
        VIEW.displayCommentAnchors();
        getAllPhotosByUserID(VIEW.getUserIdOfPageOwner());
        getThumbAvatarImg();
        VIEW.runModalWindowOnAvatarClick();
    }
    function insertComment(sender) {

        var comment = VIEW.getComment(sender);
        SERVICE.insertComment(comment)
              .then(function (data, textStatus, jqXHR) { insertCommentSucceded(data, sender); },
              onError);
        return false;
    }
    function insertStatus() {
        var status = VIEW.getStatus();       
        SERVICE.insertStatus(status)
               .then(insertStatusSucceded);
    }
    function deletePost(sender) {
        var statusId = VIEW.getStatusId(sender);
        SERVICE.deleteStatus(statusId)
               .then(function (data, textStatus, jqXHR) { VIEW.deletePostsMarkup(sender) },
               onError);
    }
    function addToFriends() {
        var message = VIEW.getInvitationMessage();
        SERVICE.addToFriendsMessage(message);
        return false;
    }
    function getPhotosByAlbumID(albumId) {
        VIEW.clearContainer();
        SERVICE.getPhotosByAlbumID(albumId)
               .then(VIEW.addAlbumGalleryMarkup,
               onError);
        return false;
    }
    function deleteComment(sender) {
        var commentId = VIEW.getCommentId(sender);

        SERVICE.deleteComment(commentId)
               .then(function (data, textStatus, jqXHR) { VIEW.deleteCommentMarkup(sender) }, onError);
    }
    function getAllPhotosByUserID() {
        VIEW.clearContainer();
        var userId = VIEW.getUserIdOfPageOwner();
        SERVICE.getAllPhotosByUserID(userId)
               .then(getAllPhotosByUserIDSucceded);
    }
    function getThumbAvatarImg() {
        var userId = VIEW.getUserIdOfPageOwner();
        SERVICE.getThumbAvatarImg(userId)
               .then(VIEW.setAvatar, onError);
    }
    function getLargeAvatarImg() {
        var userId = VIEW.getUserIdOfPageOwner();
        SERVICE.getLargeAvatarImg(userId)
               .then(VIEW.setLargeAvatar, onError);
    }
    function sendAvatar() {
        var image = VIEW.getLoadedImage();
        SERVICE.postFile(image)
               .then(getThumbAvatarImg, onError);
        return false;
    }
    function scrollToCommentsForm(sender) {
        VIEW.scrollToCommentsForm(sender);
    }
    function changeColor(sender) {
        VIEW.changeColor(sender);
    }
    function insertCommentSucceded(data, sender) {
        var template = VIEW.getCommentTemplate(data);
        VIEW.addCommentToWall(sender, template);
    }
    function insertStatusSucceded(data) {

        VIEW.addPostMarkup(data.Message, data.ID, data.UserName, data.Avatar);
        VIEW.clearMessageTextarea();
        VIEW.changePostsCount(+1);
    }
    function getAllPhotosByUserIDSucceded(data) {
        if (data != null)
            VIEW.addGalleryMarkup(data);
    }
    function upload() {
        VIEW.upload();
    }
    function createPostForm(sender) {
        VIEW.createPostForm(sender);
    }
    function showHideDetails() {
        VIEW.showHideDetails();
    }
    function createCommentsForm(sender) {
        VIEW.createCommentsForm(sender);
    }
    function showAllComments(sender) {
        VIEW.showAllComments(sender);
    }
    function onError(error) {
        VIEW.onError(error.statusText);
    }

    function setRecipientUserID(id) {
        VIEW.setRecipientUserID(id);
    }
    function createNewDialog() {
        var message = VIEW.getCurrentMessage();
        SERVICE.createNewDialog(message)
               .then(VIEW.onSuccessCreateDialog);
    }
}()

