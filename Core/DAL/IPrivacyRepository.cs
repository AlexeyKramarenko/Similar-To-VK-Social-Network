using Core.POCO;
using System.Collections.Generic;

namespace Core.DAL
{
    public interface IPrivacyRepository
    {
        List<PrivacyFlag> GetPrivacyCollection(int profileId);
    }
}