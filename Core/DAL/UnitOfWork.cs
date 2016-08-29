using Core.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.POCO;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Core.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        DBContext db;

        IRelationshipsRepository friendsRepository;
        IMessagesRepository messagesRepository;
        IPhotoRepository photoRepository;
        IProfileRepository profileRepository;
        ISettingsRepository settingsRepository;
        IUserRepository userRepository;
        IWallStatusRepository wallStatusRepository;
        ICountriesRepository countriesRepository;
        IUserRightsRepository userRightsRepository;
        IPrivacyRepository privacyRepository;
        public UnitOfWork()
        {
            db = new DBContext();
        }

        private bool disposed = false;



        public IMessagesRepository Messages
        {
            get
            {
                if (messagesRepository == null)
                    messagesRepository = new MessagesRepository(db);
                return messagesRepository;
            }
        }

        public IPhotoRepository Photos
        {
            get
            {
                if (photoRepository == null)
                    photoRepository = new PhotoRepository(db);
                return photoRepository;
            }
        }
        public IUserRepository UserManager
        {
            get
            {
                if (userRepository == null)
                    userRepository = new ApplicationUserManager(new UserStore<ApplicationUser>(db), db);
                return userRepository;
            }
        }
        public IProfileRepository Profiles
        {
            get
            {
                if (profileRepository == null)
                    profileRepository = new ProfileRepository(db, UserManager);
                return profileRepository;
            }
        }
        public IRelationshipsRepository Relationships
        {
            get
            {
                if (friendsRepository == null)
                    friendsRepository = new RelationshipsRepository(db, UserManager);
                return friendsRepository;
            }
        }
        public ISettingsRepository Settings
        {
            get
            {
                if (settingsRepository == null)
                    settingsRepository = new SettingsRepository(db);
                return settingsRepository;
            }
        }



        public IWallStatusRepository WallStatuses
        {
            get
            {
                if (wallStatusRepository == null)
                    wallStatusRepository = new WallStatusRepository(db);
                return wallStatusRepository;
            }
        }
        public ICountriesRepository Countries
        {
            get
            {
                if (countriesRepository == null)
                    countriesRepository = new CountriesRepository(db);
                return countriesRepository;
            }
        }
        public IUserRightsRepository UserRights
        {
            get
            {
                if (userRightsRepository == null)
                    userRightsRepository = new UserRightsRepository(db);
                return userRightsRepository;
            }
        }

        public IPrivacyRepository Privacy 
        {
            get
            {
                if (privacyRepository == null)
                    privacyRepository = new PrivacyRepository(db);
                return privacyRepository;
            }
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();

                    ((ApplicationUserManager)UserManager).Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Save()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                );
            }
        }
    }
}
