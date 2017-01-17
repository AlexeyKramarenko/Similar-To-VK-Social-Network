
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.IO;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using Microsoft.AspNet.Identity;
using Core.BLL.Interfaces;
using Core.POCO;
using WebFormsApp.Services;

namespace WebFormsApp.WebApi
{
    public class PhotosController : ApiController
    {
        private static int? _albumId; //null - Avatar, 0,1,2.. - id of album         
             
        IPhotoService photoService;
        ISessionService sessionService;

        public string CurrentUserId
        {
            get
            {
                return sessionService.CurrentUserId;
            }
        }

        public PhotosController(IPhotoService photoService, ISessionService sessionService)
        { 
            this.photoService =  photoService;
            this.sessionService = sessionService;
        }

        [HttpGet]
        [ActionName("GetAllPhotosByUserID")]
        public IHttpActionResult GetAllPhotosByUserID([FromUri]string userId)
        {
            Photo[] photos = photoService.GetAllPhotosByUserID(userId);

            if (photos != null)
                return Ok(photos);

            else
                return NotFound();
        }

        [HttpGet]
        [ActionName("GetPhotosByAlbumID")]
        public IHttpActionResult GetPhotosByAlbumID([FromUri]int albumId)
        {
            Photo[] photos = photoService.GetPhotosByAlbumID(albumId);

            if (photos != null)
                return Ok(photos);

            else
                return NotFound();
        }
        [HttpPost]
        [ActionName("AttachPhotosToAlbum")]
        public void AttachPhotosToAlbum([FromBody]int albumId)
        {
            _albumId = albumId;
        }

        [HttpGet]
        [ActionName("GetPhotoAlbums")]
        public IHttpActionResult GetPhotoAlbums()
        {
            object[] albums = photoService.GetPhotoAlbums(CurrentUserId);

            if (albums != null)
                return Ok(albums);

            else
                return NotFound();
        }

        [HttpPost]
        public int? PostFile()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                HttpPostedFile httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

                if (httpPostedFile != null)
                {
                    #region Variables
                    if (_albumId == 0) //Wall main album
                    {
                        PhotoAlbum album = photoService.GetWallMainAlbum(CurrentUserId);

                        if (album != null)
                            _albumId = album.PhotoAlbumID;
                    }

                    // Validate the uploaded image(optional)
                    string path = HttpContext.Current.Server.MapPath("~/UsersFolder/");
                    string subpath = CurrentUserId;

                    string foldersPath = "UsersFolder/" + subpath + "/";
                    string virtualPathFolder = "~/" + foldersPath;
                    string physicalPathFolder = HttpContext.Current.Server.MapPath(virtualPathFolder);

                    string prefix = Guid.NewGuid().ToString().Substring(0, 10);
                    string fileNameWithExtension = prefix + httpPostedFile.FileName.Replace("%", "");

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameWithExtension);
                    string extension = Path.GetExtension(fileNameWithExtension);
                    string fileSavePath = Path.Combine(physicalPathFolder, fileNameWithExtension);
                    string fileUrl = Path.Combine(foldersPath, fileNameWithExtension);

                    string thumbFileSavePath = Path.Combine(physicalPathFolder + fileNameWithoutExtension + "_thumb" + extension);
                    string thumbFileUrl = Path.Combine(foldersPath + fileNameWithoutExtension + "_thumb" + extension);
                    #endregion

                    #region Create directory

                    var pathDirectory = new DirectoryInfo(path);
                    var subpathDirectory = new DirectoryInfo(path + subpath);

                    if (!subpathDirectory.Exists)
                        pathDirectory.CreateSubdirectory(subpath);

                    #endregion

                    #region Save large file

                    httpPostedFile.SaveAs(fileSavePath);

                    #endregion

                    #region Thumbnail
                    var image = Image.FromFile(fileSavePath);

