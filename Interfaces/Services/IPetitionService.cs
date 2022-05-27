using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IPetitionService
    {
        Task<IEnumerable<IPetition>> GetPetitions();

        Task<IPetition> GetPetition();

        Task<IPetition> AddPetition(IAddPetitionModel petition);

        Task<IPetition> AddVoiceToPetition(IAddVoiceToPetitionModel petition);

        Task<IPetition> ClosePetition(IPetition petition);

    }
}
