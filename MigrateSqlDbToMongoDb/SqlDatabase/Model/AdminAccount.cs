using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class AdminAccount
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? Updatedate { get; set; }
    }
}
