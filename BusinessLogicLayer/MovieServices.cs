using DataAccessLayer;
using DataTransferObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer
{
    public class MovieServices : IMovieServices 
    {
        private readonly IMovieDataAccessLayer _movieDataAccessLayer;

        public MovieServices(IMovieDataAccessLayer movieDataAccessLayer)
        {
            _movieDataAccessLayer = movieDataAccessLayer;
        }

        public async Task<IEnumerable<ActorDto>> GetActors()
        {
            var movies = await _movieDataAccessLayer.GetMovies();

            var rolesWithoutDuplicatesPerMovie =
                movies
                    .Select(s => new Movie
                    {
                        MovieName = s.MovieName,
                        Roles = s.Roles.Select(a => new Role
                        {
                            ActorName = a.ActorName,
                            RoleName = a.RoleName
                        }).GroupBy(g => g.RoleName).Select(group => group.First())
                    });


            var rolesWithStandarisedUnspecifiedActors =
                rolesWithoutDuplicatesPerMovie
                    .OrderBy(o => o.MovieName)
                    .GroupBy(g => g.Roles).Select(group => group.First())
                    .SelectMany(sm => sm.Roles)
                    .Select(s => new Role
                    {
                        ActorName = string.IsNullOrEmpty(s.ActorName) || string.IsNullOrWhiteSpace(s.ActorName)
                            ? "*UNSPECIFIED ACTOR*" : s.ActorName,
                        RoleName = s.RoleName
                    });

            var rolesByActorIncludingGroupedUnspecifiedActors =
                rolesWithStandarisedUnspecifiedActors
                    .GroupBy(g => g.ActorName)
                    .Select(s => new ActorDto
                    {
                        Name = s.Key,
                        Roles = s.Select(a => new RoleDto
                        {
                            RoleName = a.RoleName
                        })
                    });

            return rolesByActorIncludingGroupedUnspecifiedActors;
        }
    }
}
