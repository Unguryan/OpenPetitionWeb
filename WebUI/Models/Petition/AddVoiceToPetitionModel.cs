using Interfaces.Models.Petition;

namespace WebUI.Models.Petition
{
    public class AddVoiceToPetitionModel : IAddVoiceToPetitionModel
    {

        public string IdPetition { get; set; }

        public string IdUser { get; set; }

    }
}