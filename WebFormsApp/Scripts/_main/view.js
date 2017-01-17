
function MainView() { 
    this.init();
}

MainView.prototype = function () {

    return {
        addCommentMarkup: addCommentMarkup,
        addPostMarkup: addPostMarkup,
        addCommentToWall: addCommentToWall,
        addAlbumGalleryMarkup: addAlbumGalleryMarkup,
        addGalleryMarkup: addGalleryMarkup,
        changeColor: changeColor,
        clearMessageTextarea: clearMessageTextarea,
        clearAlbumContainer: clearAlbumContainer,
        clearContainer: clearContainer,
        createPostForm: createPostForm,
        createCommentsForm: createCommentsForm,
        changePostsCount: changePostsCount,         
        deleteCommentMarkup: deleteCommentMarkup,
        deletePostsMarkup: deletePostsMarkup,
        displayCommentAnchors: displayCommentAnchorsIfCommentsCountIsNotZero,
        getCurrentUserId: getCurrentUserId,
        getLoadedImage: getLoadedImage,
        getCommentId: getCommentId,
        getComment: getComment,
        getCommentTemplate: getCommentTemplate,
        getCurrentMessage: getCurrentMessage,
        getUserIdOfPageOwner:getUserIdOfPageOwner, 
        getStatus: getStatus,
        getStatusId: getStatusId,
        getInvitationMessage: getInvitationMessage,
        init: init,
        onError: onError,
        onSuccessCreateDialog: onSuccessCreateDialog,        
        postMessage: $("textarea#txtMessage").val(),
        runModalWindowOnAvatarClick: runModalWindowOnAvatarClick,
        setAvatar: setAvatar,
        setCaretPosition: setCaretPosition,
        setLargeAvatar: setLargeAvatar,
        setRecipientUserID: setRecipientUserID,
        showHideDetails: showHideDetails,
        showAllComments: showAllComments,
        scrollToCommentsForm: scrollToCommentsForm,
        upload: upload
    }
    var STATIC = null;

    function init() {
        STATIC = {
            receiversUserID: null
        };
    }
    function getCurrentUserId() {
        var id = $('#hdnCurrentUserID').val();
        return id;
    }
    function getUserIdOfPageOwner() {
        var id = $('#hdnPageOfUserID').val();
        return id;
    }
    function getComment(sender) {
        var comment = {
            StatusID: $(sender).parent().prevAll('input').eq(0).val(),
            CommentText: $(sender).prev().val()
        };
        return comment;
    }
    function getCommentTemplate(data) {
        var html = '<div class="showInfo" style="position: relative;">' +
                     '<div class="avatar">' +
                        '<b>' + data.CommentatorsUserName + '</b>' +
                        '<a class="delete" onclick="vm.deleteComment(this)">delete</a>' +
                        '<input type="hidden" value="' + data.CommentID + '"/>' +
                     '</div>' +
                     '<div class="commentCell">' + data.CommentText + '</div>' +
                   '</div>';
        return html;
    }
    function addCommentToWall(sender, template) {
        $(sender).closest('div.').prev('div').append(template);
    }

    function getStatus() {
        var status = {
            Post: $("textarea#txtMessage").val(),
            WallOfUserID: $('#hdnPageOfUserID').val()
        }
        return status;
    }
    function getStatusId(sender) {
        var id = $(sender).closest('.table_').nextAll('input[type="hidden"]').val();
        return id;
    }
    function getInvitationMessage() {

        var currentUserId = $('#hdnCurrentUserID').val();

        var message = {
            ReceiversUserID: $('#hdnPageOfUserID').val(),
            Body: "<p>Hello! I wanna be your friend!<br/>" +
                                      "FROM: <a href='/MyPage.aspx?UserID=" + currentUserId + "'>" + "--userNamePlaceHolder--" +
                                        "</a><br/>" +
                                        "Choose status for this human:" +
                                        "<select class='relationshipType'>" +

                                            "<option value='2'>Add to friends</option>" +
                                            "<option value='3'>Ignore this human</option>" +

                                        "</select>" +
                                        "<input type = 'submit' value = 'Отправить' data-senderid='" + currentUserId + "' onclick = 'return vm.checkRelationshipType(this)' /> " +
                                       "</p>"
        };
        return message;
    }

    function clearAlbumContainer() {
        $('div.container_album').empty();
    }
    function getCommentId(sender) {
        var id = $(sender).nextAll('input[type="hidden"]').eq(0).val();
        return id;
    }
    function clearContainer() {
        $('div.container').empty();
    }
    function setAvatar(url) {
        $('#imgAvatar').attr('src', url);
    }
    function setLargeAvatar(url) {
        $('.imgLargeAvatar').attr('src', url);
        return false;
    }

    function getLoadedImage() {
        var files = $('#inputfile').get(0).files;
        if (files.length > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                data.append("UploadedImage", files[0]);
                return data;
            }
            else {
                alert("Браузер не поддерживает загрузку файлов HTML5.");
            }
        }
        else
            return null;
    }

    function showHideDetails() {
        var showText = 'Show detail info';
        var hideText = 'Hide detail info';
        if ($('#contactsInfo').hasClass('hideInfo')) {
            $('a#showDetails').text(hideText);
        } else {
            $('a#showDetails').text(showText);
        }
        $('#contactsInfo, #interestsInfo, #educationInfo').toggleClass('hideInfo');
    }
    function displayCommentAnchorsIfCommentsCountIsNotZero() {
        $('.commentsCount').each(function () {
            if ($(this).text() != 0) {
                $(this).parent('a').removeClass('hideInfo');
            }
        });
    }
    function showAllComments(sender) {
        var anchor = $(sender).prevAll('a').eq(0);
        createCommentsForm(anchor);
        var comments = $(sender).next('div.COMMENT').children('div');
        comments.removeClass('hideInfo');
        comments.addClass('showInfo');
        $(sender).remove();
    }
    function createCommentsForm(sender) {
        $(sender)
            .nextAll("div.commentContainer")
            .eq(0)
            .append('<input type="text" value="" />' +
                    '<input type="submit" onclick="return vm.insertComment(this)" value="Send message" />');
        $(sender).remove();
    }
    function scrollToCommentsForm(sender) {
        var target = $(sender).nextAll('div').eq(0);
        $('html, body').animate({
            scrollTop: target.offset().top
        }, 1500);
        return false;
    }
    function createPostForm(sender) {
        var postForm = "<br/>" +
                       "<input type='submit' id='createPost' value='Send message' onclick='vm.insertStatus();return false'  class='messageForm' />";
        $("#createMessages").append(postForm);
        $(sender).removeAttr('onclick');
    }
    function addCommentMarkup(sender, args) {
        $(sender).parent('div').prev('div').append(args);
    }
    function addPostMarkup(postMessage, statusId, userName, avatar) {
        var myDate = new Date();
        var displayDate = (myDate.getMonth() + 1) + '.' + (myDate.getDate()) + '.' + myDate.getFullYear() + ' ' + myDate.getHours() + ':' + myDate.getMinutes() + ':' + myDate.getSeconds() + ' ';
        var html = '<li>' +
                                '<div class="table_">' +
                                    '<div class="table_row">' +
                                        '<div class="table_leftCell">' +

                                            '<a href="">' +
                                                '<img id="Image3" src="' + avatar + '" class="post_image" style="width:30px;">' +
                                            '</a>' +
                                            '<br>' +
                                        '</div>' +
                                        '<div class="table_rightCell">' +
                                            '<b>' + userName + '</b><br>' +
                                            postMessage +
                                        '</div>' +
                                    '</div>' +
                                '</div>' +
                                '<input type="hidden" id = "hdnStatusId" value="' + statusId + '">' +
                                displayDate +
                                '| <a onclick="vm.createCommentsForm(this);">Comment</a>' +
                                '<hr>' +
                                '<div class="COMMENT" style="width: 300px;">' +
                                '</div>' +
                                '<div class="commentContainer"></div>' +
                            '</li>';
        $('ul#postsList').prepend(html);
    }
    function deleteCommentMarkup(sender) {
        $(sender).closest('div.showInfo').remove();
    }
    function deletePostsMarkup(sender) {
        var li = $(sender).closest('li');
        li.remove();
        changePostsCount(-1);
    }
    function changePostsCount(value) {
        var count = parseInt($('#statusesCount').text());
        count += value;
        $('#statusesCount').text(count);
    }
    function addAlbumGalleryMarkup(data) {
        var content1 = "<div  id='_GALLERY' style='margin :0 0 0 0'>" +
                                      "<link href='Content/gallery/lightgallery.css' rel='stylesheet'>" +
                                      "<link href='Content/gallery/gallery.css' rel='stylesheet' />" +
                                      "<ul id='_lightgallery' class='list-unstyled row list_style' >";
        var innerContent = "";
        var content3 = "</ul>" +
                            "<script src='https://cdn.jsdelivr.net/picturefill/2.3.1/picturefill.min.js' />" +
                            "<script src='Scripts/gallery/lightgallery.js' />" +
                            "<script src='Scripts/gallery/lg-fullscreen.js' />" +
                            "<script src='Scripts/gallery/lg-thumbnail.js' />" +
                            "<script src='Scripts/gallery/lg-video.js' />" +
                            "<script src='Scripts/gallery/lg-autoplay.js' />" +
                            "<script src='Scripts/gallery/lg-zoom.js' />" +
                            "<script src='Scripts/gallery/lg-hash.js' />" +
                            "<script src='Scripts/gallery/lg-pager.js' />" +
                       "</div>";
        $.each(data, function (index, photo) {
            innerContent += "<li style='display:none' class='list' data-photoname='" + photo.Name + "'" +
                            "class='col-xs-6 col-sm-4 col-md-3' " +
                           "data-sub-html='" +
                           "<h1><p>" + photo.Description + "</p></h1>'" +
                            "data-src='" + photo.PhotoUrl + "'  >" +
                                "<a href=''>" +
                                   "<img width='100px' height='62px' class='img-responsive' src='" + photo.ThumbnailPhotoUrl + "'  />" +
                                "</a>" +
                        "</li>";
        });
        $('div.container_album').append(content1 + innerContent + content3);
        $('#_lightgallery').lightGallery();
        $('div#_GALLERY>ul>li>a').trigger('click');
    }
    function addGalleryMarkup(data) {
        var photosCount = 4;
        var content1 = "<div  id='GALLERY' style='margin :0 0 0 0'>" +
                            "<link href='Content/gallery/lightgallery.css' rel='stylesheet'>" +
                            "<link href='Content/gallery/gallery.css' rel='stylesheet' />" +
                            "<ul id='lightgallery' class='list-unstyled row list_style' >";
        var innerContent = "";
        var content3 = "</ul>" +
                            "<script src='https://cdn.jsdelivr.net/picturefill/2.3.1/picturefill.min.js' />" +
                            "<script src='Scripts/gallery/lightgallery.js' />" +
                            "<script src='Scripts/gallery/lg-fullscreen.js' />" +
                            "<script src='Scripts/gallery/lg-thumbnail.js' />" +
                            "<script src='Scripts/gallery/lg-video.js' />" +
                            "<script src='Scripts/gallery/lg-autoplay.js' />" +
                            "<script src='Scripts/gallery/lg-zoom.js' />" +
                            "<script src='Scripts/gallery/lg-hash.js' />" +
                            "<script src='Scripts/gallery/lg-pager.js' />" +
                       "</div>";
        $.each(data, function (index, photo) {
            if (photosCount > 0) {
                innerContent += "<li class='list' data-photoname='" + photo.Name + "'" +
                                "class='col-xs-6 col-sm-4 col-md-3' " +
                               "data-sub-html='" +
                               "<h1><p>" + photo.Description + "</p></h1>'" +
                                "data-src='" + photo.PhotoUrl + "'  >" +
                                    "<a href=''>" +
                                       "<img width='100px' height='62px' class='img-responsive' src='" + photo.ThumbnailPhotoUrl + "'  />" +
                                    "</a>" +
                            "</li>";
                photosCount -= 1;
            }
            else {
                innerContent += "<li style='display:none' class='list' data-photoname='" + photo.Name + "'" +
                                "class='col-xs-6 col-sm-4 col-md-3' " +
                               "data-sub-html='" +
                               "<h1><p>" + photo.Description + "</p></h1>'" +
                                "data-src='" + photo.PhotoUrl + "'  >" +
                                    "<a href=''>" +
                                       "<img width='100px' class='img-responsive' src='" + photo.ThumbnailPhotoUrl + "'  />" +
                                    "</a>" +
                            "</li>";
            }
        });
        $('div.container').append(content1 + innerContent + content3);
        $('#lightgallery').lightGallery();
    }
    function upload() {
        document.getElementById('inputfile').click();
    }
    function runModalWindowOnAvatarClick() {
        $('#popup-wrapper').modalPopLite({
            openButton: '#imgAvatar',
            closeButton: '#close-btn',
            isModal: true
        });
    }
    function changeColor(sender) {
        $(sender)
            .toggleClass('addNewPhotos')
            .toggleClass('overAddNewPhotos')
    };
    function clearMessageTextarea() {
        $('textarea').empty();
    }
    function onError(message) {
        alert(message);
    }
    function setCaretPosition(ctrl, pos) {

        if (ctrl.setSelectionRange) {
            ctrl.focus();
            ctrl.setSelectionRange(pos, pos);
        }
        else if (ctrl.createTextRange) {
            var range = ctrl.createTextRange();
            range.collapse(true);
            range.moveEnd('character', pos);
            range.moveStart('character', pos);
            range.select();
        }
    }
    function setRecipientUserID(id) {
        STATIC.receiversUserID = id;
    }
    function getCurrentMessage() {
        var message = {
            ReceiversUserID: STATIC.receiversUserID,
            Body: $('#messageBody').val()
        };
        return message;
    }
    function onSuccessCreateDialog(data) {
        $('#modal-1').trigger('click');
        $('#messageBody').empty();
        alert(JSON.parse(data.d));
    }


}()


