
$(document).ready(function () {

    $('#addNewPhotos')
        .mouseover(function () {
            changeColor(this)
        })
        .mouseout(function () {
            changeColor(this)
        })

    generateAllPhotosTab();
});

function changeColor(id) {
    $(id)
        .toggleClass('addNewPhotos')
        .toggleClass('overAddNewPhotos')
};

var baseUri = '/webapi/photos/';

//#region Редактировать альбом
function editAlbum(albumId) {
    $('div.container').empty();

    var content =

        "<div class='A'>" +
            "<div class='B'>" +
                "<span>" +
                   "<a  href='' onclick='generateAllPhotosTab();return false;'>All photos</a>&nbsp;&nbsp;" +
                   "<a  href='' onclick='getAlbum(" + albumId + ");return false;'>My wall photos</a>&nbsp;&nbsp;" +
                   "<a class='btn' href=''>Edit photos</a>" +
                "</span>" +
            "</div>" +
        "</div>" +

        "<br /><br />" +

        "<div class='D'>" +
            "<span id='myphotos' >My wall photos</span><br />" +
        "</div>";

    $('div.container').html(content);


    $.ajax({
        url: baseUri + 'GetPhotoUrlsByAlbumID/' + albumId,
        type: 'GET',
        dataType: 'json',

        success: function (data) {

            var content1 = "<div id='table'>";
            var innerContent = "";
            var content3 = "</div>"
                + "<a onmouseover='changeColor(this)' onmouseout='changeColor(this)' onclick='saveChanges();return false' id='addNewPhotos' href='' class='addNewPhotos'>Save changes</a>";

            $.each(data, function (index, item) {

                innerContent += "<div class='tableRow' >" +

                                "<div class='divImage' style='display:table-cell;vertical-align:top;width:200px'>" +
                                    "<img src='" + item.photourl + "' width='170px' />" +
                                "</div>" +

                                "<div class='divEditImage tableCol' >" +

                                    "Description<br/>" +
                                    "<textarea class='description' data-photoID='" + item.photoid + "' style='overflow: hidden; resize: none;width:400px;height:80px'>" +
                                                item.description +
                                    "</textarea><br/>" +

                                    "<span style='position:absolute;right:40px'>" +
                                        "<a onclick='DeletePhotoFromAlbum(this," + albumId + ");return false' href=''>Delete</a>" +
                                    "</span>" +

                                "</div>" +

                          "</div>";

            });

            $('div.container').append(content1 + innerContent + content3);
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    });
}
function saveChanges() {

    var array = $('.description');

    var photos = [];

    $.each(array, function (index, item) {

        var id = $(item).attr('data-photoID');
        var description = $(item).val();

        var Photo = {
            PhotoID: id,
            Description: description
        }

        photos.push(Photo);
    });

    $.ajax({
        type: 'PUT',

        url: baseUri + 'UpdatePhotoDescription',

        contentType: "application/json;charset=utf-8",

        data: JSON.stringify(photos),
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    })
}
function DeletePhotoFromAlbum(a, albumId) {

    var divEditImage = $(a).closest('div.divEditImage');
    var divImage = divEditImage.prevAll().eq(0);
    var src = divImage.find('img').attr('src');

    var Photo = {
        PhotoUrl: src
    }

    $.ajax({
        type: "DELETE",
        url: baseUri + 'DeletePhotoFromAlbum',
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(Photo),

        success: function (result) {
            sweetAlert("Photo deleted succesfully.")
            editAlbum(albumId);

        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }

    });
}

//#endregion

//#region Создать альбом
function createAlbumForm() {
    var text =
   "<div id='modal'>" +
       "<div id='F'>" +

          "<div id='newAlbum'>" +
             "<b>New album</b>" +
              "<a onclick=\"" +
                     "$('#overlay').fadeOut().find('#modal').fadeOut();" +
                     "$('#overlay').empty();return false;\"" +
                     " id='_close' href=''>Close</a>" +
          "</div>" +

          "<div style='background-color: white; padding: 5px;'>" +
            "<div style='margin: 26px;'>" +

                               "Album title:<br />" +
                               "<input id='albumTitle' type='text' /><br />" +

                               "Description:<br />" +
                               "<input id='description' type='text' /><br />" +

                               "Who can view this album?" +
                               "<a onclick='GetPrivacies2(this);return false;' class='privacy' id='viewersVisivilityLevel' href=''>All users</a><br />" +

                               "Who can comment on the photos?" +
                               "<a onclick='GetPrivacies2(this);return false;' class='privacy' id='comentatorsVisibilityLevel' href=''>All users</a><br />" +
              "</div>" +
           "</div>" +

           "<div id='K' >" +
                "<br />" +
                "<input type='button' value='Create album' onclick='CreateAlbum()'  />" +
           "</div>" +

       "</div>" +
   "</div>";

    $('#overlay').html(text);

    $('#overlay')
       .fadeIn()
       .find('#modal')
       .fadeIn();
}
function CreateAlbum() {

    var albumTitle = $('#albumTitle').val();
    var description = $('#description').val();
    var viewersVisivilityLevel = $('#viewersVisivilityLevel').attr('data-value');
    var comentatorsVisibilityLevel = $('#comentatorsVisibilityLevel').attr('data-value');

    var PhotoAlbum = {
        Name: albumTitle,
        Description: description,
        VisibilityLevelOfViewer: viewersVisivilityLevel,
        VisibilityLevelOfСommentator: comentatorsVisibilityLevel
    }


    $('#overlay').fadeOut().find('#modal').fadeOut();
    $('#overlay').empty();

    $.ajax({
        type: 'POST',
        url: baseUri + 'CreatePhotoAlbum',
        contentType: "application/json;charset=utf-8",

        data: JSON.stringify(PhotoAlbum),


        success: function (albumId) {
            getAlbum(albumId);
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }

    });

    return false;
}
function GetPrivacies2(elem) {
    //удалить все выпадающие списки, содержащие уровни видимости
    if ($("#choosePrivacy").length)
        $("#choosePrivacy").remove();

    //отобразить ссылки на уровни видимости(на случай если какая-то не отображена)
    $(elem).show();

    //узнать на какую ссылку кликнул юзер
    var id = $(elem).attr('id');

    //считать текущее значение ссылки(на которую кликнул юзер) чтобы
    //отобразить её значение в выпадающем списке на 1-м месте
    var currentValue = $(elem).html();

    //скрыть эту ссылку
    $(elem).hide();

    //создается выпадающий список #choosePrivacy на месте ссылки
    $(elem).after(

        "<select size='1' id='choosePrivacy'  " +

        //при изменении значения выпадающего списка отобразить скрытую ранее ссылку
        "onchange=\"$('a.privacy').show();" +

        //запись в скрытую ранее ссылку нового значения из выпадающего списка
        "$('a#" + id + "').html(" +
                "$('#choosePrivacy').find('option:selected').text()" +

        "); " +
        "$('a#" + id + "').attr('data-value'," +
                "$('#choosePrivacy').find('option:selected').val()" +

        "); " +
        //выполнится SQL-команда UPDATE для нвого значения уровня видимости данного раздела
        //"updatePrivacyVisibilityLevel('" + id + "',$('select#choosePrivacy option:selected').val());" +
        //по завершении операции удалить этот выпадающий список
        "$(this).remove();" + "\" >" +
                "<option value=\"\" selected disabled>" + currentValue + "</option>" +
                "<option value=\"1\">All users</option>" +
                "<option value=\"2\">Only friends</option>" +
                "<option value=\"3\">Only me</option>" +
        "</select>"

        );
}
//#endregion

//#region Все фотографии
function allAlbumsPanel() {
    $('div.container').empty();

    var content =
        "<div class='A'>" +
            "<div class='B'>" +
                "<span>" +
                    "<a class='btn' href=''>All photos</a>" +
                "</span>" +
            "</div>" +
            "<a href='' id='createAlbum' onclick='createAlbumForm();return false;' class='C'>Create album</a>" +
        "</div>" +

        "<div class='input_container'>" +
            "<input type='file' id='inputfile' onchange='sendPhotos();return false;' />" +
        "</div>" +

        "<div id='addNewPhotos' class='addNewPhotos' onclick='upload()' onmouseover='changeColor(this)' onmouseout='changeColor(this)'>Change avatar</div><br /><br />" +

        "<div class='D'>" +
            "You have <span id='albumsCount'></span> albums" +
        "</div><br /><br />" +

        "<div id='albums'></div>" +

        "<div id='photos'></div>" +

        "<div id='overlay'></div>";

    $('div.container').html(content);
}
function upload() {

    document.getElementById('inputfile').click();
}
function GetPhotoAlbums() {
    $.ajax({
        url: baseUri + 'GetPhotoAlbums',
        type: 'GET',
        dataType: 'json',

        success: function (data) {

            $.each(data, function (index, album) {

                var content =
                    "<b>" + album.AlbumName + "</b>" +
                    "<a onclick=\"getAlbum(" + album.PhotoAlbumID + "); return false;\" >" +
                        "<img width='170px' src='" + album.LastPhotoUrl + "'/>" +
                    "</a><br/>";

                $('#albums').append(content);
            })
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    });
}
function GetAlbumsCountByUserID() {

    $.ajax({
        url: baseUri + 'GetAlbumsCountByUserID',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#albumsCount').html(data);
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    })
}
function getProfileID() {
    $.ajax({
        type: 'GET',
        url: '/webapi/settings/GetProfileID',
        datatype: 'json',
        success: function (data) {

            $('#hdnProfileID').val(data);
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    });
}
function generateAllPhotosTab() {
    allAlbumsPanel();//создать разметку
    GetPhotoAlbums();//добавить ссылки на альбомы
    GetAlbumsCountByUserID();
    getProfileID();//скрытое поле
}
//#endregion 

//#region Take album by ID
function getAlbum(albumId) {

    $('div.container').empty();

    $.ajax({
        url: baseUri + 'GetPhotosCountByAlbumID/' + albumId,
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            generateAlbumPanelMarkup(data, albumId);

            $.ajax({
                url: baseUri + 'GetPhotoUrlsByAlbumID/' + albumId,
                type: 'GET',
                dataType: 'json',

                success: function (data) {

                    generateGalleryMarkup(data);
                },
                error: function (xhr, status, p3) {
                    sweetAlert(status)
                }
            });
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    })
}
function generateAlbumPanelMarkup(data, albumId) {

    var content =
            "<div class='A'>" +

            "<div class='B'>" +
              "<span>" +
                  "<a onclick='generateAllPhotosTab();return false;' >All photos</a>&nbsp;&nbsp;" +
                  "<a class='btn'>My wall photos</a>" +
              "</span>" +
            "</div>" +

            "<a href='' class='C' onclick=\"editAlbum(" + albumId + ");return false;\"   >Edit album</a>" +
            "</div>" +

            "<a onmouseover='changeColor(this)' onmouseout='changeColor(this)' onclick='upload();return false' id='addNewPhotos' href='' class='addNewPhotos'>Add photos to album</a><br /><br />" +

            "<div class='D'>" + data + " photos in album | <span class='download'><a id='download_all' href='' onclick='multiDownload();return false;' >Download album</a></span></div>" +

            "<div class='input_container'  >" +
            "<input type='file' id='inputfile' onchange='sendPhotosToAlbum(" + albumId + ");return false;'   />" +
            "</div>";

    $('div.container').html(content);



}
function generateGalleryMarkup(data) {

    var content1 = "<div  id='GALLERY' >" +
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

        innerContent += "<li class='list' data-photoname='" + photo.name + "'" +
                        "class='col-xs-6 col-sm-4 col-md-3' " +
                       "data-sub-html='" +
                       "<h1><p>" + photo.description + "</p></h1>'" +
                        "data-src='" + photo.photourl + "'  >" +
                            "<a href=''>" +
                               "<img width='100px' class='img-responsive' src='" + photo.thumbnailphotourl + "'  />" +
                            "</a>" +
                    "</li>";

    });

    $('div.container').append(content1 + innerContent + content3);
    $('#lightgallery').lightGallery();

    //ссылки на фотографии, которые будут скачаны во время нажатия "Скачать альбом" при вызове
    var elements = (document.getElementsByClassName('list'));

    var photoNames = [];

    $('#hiddenAlbumLinks').empty();

    for (var i = 0; typeof (elements[i]) != 'undefined'; i++) {

        photoNames.push(baseUri + '/downloadimage?name=' + elements[i].getAttribute('data-photoname'));

        $('#hiddenAlbumLinks').append("<a href='" + photoNames[i] + "'  class='document' />");
    }
}
function sendPhotos() {

    var files = $('#inputfile').get(0).files;

    if (files.length > 0) {

        if (window.FormData !== undefined) {

            var data = new FormData();

            data.append("UploadedImage", files[0]);

            $.ajax({
                type: "POST",
                url: baseUri + 'postfile',
                contentType: false,
                processData: false,
                data: data,
                success: function (result) {

                    sweetAlert("Photo saved successfully")
                },
                error: function (xhr, status, p3) {
                    sweetAlert(status);
                }
            });


        } else {
            alert("Браузер не поддерживает загрузку файлов HTML5!");
        }
    }
}
function sendPhotosToAlbum(albumId) {

    var files = $('#inputfile').get(0).files;//достаем все файлы в input теге

    if (files.length > 0) {//если файлы есть

        if (window.FormData !== undefined) {

            var data = new FormData();


            //будет отправлен только один файл
            data.append("UploadedImage", files[0]);

            //передача albumId
            $.ajax({
                type: "POST",
                url: baseUri + 'AttachPhotosToAlbum',
                contentType: "application/json;charset=utf-8",

                data: JSON.stringify(albumId),

                success: function () {

                    //отпрвка фоток
                    $.ajax({
                        type: "POST",
                        url: baseUri + 'PostFile',
                        contentType: false,
                        processData: false,
                        data: data,

                        success: function (data) {

                            sweetAlert("Photo succesfully added to album");
                            getAlbum(albumId);

                        },
                        error: function (xhr, status, p3) {
                            alert(status);
                        }
                    });
                },
                error: function (xhr, status, p3) {
                    sweetAlert(status);
                }

            });


        } else {
            alert("Браузер не поддерживает загрузку файлов HTML5!");
        }
    }
}
function multiDownload() {
    $('.document').multiDownload({ delay: 2000 });
}
//#endregion 




