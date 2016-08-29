using  Core.BLL.Interfaces;
using  Core.DAL.Interfaces;
using  Core.POCO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace  Core.BLL
{
    public class PhotoService : LogicLayer, IPhotoService
    {
        IUnitOfWork Database;
        public PhotoService(IUnitOfWork db)
        {
            Database = db;
        }
        public IQueryable<PhotoAlbum> PhotoAlbums
        {
            get
            {
                return Database.Photos.PhotoAlbums;
            }
        }

        public PhotoAlbum GetWallMainAlbum(string userId)
        {
            return Database.Photos.GetWallMainAlbum(userId);
        }
        public int GetAlbumsCountByUserID(string userId)
        {
            string key = "AlbumsCountOfUserId_" + userId;

            int count;

            if (Cache[key] != null)
                count = (int)Cache[key];
            else
            {
                count = Database.Photos.GetAlbumsCountByUserID(userId);
                Cache[key] = count;
            }

            return count;
        }
        public void DeleteAlbum(int albumID, string userId)
        {
            Database.Photos.DeleteAlbum(albumID);
            Database.Save();

            string key = "AlbumsCountOfUserId_" + userId;
            PurgeCacheItems(key);
        }
        public int CreatePhotoAlbum(PhotoAlbum album, string userId)
        {
            var photoAlbumID = Database.Photos.CreatePhotoAlbum(album, userId);

            string key = "AlbumsCountOfUserId_" + userId;
            PurgeCacheItems(key);

            return photoAlbumID;
        }
        public int? AddPhoto(Photo photo)
        {
            var photoAlbumID = Database.Photos.AddPhoto(photo);

            return photoAlbumID;
        }

        public Photo[] GetAllPhotosByUserID(string userID)
        {
            var list = Database.Photos.GetAllPhotosByUserID(userID);
            return list;
        }

        public Photo[] GetPhotosByAlbumID(int albumId)
        {
            var list = Database.Photos.GetPhotosByAlbumID(albumId);
            return list;
        }

        public void UpdateAvatar(Photo photo, string userId)
        {
            Database.Photos.UpdateAvatar(photo, userId);
            Database.Save();
        }

        public void CreateDefaultAvatar(string UserID)
        {
            Database.Photos.CreateDefaultAvatar(UserID);
            Database.Save();
        }

        public void DeletePhotoFromAlbum(Photo photo, string userId)
        {
            Database.Photos.DeletePhotoFromAlbum(photo, userId);
            Database.Save();
        }


        public PhotoAlbumResult[] GetPhotoAlbums(string userId)
        {
            var albums = Database.Photos.GetPhotoAlbums(userId);
            return albums;
        }

        public int GetPhotosCountByAlbumID(int albumID)
        {
            int count = Database.Photos.GetPhotosCountByAlbumID(albumID);
            return count;
        }

        public void AttachMainWallPhotoAlbumToUser(string userId)
        {
            Database.Photos.AttachMainWallPhotoAlbumToUser(userId);
            Database.Save();
        }


        public PhotoUrlResult[] GetPhotoUrlsByAlbumID(int albumId, string userId)
        {
            var urls = Database.Photos.GetPhotoUrlsByAlbumID(albumId, userId);
            return urls;
        }

        public void DeletePhotoFromFolder(Photo photo)
        { 
            string path = HttpContext.Current.Server.MapPath("~/" + photo.PhotoUrl);
            
            if ((File.GetAttributes(path) & FileAttributes.Hidden) == FileAttributes.ReadOnly)
            {
                File.SetAttributes(path, FileAttributes.Normal);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
        public int[] GetPhotoAlbumsIds(string userId)
        {
            int[] ids = Database.Photos.GetPhotoAlbumsIds(userId);
            return ids;
        }

        public PhotoAlbum GetWallPhotoAlbum(string userId)
        {
            var wallPhotoAlbum = Database.Photos.GetWallPhotoAlbum(userId);
            return wallPhotoAlbum;
        }

        public void UpdatePhotoDescription(Photo[] photos)
        {
            Database.Photos.UpdatePhotoDescription(photos);
            Database.Save();
        }


        public string[] GetImageNamesFromAlbum(int? albumId)
        {
            string[] namesArray = Database.Photos.GetImageNamesFromAlbum(albumId);
            return namesArray;
        }

        public string GetLargeAvatarImg(string UserID)
        {
            var img = Database.Photos.GetLargeAvatarImg(UserID);
            return img;
        }

        public string GetThumbAvatarImg(string UserID)
        {
            var img = Database.Photos.GetThumbAvatarImg(UserID);
            return img;
        }

        public string GetAvatar(string userId)
        {
            return Database.Photos.GetAvatar(userId);
        }
    }
}
