using Interfaces.Models.Petition;
using Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Services
{
    public class PetitionService : IPetitionService
    {
        public async Task<IEnumerable<IPetition>> GetPetitions()
        {
            return null;
        }

        public async Task<IPetition> GetPetition()
        {
            return null;
        }

        public async Task<IPetition> AddPetition(IAddPetitionModel petition)
        {
            return null;
        }

        public async Task<IPetition> AddVoiceToPetition(IAddVoiceToPetitionModel petition)
        {
            return null;
        }

        public async Task<IPetition> ClosePetition(IPetition petition)
        {
            return null;
        }

    }
}
