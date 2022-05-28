namespace Interfaces.Models.Petition
{
    public interface IAddPetitionModel
    {

        string Name { get; set; }
        string Description { get; set; }

        uint MaxVoices { get; set; }

    }
}