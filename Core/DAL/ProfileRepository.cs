
using Core.DAL.Interfaces;
using Core.POCO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;

namespace Core.DAL
{
    public class ProfileRepository : IProfileRepository
    {
        private string minBirthYear;
        private string minStartScoolYear;
        private string minSchoolFinishYear;
        private string schoolCountry;

        private string schoolTown;
        private string minBirthDay;
        private string minBirthMonth;
        private string defaultUrl;
         

        private int minAge;
        private int maxAge;
        private int daysInMonth;
        private int startSchoolYears;
        private int monthsInYear;
        private int finishSchoolYear;
        private int minimalBirthYear;

        DBContext db;
        IUserRepository userRepository;
        public ProfileRepository(DBContext db, IUserRepository _userRepository)
        {
            this.db = db;

            minBirthYear = ConfigurationManager.AppSettings["minBirthYear"];
            minStartScoolYear = ConfigurationManager.AppSettings["minStartScoolYear"];
            minSchoolFinishYear = ConfigurationManager.AppSettings["minSchoolFinishYear"];
            schoolCountry = ConfigurationManager.AppSettings["schoolCountry"];
            schoolTown = ConfigurationManager.AppSettings["schoolTown"];
            minBirthDay = ConfigurationManager.AppSettings["minBirthDay"];
            minBirthMonth = ConfigurationManager.AppSettings["minBirthMonth"];

            defaultUrl = ConfigurationManager.AppSettings["defaultUrl"];

            minAge = Convert.ToInt32(ConfigurationManager.AppSettings["minAge"]);
            maxAge = Convert.ToInt32(ConfigurationManager.AppSettings["maxAge"]);
            daysInMonth = Convert.ToInt32(ConfigurationManager.AppSettings["daysInMonth"]);
            startSchoolYears = Convert.ToInt32(ConfigurationManager.AppSettings["startSchoolYears"]);
            monthsInYear = Convert.ToInt32(ConfigurationManager.AppSettings["monthsInYear"]);
            finishSchoolYear = Convert.ToInt32(ConfigurationManager.AppSettings["finishSchoolYear"]);
            minimalBirthYear = Convert.ToInt32(ConfigurationManager.AppSettings["minimalBirthYear"]);

            userRepository = _userRepository;
             
        }
        public string GetEmail(string userid)
        {
            return db.Users.First(u => u.Id == userid).Email;
        }


        public List<Comment> GetCommentsByStatusID(int Id)
        {
            var comments = db.Comments.Where(d => d.StatusID == Id).ToList();

            for (int i = 0; i < comments.Count; i++)
            {
                var UserID = comments[i].UserID;
                var user = db.Users.FirstOrDefault(a => a.Id == UserID);

                if (user != null)
                    comments[i].UserName = user.UserName;
            }

            return comments;
        }

