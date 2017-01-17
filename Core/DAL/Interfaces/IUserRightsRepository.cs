using Core.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DAL.Interfaces
{
    public interface IUserRightsRepository
    {
        List<RightsOfVisitorOnThePage> GetUserRights();
    }
}
