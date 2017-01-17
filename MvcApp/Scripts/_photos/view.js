
function PhotosView() {

    this.addPhotoTemplate = addPhotoTemplate;
    this.changeColor = changeColor;
    this.closeModalWindow = closeModalWindow;
    this.createAlbumForm = createAlbumForm;
    this.createAlbumPanel = createAlbumPanel;
    this.editPhotoTemplate = editPhotoTemplate;
    this.generateGalleryMarkup = generateGalleryMarkup;
    this.getAllAlbumsPanel = getAllAlbumsPanel;
    this.getChangedPhotos = getChangedPhotos;
    this.getPhotoCheckedToDelete = getPhotoCheckedToDelete;
    this.getPhotoAlbumInfo = getPhotoAlbumInfo;
    this.getLoadedImage = getLoadedImage;
    this.multiDownload = multiDownload;
    this.notificationAboutDeleting = notificationAboutDeleting;
    this.notificationAboutAddingToMainAlbum = notificationAboutAddingToMainAlbum;
    this.notificationAboutAddingToAlbum = notificationAboutAddingToAlbum;
    this.showPhotoAlbums = showPhotoAlbums;
    this.showAlbumsCount = showAlbumsCount;
    this.setProfileId = setProfileId;
    this.upload = upload;

    function notificationAboutDeleting() {
        sweetAlert("Photo was deleted from album");
    }
    function notificationAboutAddingToMainAlbum() {
        sweetAlert("Photo was sent to main album");
    }
    function notificationAboutAddingToAlbum() {
        sweetAlert("Photo succesfully added to album");
    }
    function changeColor(sender) {
        $(sender).toggleClass('addNewPhotos')
                 .toggleClass('overAddNewPhotos');
    }
    function addPhotoTemplate(albumId) {
        var template =
        "<div class='A'>" +
            "<div class='B'>" +
                "<span>" +

                   "<a  href='' " +
                       "onclick='return vm.activate()'>" +
                             "All photos" +
                   "</a>&nbsp;&nbsp;" +

                   "<a  href='' " +
                       "onclick='return vm.getAlbum(" + albumId + ")'>" +
                             "My wall photos" +
                   "</a>&nbsp;&nbsp;" +

                   "<a class='btn' href=''>Edit photos</a>" +
                "</span>" +
            "</div>" +
        "</div>" +
        "<br /><br />" +
        "<div class='D'>" +
            "<span id='myphotos' >My wall photos</span><br />" +
        "</div>";
        $('div.container').html(template);
    }
    function editPhotoTemplate(data, albumId) {
        var content1 = "<div id='table'>";

        var innerContent = "";

        var content3 = "</div>"
            + "<a " +
                  "onmouseover='vm.changeColor(this)' " +
                  "onmouseout='vm.changeColor(this)' " +
                  "onclick='return vm.saveChanges()' " +
                  "id='addNewPhotos' " +
                  "href='' " +
                  "class='addNewPhotos'>" +
                  "Save changes" +
             "</a>";

        $.each(data, function (index, item) {
            innerContent += "<div class='tableRow' >" +
                            "<div class='divImage' style='display:table-cell;vertical-align:top;width:200px'>" +
                                "<img src='" + ROOT + item.Photourl + "' width='170px' />" +
                            "</div>" +
                            "<div class='divEditImage tableCol' >" +
                                "Description<br/>" +
                                "<textarea class='description' data-photoID='" + item.PhotoId + "' style='overflow: hidden; resize: none;width:400px;height:80px'>" +
                                            item.Description +
                                "</textarea><br/>" +
                                "<span style='position:absolute;right:40px'>" +
                                    "<a " +
                                       "onclick='vm.deletePhotoFromAlbum(this," + albumId + ");return false;' " +
                                       "href=''>" +
                                          "Delete" +
                                    "</a>" +
                                "</span>" +
                            "</div>" +
                      "</div>";
        });
        $('div.container').append(content1 + innerContent + content3);
    }
    function getChangedPhotos() {
        var array = $('.description');
        var photos = [];
        $.each(array, function (index, item) {
            var id = $(item).attr('data-photoID');
            var description = $(item).val();
            var photo = {
                PhotoID: id,
                Description: description
            }
            photos.push(photo);
        });
        return photos;
    }
    function getPhotoCheckedToDelete(sender) {
        var divEditImage = $(sender).closest('div.divEditImage');
        var divImage = divEditImage.prevAll().eq(0);
        var src = divImage.find('img').attr('src');
        var photo = {
            PhotoUrl: src
        }
        return photo;
    }
    function getPhotoAlbumInfo() {
        var photoAlbum = {
            Name: $('#albumTitle').val(),
            Description: $('#description').val()
        }
        return photoAlbum;
    }
    function closeModalWindow() {
        $('#overlay').fadeOut().find('#modal').fadeOut();
        $('#overlay').empty();
    }
    function getAllAlbumsPanel() {
        var template =
            "<div class='A'>" +
                "<div class='B'>" +
                    "<span>" +
                        "<a class='btn' href=''>All photos</a>" +
                    "</span>" +
                "</div>" +

                "<a " +
                     "href='' " +
                     "id='createAlbum' " +
                     "onclick='return vm.createAlbumForm()' " +
                     "class='C'>" +
                            "Create album" +
                "</a>" +
            "</div>" +

            "<div class='input_container'>" +
                "<input " +
                      "type='file' " +
                      "id='inputfile' " +
                      "onchange='return vm.sendPhotos()' />" +
            "</div>" +

            "<div " +
                "id='addNewPhotos' " +
                "class='addNewPhotos' " +
                "onclick='vm.upload()' " +
                "onmouseover='vm.changeColor(this)' " +
                "onmouseout='vm.changeColor(this)'>" +
                "Add new avatar" +
            "</div>" +

            "<br /><br />" +
            "<div class='D'>" +
                "You have <span id='albumsCount'></span> albums" +
            "</div><br /><br />" +
            "<div id='albums'></div>" +
            "<div id='photos'></div>" +
            "<div id='overlay'></div>";

        $('div.container').html(template);
    }
    function upload() {
        $('#inputfile').click();
    }
    function showPhotoAlbums(data) {
        $.each(data, function (index, album) {
            var content =
                "<b>" + album.AlbumName + "</b>" +
                "<a onclick=\"return vm.getAlbum(" + album.PhotoAlbumID + ");\" >" +
                    "<img width='170px' src='" + ROOT + album.LastPhotoUrl + "'/>" +
                "</a><br/>";

            $('#albums').append(content);
        })
    }
    function showAlbumsCount(count) {
        $('#albumsCount').text(count);
    }
    function setProfileId(id) {
        $('#hdnProfileID').val(id);
    }
    function createAlbumPanel(data, albumId) {
        var template =
                "<div class='A'>" +

                "<div class='B'>" +
                  "<span>" +

                      "<a " +
                         "onclick='return vm.activate()' >" +
                             "All photos" +
                      "</a>" +

                      "&nbsp;&nbsp;" +

                      "<a class='btn'>My wall photos</a>" +

                  "</span>" +
                "</div>" +

                "<a " +
                    "href='' " +
                    "class='C' " +
                    "onclick=\"return vm.editAlbum(" + albumId + ")\"   >" +
                         "Edit album" +
                "</a>" +

                "</div>" +
                "<a " +
                   "onmouseover='vm.changeColor(this)' " +
                   "onmouseout='vm.changeColor(this)' " +
                   "onclick='return vm.upload()' " +
                   "id='addNewPhotos' href='' " +
                   "class='addNewPhotos'>" +
                              "Add photos to album" +
                "</a>" +

                "<br /><br />" +

                "<div class='D'>" + data + " photos in album | " +
                   "<span class='download'>" +
                        "<a " +
                           "id='download_all' " +
                           "href='' " +
                           "onclick='return vm.multiDownload()' >" +
                              "Download album" +
                        "</a>" +
                   "</span>" +
                "</div>" +

                "<div class='input_container'  >" +

                "<input " +
                      "type='file' " +
                      "id='inputfile' " +
                      "onchange='return vm.sendPhotosToAlbum(" + albumId + ")'   />" +

                "</div>";
        $('div.container').html(template);
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
            innerContent += "<li class='list' data-photoname='" + photo.Name + "'" +
                            "class='col-xs-6 col-sm-4 col-md-3' " +
                           "data-sub-html='" +
                           "<h1><p>" + photo.Description + "</p></h1>'" +
                            "data-src='" + ROOT + photo.Photourl + "'  >" +
                                "<a href=''>" +
                                   "<img width='100px' class='img-responsive' src='" + ROOT + photo.ThumbnailPhotoUrl + "'  />" +
                                "</a>" +
                        "</li>";
        });
        $('div.container').append(content1 + innerContent + content3);
        $('#lightgallery').lightGallery();

        //ссылки на фотографии, которые будут скачаны во время нажатия "Скачать альбом" 
        var elements = document.getElementsByClassName('list');
        var photoNames = [];
        $('#hiddenAlbumLinks').empty();
        for (var i = 0; typeof (elements[i]) != 'undefined'; i++) {
            photoNames.push('/webapi/photos/downloadimage?name=' + elements[i].getAttribute('data-photoname'));
            $('#hiddenAlbumLinks').append("<a href='" + photoNames[i] + "'  class='document' />");
        }
    }
    function getLoadedImage() {
        var files = $('#inputfile').get(0).files;
        if (files.length > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                data.append("UploadedImage", files[0]);
                return data;
            }
            else
                alert("Ваш браузер не поддерживает загрузку файлов HTML5.");
        }
        else
            return null;
    }
    function multiDownload() {
        $('.document').multiDownload({ delay: 2000 });
    }
    function createAlbumForm() {
        var template =
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
                  "</div>" +
               "</div>" +
               "<div id='K' >" +
                    "<br />" +
                    "<input type='button' " +
                           "value='Create album' " +
                           "onclick='vm.createAlbum()'  />" +
               "</div>" +
           "</div>" +
       "</div>";
        $('#overlay').html(template);
        $('#overlay')
           .fadeIn()
           .find('#modal')
           .fadeIn();
    }
}
 