        public List<Status> GetStatuses(string UserID)
        {
            var statuses = db.Statuses
                             .Include(s => s.Comments)
                             .Where(s => s.WallOfUserID == UserID)
                             .OrderByDescending(d => d.CreateDate);

            var _statuses = statuses.ToList();

            for (int i = 0; i < _statuses.Count; i++)
            {
                _statuses[i].CommentsCount = _statuses[i].Comments.ToList().Count;
                string id = _statuses[i].PostByUserID;
                var user = db.Users.FirstOrDefault(a => a.Id == id);
                if (user != null)
                {
                    _statuses[i].UserName = user.UserName;
                }
            }
            return _statuses;
        } 
        public Profile GetMainInfo(string userId)
        {
            var _profile = db.Profiles
                .Select(o => new
                {
                    o.ProfileID,
                    o.FirstName,
                    o.LastName,
                    o.Gender,
                    o.MaritalStatus,
                    o.BirthDay,
                    o.BirthMonth,
                    o.BirthYear,
                    o.HomeTown,
                    o.Language,
                    o.UserID
                }).FirstOrDefault(o => o.UserID == userId);


            var mainInfo = new Profile
            {
                ProfileID = _profile.ProfileID,
                FirstName = _profile.FirstName,
                LastName = _profile.LastName,
                Gender = _profile.Gender,
                MaritalStatus = _profile.MaritalStatus,
                BirthDay = _profile.BirthDay,
                BirthMonth = _profile.BirthMonth,
                BirthYear = _profile.BirthYear,
                HomeTown = _profile.HomeTown,
                Language = _profile.Language
            };

            return mainInfo;

        }
        public Profile GetInterests(string userId)
        {
            var _profile = db.Profiles
                .Select(o => new
                {
                    o.UserID,
                    o.Activity,
                    o.ProfileID,
                    o.Interests,
                    o.Music,
                    o.Films,
                    o.Books,
                    o.Games,
                    o.Quotes,
                    o.AboutMe
                }).FirstOrDefault(o => o.UserID == userId);


            var profile = new Profile
            {
                ProfileID = _profile.ProfileID,
                Activity = _profile.Activity,
                Interests = _profile.Interests,
                Music = _profile.Music,
                Films = _profile.Films,
                Books = _profile.Books,
                Games = _profile.Games,
                Quotes = _profile.Quotes,
                AboutMe = _profile.AboutMe
            };

            return profile;

        }
        public Profile GetContacts(string userId)
        {
            var _profile = db.Profiles
                   .Select(o => new
                   {
                       o.ProfileID,
                       o.Country,
                       o.Town,
                       o.Skype,
                       o.WebSite,
                       o.UserID
                   }).FirstOrDefault(o => o.UserID == userId);

            Profile contacts = null;

            if (_profile != null)
            {
                contacts = new Profile
                {
                    ProfileID = _profile.ProfileID,
                    Country = _profile.Country,
                    Town = _profile.Town,
                    Skype = _profile.Skype,
                    WebSite = _profile.WebSite
                };
            }
            return contacts;
        }
        public List<string> GetEducationInfo(string userId)
        {
            var profile = db.Profiles.FirstOrDefault(a => a.UserID == userId);

            var list = new List<string>();

            if (profile != null)
            {
                list.AddRange(new List<string>{
                                            (profile.SchoolCountry!=null)?profile.SchoolCountry:"",
                                            (profile.SchoolTown!=null)?profile.SchoolTown:"",
                                            (profile.School!=null)?profile.School:"",
                                            (profile.StartSchoolYear !=0)?profile.StartSchoolYear.ToString() : "",

                                            (profile.FinishSchoolYear!=0)?profile.FinishSchoolYear.ToString() : ""
                });

            }
            return list;

        }

        public Education GetEducation(string userId)
        {
            var profile = db.Profiles.FirstOrDefault(a => a.UserID == userId);

            Education education = null;

            if (profile != null)
            {
                education = new Education
                {
                    FinishScoolYear = profile.FinishSchoolYear,
                    StartScoolYear = profile.StartSchoolYear,
                    School = profile.School,
                    SchoolCountry = profile.SchoolCountry,
                    SchoolTown = profile.SchoolTown
                };
            }
            return education;
        }

        public List<string> GetProfileTowns(string countryName)
        {
            var id = db.Countries.First(a => a.CountryName == countryName).CountryID;

            var towns = db.Towns.Join(db.Countries.Where(a => a.CountryID == id),
                t => t.CountryID,
                c => c.CountryID,

                (t, c) => t.TownName).ToList();

            return towns;
        }
        public string[] GetCountries()
        {
            return db.Countries.Select(a => a.CountryName).ToArray();
        }




