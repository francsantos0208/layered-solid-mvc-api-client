using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessLogicLayer.Tests
{
    [TestFixture]
    public class GetActorsTests
    {
        [Test]
        public void GetActors_WithoutDuplicateRoles_PerMovie()
        {
            var mockMovieDataAccessLayer = new Mock<IMovieDataAccessLayer>();

            var rolesForMovieSpidermanHomecoming = new List<Role>
            {
                new Role { ActorName = "Tom Holland", RoleName = "Spider-Man"},
                new Role { ActorName = "Tom Holland", RoleName = "Spider-Man"},
                new Role { ActorName = "Michael Keaton", RoleName = "Vulture"},
                new Role { ActorName = "Robert Downey Jr.", RoleName = "Tony Stark"}
            };

            var rolesForMovieAvengersInfinityWar = new List<Role>
            {
                new Role { ActorName = "Tom Holland", RoleName = "Spider-Man"},
                new Role { ActorName = "Chris Hemsworth", RoleName = "Thor"},
                new Role { ActorName = "Mark Ruffalo", RoleName = "Hulk"},
                new Role { ActorName = "Chris Evans", RoleName = "Steve Rogers"}
            };

            var movies = new List<Movie>
            {
                new Movie { MovieName = "Spider-Man: Homecoming", Roles = rolesForMovieSpidermanHomecoming},
                new Movie { MovieName = "Avengers: Infinity War", Roles = rolesForMovieAvengersInfinityWar}
            };

            mockMovieDataAccessLayer.Setup(s => s.GetMovies())
                .Returns(Task.FromResult<IEnumerable<Movie>>(movies));

            var movieServices = new MovieServices(mockMovieDataAccessLayer.Object);

            var actorDtos = movieServices.GetActors().Result;

            var rolesOfTomHolland = actorDtos
                        .Where(w => w.Name.Equals("Tom Holland")).ToList();

            Assert.AreEqual(2, rolesOfTomHolland.SelectMany(s => s.Roles).ToList().Count);
        }

        [Test]
        public void GetActors_GroupingUnspecifiedActors()
        {
            var mockMovieDataAccessLayer = new Mock<IMovieDataAccessLayer>();

            var rolesForMovieSpidermanHomecoming = new List<Role>
            {
                new Role { ActorName = "Tom Holland", RoleName = "Spider-Man"},
                new Role { ActorName = "Tom Holland", RoleName = "Spider-Man"},
                new Role { RoleName = "Vulture"},
                new Role { ActorName = "Robert Downey Jr.", RoleName = "Tony Stark"}
            };

            var rolesForMovieAvengersInfinityWar = new List<Role>
            {
                new Role { ActorName = "Tom Holland", RoleName = "Spider-Man"},
                new Role { ActorName = "Chris Hemsworth", RoleName = "Thor"},
                new Role { ActorName = "     ", RoleName = "Hulk"},
                new Role { ActorName = "", RoleName = "Steve Rogers"}
            };

            var movies = new List<Movie>
            {
                new Movie { MovieName = "Spider-Man: Homecoming", Roles = rolesForMovieSpidermanHomecoming},
                new Movie { MovieName = "Avengers: Infinity War", Roles = rolesForMovieAvengersInfinityWar}
            };

            mockMovieDataAccessLayer.Setup(s => s.GetMovies())
                .Returns(Task.FromResult<IEnumerable<Movie>>(movies));

            var movieServices = new MovieServices(mockMovieDataAccessLayer.Object);

            var actorDtos = movieServices.GetActors().Result;

            var rolesOfUnspecifiedActor = actorDtos
                .Where(w => w.Name.Equals("*UNSPECIFIED ACTOR*")).ToList();

            Assert.AreEqual(3, rolesOfUnspecifiedActor.SelectMany(s => s.Roles).ToList().Count);
        }
    }
}