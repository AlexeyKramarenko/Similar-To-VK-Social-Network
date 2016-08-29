using  Core.BLL.Interfaces;
using  Core.DAL.Interfaces;
using  Core.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Core.BLL
{
    public class WallStatusService : LogicLayer, IWallStatusService
    {
        public IUnitOfWork Database { get; set; }

        public WallStatusService(IUnitOfWork db)
        {
            Database = db;
        }
        public Status GetStatusById(int id)
        {
            string key = "StatusID_" + id;

            Status status = null;

            if (Cache[key] != null)
                status = (Status)Cache[key];

            else
            {
                status = Database.WallStatuses.GetStatusById(id);
                Cache[key] = status;
            }
            return status;
        }
        public InsertStatusResult InsertStatus(Status status)
        {
            string key = "StatusID_" + status.ID;

            InsertStatusResult result = Database.WallStatuses.InsertStatus(status);

            PurgeCacheItems(key);

            return result;
        }
        public void DeleteStatus(Status status)
        {
            Database.WallStatuses.DeleteStatus(status);
            Database.Save();

            string key = "StatusID_" + status.ID;
            PurgeCacheItems(key);
        }

        public object InsertComment(Comment com)
        {
            object result = Database.WallStatuses.InsertComment(com);
            return result;
        }

        public void DeleteComment(Comment comment)
        {
            Database.WallStatuses.DeleteComment(comment);
            Database.Save();
        }

        public Comment GetComment(int id, string userId)
        {
            return Database.WallStatuses.GetComment(id, userId);
        }


    }
}
