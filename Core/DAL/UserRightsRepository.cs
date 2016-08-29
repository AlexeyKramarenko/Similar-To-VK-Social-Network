using Core.DAL.Interfaces;
using Core.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DAL
{
    public class UserRightsRepository: IUserRightsRepository
    {
        DBContext db;
        public UserRightsRepository(DBContext db)
        {
            this.db = db;
        }
        public List<UserRights> GetUserRights()
        {
          return  db.UserRights.ToList();
        }
    }
}
