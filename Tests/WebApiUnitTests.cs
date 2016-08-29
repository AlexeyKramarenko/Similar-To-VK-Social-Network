
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebFormsApp.WebApi;
using Moq;
using Core.BLL.Interfaces;
using Core.POCO;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using System.Web.Http.Results;

namespace Tests
{
    [TestClass]
    public class WebApiUnitTests
    {
        MainController mainController;
        PhotosController photosController;

        Mock<IWallStatusService> wallStatusService;
        Mock<IRelationshipsService> relationshipService;
        Mock<IPhotoService> photoService;


        [TestInitialize]
        public void TestInit()
        {
            wallStatusService = new Mock<IWallStatusService>();
            relationshipService = new Mock<IRelationshipsService>();
            photoService = new Mock<IPhotoService>();

            mainController = new MainController(wallStatusService.Object, relationshipService.Object);
            mainController.Request = new HttpRequestMessage();
            mainController.Configuration = new HttpConfiguration();

            photosController = new PhotosController(photoService.Object);
        }


        [TestMethod]
        public void InsertComment_Should_Return_201()
        {
            wallStatusService.Setup(a => a.InsertComment(It.IsAny<Comment>())).Returns(typeof(object));

            HttpResponseMessage result = mainController.InsertComment(new Comment());

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void DeleteComment_Should_Return_404()
        {
            wallStatusService.Setup(a => a.GetComment(It.IsAny<int>(), It.IsAny<string>())).Returns((Comment)null);
            HttpResponseMessage result = mainController.DeleteComment(It.IsAny<int>());

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void InsertStatus_Should_Return_201()
        {
            wallStatusService.Setup(a => a.InsertStatus(It.IsAny<Status>())).Returns(new InsertStatusResult());

            HttpResponseMessage result = mainController.InsertStatus(new Status());

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void DeleteStatus_Should_Return_204()
        {
            wallStatusService.Setup(a => a.GetStatusById(It.IsAny<int>())).Returns(new Status());

            HttpResponseMessage result = mainController.DeleteStatus(1);

            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void AddToFriendsMessage_Should_Return_201()
        {
            relationshipService.Setup(a => a.CheckIfTheSameInvitationAlreadyExistsInDB(It.IsAny<Message>())).Returns(true);
            relationshipService.Setup(a => a.AddToFriendsMessage(It.IsAny<Message>()));


            HttpResponseMessage result = mainController.AddToFriendsMessage(new Message());

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.AreEqual("text/plain", result.Content.Headers.ContentType.MediaType);
            Assert.AreEqual("Created", result.ReasonPhrase);
        }

        [TestMethod]
        public void GetAllPhotosByUserID_Should_Return_OK()
        {
            photoService.Setup(a => a.GetAllPhotosByUserID(It.IsAny<string>())).Returns(new Photo[] { new Photo(), new Photo() });

            IHttpActionResult result = photosController.GetAllPhotosByUserID("123") as OkNegotiatedContentResult<Photo[]>;

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(OkNegotiatedContentResult<Photo[]>), result.GetType());

        }

        [TestMethod]
        public void GetPhotosCountByAlbumID_Should_Return_404()
        {
            photoService.Setup(a => a.GetPhotosCountByAlbumID(It.IsAny<int>())).Returns(-1);

            IHttpActionResult result = photosController.GetPhotosCountByAlbumID(1) as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }


        [TestCleanup]
        public void TestCleanUp()
        {
            photosController.Dispose();
            mainController.Dispose();
        }
    }
}
