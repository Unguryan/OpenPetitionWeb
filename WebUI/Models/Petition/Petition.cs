using Interfaces.Models.Petition;
using System.Collections.Generic;

namespace WebUI.Models.Petition
{
    public class Petition : IPetition
    {

        public string Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //Holds idUser
        public List<string> CurrentVoices { get; set; }

        public uint MaxVoices { get; set; }

        public bool IsClosed { get; set; }

    }
}