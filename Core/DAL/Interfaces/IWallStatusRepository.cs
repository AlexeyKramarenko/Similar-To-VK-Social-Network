using  Core.POCO;

namespace  Core.DAL.Interfaces
{
    public interface IWallStatusRepository
    {
        PhotoRepository photoRepository { get; set; }
        Comment GetComment(int id, string userId);
        void DeleteComment(Comment comment);
        void DeleteStatus(Status status);
        void InsertComment(Comment com);
        InsertStatusResult InsertStatus(Status status); 
        Status GetStatusById(int id);
    }
}