                    using (Image bigImage = new Bitmap(fileSavePath))
                    {
                        // Algorithm simplified for purpose of example.
                        int height = bigImage.Height / 10;
                        int width = bigImage.Width / 10;

                        // Now create a thumbnail
                        using (Image smallImage = image.GetThumbnailImage(width, height, new Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
                            smallImage.Save(thumbFileSavePath);

                    }
                    #endregion

                    #region Save img sources

                    var photo = new Photo
                    {
                        PhotoAlbumID = _albumId,
                        UserID = CurrentUserId,
                        PhotoUrl = fileUrl,
                        ThumbnailPhotoUrl = thumbFileUrl,
                        Name = fileNameWithExtension
                    };

                    int? albumID = null;

                    if (_albumId != null)
                        albumID = photoService.AddPhoto(photo);

                    else
                        photoService.UpdateAvatar(photo, CurrentUserId);

                    _albumId = null; //reset for the next reqest
                    #endregion

                    return albumID;
                }
                return null;
            }
            return null;

        }
        public bool ThumbnailCallback()
        {
            return false;
        }

        [HttpGet]
        [ActionName("GetLargeAvatarImg")]
        public IHttpActionResult GetLargeAvatarImg([FromUri]string UserID)
        {
            string img = photoService.GetLargeAvatarImg(UserID);

            if (img != null)
                return Ok(img);

            else
                return NotFound();
        }

        [HttpGet]
        [ActionName("GetThumbAvatarImg")]
        public IHttpActionResult GetThumbAvatarImg([FromUri]string UserID)
        {
            string img = photoService.GetThumbAvatarImg(UserID);

            if (img != null)
                return Ok(img);

            else
                return NotFound();
        }

        [HttpGet]
        [ActionName("GetImageNamesFromAlbum")]
        public IHttpActionResult GetImageNamesFromAlbum()//[FromBody] int albumId)
        {
            string[] names = photoService.GetImageNamesFromAlbum(_albumId);

            if (names != null)
                return Ok(names);

            else
                return NotFound();
        }


        [HttpGet]
        [ActionName("DownloadImage")]
        public HttpResponseMessage DownloadImage([FromUri] string Name)
        {
            HttpResponseMessage result = null;
            var localFilePath = HttpContext.Current.Server.MapPath("~/UsersFolder/" + CurrentUserId + "/" + Name);

            // check if parameter is valid
            if (String.IsNullOrEmpty(Name))
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            // check if file exists on the server
            else if (!File.Exists(localFilePath))
            {
                result = Request.CreateResponse(HttpStatusCode.Gone);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = Name;
            }

            return result;
        }



        [HttpGet]
        [ActionName("GetPhotoAlbumsIds")]
        public IHttpActionResult GetPhotoAlbumsIds()
        {
            int[] ids = photoService.GetPhotoAlbumsIds(CurrentUserId);

            if (ids != null)
                return Ok(ids);

            else
                return NotFound();
        }

        [HttpGet]
        [ActionName("GetPhotoUrlsByAlbumID")]
        public IHttpActionResult GetPhotoUrlsByAlbumID(int Id)
        {
            object[] urls = photoService.GetPhotoUrlsByAlbumID(Id, CurrentUserId);

            if (urls != null)
                return Ok(urls);

            else
                return NotFound();
        }

        [HttpPut]
        [ActionName("UpdatePhotoDescription")]
        public void UpdatePhotoDescription([FromBody] Photo[] photos)
        {
            photoService.UpdatePhotoDescription(photos);
        }


        [HttpGet]
        [ActionName("GetAlbumsCountByUserID")]
        public IHttpActionResult GetAlbumsCountByUserID()
        {
            int count = photoService.GetAlbumsCountByUserID(CurrentUserId);

            if (count >= 0)
                return Ok(count);

            else
                return NotFound();
        }


        [HttpGet]
        [ActionName("GetPhotosCountByAlbumID")]
        public IHttpActionResult GetPhotosCountByAlbumID(int id)
        {
            int count = photoService.GetPhotosCountByAlbumID(id);

            if (count >= 0)
                return Ok(count);

            else
                return NotFound();
        }

        [HttpPost]
        [ActionName("CreatePhotoAlbum")]
        public IHttpActionResult CreatePhotoAlbum(PhotoAlbum PhotoAlbum)
        {
            if (PhotoAlbum != null)
            {
                int albumId = photoService.CreatePhotoAlbum(PhotoAlbum, CurrentUserId);

                return Created(Request.RequestUri, albumId);
            }
            else
                return BadRequest("Empty");
        }

        [HttpDelete]
        [ActionName("DeletePhotoFromAlbum")]
        public void DeletePhotoFromAlbum([FromBody] Photo photo)
        {
            photoService.DeletePhotoFromAlbum(photo, CurrentUserId);
            photoService.DeletePhotoFromFolder(photo);
        }
    }
}
