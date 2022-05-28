using System.Collections.Generic;

namespace Interfaces.Models.Petition
{
    public interface IPetition
    {
        
        string Id { get; set; }

        string UserId { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        //Holds idUser
        List<string> CurrentVoices { get; set; }

        uint MaxVoices { get; set; }

        bool IsClosed { get; set; }

    }
}