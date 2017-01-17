using Core.DAL.Interfaces;
using Core.POCO;

namespace Core.BLL.Interfaces
{
    public interface IWallStatusService
    {
        IUnitOfWork Database { get; set; }
        Comment GetComment(int id, string userId);
        void DeleteComment(Comment comment);
        Status GetStatusById(int id);
        void DeleteStatus(Status status);
        void InsertComment(Comment com);
        InsertStatusResult InsertStatus(Status status);
    }
}