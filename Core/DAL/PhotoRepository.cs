using Core.DAL.Interfaces;
using Core.POCO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Core.DAL
{
    public class PhotoRepository : IPhotoRepository
    {

        string defaultUrl;

        private DBContext db;

        public IQueryable<PhotoAlbum> PhotoAlbums
        {
            get
            {
                return db.PhotoAlbums;
            }
        }
        public string GetAvatar(string userId)
        {
            Photo photo = db.Photos.FirstOrDefault(p => p.UserID == userId);

            if (photo != null)
                return photo.ThumbnailPhotoUrl;

            return defaultUrl;
        }
        public PhotoRepository(DBContext db)
        {
            this.db = db;
            defaultUrl = ConfigurationManager.AppSettings["defaultUrl"];
        }

        public PhotoAlbum GetWallMainAlbum(string userId)
        {
            PhotoAlbum album = PhotoAlbums.FirstOrDefault(a => a.IsWallAlbum == true && a.UserID == userId);
            return album;
        }
        public int? AddPhoto(Photo photo)
        {
            db.Entry(photo).State = System.Data.Entity.EntityState.Added;

            db.SaveChanges();

            return photo.PhotoAlbumID;
        }

        public Photo[] GetAllPhotosByUserID(string userID)
        {
            Photo[] list = db.Photos.Where(a => a.UserID == userID && a.PhotoAlbumID != null).ToArray();

            return list;
        }

        public Photo[] GetPhotosByAlbumID(int albumId)
        {
            Photo[] list = db.Photos.Where(a => a.PhotoAlbumID == albumId).ToArray();

            return list;
        }

        public void UpdateAvatar(Photo photo, string userId)
        {
            var _photo = db.Photos.FirstOrDefault(a => a.UserID == userId && a.PhotoAlbumID == null);

            if (_photo != null)
            {
                _photo.PhotoUrl = photo.PhotoUrl;
                _photo.ThumbnailPhotoUrl = photo.ThumbnailPhotoUrl;
            }
        }

        public void CreateDefaultAvatar(string UserID)
        {
            var photo = new Photo
            {
                UserID = UserID,
                PhotoAlbumID = null,
                PhotoUrl = defaultUrl,
                ThumbnailPhotoUrl = defaultUrl
            };
            db.Entry(photo).State = System.Data.Entity.EntityState.Added;
        }
        public void DeletePhotoFromAlbum(Photo photo, string userId)
        {
            var _photo = db.Photos.FirstOrDefault(p => p.PhotoUrl == photo.PhotoUrl && p.UserID == userId);

            if (_photo != null)
            {
                db.Entry(_photo).State = System.Data.Entity.EntityState.Deleted;
            }
        }
        public void DeleteAlbum(int albumID)
        {
            var album = db.PhotoAlbums.Find(albumID);

            if (album != null)
                db.Entry(album).State = System.Data.Entity.EntityState.Deleted;

        }
        public PhotoAlbumResult[] GetPhotoAlbums(string userId)
        {
            PhotoAlbumResult[] albums = db.PhotoAlbums
                            .Where(p => p.UserID == userId)
                            .Select(a => new PhotoAlbumResult
                            {
                                PhotoAlbumID = a.PhotoAlbumID,
                                AlbumName = a.Name,
                                LastPhotoUrl = (a.Photos.OrderByDescending(b => b.PhotoID).FirstOrDefault() != null)
                                                                ? a.Photos.OrderByDescending(b => b.PhotoID).FirstOrDefault().ThumbnailPhotoUrl
                                                                : defaultUrl
                            }).ToArray();

            return albums;
        }


        public int CreatePhotoAlbum(PhotoAlbum album, string userId)
        {
            album.IsWallAlbum = false;
            album.UserID = userId;
            db.Entry(album).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return album.PhotoAlbumID;
        }
        public int GetAlbumsCountByUserID(string userId)
        {
            return db.PhotoAlbums.Where(a => a.UserID == userId).Count();
        }
        public int GetPhotosCountByAlbumID(int albumID)
        {
            int count = db.Photos.Where(a => a.PhotoAlbumID == albumID).Count();
            return count;
        }
        public void AttachMainWallPhotoAlbumToUser(string userId)
        {
            var album = new PhotoAlbum { UserID = userId, Name = "My wall photos", IsWallAlbum = true };
            db.Entry(album).State = System.Data.Entity.EntityState.Added;
        }

        public PhotoUrlResult[] GetPhotoUrlsByAlbumID(int albumId, string userId)
        {
            List<Photo> photos = db.Photos.Where(a => a.PhotoAlbumID == albumId && a.PhotoAlbum.UserID == userId).ToList();

            var data = new PhotoUrlResult[photos.Count];

            for (int i = 0; i < photos.Count; i++)
            {
                data[i] = new PhotoUrlResult
                {
                    PhotoId = photos[i].PhotoID,
                    Photourl = photos[i].PhotoUrl,
                    ThumbnailPhotoUrl = photos[i].ThumbnailPhotoUrl,
                    Description = (photos[i].Description == null) ? "" : photos[i].Description,
                    Name = photos[i].Name
                };
            }
            return data;
        }

        public int[] GetPhotoAlbumsIds(string userId)
        {
            int[] ids = db.PhotoAlbums.Where(a => a.UserID == userId).Select(a => a.PhotoAlbumID).ToArray();

            return ids;
        }
        public PhotoAlbum GetWallPhotoAlbum(string userId)
        {
            var wallPhotoAlbum = db.PhotoAlbums.FirstOrDefault(a => a.UserID == userId && a.IsWallAlbum);
            return wallPhotoAlbum;
        }
        public void UpdatePhotoDescription(Photo[] photos)
        {
            foreach (var photo in photos)
            {
                var _photo = db.Photos.Find(photo.PhotoID);

                if (_photo != null)
                {
                    _photo.Description = photo.Description;
                }
            }
        }


        public string[] GetImageNamesFromAlbum(int? albumId)
        {
            var _anyPhotoInAlbum = db.Photos.Any(a => a.PhotoAlbumID == albumId);

            string[] namesArray = null;

            if (_anyPhotoInAlbum)
            {
                namesArray = db.Photos.Select(a => a.Name).ToArray();
            }

            return namesArray;
        }

        public string GetLargeAvatarImg(string UserID)
        {
            var img = db.Photos.FirstOrDefault(a => a.UserID == UserID && a.PhotoAlbumID == null);

            if (img != null)
                return img.PhotoUrl;

            return defaultUrl;
        }

        public string GetThumbAvatarImg(string UserID)
        {
            var img = db.Photos.FirstOrDefault(a => a.UserID == UserID && a.PhotoAlbumID == null);

            if (img != null)
                return img.ThumbnailPhotoUrl;

            return defaultUrl;
        }


    }
}
