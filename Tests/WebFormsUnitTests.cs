using Core.BLL.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebFormsApp;
using Core.BLL.Interfaces;
using Core.POCO;
using WebFormsApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.ModelBinding;
using WebFormsApp.Services;
using WebFormsApp.CustomControls;

namespace Tests
{
    [TestClass]
    public class WebFormsUnitTests
    {
        Mock<HttpRequestBase> httpRequestBase;
        Mock<HttpContextBase> httpContextBase;
        Mock<IUserService> userService;
        Mock<IRelationshipsService> relationshipsService;
        Mock<IMappingService> mappingService;
        Mock<IProfileService> profileService;
        Mock<IPhotoService> photoService;
        Mock<IEmailService> emailService;
        Mock<IRedirectService> redirectService;
        Mock<ISessionService> sessionService;

        [TestInitialize]
        public void TestInit()
        {
            httpRequestBase = new Mock<HttpRequestBase>();
            httpContextBase = new Mock<HttpContextBase>();
            userService = new Mock<IUserService>();
            relationshipsService = new Mock<IRelationshipsService>();
            mappingService = new Mock<IMappingService>();
            profileService = new Mock<IProfileService>();
            photoService = new Mock<IPhotoService>();
            emailService = new Mock<IEmailService>();
            redirectService = new Mock<IRedirectService>();
            sessionService = new Mock<ISessionService>();
        }
        
        [TestMethod]
        public void LoginUser_Should_Redirect_To_MainPage()
        {
            //Arrange 
            var loginPage = new LoginPage();
            userService.Setup(a => a.LoginUser(It.IsAny<LoginDTO>(), It.IsAny<Func<string>>())).Returns(new OperationResult { Succedeed = true });
         
            loginPage.UserService = userService.Object;
            loginPage.Redirect = redirectService.Object;
            loginPage.SessionService = sessionService.Object;
            
            var lvm = new LoginViewModel();
            
            //Act
            loginPage.LoginUser(lvm);

            //Assert            
            redirectService.Verify(a => a.GoToMainPage());
        }

