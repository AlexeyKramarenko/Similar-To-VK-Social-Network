$(document).ready(function () {

    anchorsDisplay();
    calculateHiddenFieldsValue();
    getAllPhotosByUserID();


    GetThumbAvatarImg();
    runModalPopLiteForLargerImage();
    //runModalPopLiteForCropImage();

    $('#sendBtn').click(function () {

        sendMessage();
    });
});


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


function anchorsDisplay() {

    $('.commentsCount').each(function () {

        if ($(this).text() != 0 && $(this).text() != '0') {
            $(this).parent('a').removeClass('hideInfo');
        }
    });
}

function showAllComments(el) {

    var comm = $(el).prevAll('a').eq(0);
    createCommentsForm(comm);
    var comments = $(el).next('div.COMMENT').children('div');
    comments.removeClass('hideInfo');
    comments.addClass('showInfo');

    $(el).remove();
}

function createCommentsForm(el) {

    $(el)
        .nextAll("div.commentContainer")
        .eq(0)
        .append('<input type="text" value="" />' +
                '<input type="submit" value="Send message"' +
                'onclick="insertComment(this);' +
                '  return false;' +
                '" />');

    $(el).remove();
}

function scrollToCommentsForm(el) {

    var target = $(el).nextAll('div').eq(0);

    $('html, body').animate({
        scrollTop: target.offset().top
    }, 1500);

    return false;
}

function createPostForm(el) {

    $("#createMessages").append("<br/><input type='submit' id='createPost' value='Send message' onclick='insertStatus();return false'  class='messageForm' />");
    $(el).removeAttr('onclick');
}



function addTempComment(el, addedHtml) {
    $(el).parent('div').prev('div').append(addedHtml);
}

var baseUri = '/webapi/main/';

function insertComment(el) {

    var statusId = $(el).parent().prevAll('input').eq(0).val();
    var commentText = $(el).prev().val();


    var Comment = {

        StatusID: statusId,
        CommentText: commentText

    };


    $.ajax({
        url: baseUri + 'InsertComment',
        type: 'POST',
        data: JSON.stringify(Comment),
        contentType: "application/json;charset=utf-8",

        success: function (data) {

            var addedHtml = '<div class="showInfo" style="position: relative">' +
                     '<div class="avatar">' +
                        '<b>' + data.commentatorsUserName + '</b>' +
                        '<a class="delete" onclick="deleteComment(this)">delete</a>' +
                        '<input type="hidden" value="' + data.id + '"/>' +
                     '</div>' +
                     '<div class="commentCell">' + commentText + '</div>' +
                   '</div>';

            addTempComment(el, addedHtml);

            incrementCount(el);
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }

    });
}

function incrementCount(el) {
    var countStr = $(el).parent('div').prevAll('a.showDetails').eq(0).children('span').text();
    var count = parseInt(countStr);
    count++;
    $(el).parent('div').prevAll('a.showDetails').eq(0).children('span').text(count);
}


