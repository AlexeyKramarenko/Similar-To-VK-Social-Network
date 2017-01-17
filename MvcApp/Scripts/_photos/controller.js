
function PhotosController(VIEW, SERVICE) {

    this.activate = activate;
    this.changeColor = changeColor;
    this.createAlbumForm = createAlbumForm;
    this.createAlbum = createAlbum;
    this.deletePhotoFromAlbum = deletePhotoFromAlbum;
    this.editAlbum = editAlbum;
    this.getAllAlbumsPanel = getAllAlbumsPanel;
    this.getPhotoAlbums = getPhotoAlbums;
    this.getAlbum = getAlbum;
    this.getAlbumsCountByUserID = getAlbumsCountByUserID;
    this.getProfileID = getProfileID;
    this.multiDownload = multiDownload;
    this.saveChanges = saveChanges;
    this.sendPhotos = sendPhotos;
    this.sendPhotosToAlbum = sendPhotosToAlbum;
    this.upload = upload;

    activate();

    function activate() {
        getAllAlbumsPanel();//создать разметку
        getPhotoAlbums();//добавить ссылки на альбомы
        getAlbumsCountByUserID();
        getProfileID();
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
} 
