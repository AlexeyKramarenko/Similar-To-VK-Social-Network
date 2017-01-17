using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcApp.Controllers;
using Core.BLL.Interfaces;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using Core.POCO;
using System.Collections.Specialized;
using Core.BLL.DTO;
using System;
using MvcApp.ViewModel;
using System.Collections.Generic;
using MvcApp.Services;

namespace Tests
{
    [TestClass]
    public class MvcUnitTests
    {
        Mock<IUserService> userService = null;
        Mock<IEmailService> emailService = null;
        Mock<IMappingService> mappingService = null;
        Mock<IProfileService> profileService = null;
        Mock<ICountriesService> countriesService = null;
        Mock<ISessionService> sessionService = null;

        Mock<HttpRequestBase> mockHttpRequest = null;
        Mock<HttpContextBase> mockHttpContext = null;
        Mock<ControllerContext> mockControllerContext = null;

        AccountController accountController = null;
        ProfileController profileController = null;

        [TestInitialize]
        public void TestInit()
        {
            userService = new Mock<IUserService>();
            emailService = new Mock<IEmailService>();
            mappingService = new Mock<IMappingService>();
            profileService = new Mock<IProfileService>();
            countriesService = new Mock<ICountriesService>();
            sessionService = new Mock<ISessionService>();
            sessionService.SetupGet(a => a.CurrentUserId).Returns("some_userId");

            mockHttpRequest = new Mock<HttpRequestBase>();
            mockHttpContext = new Mock<HttpContextBase>();
            mockControllerContext = new Mock<ControllerContext>();
        }

        [TestMethod]
        public void EmailVerification_Should_Return_Success()
        {
            //Arrange
            userService.Setup(a => a.GetUserByName(It.IsAny<string>())).Returns(new ApplicationUser());
            userService.Setup(a => a.UpdateUser(It.IsAny<ApplicationUser>()));
            accountController = new AccountController(userService.Object, emailService.Object, mappingService.Object);
            accountController.ControllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), accountController);

