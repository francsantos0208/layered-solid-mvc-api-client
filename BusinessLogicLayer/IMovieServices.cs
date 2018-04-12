using DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IMovieServices
    {
        Task<IEnumerable<ActorDto>> GetActors();
    }
}
