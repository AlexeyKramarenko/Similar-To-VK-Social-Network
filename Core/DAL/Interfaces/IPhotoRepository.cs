using  Core.POCO; 
using System.Linq;

namespace  Core.DAL.Interfaces
{
    public interface IPhotoRepository
    {
        IQueryable<PhotoAlbum> PhotoAlbums { get; }
        int? AddPhoto(Photo photo);
        PhotoAlbum GetWallMainAlbum(string userId);
        void AttachMainWallPhotoAlbumToUser(string userId);
        void CreateDefaultAvatar(string UserID);
        int CreatePhotoAlbum(PhotoAlbum album, string userId);
        void DeleteAlbum(int albumID);
        void DeletePhotoFromAlbum(string photoUrl, string userId);
        void DeletePhotoFromAlbum(Photo photo, string userId);

        int GetAlbumsCountByUserID(string userId);
        Photo[] GetAllPhotosByUserID(string userID);
        string[] GetImageNamesFromAlbum(int? albumId);
        string GetLargeAvatarImg(string UserID);
        PhotoAlbumResult[] GetPhotoAlbums(string userId);
        int[] GetPhotoAlbumsIds(string userId);
        Photo[] GetPhotosByAlbumID(int albumId);
        int GetPhotosCountByAlbumID(int albumID);
        PhotoUrlResult[] GetPhotoUrlsByAlbumID(int albumId, string userId);
        string GetThumbAvatarImg(string UserID);
        PhotoAlbum GetWallPhotoAlbum(string userId);
        void UpdateAvatar(Photo photo, string userId);
        void UpdatePhotoDescription(Photo[] photos);
        string GetAvatar(string userId);
        void DeletePhotoFromFolder(string physicalPath);
    }
}