using System.IO;

namespace BusinessTest
{
    public class DirectorTest
    {

        private readonly IEnumerable<Director> directors = new List<Director>
            {
                new Director { DirectorID = 1, FullName = "Director One" },
                new Director { DirectorID = 2, FullName = "Director Two" },
                new Director { DirectorID = 3, FullName = "Director Three" }
            };

        private readonly Mock<IRepositoryDirector> mockRepositoryDirector;

        public DirectorTest()
        {
            mockRepositoryDirector = new Mock<IRepositoryDirector>();
            
        }

        [Fact]
        public async System.Threading.Tasks.Task GetDirector_WhenHasId()
        {
            
            _mockRepositoryDirector.setup(repo => repo.ReadAsync(1))
                .ReturnsAsync(this.directors.First());

            var product = mockRepositoryDirector.ReadAsync(1);

            Assert.Equal("Director One", product.Result.FullName);
        }


    }
}