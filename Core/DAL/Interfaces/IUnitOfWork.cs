
using System;

namespace Core.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRelationshipsRepository Relationships { get; }
        IMessagesRepository Messages { get; }
        IPhotoRepository Photos { get; }
        IProfileRepository Profiles { get; }
        ISettingsRepository Settings { get; }
        IUserRepository UserManager { get; }
        IWallStatusRepository WallStatuses { get; }
        ICountriesRepository Countries { get; }
        IUserRightsRepository UserRights { get; }
        IPrivacyRepository Privacy { get; }
        int Save();
    }
}
