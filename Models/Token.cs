using System;
using System.Collections.Generic;

namespace store_api.Models
{
    public partial class Token
    {
        public string SessionId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiredTime { get; set; }
    }
}
