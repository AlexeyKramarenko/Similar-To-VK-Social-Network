using Core.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DAL
{
    public class PrivacyRepository : IPrivacyRepository
    {
        DBContext db;
        public PrivacyRepository(DBContext db)
        {
            this.db = db;
        }
        public List<PrivacyFlag> GetPrivacyCollection(int profileId)
        {
            return db.PrivacyFlags.Where(a => a.ProfileID == profileId).ToList();
        }
    }
}
