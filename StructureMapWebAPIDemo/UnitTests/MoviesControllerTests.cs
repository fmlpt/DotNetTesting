using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using StructureMapWebAPIDemo.Lib.DataModel;
using StructureMapWebAPIDemo.Lib.Repositories;
using StructureMapWebAPIDemo.Controllers;

namespace UnitTests
{
    [TestFixture]
    public class MoviesControllerTests
    {
        [Test]
        public void Test_GetByID_ValidID()
        {
            //Given
            const int movieId = 2;
            var mockMovieRepo = new Mock<IMovieRepository>();
            mockMovieRepo.Setup(x => x.GetById(movieId)).Returns(new Movie() { Id = 2 });
            var controller = new MoviesController(mockMovieRepo.Object);
            
            //When
            var response = controller.GetById(movieId);
            var contentResult = response as OkNegotiatedContentResult<Movie>;
            
            //Then
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(movieId, contentResult.Content.Id);
        }
        
        [Test]
        public void Test_GetByID_InvalidID()
        {
            //Given
            var mockMovieRepo = new Mock<IMovieRepository>();
            const int movieId = 6; //This movie does not exist
            mockMovieRepo.Setup(x => x.GetById(movieId)).Returns((Movie)null);
            var controller = new MoviesController(mockMovieRepo.Object);

            //When
            var response = controller.GetById(movieId);
            var contentResult = response as NotFoundResult;

            //Then
            Assert.IsNotNull(contentResult);
        }
    }
}