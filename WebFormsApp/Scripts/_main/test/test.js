/// <reference path="Scripts/jasmine-2.5.2/jasmine.js"/>

function MainControllerTest() {

    var spyView = null,
        spyService = null,
        controller = null,

   //fake arguments
        sender = {},
        albumId = 5;

    //Arrange
    beforeEach(function () {

        InitSpyView();
        InitSpyService();
        InitController();         
    });

    it("insertComment", function () {

        //Act
        controller.insertComment(sender);

        //Assert
        var comment = expect(spyView.getComment).toHaveBeenCalledWith(sender);
        expect(spyView.getComment).not.toBeNull();
        expect(spyService.insertComment).toHaveBeenCalledWith(comment);
        expect(spyView.getCommentTemplate).toHaveBeenCalled();
        expect(spyView.addCommentToWall).toHaveBeenCalled();
    });

    it("insertStatus", function () {

        //Act
        controller.insertStatus(sender);

        //Assert
        var status = expect(spyView.getStatus).toHaveBeenCalled();
        expect(spyService.insertStatus).toHaveBeenCalledWith(status);
        expect(spyView.addPostMarkup).toHaveBeenCalled();
        expect(spyView.clearMessageTextarea).toHaveBeenCalled();
        expect(spyView.changePostsCount).toHaveBeenCalledWith(1);
    });
    it("deletePost", function () {

        //Act
        controller.deletePost(sender);

        //Assert
        var id = expect(spyView.getStatusId).toHaveBeenCalledWith(sender);
        expect(spyService.deleteStatus).toHaveBeenCalledWith(id);
        expect(spyView.deletePostsMarkup).toHaveBeenCalledWith(sender);
    });
    it("addToFriends", function () {

        //Act
        controller.addToFriends();

        //Assert
        var message = expect(spyView.getInvitationMessage).toHaveBeenCalled();
        expect(spyService.addToFriendsMessage).toHaveBeenCalledWith(message);
    });
    it("getPhotosByAlbumID", function () {

        //Act
        controller.getPhotosByAlbumID(albumId);

        //Assert
        expect(spyView.clearContainer).toHaveBeenCalled();
        expect(spyService.getPhotosByAlbumID).toHaveBeenCalledWith(albumId);
        expect(spyView.addAlbumGalleryMarkup).toHaveBeenCalled();
    });
    it("deleteComment", function () {

        //Act
        controller.deleteComment(sender);

        //Assert
        var commentId = expect(spyView.getCommentId).toHaveBeenCalledWith(sender);
        expect(spyService.deleteComment).toHaveBeenCalledWith(commentId);
        expect(spyView.deleteCommentMarkup).toHaveBeenCalledWith(sender);
    });

    it("getAllPhotosByUserID", function () {

        //Act
        controller.getAllPhotosByUserID();

        //Assert
        expect(spyView.clearContainer).toHaveBeenCalled();
        var id = expect(spyView.getUserIdOfPageOwner).toHaveBeenCalled();
        expect(spyService.getAllPhotosByUserID).toHaveBeenCalledWith(id);

    });

    it("getThumbAvatarImg", function () {

        //Act
        controller.getThumbAvatarImg();

        //Assert
        var id = expect(spyView.getUserIdOfPageOwner).toHaveBeenCalled();
        expect(spyService.getThumbAvatarImg).toHaveBeenCalledWith(id);
        expect(spyView.setAvatar).toHaveBeenCalled();
    });

    it("getLargeAvatarImg", function () {

        //Act
        controller.getLargeAvatarImg();

        //Assert
        var id = expect(spyView.getUserIdOfPageOwner).toHaveBeenCalled();
        expect(spyService.getLargeAvatarImg).toHaveBeenCalledWith(id);
        expect(spyView.setLargeAvatar).toHaveBeenCalled();
    });

    it("sendAvatar", function () {

        //Act
        controller.sendAvatar();

        //Assert
        var image = expect(spyView.getLoadedImage).toHaveBeenCalled();
        expect(spyService.postFile).toHaveBeenCalledWith(image);
    });
    
    function InitSpyView() {

        spyView = new MainView();

        spyOn(window, 'MainView').and.callFake(function () {
            spyOn(spyView, 'getComment');
            spyOn(spyView, 'getCommentTemplate');
            spyOn(spyView, 'addCommentToWall');
            spyOn(spyView, 'getStatus');
            spyOn(spyView, 'getStatusId');
            spyOn(spyView, 'getInvitationMessage');
            spyOn(spyView, 'clearAlbumContainer');
            spyOn(spyView, 'getCommentId');
            spyOn(spyView, 'clearContainer');
            spyOn(spyView, 'changePostsCount');
            spyOn(spyView, 'setAvatar');
            spyOn(spyView, 'setLargeAvatar');
            spyOn(spyView, 'getLoadedImage');
            spyOn(spyView, 'showHideDetails');
            spyOn(spyView, 'showAllComments');
            spyOn(spyView, 'createCommentsForm');
            spyOn(spyView, 'scrollToCommentsForm');
            spyOn(spyView, 'createPostForm');
            spyOn(spyView, 'addCommentMarkup');
            spyOn(spyView, 'addPostMarkup');
            spyOn(spyView, 'deleteCommentMarkup');
            spyOn(spyView, 'deletePostsMarkup');
            spyOn(spyView, 'addAlbumGalleryMarkup');
            spyOn(spyView, 'addGalleryMarkup');
            spyOn(spyView, 'upload');
            spyOn(spyView, 'runModalWindowOnAvatarClick');
            spyOn(spyView, 'changeColor');
            spyOn(spyView, 'clearMessageTextarea');
            spyOn(spyView, 'getCurrentUserId'); 
            spyOn(spyView, 'getUserIdOfPageOwner');
            return spyView;
        });
    }
    function InitSpyService() {

        spyService = new MainService();
        var successfulAjaxRequest = $.Deferred().resolve({}).promise();

        spyOn(window, 'MainService').and.callFake(function () {
            spyOn(spyService, 'insertComment').and.callFake(function (comment) { return successfulAjaxRequest; });
            spyOn(spyService, 'insertStatus').and.callFake(function (status) { return successfulAjaxRequest; });
            spyOn(spyService, 'deleteStatus').and.callFake(function (id) { return successfulAjaxRequest; });
            spyOn(spyService, 'addToFriendsMessage').and.callFake(function (message) { return successfulAjaxRequest; });
            spyOn(spyService, 'getPhotosByAlbumID').and.callFake(function (albumId) { return successfulAjaxRequest; });
            spyOn(spyService, 'deleteComment').and.callFake(function (id) { return successfulAjaxRequest; });
            spyOn(spyService, 'getAllPhotosByUserID').and.callFake(function (userId) { return successfulAjaxRequest; });
            spyOn(spyService, 'getThumbAvatarImg').and.callFake(function (userId) { return successfulAjaxRequest; });
            spyOn(spyService, 'getLargeAvatarImg').and.callFake(function (userId) { return successfulAjaxRequest; });
            spyOn(spyService, 'postFile').and.callFake(function (data) { return successfulAjaxRequest; });
            return spyService;
        });
    }

    function InitController() {
        var view = new MainView();
        var service = new MainService();
        controller = new MainController(view, service);
    }
}

describe("MainController Test", MainControllerTest);
 