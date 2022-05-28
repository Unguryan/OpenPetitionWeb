using Interfaces.Models.Petition;

namespace WebUI.Models.Petition
{
    public class AddPetitionModel : IAddPetitionModel
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public uint MaxVoices { get; set; }

    }
}