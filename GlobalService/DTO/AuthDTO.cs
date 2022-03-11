using System;

namespace GlobalService.DTO
{
    public class AuthDTO
    {
        public Guid uuid { get; set; }

        public string Token { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ExpiredAt { get; set; }
    }
}
