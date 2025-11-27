using Moq;
using PAW.Data.Models;
using PAW.Data.Repositories;
using System.IO;

namespace BusinessTest
{
    public class DirectorTest
    {

        private readonly IEnumerable<Director> directors = new List<Director>
            {
                new Director { DirectorId = 1, FullName = "Director One" },
                new Director { DirectorId = 2, FullName = "Director Two" },
                new Director { DirectorId = 3, FullName = "Director Three" }
            };

        private readonly Mock<IRepositoryDirector> mockRepositoryDirector;

        public DirectorTest()
        {
            mockRepositoryDirector = new Mock<IRepositoryDirector>();

        }

        [Fact]
        public async System.Threading.Tasks.Task GetDirector_WhenHasId()
        {

            mockRepositoryDirector.Setup(repo => repo.FindAsync(1))
                .ReturnsAsync(this.directors.First());

            var product = await mockRepositoryDirector.Object.FindAsync(1);

            Assert.Equal("Director One", product.FullName);
        }
    }
}