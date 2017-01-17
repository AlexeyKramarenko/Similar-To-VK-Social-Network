
function PhotosController(view, service) {
    this.init(view, service);
    this.activate();
}
PhotosController.prototype = function () {

    return {
        activate: activate,
        changeColor: changeColor,
        createAlbumForm: createAlbumForm,
        createAlbum: createAlbum,
        deletePhotoFromAlbum: deletePhotoFromAlbum,
        editAlbum: editAlbum,
        getAllAlbumsPanel: getAllAlbumsPanel,
        getPhotoAlbums: getPhotoAlbums,
        getAlbum: getAlbum,
        getAlbumsCountByUserID: getAlbumsCountByUserID,
        getProfileID: getProfileID, 
        init: init,
        multiDownload: multiDownload,
        saveChanges: saveChanges,
        sendPhotos: sendPhotos,
        sendPhotosToAlbum: sendPhotosToAlbum, 
        upload: upload
    }

    var VIEW, SERVICE;

    function init(view, service) {
        VIEW = view;
        SERVICE = service;
    }
    function activate() {
        getAllAlbumsPanel();//создать разметку
        getPhotoAlbums();//добавить ссылки на альбомы
        getAlbumsCountByUserID();
        getProfileID();//скрытое поле
        return false;
    }
    function changeColor(sender) {
        VIEW.changeColor(sender);
    }
    function deletePhotoFromAlbum(sender, albumId) {
        var photo = VIEW.getPhotoCheckedToDelete(sender);
        SERVICE.deletePhotoFromAlbum(photo)
               .then(function (data, textStatus, jqXHR) {
                   VIEW.notificationAboutDeleting();
                   editAlbum(albumId);
               }, onError); 
    }
    function editAlbum(albumId) {
        VIEW.addPhotoTemplate(albumId);
        SERVICE.getPhotoUrlsByAlbumID(albumId)
               .then(function (data) {
                   VIEW.editPhotoTemplate(data, albumId);
               });
        return false;
    }
    function saveChanges() {
        var photos = VIEW.getChangedPhotos();
        SERVICE.updatePhotoDescription(photos);
        return false;
    }
    function createAlbumForm() {
        VIEW.createAlbumForm();
        return false;
    }
    function createAlbum() {
        var photoAlbum = VIEW.getPhotoAlbumInfo();
        VIEW.closeModalWindow();
        SERVICE.createPhotoAlbum(photoAlbum)
               .then(getAlbum);

    }
    function getAlbum(albumId) {
        SERVICE.getPhotosCountByAlbumID(albumId)
               .then(function (data, textStatus, jqXHR) {
                   VIEW.createAlbumPanel(data, albumId);
                   SERVICE.getPhotoUrlsByAlbumID(albumId)
                          .then(VIEW.generateGalleryMarkup);
               });
        return false;
    }
    function upload() {
        VIEW.upload();
        return false;
    }
    function sendPhotos() {
        var image = VIEW.getLoadedImage();
        if (image != null)
            SERVICE.postFile(image)
                   .then(function (data, textStatus, jqXHR) {
                       VIEW.notificationAboutAddingToMainAlbum();
                   });
        return false;
    }
    function multiDownload() {
        VIEW.multiDownload();
        return false;
    }
    function sendPhotosToAlbum(albumId) {
        var image = VIEW.getLoadedImage();
        if (image != null)
            SERVICE.attachPhotosToAlbum(albumId)
                   .then(function (data, textStatus, jqXHR) {
                       SERVICE.postFile(image)
                              .then(function (data, textStatus, jqXHR) {
                                  VIEW.notificationAboutAddingToAlbum();
                                  getAlbum(albumId);
                              })
                   });
        return false;
    }
    function getAllAlbumsPanel() {
        VIEW.getAllAlbumsPanel();
    }
    function getPhotoAlbums() {
        SERVICE.getPhotoAlbums()
               .then(VIEW.showPhotoAlbums)
    }
    function getAlbumsCountByUserID() {
        SERVICE.getAlbumsCountByUserID()
               .then(VIEW.showAlbumsCount);
    }
    function getProfileID() {
        SERVICE.getProfileID()
               .then(VIEW.setProfileId);
    }
    function onError(error) {
        alert(error.statusText);
    }
}()
