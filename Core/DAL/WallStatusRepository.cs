using Core.DAL;
using Core.DAL.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Core.POCO;

namespace Core.DAL
{
    public class WallStatusRepository : IWallStatusRepository
    {
        DBContext db = null;

        public PhotoRepository photoRepository { get; set; }

        public WallStatusRepository(DBContext db)
        {
            this.db = db;
            photoRepository = new PhotoRepository(db);
        }

        public Comment GetComment(int id, string userId)
        {
            //удалить может только админ или автор комментария
            var comment = db.Comments.FirstOrDefault(a => a.ID == id && a.UserID == userId);

            if (comment == null)
                //или админ страницы
                comment = db.Comments
                      .Include(a => a.Status)
                      .FirstOrDefault(a => a.ID == id && a.Status.WallOfUserID == userId);

            return comment;
        }

        public void DeleteComment(Comment comment)
        {
            db.Entry(comment).State = System.Data.Entity.EntityState.Deleted;
        }

        public InsertStatusResult InsertStatus(Status status)
        {
            status.CreateDate = DateTime.Now;
            db.Entry(status).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();

            string Avatar = photoRepository.GetThumbAvatarImg(status.PostByUserID);

            return new InsertStatusResult { ID = status.ID, UserName = status.UserName, Avatar = Avatar };
        }

        public object InsertComment(Comment com)
        {
            com.CreateDate = DateTime.Now;

            db.Entry(com).State = System.Data.Entity.EntityState.Added;

            string avatar = photoRepository.GetAvatar(com.UserID);

            db.SaveChanges();

            return new { id = com.ID, commentatorsUserName = com.UserName, avatar };
        }
        public Status GetStatusById(int id)
        {
            var status = db.Statuses.FirstOrDefault(a => a.ID == id);
            return status;
        }
        public void DeleteStatus(Status status)
        {
            db.Entry(status).State = System.Data.Entity.EntityState.Deleted;
        }


    }
}
