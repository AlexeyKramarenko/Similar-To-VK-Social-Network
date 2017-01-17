function PhotosService() {

    this.attachPhotosToAlbum = attachPhotosToAlbum;
    this.createPhotoAlbum = createPhotoAlbum;
    this.deletePhotoFromAlbum = deletePhotoFromAlbum;
    this.getAlbumsCountByUserID = getAlbumsCountByUserID;
    this.getPhotoUrlsByAlbumID = getPhotoUrlsByAlbumID;
    this.getPhotoAlbums = getPhotoAlbums;
    this.getProfileID = getProfileID;
    this.getPhotosCountByAlbumID = getPhotosCountByAlbumID;
    this.postFile = postFile;
    this.updatePhotoDescription = updatePhotoDescription;

    var ajax = null;

    init();

    function init() {
        ajax = new Ajax().ajaxCall;
    }

    function getPhotoUrlsByAlbumID(albumId) {

        var config = {
            url: '/webapi/photos/GetPhotoUrlsByAlbumID/' + albumId,
            type: 'GET',
            dataType: 'json'
        };
        return ajax(config);
    }
    function updatePhotoDescription(photos) {

        var config = {
            type: 'PUT',
            url: '/webapi/photos/UpdatePhotoDescription',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(photos)
        };
        return ajax(config);
    }
    function deletePhotoFromAlbum(photo) {

        var config = {
            type: "DELETE",
            url: '/webapi/photos/DeletePhotoFromAlbum',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(photo)
        };
        return ajax(config);
    }
    function createPhotoAlbum(photoAlbum) {

        var config = {
            type: 'POST',
            url: '/webapi/photos/CreatePhotoAlbum',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(photoAlbum)
        };
        return ajax(config);
    }
    function getPhotoAlbums() {

        var config = {
            url: '/webapi/photos/GetPhotoAlbums',
            type: 'GET',
            dataType: 'json'
        };
        return ajax(config);
    }
    function getAlbumsCountByUserID() {

        var config = {
            url: '/webapi/photos/GetAlbumsCountByUserID',
            type: 'GET',
            dataType: 'json'
        }
        return ajax(config);
    }
    function getProfileID() {

        var config = {
            type: 'GET',
            url: 'webapi/settings/GetProfileID',
            datatype: 'json'
        };
        return ajax(config);
    }
    function getPhotosCountByAlbumID(albumId) {

        var config = {
            url: '/webapi/photos/GetPhotosCountByAlbumID/' + albumId,
            type: 'GET',
            dataType: 'json'
        };
        return ajax(config);
    }
    function postFile(data) {

        var config = {
            type: "POST",
            url: '/webapi/photos/postfile',
            contentType: false,
            processData: false,
            data: data
        };
        return ajax(config);
    }
    function attachPhotosToAlbum(albumId) {

        var config = {
            type: "POST",
            url: '/webapi/photos/AttachPhotosToAlbum',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(albumId)
        };
        return ajax(config);
    }
}
 