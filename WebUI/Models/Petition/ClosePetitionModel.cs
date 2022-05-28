using Interfaces.Models.Petition;

namespace WebUI.Models.Petition
{
    public class ClosePetitionModel : IClosePetitionModel
    {
        public string IdPetition { get; set; }

        public string IdUser { get; set; }
    }
}
