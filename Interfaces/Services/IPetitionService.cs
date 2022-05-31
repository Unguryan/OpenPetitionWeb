using Interfaces.Models.Petition;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IPetitionService
    {
        Task<IEnumerable<IPetition>> GetPetitions();

        Task<IPetition> GetPetition(string id);

        Task<IEnumerable<IPetition>> GetUserPetitions(string userId);

        Task<IPetition> AddPetition(IAddPetitionModel petition);

        Task<IPetition> AddVoiceToPetition(IAddVoiceToPetitionModel petition);

        Task<IPetition> ClosePetition(IClosePetitionModel petition);

    }
}
