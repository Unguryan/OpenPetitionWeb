using Interfaces.Models.Petition;
using Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.DB;
using WebUI.Models.Petition;
using WebUI.Models.User;

namespace WebUI.Services
{
    public class PetitionService : IPetitionService
    {
        private readonly Web_Context _context;

        public PetitionService(Web_Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IPetition>> GetPetitions()
        {
            return await _context.Petitions.ToListAsync();
        }

        public async Task<IPetition> GetPetition(string id)
        {
            return await _context.Petitions.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<IPetition>> GetUserPetitions(string userId)
        {
            var res = new List<IPetition>();

            await foreach(var item in _context.Petitions.AsAsyncEnumerable())
            {
                if(item.UserId == userId)
                {
                    res.Add(item);
                }
            }

            return res;
        }

        #region ByAuth

        public async Task<IPetition> AddPetition(IAddPetitionModel petition)
        {
            if (GlobalProperties.User == null)
            {
                return new Petition();
            }

            var p = new Petition()
            {
                UserId = GlobalProperties.User.Id,
                IsClosed = false,
                Name = petition.Name,
                Description = petition.Description,
                MaxVoices = petition.MaxVoices,
                CurrentVoices = new List<string>() { GlobalProperties.User.Id }
            };

            var res = await _context.Petitions.AddAsync(p);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IPetition> AddVoiceToPetition(IAddVoiceToPetitionModel petition)
        {
            var p = await _context.Petitions.FirstOrDefaultAsync(p => p.Id == petition.IdPetition);
            if(p == null || p.IsClosed)
            {
                return new Petition();
            }

            _context.Petitions.Remove(p);
            await _context.SaveChangesAsync();
            p.CurrentVoices.Add(petition.IdUser);
            p.Id = null;

            var res = await _context.Petitions.AddAsync(p);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IPetition> ClosePetition(IClosePetitionModel petition)
        {
            var p = await _context.Petitions.FirstOrDefaultAsync(p => p.Id == petition.IdPetition);
            if (p == null || p.IsClosed || p.UserId != petition.IdUser)
            {
                return new Petition();
            }

            _context.Petitions.Remove(p);
            await _context.SaveChangesAsync();
            p.IsClosed = true;
            p.Id = null;

            var res = await _context.Petitions.AddAsync(p);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        #endregion

    }
}
