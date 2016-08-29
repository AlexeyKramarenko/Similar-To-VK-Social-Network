using Microsoft.AspNet.Identity.EntityFramework;
using Core.POCO;
using System;
using System.Collections.Generic;
using System.Data.Entity;

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
        public DbSet<UserRights> UserRights { get; set; }
    }

    //public class DBInitializer : DropCreateDatabaseIfModelChanges<DBContext>

    public class DBInitializer : DropCreateDatabaseAlways<DBContext>
    {
        protected override void Seed(DBContext db)
        {
            #region Agreement
            db.Agreements.Add(new Agreement { CreateDate = DateTime.Now, Terms = "aaaaaaaaaaaa" });
            db.SaveChanges();
            #endregion

            #region Roles
            db.Roles.Add(new Role { Id = "1", Name = "registered" });
            db.Roles.Add(new Role { Id = "2", Name = "public" });
            db.SaveChanges();
            #endregion

            #region RelationshipDefinitions

            db.RelationshipDefinitions.AddRange(

                new List<RelationshipDefinition> {
                    new RelationshipDefinition { RelationshipDefinitionID = 1, Definition = "pageOwner" },
                    new RelationshipDefinition { RelationshipDefinitionID = 2, Definition = "friend" },
                    new RelationshipDefinition { RelationshipDefinitionID = 3, Definition = "subscriber" },
                    new RelationshipDefinition { RelationshipDefinitionID = 4, Definition = "unknown" } }
                );

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
            var privacyFlagType6 = new PrivacyFlagType()
            {
                ID = 6,
                FieldName = "who can send me messages"
            };
            var privacyFlagType7 = new PrivacyFlagType()
            {
                ID = 7,
                FieldName = "who can see my main page on SEARCH request on PEOPLE page"
            };


            db.PrivacyFlagTypes.AddRange(new List<PrivacyFlagType> { privacyFlagType1, privacyFlagType2, privacyFlagType3, privacyFlagType4, privacyFlagType5, privacyFlagType6, privacyFlagType7 });
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
                Name = "Only friends"
            };
            var visLevel3 = new VisibilityLevel()
            {
                ID = 3,
                Name = "Only me"
            };
            db.VisibilityLevels.AddRange(new List<VisibilityLevel> { visLevel1, visLevel2, visLevel3 });
            db.SaveChanges();
            #endregion

            //level = 3 : =1
            //level = 2 : <=2
            //level = 1 : <=4

            #region UserRights
            //owner of page
            var right1 = new UserRights
            {
                UserRightID = 1,
                RelationshipDefinitionID = 1,
                VisibilityLevelId = 3
            };
            //friend
            var right2 = new UserRights
            {
                UserRightID = 2,
                RelationshipDefinitionID = 1,
                VisibilityLevelId = 2
            };
            //subscriber
            var right3 = new UserRights
            {
                UserRightID = 3,
                RelationshipDefinitionID = 2,
                VisibilityLevelId = 2
            };
            //unknown
            var right4 = new UserRights
            {
                UserRightID = 4,
                RelationshipDefinitionID = 1,
                VisibilityLevelId = 1
            };
            var right5= new UserRights
            {
                UserRightID = 5,
                RelationshipDefinitionID = 1,
                VisibilityLevelId = 2
            };
            var right6 = new UserRights
            {
                UserRightID = 6,
                RelationshipDefinitionID = 1,
                VisibilityLevelId = 3
            };
            var right7 = new UserRights
            {
                UserRightID = 7,
                RelationshipDefinitionID = 1,
                VisibilityLevelId = 4
            };

            db.UserRights.AddRange(new List<UserRights> { right1, right2, right3, right4, right5, right6, right7 });
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
            db.Users.Add(new ApplicationUser { Id = "c339d83f-4224-4c68-a5ea-e833b6ac27b2", UserName = "ALEX", Password = "ALEXALEX", EmailConfirmed = true, Email = "ALEX@gmail.com", AgreementID = 1, PhoneNumber = "0992304224", PasswordHash = "AOs/HKN2pdOWq2uF5xgwnHjV/S0lWRTMJxmuHI4zSoJpqEX4xhjOdGso5W+pk1OUzw==", SecurityStamp = "92c880fb-44e6-4e2c-9049-abf9a62fd2d6" });
            db.Users.Add(new ApplicationUser { Id = "c339d83f-4224-4c68-a5ea-e833b6ac27b3", UserName = "OLEG", Password = "OLEGOLEG", EmailConfirmed = true, Email = "OLEG@gmail.com", AgreementID = 1, PhoneNumber = "0992304225", PasswordHash = "AOs/HKN2pdOWq2uF5xgwnHjV/S0lWRTMJxmuHI4zSoJpqEX4xhjOdGso5W+pk1OUzw==", SecurityStamp = "92c880fb-44e6-4e2c-9049-abf9a62fd2d6" });
            db.Users.Add(new ApplicationUser { Id = "c339d83f-4224-4c68-a5ea-e833b6ac27b4", UserName = "IGOR", Password = "IGORIGOR", EmailConfirmed = true, Email = "IGOR@gmail.com", AgreementID = 1, PhoneNumber = "0992304226", PasswordHash = "AOs/HKN2pdOWq2uF5xgwnHjV/S0lWRTMJxmuHI4zSoJpqEX4xhjOdGso5W+pk1OUzw==", SecurityStamp = "92c880fb-44e6-4e2c-9049-abf9a62fd2d6" });
            db.Users.Add(new ApplicationUser { Id = "c339d83f-4224-4c68-a5ea-e833b6ac27b5", UserName = "FIREFOX", Password = "FIREFOX", EmailConfirmed = true, Email = "FIREFOX@gmail.com", AgreementID = 1, PhoneNumber = "0992304227", PasswordHash = "AOs/HKN2pdOWq2uF5xgwnHjV/S0lWRTMJxmuHI4zSoJpqEX4xhjOdGso5W+pk1OUzw==", SecurityStamp = "92c880fb-44e6-4e2c-9049-abf9a62fd2d6" });
            db.SaveChanges();
            #endregion


            #region Profiles
            db.Profiles.Add(new Profile { UserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b2", FirstName = "Olexiy", LastName = "Kramarenko", ProfileID = 1, Country = "Ukraine", Town = "Lviv", Gender = "Man", BirthDay = 1, BirthMonth = 1, BirthYear = 1990, School = "", SchoolTown = "", SchoolCountry = "", StartSchoolYear = 1990, FinishSchoolYear = 1990, Language = "" });
            db.Profiles.Add(new Profile { UserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b3", FirstName = "Oleg", LastName = "Kramarenko", ProfileID = 2, Country = "Ukraine", Town = "Lviv", Gender = "Man", BirthDay = 1, BirthMonth = 1, BirthYear = 1980, School = "", SchoolTown = "", SchoolCountry = "", StartSchoolYear = 1980, FinishSchoolYear = 1980, Language = "" });
            db.Profiles.Add(new Profile { UserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b4", FirstName = "Igor", LastName = "Korniychuk", ProfileID = 3, Country = "Ukraine", Town = "Lviv", Gender = "Man", BirthDay = 1, BirthMonth = 1, BirthYear = 1970, School = "", SchoolTown = "", SchoolCountry = "", StartSchoolYear = 1970, FinishSchoolYear = 1970, Language = "" });
            db.Profiles.Add(new Profile { UserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b5", FirstName = "Fire", LastName = "Fox", ProfileID = 4, Country = "Ukraine", Town = "Lviv", Gender = "Man", BirthDay = 1, BirthMonth = 1, BirthYear = 1970, School = "", SchoolTown = "", SchoolCountry = "", Language = "" });
            db.SaveChanges();
            #endregion

            #region Photo
            var photo = new Photo
            {
                UserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b2",
                PhotoAlbumID = null,
                PhotoUrl = "UsersFolder/default.jpg",
                ThumbnailPhotoUrl = "UsersFolder/default.jpg"
            };
            db.Photos.Add(photo);
            var photo1 = new Photo
            {
                UserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b3",
                PhotoAlbumID = null,
                PhotoUrl = "UsersFolder/default.jpg",
                ThumbnailPhotoUrl = "UsersFolder/default.jpg"
            };
            db.Photos.Add(photo1);
            var photo2 = new Photo
            {
                UserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b4",
                PhotoAlbumID = null,
                PhotoUrl = "UsersFolder/default.jpg",
                ThumbnailPhotoUrl = "UsersFolder/default.jpg"
            };
            db.Photos.Add(photo2);
            db.SaveChanges();
            #endregion

            //#region PhotoAlbum
            var album = new PhotoAlbum
            {
                PhotoAlbumID = 1,
                UserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b2",
                Name = "My wall photos",
                IsWallAlbum = true
            };
            db.PhotoAlbums.Add(album);
            db.SaveChanges();
            //#endregion



            #region PrivacyFlag
            var privacyFlag1 = new PrivacyFlag()
            {
                ID = 1,
                PrivacyFlagTypeID = 1,
                ProfileID = 1,
                VisibilityLevelID = 1
            };
            var privacyFlag2 = new PrivacyFlag()
            {
                ID = 2,
                PrivacyFlagTypeID = 2,
                ProfileID = 1,
                VisibilityLevelID = 1
            };
            var privacyFlag3 = new PrivacyFlag()
            {
                ID = 3,
                PrivacyFlagTypeID = 3,
                ProfileID = 1,
                VisibilityLevelID = 1
            };
            var privacyFlag4 = new PrivacyFlag()
            {
                ID = 4,
                PrivacyFlagTypeID = 4,
                ProfileID = 1,
                VisibilityLevelID = 1
            };
            var privacyFlag5 = new PrivacyFlag()
            {
                ID = 5,
                PrivacyFlagTypeID = 5,
                ProfileID = 1,
                VisibilityLevelID = 1
            };
            var privacyFlag6 = new PrivacyFlag()
            {
                ID = 6,
                PrivacyFlagTypeID = 6,
                ProfileID = 1,
                VisibilityLevelID = 1
            };

            var privacyFlag7 = new PrivacyFlag()
            {
                ID = 7,
                PrivacyFlagTypeID = 7,
                ProfileID = 1,
                VisibilityLevelID = 1
            };

            db.PrivacyFlags.AddRange(new List<PrivacyFlag> { privacyFlag1, privacyFlag2, privacyFlag3, privacyFlag4, privacyFlag5, privacyFlag6, privacyFlag7 });
            db.SaveChanges();



            var privacyFlag11 = new PrivacyFlag()
            {
                ID = 11,
                PrivacyFlagTypeID = 1,
                ProfileID = 2,
                VisibilityLevelID = 1
            };
            var privacyFlag12 = new PrivacyFlag()
            {
                ID = 12,
                PrivacyFlagTypeID = 2,
                ProfileID = 2,
                VisibilityLevelID = 1
            };
            var privacyFlag13 = new PrivacyFlag()
            {
                ID = 13,
                PrivacyFlagTypeID = 3,
                ProfileID = 2,
                VisibilityLevelID = 1
            };
            var privacyFlag14 = new PrivacyFlag()
            {
                ID = 14,
                PrivacyFlagTypeID = 4,
                ProfileID = 2,
                VisibilityLevelID = 1
            };
            var privacyFlag15 = new PrivacyFlag()
            {
                ID = 15,
                PrivacyFlagTypeID = 5,
                ProfileID = 2,
                VisibilityLevelID = 1
            };
            var privacyFlag16 = new PrivacyFlag()
            {
                ID = 16,
                PrivacyFlagTypeID = 6,
                ProfileID = 2,
                VisibilityLevelID = 1
            };

            var privacyFlag17 = new PrivacyFlag()
            {
                ID = 17,
                PrivacyFlagTypeID = 7,
                ProfileID = 2,
                VisibilityLevelID = 1
            };

            db.PrivacyFlags.AddRange(new List<PrivacyFlag> { privacyFlag11, privacyFlag12, privacyFlag13, privacyFlag14, privacyFlag15, privacyFlag16, privacyFlag17 });
            db.SaveChanges();



            var privacyFlag21 = new PrivacyFlag()
            {
                ID = 21,
                PrivacyFlagTypeID = 1,
                ProfileID = 3,
                VisibilityLevelID = 1
            };
            var privacyFlag22 = new PrivacyFlag()
            {
                ID = 22,
                PrivacyFlagTypeID = 2,
                ProfileID = 3,
                VisibilityLevelID = 1
            };
            var privacyFlag23 = new PrivacyFlag()
            {
                ID = 23,
                PrivacyFlagTypeID = 3,
                ProfileID = 3,
                VisibilityLevelID = 1
            };
            var privacyFlag24 = new PrivacyFlag()
            {
                ID = 24,
                PrivacyFlagTypeID = 4,
                ProfileID = 3,
                VisibilityLevelID = 1
            };
            var privacyFlag25 = new PrivacyFlag()
            {
                ID = 25,
                PrivacyFlagTypeID = 5,
                ProfileID = 3,
                VisibilityLevelID = 1
            };
            var privacyFlag26 = new PrivacyFlag()
            {
                ID = 26,
                PrivacyFlagTypeID = 6,
                ProfileID = 3,
                VisibilityLevelID = 1
            };

            var privacyFlag27 = new PrivacyFlag()
            {
                ID = 27,
                PrivacyFlagTypeID = 7,
                ProfileID = 3,
                VisibilityLevelID = 1
            };

            db.PrivacyFlags.AddRange(new List<PrivacyFlag> {
                privacyFlag21,
                privacyFlag22,
                privacyFlag23,
                privacyFlag24,
                privacyFlag25,
                privacyFlag26,
                privacyFlag27 });
            db.SaveChanges();
            #endregion

            /*

            #region Messages
            db.Messages.Add(new Message
            {
                Body = @"<tr><td class='log_author'>1111111111111</td><td class='log_body'>222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222</td><td class='log_date'>31.08.15</td></tr>
                         <tr><td class='log_author'>1111111111111</td><td class='log_body'><p>1111111111</p><p>111111</p></td><td class='log_date'>31.08.15</td></tr>",

                SendersUserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b2",
                ReceiversUserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b3",
                RequestDate = "",
                DialogID = 1,
                ViewedByReceiver = true
            });
            db.Messages.Add(new Message
            {
                Body = @"<tr><td class='log_author'>1111111111111</td><td class='log_body'>222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222</td><td class='log_date'>31.08.15</td></tr>
                         <tr><td class='log_author'>1111111111111</td><td class='log_body'><p>1111111111</p><p>222222222</p></td><td class='log_date'>31.08.15</td></tr>",

                SendersUserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b3",
                ReceiversUserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b2",
                DialogID = 1,
                RequestDate = "",
                ViewedByReceiver = true
            });
            db.Messages.Add(new Message
            {
                Body = @"<tr><td class='log_author'>1111111111111</td><td class='log_body'>222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222</td><td class='log_date'>31.08.15</td></tr>
                         <tr><td class='log_author'>1111111111111</td><td class='log_body'><p>1111111111</p><p>33333333333</p></td><td class='log_date'>31.08.15</td></tr>",

                SendersUserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b2",
                ReceiversUserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b4",
                RequestDate = "",
                DialogID = 2,
                ViewedByReceiver = true
            });
            db.Messages.Add(new Message
            {
                Body = @"<tr><td class='log_author'>1111111111111</td><td class='log_body'>222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222</td><td class='log_date'>31.08.15</td></tr>
                         <tr><td class='log_author'>1111111111111</td><td class='log_body'><p>1111111111</p><p>4444444444</p></td><td class='log_date'>31.08.15</td></tr>",

                SendersUserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b4",
                ReceiversUserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b2",
                DialogID = 2,
                RequestDate = "",
                ViewedByReceiver = true
            });
            db.SaveChanges();
            #endregion


            #region Relationships 
            db.Relationships.Add(new Relationship { SenderAccountID = "c339d83f-4224-4c68-a5ea-e833b6ac27b2", ReceiverAccountID = "c339d83f-4224-4c68-a5ea-e833b6ac27b3", RelationshipDefinitionID = 1 });
            db.Relationships.Add(new Relationship { SenderAccountID = "c339d83f-4224-4c68-a5ea-e833b6ac27b2", ReceiverAccountID = "c339d83f-4224-4c68-a5ea-e833b6ac27b4", RelationshipDefinitionID = 1 });
            db.SaveChanges();
            #endregion



            #region Status
            var status = new Status
            {
                //ID = 1,
                CreateDate = DateTime.Now,
                Post = "AAAAAAAAAAAAAA",
                PostByUserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b2"
            };
            db.Statuses.Add(status);
            db.SaveChanges();
            #endregion

            #region Comment
            var comment1 = new Comment
            {
                CommentText = "LaLaLa",
                CreateDate = DateTime.Now,
                StatusID = 1,
                UserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b2",
            };
            var comment2 = new Comment
            {
                CommentText = "DaDaDa",
                CreateDate = DateTime.Now,
                StatusID = 1,
                UserID = "c339d83f-4224-4c68-a5ea-e833b6ac27b2",
            };
            db.Comments.Add(comment1);
            db.Comments.Add(comment2);
            db.SaveChanges();
            #endregion
            */


        }
    }
}