            //Act
            ActionResult result = accountController.EmailVerification("SomeUserName");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsTrue(((string)accountController.ViewBag.Message).StartsWith("Ваш електронный адресс подтвержден"));

        }

        [TestMethod]
        public void Login_Redirects_To_MainPage()
        {
            //Arrange
            userService.Setup(a => a.LoginUser(It.IsAny<LoginDTO>(), It.IsAny<Func<string>>())).Returns(new OperationResult { Succedeed = true });

            accountController = new AccountController(userService.Object, emailService.Object, mappingService.Object);

            //Act
            ActionResult result = accountController.Login(new LoginViewModel() { UserName = "name", Password = "pswd" });

            //Assert
            userService.Verify(a => a.LoginUser(It.IsAny<LoginDTO>(), It.IsAny<Func<string>>()), Times.Once);
            Assert.IsTrue(accountController.ModelState.IsValid);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(((RedirectToRouteResult)result).RouteValues["action"], "MainPage");
        }

        [TestMethod]
        public void Login_Should_Return_Error_If_LoginUser_Returns_False()
        {
            //Arrange
            userService.Setup(a => a.LoginUser(It.IsAny<LoginDTO>(), It.IsAny<Func<string>>())).Returns(new OperationResult { Succedeed = false, Message = "ErrorMessage" });

            accountController = new AccountController(userService.Object, emailService.Object, mappingService.Object);

            //Act
            ActionResult result = accountController.Login(new LoginViewModel() { UserName = "name", Password = "pswd" });

            //Assert
            userService.Verify(a => a.LoginUser(It.IsAny<LoginDTO>(), It.IsAny<Func<string>>()), Times.Once);
            Assert.IsFalse(accountController.ModelState.IsValid);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsTrue(((ViewResult)result).ViewData.ModelState["ErrorMessage"].Errors.Count > 0);
            Assert.AreEqual("", ((ViewResult)result).ViewName);//method is returning the default view
        }

        [TestMethod]
        public void RecoverPassword_Should_Return_Successful_Result_If_GetUserByEmail_Returns_NotNull()
        {
            //Arrange
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>())).Returns(new ApplicationUser());
            emailService.Setup(a => a.SendPasswordReminderEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            accountController = new AccountController(userService.Object, emailService.Object, mappingService.Object);

            //Act
            ViewResult result = accountController.RecoverPassword("  ") as ViewResult;

            //Assert
            emailService.Verify(a => a.SendPasswordReminderEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(result.ViewBag.Message, "An email was sent to your account!");
        }

        [TestMethod]
        public void RecoverPassword_Should_Fail_If_GetUserByEmail_Returns_Null()
        {
            //Arrange
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>())).Returns((ApplicationUser)null);
            emailService.Setup(a => a.SendPasswordReminderEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            accountController = new AccountController(userService.Object, emailService.Object, mappingService.Object);

            //Act
            ViewResult result = accountController.RecoverPassword("  ") as ViewResult;

            //Assert                      
            Assert.AreEqual(result.ViewBag.Message, "We couldn't find the account you requested.");
        }

        [TestMethod]
        public void Registration_Redirects_To_WaitingForConfirmationPage_If_ModelIsValid()
        {
            //Arrange
            userService.Setup(a => a.CreateUser(It.IsAny<Profile>(), It.IsAny<ApplicationUser>(), It.IsAny<Func<string>>())).Returns(new OperationResult() { Succedeed = true });
            mappingService.Setup(a => a.Map<RegistrationViewModel, ApplicationUser>(new RegistrationViewModel()));

            mockHttpRequest.Setup(a => a.Form).Returns(new NameValueCollection {
                { "BirthYear", "1990" },
                { "BirthMonth", "5" },
                { "BirthDay", "5" },
                { "Country", "Ukraine" },
                { "Town", "Lviv" }
            });

            mockHttpRequest.Setup(a => a.HttpMethod).Returns("POST");
            mockHttpContext.Setup(a => a.Request).Returns(mockHttpRequest.Object);

            accountController = new AccountController(userService.Object, emailService.Object, mappingService.Object);
            accountController.ControllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), accountController);

            var rvm = new RegistrationViewModel
            {
                FirstName = "Ali",
                LastName = "Ali",
                Email = "ali@gmail.com",
                UserName = "SomeName",
                Password = "ABC1234",
                PasswordConfirm = "ABC1234",
                PhoneNumber = "0992304226",
                 MaritalStatus= "single",
                Gender = "man"
            };

            //Act
            ActionResult result = accountController.Registration(rvm);

            //Assert   
            Assert.IsTrue(accountController.ViewData.ModelState.IsValid);
            userService.Verify(a => a.CreateUser(It.IsAny<Profile>(), It.IsAny<ApplicationUser>(), It.IsAny<Func<string>>()));
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            //Assert.AreEqual(((RedirectToRouteResult)result).RouteValues["action"], "WaitingForConfirmation");
            Assert.AreEqual(((RedirectToRouteResult)result).RouteValues["action"], "Login"); 
        }

        [TestMethod]
        public void Registration_Should_Fail_If_CreateUser_Returns_False()
        {
            //Arrange
            userService.Setup(a => a.CreateUser(It.IsAny<Profile>(), It.IsAny<ApplicationUser>(), It.IsAny<Func<string>>())).Returns(new OperationResult() { Succedeed = false });
            mappingService.Setup(a => a.Map<RegistrationViewModel, ApplicationUser>(new RegistrationViewModel()));

            mockHttpRequest.Setup(a => a.Form).Returns(new NameValueCollection {
                { "BirthYear", "1990" },
                { "BirthMonth", "5" },
                { "BirthDay", "5" },
                { "Country", "Ukraine" },
                { "Town", "Lviv" }
            });

            mockHttpRequest.Setup(a => a.HttpMethod).Returns("POST");
            mockHttpContext.Setup(a => a.Request).Returns(mockHttpRequest.Object);

            accountController = new AccountController(userService.Object, emailService.Object, mappingService.Object);

            accountController.ControllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), new Mock<ControllerBase>().Object);

            var rvm = new RegistrationViewModel
            {
                FirstName = "Ali",
                LastName = "Ali",
                Email = "ali@gmail.com",
                UserName = "SomeName",
                Password = "ABC1234",
                PasswordConfirm = "ABC1234",
                PhoneNumber = "0992304226",
                MaritalStatus = "Single",
                Gender = "man"
            };

            //Act
            ActionResult result = accountController.Registration(rvm);

            //Assert   
            userService.Verify(a => a.CreateUser(It.IsAny<Profile>(), It.IsAny<ApplicationUser>(), It.IsAny<Func<string>>()));
            Assert.IsFalse(accountController.ViewData.ModelState.IsValid);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void EmailVerification_Should_Return_Confirmation_If_GetUserByName_Returns_UserObject()
        {
            //Arrange
            userService.Setup(a => a.GetUserByName(It.IsAny<string>())).Returns(new ApplicationUser());
            userService.Setup(a => a.UpdateUser(It.IsAny<ApplicationUser>()));

            accountController = new AccountController(userService.Object, emailService.Object, mappingService.Object);

            //Act
            ViewResult result = accountController.EmailVerification("SomeUserName") as ViewResult;

            //Assert   
            Assert.IsTrue(result.ViewBag.Message.StartsWith("Ваш електронный адресс подтвержден."));
        }

        [TestMethod]
        public void EmailVerification_Should_Return_ErrorMessage()
        {
            //Arrange
            userService.Setup(a => a.GetUserByName(It.IsAny<string>())).Returns((ApplicationUser)null);
            accountController = new AccountController(userService.Object, emailService.Object, mappingService.Object);

            //Act
            ViewResult result = accountController.EmailVerification("SomeUserName") as ViewResult;

            //Assert                  
            Assert.AreEqual(result.ViewBag.Message, "Пользователя с таким именем нет.");
        }
      
        [TestMethod]
        public void MainInfo_Should_Return_CorrectPartialView()
        {
            //Arrange
            profileService.Setup(a => a.GetMainInfo(It.IsAny<string>())).Returns(new Profile());
            profileService.Setup(a => a.GetBirthDays()).Returns(new string[] { });
            profileService.Setup(a => a.GetBirthMonths()).Returns(new string[] { });
            profileService.Setup(a => a.GetBirthYears(It.IsAny<string>())).Returns(new string[] { });
            mappingService.Setup(a => a.Map<Profile, MainViewModel>(It.IsAny<Profile>())).Returns(new MainViewModel());
            profileController = new ProfileController(userService.Object, mappingService.Object, profileService.Object, countriesService.Object, sessionService.Object);

            //Act
            PartialViewResult result = profileController.MainInfo();

            //Assert                  
            Assert.AreEqual("MainInfo", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(MainViewModel));
        }

        [TestMethod]
        public void MainInfo_Should_Return_EmptyResult()
        {
            //Arrange            
            mappingService.Setup(a => a.Map<MainViewModel, Profile>(It.IsAny<MainViewModel>())).Returns(new Profile());
            profileService.Setup(a => a.SaveMainInfo(It.IsAny<Profile>(), It.IsAny<string>()));

            profileController = new ProfileController(userService.Object, mappingService.Object, profileService.Object, countriesService.Object, sessionService.Object);

            //Act
            EmptyResult result = profileController.MainInfo(new MainViewModel()) as EmptyResult;

            //Assert                  
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ContactInfo_Should_Return_NotNull_ViewResult()
        {
            //Arrange   
            profileService.Setup(a => a.GetContacts(It.IsAny<string>())).Returns(new Profile());
            mappingService.Setup(a => a.Map<Profile, ContactsViewModel>(It.IsAny<Profile>())).Returns(new ContactsViewModel { Country = "Ukraine" });
            userService.Setup(a => a.GetPhoneNumber(It.IsAny<string>())).Returns(It.IsAny<string>());
            profileController = new ProfileController(userService.Object, mappingService.Object, profileService.Object, countriesService.Object, sessionService.Object);


            //Act
            PartialViewResult result = profileController.ContactInfo();

            //Assert                  
            Assert.IsNotNull(result);
            Assert.AreEqual("ContactInfo", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(ContactsViewModel));
            Assert.AreEqual(((ContactsViewModel)result.Model).Country, "Ukraine");
        }

        [TestMethod]
        public void ContactInfo_Should_Return_EmptyResult()
        {
            profileService.Setup(a => a.SaveContacts(It.IsAny<Profile>()));
            userService.Setup(a => a.UpdatePhoneNumber(It.IsAny<string>(), It.IsAny<string>()));
          
            //Arrange   
            profileController = new ProfileController(userService.Object, mappingService.Object, profileService.Object, countriesService.Object, sessionService.Object);            
            var model = new ContactsViewModel();
            //Act
            ActionResult result = profileController.ContactInfo(model);

            //Assert                  
            Assert.IsTrue(profileController.ViewData.ModelState.IsValid);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }

        [TestMethod]
        public void InterestsInfo_Should_Return_PartialView()
        {
            //Arrange   
            profileService.Setup(a => a.GetInterests(It.IsAny<string>())).Returns(new Profile());
            mappingService.Setup(a => a.Map<Profile, InterestsViewModel>(It.IsAny<Profile>())).Returns(new InterestsViewModel());
            profileController = new ProfileController(userService.Object, mappingService.Object, profileService.Object, countriesService.Object, sessionService.Object);

            //Act
            ActionResult result = profileController.InterestsInfo();

            //Assert                  
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            Assert.AreEqual("InterestsInfo", ((PartialViewResult)result).ViewName);
            Assert.IsNotNull(profileController.ViewData.Model);
            Assert.IsInstanceOfType(profileController.ViewData.Model, typeof(InterestsViewModel));
        }

        [TestMethod]
        public void InterestsInfo_Should_Return_EmptyResult()
        {
            //Arrange   
            profileService.Setup(a => a.GetInterests(It.IsAny<string>())).Returns(new Profile());
            mappingService.Setup(a => a.Map<InterestsViewModel, Profile>(It.IsAny<InterestsViewModel>())).Returns(new Profile());
            profileController = new ProfileController(userService.Object, mappingService.Object, profileService.Object, countriesService.Object, sessionService.Object);

            //Act
            ActionResult result = profileController.InterestsInfo(new InterestsViewModel());

            //Assert                  
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EmptyResult));
        }

        [TestMethod]
        public void EducationInfo_Should_Return_EmptyResult()
        {
            //Arrange  
            mappingService.Setup(a => a.Map<EducationViewModel, Profile>(It.IsAny<EducationViewModel>())).Returns(new Profile());
            profileService.Setup(a => a.SaveEducation(It.IsAny<Profile>(), It.IsAny<string>()));
            profileController = new ProfileController(userService.Object, mappingService.Object, profileService.Object, countriesService.Object, sessionService.Object);

            //Act
            ActionResult result = profileController.EducationInfo(new EducationViewModel());

            //Assert                  
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EmptyResult));
        }

        [TestMethod]
        public void GetFinishYears_Should_Return_StringArray_Within_Json_Object()
        {
            //Arrange   
            profileService.Setup(a => a.GetFinishYears(It.IsAny<int>(), It.IsAny<string>())).Returns(new string[] { });
            profileController = new ProfileController(userService.Object, mappingService.Object, profileService.Object, countriesService.Object, sessionService.Object);

            //Act
            JsonResult result = profileController.GetFinishYears(5);

            //Assert                  
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(string[]));
        }
    }
}



