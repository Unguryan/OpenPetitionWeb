using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Models.Petition
{
    public interface IClosePetitionModel
    {
        string IdPetition { get; set; }

        string IdUser { get; set; }
    }
}