        public string[] GetAgeYears()
        {
            string[] ages = new string[maxAge - minAge];

            int start = minAge;

            for (int i = 0; i < maxAge - minAge; i++)
            {
                ages[i] = (start + i).ToString();
            }

            return ages;
        }
        public Profile GetProfile(string UserID)
        {
            Profile profile = db.Profiles.FirstOrDefault(p => p.UserID == UserID);

            return profile;
        }
        public string GetCountryByUserID(string UserID)
        {
            var profile = db.Profiles.FirstOrDefault(a => a.UserID == UserID);

            if (profile != null)
                return profile.Country;
            return "";
        }
        public Profile GetCurrentProfile(string userId)
        {
            return db.Profiles.FirstOrDefault(p => p.UserID == userId);
        }
        public string[] GetBirthDays()
        {
            var array = new string[daysInMonth];

            for (int i = 0; i < daysInMonth; i++)
            {
                array[i] = (i + 1).ToString();
            }
            return array;
        }
        private string[] GetStringArray(string defaultValue, int number, int startValue = 0)
        {
            int value = Convert.ToInt32(defaultValue);

            var list = new string[number];

            for (int i = 0; i < number; i++)
            {
                list[i] = (i + startValue).ToString();

                if ((i + startValue) == value)
                {
                    //выбранный елемент
                    list[0] = defaultValue.ToString();
                }
            }

            return list;
        }
        public string[] GetSchoolStartYears(string userId)
        {
            Profile profile = null;

            string startScoolYear = minStartScoolYear;

            using (DBContext dc = new DBContext())
            {
                profile = dc.Profiles.FirstOrDefault(p => p.UserID == userId);
            }
            if (profile.StartSchoolYear != 0)
            {
                startScoolYear = profile.StartSchoolYear.ToString();
            }
            int start = startSchoolYears;
            int yearsCount = DateTime.Now.Year - start;

            return GetStringArray(defaultValue: startScoolYear, number: yearsCount, startValue: start);
        }
        public string[] GetBirthMonths()
        {
            var array = new string[monthsInYear];

            for (int i = 0; i < monthsInYear; i++)
            {
                array[i] = (i + 1).ToString();
            }
            return array;
        }
        public string[] GetSchoolFinishYears(string userId)
        {
            string finishScoolYear = minSchoolFinishYear;

            Profile profile = db.Profiles.FirstOrDefault(p => p.UserID == userId);

            if (profile.FinishSchoolYear != 0)
                finishScoolYear = profile.FinishSchoolYear.ToString();


            int start = finishSchoolYear;
            int yearsCount = DateTime.Now.Year - start;

            return GetStringArray(defaultValue: finishScoolYear, number: yearsCount, startValue: start);
        }
        public string[] GetBirthYears(string userId)
        {
            string birthYear = minBirthYear;

            Profile profile = GetCurrentProfile(userId);

            if (profile != null && profile.BirthYear.ToString() != "")
                birthYear = profile.BirthYear.ToString();

            int start = minimalBirthYear;

            int yearsCount = DateTime.Now.Year - start;

            return GetStringArray(
                defaultValue: birthYear,
                number: yearsCount,
                startValue: start);
        }

        public int AttachProfileToUser(string userId, Profile profile)
        {

            profile.UserID = userId;
            db.Entry(profile).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return profile.ProfileID;
        }
        public void AttachProfileToUser(Profile profile, string userId)
        {
            profile.UserID = userId;
        }
        public void SaveEducation(Profile profile, string userId)
        {
            if (profile.ProfileID > 0)
            {
                var _profile = GetProfile(userId);
                _profile.SchoolCountry = profile.SchoolCountry;
                _profile.SchoolTown = profile.SchoolTown;
                _profile.School = profile.School;
                _profile.StartSchoolYear = profile.StartSchoolYear;
                _profile.FinishSchoolYear = profile.FinishSchoolYear;
            }
            else
            {
                AttachProfileToUser(profile, userId);
                db.Entry(profile).State = System.Data.Entity.EntityState.Added;
            }
        }
        public void SaveMainInfo(Profile profile, string userId)
        {
            if (profile.ProfileID > 0)
            {
                var _profile = db.Profiles.Find(profile.ProfileID);

                _profile.FirstName = profile.FirstName;
                _profile.LastName = profile.LastName;
                _profile.Gender = profile.Gender;
                _profile.MaritalStatus = profile.MaritalStatus;
                _profile.BirthDay = profile.BirthDay;
                _profile.BirthMonth = profile.BirthMonth;
                _profile.BirthYear = profile.BirthYear;
                _profile.HomeTown = profile.HomeTown;
                _profile.Language = profile.Language;
            }
            else
            {
                AttachProfileToUser(profile, userId);
                db.Entry(profile).State = System.Data.Entity.EntityState.Added;
            }
        }
        public void SaveContacts(Profile profile)
        {
            if (profile.ProfileID > 0)
            {
                var _profile = db.Profiles.Find(profile.ProfileID);

                _profile.Skype = profile.Skype;
                _profile.WebSite = profile.WebSite;
            }
            else
            {
                AttachProfileToUser(profile, profile.UserID);
                db.Entry(profile).State = System.Data.Entity.EntityState.Added;
            }
        }
        public void SaveInterests(Profile profile, string userId)
        {
            if (profile.ProfileID > 0)
            {
                var _profile = db.Profiles.Find(profile.ProfileID);

                _profile.Activity = profile.Activity;
                _profile.Interests = profile.Interests;
                _profile.Music = profile.Music;
                _profile.Films = profile.Films;
                _profile.Books = profile.Books;
                _profile.Games = profile.Games;
                _profile.Quotes = profile.Quotes;
                _profile.AboutMe = profile.AboutMe;
            }
            else
            {
                AttachProfileToUser(profile, userId);
                db.Entry(profile).State = System.Data.Entity.EntityState.Added;
            }
        }
        public void UpdateEmail(string currentUserId, string newEmail)
        {
            var user = userRepository.GetAccount(currentUserId);
            user.Email = newEmail;

        }

