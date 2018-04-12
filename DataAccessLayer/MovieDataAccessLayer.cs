using API.Client;
using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MovieDataAccessLayer : IMovieDataAccessLayer
    {
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var movieDataSourceEndpoint = ConfigurationManager.AppSettings["MovieDataSourceEndpoint"];
            var movies =
                await HttpClientSingleton.Instance.GetData<IEnumerable<Movie>>(movieDataSourceEndpoint);
            return movies;
        }
    }
}
