using Core.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Core.POCO;
using Core.BLL.Interfaces;
using Core.BLL.DTO;
using System.Configuration;
using System;

namespace Core.BLL
{
    public class ProfileService : LogicLayer, IProfileService
    {
        IUnitOfWork Database;
        ICountriesRepository countriesRepository;
        IMappingService mappingService;
        public ProfileService(IUnitOfWork db, ICountriesRepository countriesRepository, IMappingService mappingService)
        {
            Database = db;
            this.countriesRepository = countriesRepository;
            this.mappingService = mappingService;
        }
        public void UpdateBirthDay(Profile profile)
        {
            Database.Profiles.UpdateBirthDay(profile);
            Database.Save();

            string key = "BirthDay_of_userID_" + profile.UserID;
            PurgeCacheItems(key);
        }

        public Education GetEducation(string userId)
        {
            return Database.Profiles.GetEducation(userId);
        }

        public List<Comment> GetCommentsByStatusID(int Id)
        {
            var comments = Database.Profiles.GetCommentsByStatusID(Id);
            return comments;
        }
        public List<Status> GetStatuses(string UserID)
        {
            var statuses = Database.Profiles.GetStatuses(UserID);
            return statuses;
        }

        public void UpdateBirthMonth(Profile profile)
        {
            Database.Profiles.UpdateBirthMonth(profile);
            Database.Save();

            string key = "BirthMonth_of_userID_" + profile.UserID;
            PurgeCacheItems(key);
        }

        public void UpdateBirthYear(Profile profile)
        {
            Database.Profiles.UpdateBirthYear(profile);
            Database.Save();

            string key = "BirthYear_of_userID_" + profile.UserID;
            PurgeCacheItems(key);
        }
        public Profile GetMainInfo(string userId)
        {
            string key = "MainInfo_of_userId_" + userId;

            Profile mainInfo = null;

            if (Cache[key] != null)
                mainInfo = (Profile)Cache[key];
            else
            {
                mainInfo = Database.Profiles.GetMainInfo(userId);
                Cache[key] = mainInfo;
            }
            return mainInfo;
        }
        public void SaveMainInfo(Profile profile, string userId)
        {
            Database.Profiles.SaveMainInfo(profile, userId);
            Database.Save();

            string key = "MainInfo_of_userId_" + userId;
            PurgeCacheItems(key);
        }
        public Profile GetInterests(string userId)
        {
            string key = "Interests_of_userId_" + userId;

            Profile interests = null;

            if (Cache[key] != null)
                interests = (Profile)Cache[key];
            else
            {
                interests = Database.Profiles.GetInterests(userId);
                Cache[key] = interests;
            }

            return interests;
        }
        public void SaveInterests(Profile profile, string userId)
        {
            Database.Profiles.SaveInterests(profile, userId);
            Database.Save();

            string key = "Interests_of_userId_" + userId;
            PurgeCacheItems(key);
        }
        public Profile GetContacts(string userId)
        {
            string key = "Contacts_of_userId_" + userId;

            Profile contacts = null;

            if (Cache[key] != null)
                contacts = (Profile)Cache[key];
            else
            {
                contacts = Database.Profiles.GetContacts(userId);
                Cache[key] = contacts;
            }

            return contacts;
        }
        public void SaveContacts(Profile profile)
        {
            Database.Profiles.SaveContacts(profile);
            Database.Save();

            string key = "Contacts_of_userId_" + profile.UserID;
            PurgeCacheItems(key);
        }
        public List<string> GetEducationInfo(string userId)
        {
            string key = "Education_of_userId_" + userId;

            List<string> educationInfo = null;

            if (Cache[key] != null)
                educationInfo = (List<string>)Cache[key];
            else
            {
                educationInfo = Database.Profiles.GetEducationInfo(userId);
                Cache[key] = educationInfo;
            }
            return educationInfo;
        }

        public void SaveEducation(Profile profile, string userId)
        {
            Database.Profiles.SaveEducation(profile, userId);
            Database.Save();

            string key = "Education_of_userId_" + userId;
            PurgeCacheItems(key);
        }
        public string GetEmail(string userId)
        {
            string key = "Email_of_userId_" + userId;

            string email = Database.Profiles.GetEmail(userId);

            if (Cache[key] != null)
                email = (string)Cache[key];
            else
            {
                email = Database.Profiles.GetEmail(userId);
                Cache[key] = email;
            }
            return email;
        }
        public void UpdateEmail(string currentUserId, string newEmail)
        {
            Database.Profiles.UpdateEmail(currentUserId, newEmail);
            Database.Save();

            string key = "Email_of_userId_" + currentUserId;
            PurgeCacheItems(key);
        }


        public EducationDTO GetEducationInfoOfUser(string CurrentUserId)
        {
            Profile profile = GetProfileByUserId(CurrentUserId);
            EducationDTO educationDto = mappingService.Map<Profile, EducationDTO>(profile);

            var countries = countriesRepository.GetAllCountries().Select(a => a.CountryName).ToArray();
            var startYears = GetSchoolStartYears(CurrentUserId);

            if (string.IsNullOrEmpty(educationDto.SchoolCountry))
                educationDto.SchoolCountry = countries.First();

            var towns = countriesRepository.GetTownsByCountryName(educationDto.SchoolCountry);
            var finishYears = GetSchoolFinishYears(CurrentUserId);

            //skip all finish years behind current start year
            int index = Array.IndexOf(finishYears, profile.StartSchoolYear.ToString());
            var restElements = finishYears.Count() - index;
            finishYears = finishYears.Skip(index).Take(restElements).ToArray();


            educationDto.CountriesList = countries;
            educationDto.FinishYears = finishYears;
            educationDto.StartYears = startYears;
            educationDto.Towns = towns;

            return educationDto;
        }
        public string[] GetFinishYears(int selectedStartYear, string CurrentUserId)
        {
            string[] finishYears = GetSchoolFinishYears(CurrentUserId);

            int index = Array.IndexOf(finishYears, selectedStartYear.ToString());

            var restElements = finishYears.Count() - index;

            finishYears = finishYears.Skip(index).Take(restElements).ToArray();
            return finishYears;
        }