        public void UpdateBirthDay(Profile profile)
        {
            if (profile.ProfileID > 0)
            {
                var _profile = db.Profiles.Find(profile.ProfileID);

                _profile.BirthDay = profile.BirthDay;

            }
        }
        public void UpdateBirthMonth(Profile profile)
        {
            if (profile.ProfileID > 0)
            {
                var _profile = db.Profiles.Find(profile.ProfileID);

                _profile.BirthMonth = profile.BirthMonth;

            }
        }
        public void UpdateBirthYear(Profile profile)
        {
            if (profile.ProfileID > 0)
            {
                var _profile = db.Profiles.Find(profile.ProfileID);

                _profile.BirthYear = profile.BirthYear;

            }
        }

        public Profile GetProfileByUserId(string userId)
        {
            var profile = (from p in db.Profiles
                           join
                           a in db.Users
                           on p.UserID equals a.Id
                           select p)
                           .FirstOrDefault(g => g.UserID == userId);
            if (profile != null)
                return profile;

            return null;
        }
        public IQueryable<Profile> GetAllProfiles()
        {
            var profiles = from p in db.Profiles
                           join u in db.Users
                           on p.UserID equals u.Id
                           select p;

            return profiles;
        }

        public int[] GetAlbumIds(string userId)
        {
            int[] ids = db.PhotoAlbums.Where(a => a.UserID == userId).Select(a => a.PhotoAlbumID).Distinct().ToArray();
            return ids;
        }

        public Photo GetMainPhoto(int albumId)
        {
            var photo = db.Photos.Where(a => a.PhotoAlbumID == albumId).OrderByDescending(a => a.PhotoID).FirstOrDefault();
            return photo;
        }
        public PhotoAlbum GetAlbumById(int albumId)
        {
            var album = db.PhotoAlbums.FirstOrDefault(a => a.PhotoAlbumID == albumId);
            return album;
        }

        public List<Photo> GetFirstPhotosToDisplay(int amount)
        {
            var photos = db.Photos.Take(amount).ToList();
            return photos;
        }


        public List<Profile> GetProfiles(int from, int to, int country, int? town, string gender, IEnumerable<Profile> Profiles)
        {
            var expression = new StringBuilder();
            var db = new DBContext();
            var list = new List<string>();


            list.Add("BirthYear  >=" + (DateTime.Now.Year - to) + " and BirthYear <=" + (DateTime.Now.Year - from));

            if (country != 0)
            {
                var countries = db.Countries.Include("Towns");

                var iii = countries.ToList();

                Country _country = countries.FirstOrDefault(a => a.CountryID == country);

                if (_country != null)
                {
                    list.Add("Country == \"" + _country.CountryName + "\" ");

                    if (town != 0 && town != null)
                    {
                        Town TOWN = _country.Towns.FirstOrDefault(y => y.TownID == town);

                        var townName = TOWN.TownName;

                        list.Add("Town == \"" + townName + "\" ");
                    }
                }
            }

            if (gender != "Any" && gender != null)
                list.Add("Gender == \"" + gender + "\" ");

            for (int i = 0; i < list.Count; i++)
            {
                expression.Append(list[i]);
                if (i != list.Count - 1)
                    expression.Append(" and ");
            }

            var expr = expression.ToString();


            List<Profile> filteredProfiles = Profiles.Where(expr).ToList();


            return filteredProfiles;
            
        }
        public string GetProfileSchoolTown(string currentUserId)
        {
            var profile = db.Profiles.FirstOrDefault(a => a.UserID == currentUserId);

            if (profile != null)
            {
                return profile.SchoolTown;
            }

            return null;
        }
    }
}
