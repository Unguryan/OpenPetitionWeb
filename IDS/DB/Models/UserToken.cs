using System;

namespace IDS.DB.Models
{
    public class UserToken
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Token { get; set; }

        public DateTime ExpairedAt { get; set; }

    }
}