        [TestMethod]
        public void VerificationResult_Should_Return_Error_Message()
        {
            //Arrange            
            var emailVerificationPage = new EmailVerification();
            userService.Setup(a => a.GetUserByName(It.IsAny<string>())).Returns((ApplicationUser)null);
            emailVerificationPage.UserService = userService.Object;

            var queryString = new NameValueCollection();
            queryString.Add("username", "SomeUserName");
            httpRequestBase.Setup(a => a.QueryString).Returns(queryString);

            emailVerificationPage.Request = httpRequestBase.Object;

            //Act                      
            string resultFromQueryString = emailVerificationPage.VerificationResult(null);
            string resultFromRoute = emailVerificationPage.VerificationResult("SomeUserName");

            //Assert
            Assert.AreEqual("Возникла неизвестная ошибка.", resultFromQueryString);
            Assert.AreEqual("Возникла неизвестная ошибка.", resultFromRoute);

        }
        [TestMethod]
        public void GetFriendsRefs_Should_Return_NotNull_Result()
        {
            //Arrange 
            var mainPage = new MyPage();

            var dto = new List<FriendsDTO>() { new FriendsDTO() };
            var vm = new FriendsViewModel();
            var viewModel = new List<FriendsViewModel>() { new FriendsViewModel() };
            int usersCount = 0;
            relationshipsService.Setup(a => a.CreateLinks(It.IsAny<int>(), It.IsAny<string>(), out usersCount)).Returns(dto);
            mappingService.Setup(a => a.Map<List<FriendsDTO>, List<FriendsViewModel>>(dto)).Returns(viewModel);

            mainPage.RelationshipsService = relationshipsService.Object;
            mainPage.MappingService = mappingService.Object;

            //Act
            List<FriendsViewModel> result = mainPage.GetFriendsRefs("   ");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetAlbums_Should_Return_NotNull_Result()
        {
            //Arrange 
            var mainPage = new MyPage();
            var albumsDto = new AlbumDTO[] { new AlbumDTO() };
            var albumsVM = new WebFormsApp.ViewModel.AlbumViewModel[] { new AlbumViewModel() };

            profileService.Setup(a => a.GetAlbums(It.IsAny<string>())).Returns(albumsDto);
            mappingService.Setup(a => a.Map<AlbumDTO[], AlbumViewModel[]>(albumsDto)).Returns(albumsVM);

            mainPage.ProfileService = profileService.Object;
            mainPage.MappingService = mappingService.Object;

            //Act
            AlbumViewModel[] result = mainPage.GetAlbums("  ");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void GetProfile_Should_Return_NotNull_Result()
        {
            //Arrange 
            var mainPage = new MyPage();
            var profile = new Core.POCO.Profile();
            var profileVM = new WebFormsApp.ViewModel.ProfileViewModel { BirthYear = 2000 };

            profileService.Setup(a => a.GetProfile(It.IsAny<string>())).Returns(profile);
            mappingService.Setup(a => a.Map<Core.POCO.Profile, WebFormsApp.ViewModel.ProfileViewModel>(profile)).Returns(profileVM);
            userService.Setup(a => a.GetPhoneNumber(It.IsAny<string>())).Returns("123");

            mainPage.ProfileService = profileService.Object;
            mainPage.MappingService = mappingService.Object;
            mainPage.UserService = userService.Object;

            //Act
            ProfileViewModel result = mainPage.GetProfile("  ");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2000, result.BirthYear);
        }

        [TestMethod]
        public void GetStatuses_Should_Return_NotNull_Result()
        {
            //Arrange 
            var mainPage = new Statuses();
            var statuses = new List<Status>() { new Status() };

            profileService.Setup(a => a.GetStatuses(It.IsAny<string>())).Returns(statuses);
            photoService.Setup(a => a.GetAvatar(It.IsAny<string>())).Returns(string.Empty);

            mainPage.ProfileService = profileService.Object;
            mainPage.PhotoService = photoService.Object;

            //Act
            List<Status> result = mainPage.GetStatuses("  ");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(statuses.Count > 0);
        }

        [TestMethod]
        public void RecoverPassword_Should_Return_Message_About_Successful_Operation()
        {
            //Arrange
            var recoverPswdPage = new RecoverPasswordPage();
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>())).Returns(new ApplicationUser());
            emailService.Setup(a => a.SendPasswordReminderEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));


            recoverPswdPage.UserService = userService.Object;
            recoverPswdPage.EmailService = emailService.Object;

            //Act
            string result = recoverPswdPage.RecoverPassword("  ");

            //Assert
            emailService.Verify(a => a.SendPasswordReminderEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            Assert.AreEqual("An email was sent to your account!", result);

        }

        [TestMethod]
        public void CreateUser_Should_Call_ServiceMethod()
        {
            //Arrange
            var rvm = new CreateUserViewModel();
            var user = new ApplicationUser();

            mappingService.Setup(a => a.Map<CreateUserViewModel, ApplicationUser>(rvm)).Returns(user);
            userService.Setup(a => a.CreateUser(It.IsAny<Core.POCO.Profile>(), It.IsAny<ApplicationUser>(), It.IsAny<Func<string>>())).Returns(new OperationResult { Succedeed = true });

            var form = new NameValueCollection();
            form.Add("Country", "P");
            form.Add("Town", "P");
            form.Add("BirthYear", "1950");
            form.Add("BirthMonth", "10");
            form.Add("BirthDay", "10");

            var registrationPage = new CreateUserPage();

            httpRequestBase.Setup(a => a.Form).Returns(form);
           
            registrationPage.RequestBase = httpRequestBase.Object;
            registrationPage.MappingService = mappingService.Object;
            registrationPage.UserService = userService.Object;
            registrationPage.Redirect = redirectService.Object;

            //Act
            registrationPage.CreateUser(rvm);

            //Assert
            Assert.IsTrue(registrationPage.ModelState.IsValid);
            mappingService.Verify(a => a.Map<CreateUserViewModel, ApplicationUser>(rvm));
            userService.Verify(a => a.CreateUser(It.IsAny<Core.POCO.Profile>(), It.IsAny<ApplicationUser>(), It.IsAny<Func<string>>()));
            redirectService.Verify(a => a.GoToLoginPage());
        }
        
    }

}
