using  Core.POCO; 

namespace  Core.BLL.Interfaces
{
    public interface IPhotoService
    {
        int? AddPhoto(Photo photo);
        void AttachMainWallPhotoAlbumToUser(string userId);
        void CreateDefaultAvatar(string userId);
        PhotoAlbum GetWallMainAlbum(string userId);
        int CreatePhotoAlbum(PhotoAlbum album, string userId);
        void DeleteAlbum(int albumID, string userId);
        void DeletePhotoFromAlbum(Photo photo, string userId);
        void DeletePhotoFromFolder(Photo photo);
        int GetAlbumsCountByUserID(string userId);
        Photo[] GetAllPhotosByUserID(string userId);
        string[] GetImageNamesFromAlbum(int? albumId);
        string GetLargeAvatarImg(string UserId);
        PhotoAlbumResult[] GetPhotoAlbums(string userId);
        int[] GetPhotoAlbumsIds(string userId);
        Photo[] GetPhotosByAlbumID(int albumId);
        int GetPhotosCountByAlbumID(int albumID);
        PhotoUrlResult[] GetPhotoUrlsByAlbumID(int albumId, string userId);
        string GetThumbAvatarImg(string userId);
        PhotoAlbum GetWallPhotoAlbum(string userId);
        void UpdateAvatar(Photo photo, string userId);
        void UpdatePhotoDescription(Photo[] photos);
        string GetAvatar(string userId);
    }
}