        public List<string> GetProfileTowns(string countryName)
        {
            var towns = Database.Profiles.GetProfileTowns(countryName);
            return towns;
        }
        public string[] GetCountries()
        {
            var countries = Database.Profiles.GetCountries();
            return countries;
        }
        public string[] GetAgeYears()
        {
            var years = Database.Profiles.GetAgeYears();
            return years;
        }
        public Profile GetProfile(string UserID)
        {
            Profile profile = Database.Profiles.GetProfile(UserID);
            return profile;
        }
        public string GetCountryByUserID(string UserID)
        {
            var str = Database.Profiles.GetCountryByUserID(UserID);
            return str;
        }
        public Profile GetCurrentProfile(string userId)
        {
            var p = Database.Profiles.GetCurrentProfile(userId);
            return p;
        }
        public string[] GetBirthDays()
        {
            var days = Database.Profiles.GetBirthDays();
            return days;
        }
        public string[] GetSchoolStartYears(string userId)
        {
            var years = Database.Profiles.GetSchoolStartYears(userId);
            return years;
        }
        public string[] GetBirthMonths()
        {
            var months = Database.Profiles.GetBirthMonths();
            return months;
        }
        public string[] GetSchoolFinishYears(string userId)
        {
            string[] years = Database.Profiles.GetSchoolFinishYears(userId);
            return years;
        }
        public string[] GetBirthYears(string userId)
        {
            var years = Database.Profiles.GetBirthYears(userId);
            return years;
        }
        public int AttachProfileToUser(string userId, Profile profile)
        {

            var profileId = Database.Profiles.AttachProfileToUser(userId, profile);
            return profileId;
        }
        public void AttachProfileToUser(Profile profile, string userId)
        {
            Database.Profiles.AttachProfileToUser(profile, userId);
            Database.Save();
        }
        public Profile GetProfileByUserId(string userId)
        {
            var profile = Database.Profiles.GetProfileByUserId(userId);
            return profile;
        }
        public List<Profile> GetUsers(int from, int to, int country, int? town, string gender, IEnumerable<Profile> Profiles)
        {
            var profiles = Database.Profiles.GetProfiles(from, to, country, town, gender, Profiles);
            return profiles;
        }
        public IQueryable<Profile> GetAllProfiles()
        {
            var profiles = Database.Profiles.GetAllProfiles();
            return profiles;
        }

        public int[] GetAlbumIds(string userId)
        {
            return Database.Profiles.GetAlbumIds(userId);
        }

        public Photo GetMainPhoto(int albumId)
        {
            return Database.Profiles.GetMainPhoto(albumId);
        }

        public PhotoAlbum GetAlbumById(int albumId)
        {
            return Database.Profiles.GetAlbumById(albumId);
        }

        public List<Photo> GetFirstPhotosToDisplay(int amount)
        {
            return Database.Profiles.GetFirstPhotosToDisplay(amount);
        }

        public List<Profile> GetFriendsByUserID(int from, int to, int country, int? town, string UserID, string gender, List<string> userIds)
        {
            var USERS = new List<Profile>();

            var profilesOfFriends = new List<Profile>();



            foreach (string id in userIds)
            {
                Profile profile = Database.Profiles.GetProfileByUserId(id);

                profilesOfFriends.Add(profile);
            }

            USERS = Database.Profiles.GetProfiles(from, to, country, town, gender, profilesOfFriends);

            return USERS;
        }

        public AlbumDTO[] GetAlbums(string UserID)
        {
            AlbumDTO[] albums = null;

            List<Photo> minPhotosToDisplayAlbums = GetFirstPhotosToDisplay(2);

            string defaultUrl = ConfigurationManager.AppSettings["defaultUrl"];

            if (minPhotosToDisplayAlbums.Count > 0)
            {
                int[] albumIds = GetAlbumIds(UserID);

                if (albumIds != null)
                {
                    albums = new AlbumDTO[albumIds.Length];

                    for (int i = 0; i < albumIds.Length; i++)
                    {
                        string AlbumTitle = null;
                        string ThumbnailPhotoUrl = defaultUrl;

                        int albumId = albumIds[i];


                        var photo = GetMainPhoto(albumId);

                        if (photo != null)
                            ThumbnailPhotoUrl = photo.ThumbnailPhotoUrl;

                        var album = GetAlbumById(albumId);

                        if (album != null)
                            AlbumTitle = album.Name;


                        albums[i] = new AlbumDTO
                        {
                            AlbumID = albumId,
                            AlbumTitle = AlbumTitle,
                            ThumbnailPhotoUrl = ThumbnailPhotoUrl
                        };
                    }
                }
            }
            return albums;
        }

        public string GetProfileSchoolTown(string currentUserId)
        {
            return Database.Profiles.GetProfileSchoolTown(currentUserId);
        }
    }
}
