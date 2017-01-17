/// <reference path="Scripts/jasmine-2.5.2/jasmine.js"/>

describe("PhotoController Test", function () {

    var spyView, spyService, controller;

    var sender = {};
    var data = {};
    var albumId = 5;


    //Arrange
    beforeEach(function () {
        InitSpyView();
        InitSpyService();
        InitController();         
    });

    it("editAlbum should return edit template", function () {

        //Act
        controller.editAlbum(albumId);

        //Assert
        expect(spyView.addPhotoTemplate).toHaveBeenCalledWith(albumId);
        expect(spyService.getPhotoUrlsByAlbumID).toHaveBeenCalledWith(albumId);
        expect(spyView.editPhotoTemplate).toHaveBeenCalled();
    });

    it("changeColor calls spyView", function () {

        //Act
        controller.changeColor(sender);

        //Assert
        expect(spyView.changeColor).toHaveBeenCalledWith(sender);
    });

    it("deletePhotoFromAlbum calls spyService", function () {

        //Act
        controller.deletePhotoFromAlbum(sender, albumId);

        //Assert
        var photo = expect(spyView.getPhotoCheckedToDelete).toHaveBeenCalledWith(sender);
        expect(spyView.getPhotoCheckedToDelete).not.toBeNull();
        expect(spyService.deletePhotoFromAlbum).toHaveBeenCalledWith(photo);
        expect(spyView.notificationAboutDeleting).toHaveBeenCalled();
    });

    it("editAlbum returns editPhotoTemplate", function () {

        //Act
        controller.editAlbum(albumId);

        //Assert
        expect(spyView.addPhotoTemplate).toHaveBeenCalledWith(albumId);
        expect(spyService.getPhotoUrlsByAlbumID).toHaveBeenCalledWith(albumId);
        expect(spyView.editPhotoTemplate).toHaveBeenCalled();
    });
    it("saveChanges calls updatePhotoDescription", function () {

        //Act
        controller.saveChanges();

        //Assert
        var photos = expect(spyView.getChangedPhotos).toHaveBeenCalled();
        expect(spyView.getChangedPhotos).not.toBeNull();
        expect(spyService.updatePhotoDescription).toHaveBeenCalledWith(photos);
    });
    it("createAlbumForm", function () {

        //Act
        controller.createAlbumForm();

        //Assert
        expect(spyView.createAlbumForm).toHaveBeenCalled();
    });
    it("createAlbum calls createPhotoAlbum", function () {

        //Act
        controller.createAlbum();

        //Assert
        var photoAlbum = expect(spyView.getPhotoAlbumInfo).toHaveBeenCalled();
        expect(spyView.getPhotoAlbumInfo).not.toBeNull();
        expect(spyView.closeModalWindow).toHaveBeenCalled();
        expect(spyService.createPhotoAlbum).toHaveBeenCalledWith(photoAlbum);
    });
    it("getAlbum", function () {

        //Act
        controller.getAlbum(albumId);

        //Assert
        expect(spyService.getPhotosCountByAlbumID).toHaveBeenCalledWith(albumId);
        expect(spyView.createAlbumPanel).toHaveBeenCalledWith(data, albumId);
        expect(spyService.getPhotoUrlsByAlbumID).toHaveBeenCalledWith(albumId);
        expect(spyView.generateGalleryMarkup).toHaveBeenCalled();
    });
    it("upload", function () {

        //Act
        controller.upload();

        //Assert
        expect(spyView.upload).toHaveBeenCalled();
    });
    it("sendPhotos", function () {

        //Act
        controller.sendPhotos();

        //Assert
        var image = expect(spyView.getLoadedImage).toHaveBeenCalled();
        expect(spyService.postFile).toHaveBeenCalled();
        expect(spyView.notificationAboutAddingToMainAlbum).toHaveBeenCalled();
    });

    it("sendPhotosToAlbum", function () {

        //Act
        controller.sendPhotosToAlbum(albumId);

        //Assert
        var image = expect(spyView.getLoadedImage).toHaveBeenCalled();
        expect(spyView.getLoadedImage).not.toBeNull();
        expect(spyService.attachPhotosToAlbum).toHaveBeenCalledWith(albumId);
        expect(spyService.postFile).toHaveBeenCalled();
        expect(spyView.notificationAboutAddingToAlbum).toHaveBeenCalled();
    });

    it("getPhotoAlbums", function () {

        //Act
        controller.getPhotoAlbums();

        //Assert        
        expect(spyService.getPhotoAlbums).toHaveBeenCalled();
        expect(spyView.showPhotoAlbums).toHaveBeenCalled();
    });

    it("getAlbumsCountByUserID", function () {

        //Act
        controller.getAlbumsCountByUserID();

        //Assert
        expect(spyService.getAlbumsCountByUserID).toHaveBeenCalled();
        expect(spyView.showAlbumsCount).toHaveBeenCalled();
    });

    function InitSpyView() {

        spyView = new PhotosView();
        spyOn(window, 'PhotosView').and.callFake(function () {
            spyOn(spyView, 'changeColor');
            spyOn(spyView, 'addPhotoTemplate');
            spyOn(spyView, 'editPhotoTemplate');
            spyOn(spyView, 'getChangedPhotos');
            spyOn(spyView, 'getPhotoCheckedToDelete');
            spyOn(spyView, 'getPhotoAlbumInfo');
            spyOn(spyView, 'closeModalWindow');
            spyOn(spyView, 'getAllAlbumsPanel');
            spyOn(spyView, 'upload');
            spyOn(spyView, 'showPhotoAlbums');
            spyOn(spyView, 'showAlbumsCount');
            spyOn(spyView, 'setProfileId');
            spyOn(spyView, 'createAlbumPanel');
            spyOn(spyView, 'generateGalleryMarkup');
            spyOn(spyView, 'getLoadedImage').and.callFake(function () {
                return {};
            });
            spyOn(spyView, 'multiDownload');
            spyOn(spyView, 'createAlbumForm');
            spyOn(spyView, 'notificationAboutDeleting');
            spyOn(spyView, 'notificationAboutAddingToMainAlbum');
            spyOn(spyView, 'notificationAboutAddingToAlbum');
            return spyView;
        });
    }
    function InitSpyService() {

        spyService = new PhotosService(),
        successfulAjaxRequest = $.Deferred().resolve({}).promise();
        spyOn(window, 'PhotosService').and.callFake(function () {
            spyOn(spyService, 'getPhotoUrlsByAlbumID').and.callFake(function (id) { return successfulAjaxRequest; });
            spyOn(spyService, 'updatePhotoDescription').and.callFake(function (photos) { return successfulAjaxRequest; });
            spyOn(spyService, 'deletePhotoFromAlbum').and.callFake(function (photo) { return successfulAjaxRequest; });
            spyOn(spyService, 'createPhotoAlbum').and.callFake(function (photoAlbum) { return successfulAjaxRequest; });
            spyOn(spyService, 'getPhotoAlbums').and.callFake(function () { return successfulAjaxRequest; });
            spyOn(spyService, 'getAlbumsCountByUserID').and.callFake(function () { return successfulAjaxRequest; });
            spyOn(spyService, 'getProfileID').and.callFake(function () { return successfulAjaxRequest; });
            spyOn(spyService, 'getPhotosCountByAlbumID').and.callFake(function (albumId) { return successfulAjaxRequest; });
            spyOn(spyService, 'postFile').and.callFake(function (image) { return successfulAjaxRequest; });
            spyOn(spyService, 'attachPhotosToAlbum').and.callFake(function (albumId) { return successfulAjaxRequest; });
            return spyService;
        });
    }
    function InitController() {
        controller = new PhotosController(new PhotosView(), new PhotosService());
    }
 
    
})


