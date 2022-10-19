using System;
using System.Collections.Generic;

namespace store_api.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
