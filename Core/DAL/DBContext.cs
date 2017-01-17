using Microsoft.AspNet.Identity.EntityFramework;
using Core.POCO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Core.BLL;

namespace Core.DAL
{
    public class DBContext : IdentityDbContext<ApplicationUser>
    {
        public DBContext() : base("NW_DB")
        {
            Database.SetInitializer(new DBInitializer());
        }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<RelationshipDefinition> RelationshipDefinitions { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PrivacyFlag> PrivacyFlags { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<VisibilityLevel> VisibilityLevels { get; set; }
        public DbSet<PrivacyFlagType> PrivacyFlagTypes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoAlbum> PhotoAlbums { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<RightsOfVisitorOnThePage> UserRights { get; set; }
    }

    public class DBInitializer : DropCreateDatabaseIfModelChanges<DBContext>

    //public class DBInitializer : DropCreateDatabaseAlways<DBContext>
    {
        protected override void Seed(DBContext db)
        {
            #region Agreement
            db.Agreements.Add(new Agreement { CreateDate = DateTime.Now, Terms = "This is agreement..." });
            db.SaveChanges();
            #endregion

            #region RelationshipDefinitions

            db.RelationshipDefinitions.AddRange(

                new List<RelationshipDefinition> {
                    new RelationshipDefinition { RelationshipDefinitionID = 1, Definition = "pageOwner" },
                    new RelationshipDefinition { RelationshipDefinitionID = 2, Definition = "friend" },
                    new RelationshipDefinition { RelationshipDefinitionID = 3, Definition = "unknown" }
                });

            db.SaveChanges();

            #endregion

            #region PrivacyFlagType
            var privacyFlagType1 = new PrivacyFlagType()
            {
                ID = 1,
                FieldName = "who can view the main information on my profile"
            };
            var privacyFlagType2 = new PrivacyFlagType()
            {
                ID = 2,
                FieldName = "who can view posts and comments on the main page"
            };
            var privacyFlagType3 = new PrivacyFlagType()
            {
                ID = 3,
                FieldName = "who can leave posts on my main page"
            };
            var privacyFlagType4 = new PrivacyFlagType()
            {
                ID = 4,
                FieldName = "who can see comments on my main page"
            };
            var privacyFlagType5 = new PrivacyFlagType()
            {
                ID = 5,
                FieldName = "who can leave comments on my main page"
            };

            db.PrivacyFlagTypes.AddRange(new List<PrivacyFlagType> { privacyFlagType1, privacyFlagType2, privacyFlagType3, privacyFlagType4, privacyFlagType5 });
            db.SaveChanges();
            #endregion

            #region VisibilityLevel

            //ДОСТУПНЫЕ УРОВНИ ВИДИМОСТИ - КОНСТАНТЫ
            var visLevel1 = new VisibilityLevel()
            {
                ID = 1,
                Name = "All users"
            };
            var visLevel2 = new VisibilityLevel()
            {
                ID = 2,
                Name = "Friends"
            };
            var visLevel3 = new VisibilityLevel()
            {
                ID = 3,
                Name = "Only me"
            };
            db.VisibilityLevels.AddRange(new List<VisibilityLevel> { visLevel1, visLevel2, visLevel3 });
            db.SaveChanges();
            #endregion

            #region UserRights

            //------Only me------
            var right1 = new RightsOfVisitorOnThePage
            {
                VisibilityLevelId = 3, //Me
                RelationshipDefinitionID = 1 //page owner 
            };

            //------Only friends------
            var right2 = new RightsOfVisitorOnThePage
            {
                VisibilityLevelId = 2, //Friends
                RelationshipDefinitionID = 2 //friend
            };
            var right3 = new RightsOfVisitorOnThePage
            {
                VisibilityLevelId = 2, //Friends
                RelationshipDefinitionID = 1 //page owner
            };

            //------All users------
            var right4 = new RightsOfVisitorOnThePage
            {
                VisibilityLevelId = 1, //All users
                RelationshipDefinitionID = 3 //unknown 
            };
            var right5 = new RightsOfVisitorOnThePage
            {
                VisibilityLevelId = 1, //All users
                RelationshipDefinitionID = 2 //friend
            };
            var right6 = new RightsOfVisitorOnThePage
            {
                VisibilityLevelId = 1, //All users
                RelationshipDefinitionID = 1 //page owner 
            };

            db.UserRights.AddRange(
                new List<RightsOfVisitorOnThePage> { right1, right2, right3, right4, right5, right6 });
            db.SaveChanges();
            #endregion

            #region Country
            var Ukraine = new Country
            {
                CountryID = 1,
                CountryName = "Ukraine",

                Towns = new List<Town> {
                    new Town { TownName = "Odesa", CountryID = 1, TownID=1 },
                    new Town { TownName = "Gorishni Plavni", CountryID = 1,TownID=2 },
                    new Town { TownName = "Khmelnytskyy", CountryID = 1,TownID=3 },
                    new Town { TownName = "Lviv", CountryID = 1,TownID=4 },
                    new Town { TownName = "Vinnitsya", CountryID = 1,TownID=5 },
                    new Town { TownName = "Rivno", CountryID = 1 ,TownID=6 },
                    new Town { TownName = "Sumy", CountryID = 1 ,TownID=7 },
                    new Town { TownName = "Chernigiv", CountryID = 1,TownID=8 }
                }
            };
            db.Countries.Add(Ukraine);

            var Australia = new Country
            {
                CountryID = 2,
                CountryName = "Australia",

                Towns = new List<Town> {
                    new Town { TownName = "Broome", CountryID = 2,TownID=9 },
                    new Town { TownName = "Perth", CountryID = 2, TownID=10 },
                    new Town { TownName = "Albany", CountryID = 2, TownID=11 },
                    new Town { TownName = "Darwin", CountryID = 2,TownID=12 },
                    new Town { TownName = "Cairns", CountryID = 2,TownID=13 },
                    new Town { TownName = "Brisbane", CountryID = 2,TownID=14 }
                }
            };
            db.Countries.Add(Australia);

            var Botswana = new Country
            {
                CountryID = 3,
                CountryName = "Botswana",

                Towns = new List<Town> {
                    new Town { TownName = "Gaborone", CountryID = 3,TownID=15 },
                    new Town { TownName = "Francistown", CountryID = 3,TownID=16 },
                    new Town { TownName = "Molepolole", CountryID = 3,TownID=17 },
                    new Town { TownName = "Serowe", CountryID = 3,TownID=18 },
                    new Town { TownName = "Maun", CountryID = 3,TownID=19 },
                    new Town { TownName = "Kanye", CountryID = 3,TownID=20 },
                    new Town { TownName = "Mahalapye", CountryID = 3,TownID=21 },
                    new Town { TownName = "Mogoditshane", CountryID = 3,TownID=22 },
                    new Town { TownName = "Mochudi",CountryID = 3, TownID=23 }
                }
            };
            db.Countries.Add(Botswana);
            db.SaveChanges();
            #endregion

            #region Users 
            var profileService = new ProfileService(new UnitOfWork(), new CountriesRepository(db), new MappingService());
            var settingsService = new SettingsService(new UnitOfWork());
            var photoService = new PhotoService(new UnitOfWork());
            string userId = null;
            var store = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(store);


            var profile = new Profile
            {
                Country = "Ukraine",
                Town = "Odesa",
                FirstName = "Гена",
                LastName = "Василенко",
                BirthDay = 1,
                BirthMonth = 4,
                BirthYear = 1980,
                Gender = "Male",
                MaritalStatus = "Single",
                CreationDate = DateTime.Now,
                Language = "English",
                Music = "Владимирский централ"
            };
            var user = new ApplicationUser()
            {
                UserName = "Gena",
                Email = "gena@gmail.com",
                PhoneNumber = "(099)23-04-227"
            };
            string password = "GenaGena";
            userManager.Create(user, password);
            userId = user.Id;
            int profileId = profileService.AttachProfileToUser(userId, profile);
            settingsService.AttachPrivacyToUserProfile(profileId);
            photoService.AttachMainWallPhotoAlbumToUser(userId);
            photoService.CreateDefaultAvatar(userId);


            profile = new Profile
            {
                Country = "Ukraine",
                Town = "Odesa",
                FirstName = "Олег",
                LastName = "Дятлов",
                BirthDay = 1,
                BirthMonth = 4,
                BirthYear = 1985,
                Gender = "Male",
                MaritalStatus = "Single",
                CreationDate = DateTime.Now,
                Language = "English",
                Music = "Beatles"
            };
            user = new ApplicationUser()
            {
                UserName = "Oleg",
                Email = "oleg@gmail.com",
                PhoneNumber = "(099)23-04-224"
            };
            password = "OlegOleg";
            userManager.Create(user, password);
            userId = user.Id;
            profileId = profileService.AttachProfileToUser(userId, profile);
            settingsService.AttachPrivacyToUserProfile(profileId);
            photoService.AttachMainWallPhotoAlbumToUser(userId);
            photoService.CreateDefaultAvatar(userId);


            profile = new Profile
            {
                Country = "Ukraine",
                Town = "Gorishni Plavni",
                FirstName = "Герасим",
                LastName = "Иванов",
                BirthDay = 1,
                BirthMonth = 4,
                BirthYear = 1990,
                Gender = "Male",
                MaritalStatus = "Married",
                CreationDate = DateTime.Now,
                Language = "English",
                Music = "Sher"
            };
            user = new ApplicationUser()
            {
                UserName = "Gerasim",
                Email = "gerasim@gmail.com",
                PhoneNumber = "(099)23-04-224"
            };
            password = "GerasimGerasim";
            userManager.Create(user, password);
            userId = user.Id;
            profileId = profileService.AttachProfileToUser(userId, profile);
            settingsService.AttachPrivacyToUserProfile(profileId);
            photoService.AttachMainWallPhotoAlbumToUser(userId);
            photoService.CreateDefaultAvatar(userId);


            profile = new Profile
            {
                Country = "Australia",
                Town = "Broome",
                FirstName = "Eva",
                LastName = "Lancer",
                BirthDay = 1,
                BirthMonth = 4,
                BirthYear = 1995,
                Gender = "Female",
                MaritalStatus = "Single",
                CreationDate = DateTime.Now,
                Language = "English",
                Music = "Beatles"
            };
            user = new ApplicationUser()
            {
                UserName = "Eva",
                Email = "eva@gmail.com",
                PhoneNumber = "(099)23-04-224"
            };
            password = "EvaEva";
            userManager.Create(user, password);
            userId = user.Id;
            profileId = profileService.AttachProfileToUser(userId, profile);
            settingsService.AttachPrivacyToUserProfile(profileId);
            photoService.AttachMainWallPhotoAlbumToUser(userId);
            photoService.CreateDefaultAvatar(userId);


            profile = new Profile
            {
                Country = "Australia",
                Town = "Perth",
                FirstName = "Albina",
                LastName = "Potapenko",
                BirthDay = 1,
                BirthMonth = 4,
                BirthYear = 2000,
                Gender = "Female",
                MaritalStatus = "Single",
                CreationDate = DateTime.Now,
                Language = "English",
                Music = "Beatles"
            };
            user = new ApplicationUser()
            {
                UserName = "Albina",
                Email = "albina@gmail.com",
                PhoneNumber = "(099)23-04-224"
            };
            password = "Albina";
            userManager.Create(user, password);
            userId = user.Id;
            profileId = profileService.AttachProfileToUser(userId, profile);
            settingsService.AttachPrivacyToUserProfile(profileId);
            photoService.AttachMainWallPhotoAlbumToUser(userId);
            photoService.CreateDefaultAvatar(userId);
           

            profile = new Profile
            {
                Country = "Botswana",
                Town = "Gaborone",
                FirstName = "Olga",
                LastName = "Isaenko",
                BirthDay = 1,
                BirthMonth = 4,
                BirthYear = 1975,
                Gender = "Female",
                MaritalStatus = "Married",
                CreationDate = DateTime.Now,
                Language = "English",
                Music = "Beatles"
            };
            user = new ApplicationUser()
            {
                UserName = "Olga",
                Email = "olga@gmail.com",
                PhoneNumber = "(099)23-04-224"
            };
            password = "OlgaOlga";
            userManager.Create(user, password);
            userId = user.Id;
            profileId = profileService.AttachProfileToUser(userId, profile);
            settingsService.AttachPrivacyToUserProfile(profileId);
            photoService.AttachMainWallPhotoAlbumToUser(userId);
            photoService.CreateDefaultAvatar(userId);
            #endregion Users

        }
    }
}