function insertStatus() {

    var postMessage = $("textarea#txtMessage").val();

    var Status = {

        Post: postMessage,
        WallOfUserID: $('#hdnPageOfUserID').val()
    }
    $.ajax({
        url: baseUri + 'InsertStatus',
        type: 'POST',
        data: JSON.stringify(Status),
        contentType: 'application/json;charset=utf-8',

        success: function (data) {
            generatePost(postMessage, data.ID, data.UserName, data.Avatar);
            changePostsCount(+1);
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    });
}

function calculateHiddenFieldsValue() {

    var count = 0;

    $('li > input[type=hidden]#lbl').each(function (index, value) {
        count++;
    });

    return count + 1;
}

function generatePost(postMessage, statusId, userName, avatar) {

    var myDate = new Date();
    var displayDate = (myDate.getMonth() + 1) + '.' + (myDate.getDate()) + '.' + myDate.getFullYear() + ' ' + myDate.getHours() + ':' + myDate.getMinutes() + ':' + myDate.getSeconds() + ' ';

    var value = calculateHiddenFieldsValue();

    var post = '<li>' +
                            '<div class="table3">' +
                                '<div class="table3row">' +
                                    '<div class="table3leftCell">' +

                                        '<a href="">' +
                                            '<img id="Image3" src="' + avatar + '" class="post_image" style="width:30px;">' +
                                        '</a>' +
                                        '<br>' +
                                    '</div>' +
                                    '<div class="table3rightCell">' +
                                        '<b>' + userName + '</b><br>' +
                                        postMessage +
                                    '</div>' +
                                '</div>' +
                            '</div>' +
                            '<input type="hidden"  data-statusid="hdnStatusId" value="' + statusId + '">' +
                            displayDate +
                            '| <a onclick="createCommentsForm(this);">Комментировать</a>' +
                            '<hr>' +
                            '<div class="COMMENT" style="width: 300px;">' +
                            '</div>' +
                            '<div class="commentContainer"></div>' +
                        '</li>'

    $('ul#postsList').prepend(post);
};

//---------------------------------------------------------------
function deleteCommentMarkup(el) {

    $(el).parent('div').parent('div').remove();
}
function deleteComment(el) {

    //var wallOfUserId = $('hdnCurrentUserID').val();

    var id = $(el).nextAll('input[type="hidden"]').eq(0).val();

    $.ajax({
        url: baseUri + 'DeleteComment/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",

        success: function () {

            deleteCommentMarkup(el);
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    });
}
function deletePostsMarkup(el) {

    var li = $(el).closest('li');

    li.remove();

    changePostsCount(-1);
}
function changePostsCount(value) {

    var count = parseInt($('#statusesCount').text());
    count += value;
    $('#statusesCount').text(count);
}

function deletePost(el) {

    var id = $(el).closest('.table3').nextAll('input[type="hidden"]').val();

    $.ajax({
        url: baseUri + 'DeleteStatus/' + parseInt(id),
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",

        success: function () {
            deletePostsMarkup(el);
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    });
}

function addToFriends() {

    var Message = {
        ReceiversUserID: $('#hdnPageOfUserID').val()
    };

    $.ajax({
        url: baseUri + 'AddToFriendsMessage/',
        type: 'POST',
        contentType: 'application/json;charset=utf-8',

        data: JSON.stringify(Message),

        error: function (xhr, status, p3) {
            sweetAlert(status);
        }

    })
}
function getPhotosByAlbumID(albumId) {

    $('div.container_album').empty();

    $.ajax({
        url: '/webapi/photos/GetPhotosByAlbumID/',
        type: 'GET',
        dataType: 'json',
        data: { albumId: albumId },

        success: function (data) {

            generateAlbumGalleryMarkup(data);
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    })
}
function generateAlbumGalleryMarkup(data) {

    var content1 = "<div  id='_GALLERY' style='margin :0 0 0 0'>" +
                   "<script src='https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js'></script>" +
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
//#region Take album by ID
function getAllPhotosByUserID() {

    var userId = $('#hdnCurrentUserID').val();

    $('div.container').empty();

    $.ajax({
        url: '/webapi/photos/GetAllPhotosByUserID/',
        type: 'GET',
        dataType: 'json',
        data: { userId: userId },
        success: function (data) {
            if (data != null)
                generateGalleryMarkup(data);
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    })
}


function generateGalleryMarkup(data) {

    var photosCount = 4;

    var content1 = "<div  id='GALLERY' style='margin :0 0 0 0'>" +
                                      "<script src='https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js'></script>" +
                                      "<link href='/Content/gallery/lightgallery.css' rel='stylesheet'>" +
                                      "<link href='/Content/gallery/gallery.css' rel='stylesheet' />" +

                                      "<ul id='lightgallery' class='list-unstyled row list_style' >";

    var innerContent = "";

    var content3 = "</ul>" +
                        "<script src='https://cdn.jsdelivr.net/picturefill/2.3.1/picturefill.min.js' />" +
                        "<script src='/Scripts/gallery/lightgallery.js' />" +
                        "<script src='/Scripts/gallery/lg-fullscreen.js' />" +
                        "<script src='/Scripts/gallery/lg-thumbnail.js' />" +
                        "<script src='/Scripts/gallery/lg-video.js' />" +
                        "<script src='/Scripts/gallery/lg-autoplay.js' />" +
                        "<script src='/Scripts/gallery/lg-zoom.js' />" +
                        "<script src='/Scripts/gallery/lg-hash.js' />" +
                        "<script src='/Scripts/gallery/lg-pager.js' />" +

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

function sendAvatar() {

    var files = $('#inputfile').get(0).files;

    if (files.length > 0) {

        if (window.FormData !== undefined) {

            var data = new FormData();

            data.append("UploadedImage", files[0]);

            $.ajax({
                type: "POST",
                url: '/webapi/photos/PostFile',
                contentType: false,
                processData: false,
                data: data,

                success: function () {

                    GetThumbAvatarImg();
                },
                error: function (xhr, status, p3) {
                    sweetAlert(status);
                }
            });


        } else {
            alert("Браузер не поддерживает загрузку файлов HTML5! Скачайте наконец нормальный браузер.");
        }
    }
}
function GetThumbAvatarImg() {

    var userId = $('#hdnPageOfUserID').val();

    $.ajax({
        type: 'GET',
        url: '/webapi/photos/GetThumbAvatarImg',
        dataType: 'json',
        data: { userId: userId },

        success: function (url) {
            $('#imgAvatar').attr('src', url);
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    })

}
function GetLargeAvatarImg() {

    var userId = $('#hdnPageOfUserID').val();

    $.ajax({
        type: 'GET',
        url: '/webapi/photos/GetLargeAvatarImg',
        dataType: 'json',
        data: { userId: userId },

        success: function (url) {
            $('.imgLargeAvatar').attr('src', url);
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    })

}
function renameUrl() {
    var userId = $('#hdnCurrentUserID').val();
    window.history.pushState("", "", userId);
}
function upload() {
    document.getElementById('inputfile').click();
}


function runModalPopLiteForLargerImage() {
    $('#popup-wrapper').modalPopLite({
        openButton: '#imgAvatar',
        closeButton: '#close-btn',
        isModal: true
    });
}
function runModalPopLiteForCropImage() {
    $('#popup-wrapper2').modalPopLite({
        openButton: '#cropPhoto',
        closeButton: '#close-btn2',
        isModal: true
    });
}
function changeColor(id) {
    $(id)
        .toggleClass('addNewPhotos')
        .toggleClass('overAddNewPhotos')
};

function sendMessage() {

    var FromUserId = $('#hdnCurrentUserID').val();
    var ToUserId = $('#hdnPageOfUserID').val();

    var dialogID = 0; //MessagesRepository will calculate dialogId by usersIds

    var message = $('#messageBody').val();

    var hub = $.connection.chatHub.server;

    hub.sendMessage(FromUserId, ToUserId, dialogID, message);
}