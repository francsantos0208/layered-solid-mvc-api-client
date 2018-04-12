using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IMovieDataAccessLayer
    {
        Task<IEnumerable<Movie>> GetMovies();
    }
}
